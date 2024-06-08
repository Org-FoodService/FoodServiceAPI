using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FoodServiceAPI.Core.Errors;
using Newtonsoft.Json;

namespace FoodServiceAPI.Filters
{
    /// <summary>
    /// A global exception filter that will handle all unhandled exceptions for the application.
    /// </summary>
    internal class AsyncExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<AsyncExceptionFilter> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncExceptionFilter"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="hostEnvironment"></param>
        public AsyncExceptionFilter(ILogger<AsyncExceptionFilter> logger, IHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        /// <inheritdoc/>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            context.Result = context.Exception is ServiceException serviceException ? GetResult(serviceException) : GetResult(context.Exception);
            context.ExceptionHandled = true;

            return Task.CompletedTask;
        }

        /// <summary>
        /// Ensures a string ends in a period.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string EnsureStringEndsInPeriod(string s)
        {
            return $"{s.TrimEnd('.')}.";
        }

        /// <summary>
        /// Escapes braces for string formatting.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string EscapeJsonForStringFormatInput(string s)
        {
            return s.Replace("{", "{{").Replace("}", "}}");
        }

        private IActionResult GetResult(Exception exception)
        {
            // This will generate: "{ExceptionType} - {Message}." or "{ExceptionType}." if no message is present.
            var exceptionDescription = exception.GetType().ToString();

            if (!string.IsNullOrEmpty(exception.Message))
            {
                exceptionDescription += $" - {exception.Message}";
            }

            exceptionDescription = EnsureStringEndsInPeriod(exceptionDescription);
            exceptionDescription = EscapeJsonForStringFormatInput(exceptionDescription);

            var log = new ErrorLog
            {
                ErrorLogId = Guid.NewGuid().ToString()
            };

            _logger.LogError(exception, "UnhandledException: {ExceptionDescription} {{{Log}}}", exceptionDescription, log);

            var content = $"An unexpected error has occurred. You can use the following reference id to help us diagnose your problem: {log.ErrorLogId}";

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(content),
                StatusCode = StatusCodes.Status500InternalServerError,
                ContentType = "application/json"
            };
        }

        private IActionResult GetResult(ServiceException exception)
        {
            var responseBody = new ProblemDetails
            {
                Type = exception.ErrorCode.ToString(),
                Title = exception.ErrorCode.GetDescription(),
                Detail = exception.Detail
            };

            if (_hostEnvironment.IsDevelopment())
            {
                responseBody.Extensions.Add("stackTrace", exception.ToString());
            }

            var log = new ServiceExceptionLog
            {
                ServiceExceptionLogId = Guid.NewGuid().ToString()
            };

            var exceptionDescription = exception.Message;
            exceptionDescription = EnsureStringEndsInPeriod(exceptionDescription);
            exceptionDescription = EscapeJsonForStringFormatInput(exceptionDescription);

            _logger.LogInformation(exception, "ServiceException: {ExceptionDescription} {{{Log}}}", exceptionDescription, log);

            switch (exception.ErrorCode)
            {
                case ErrorCode.Forbidden:
                    responseBody.Status = StatusCodes.Status403Forbidden;
                    return new ObjectResult(responseBody)
                    {
                        StatusCode = responseBody.Status,
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                case ErrorCode.InvalidRequest:
                    responseBody.Status = StatusCodes.Status400BadRequest;
                    return new BadRequestObjectResult(responseBody)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                case ErrorCode.NotFound:
                    responseBody.Status = StatusCodes.Status404NotFound;
                    return new NotFoundObjectResult(responseBody)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };

                default:
                    responseBody.Status = StatusCodes.Status500InternalServerError;
                    return new ObjectResult(responseBody)
                    {
                        StatusCode = responseBody.Status,
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
            }
        }

        /// <summary>
        /// Additional details for an Error Log.
        /// </summary>
        private sealed class ErrorLog
        {
            public string ErrorLogId { get; init; } = default!;

            public string AuditLogId { get; set; } = default!;

            public string RequestId { get; set; } = default!;

            /// <summary>
            /// Returns ReferenceId: <see cref="ErrorLogId"/>.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"ReferenceId: {ErrorLogId}";
            }
        }

        /// <summary>
        /// Additional details for an Service Exception Log.
        /// </summary>
        private sealed class ServiceExceptionLog
        {
            public string ServiceExceptionLogId { get; init; } = default!;

            public string AuditLogId { get; set; } = default!;

            /// <summary>
            /// Returns ReferenceId: <see cref="ServiceExceptionLogId"/>.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return $"ReferenceId: {ServiceExceptionLogId}";
            }
        }
    }
}
