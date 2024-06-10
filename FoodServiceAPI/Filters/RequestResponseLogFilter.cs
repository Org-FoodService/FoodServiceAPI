using Microsoft.AspNetCore.Mvc.Filters;

namespace FoodServiceAPI.Filters
{
    public class RequestResponseLogFilter : IAsyncActionFilter
    {
        private readonly ILogger<RequestResponseLogFilter> _logger;
        private readonly bool _isRequestResponseLoggingEnabled;
        public RequestResponseLogFilter(ILogger<RequestResponseLogFilter> logger, IConfiguration config)
        {
            _logger = logger;
            _isRequestResponseLoggingEnabled = config.GetValue<bool>("EnableRequestResponseLogging", false);
        }

        private static string FormatHeaders(IHeaderDictionary headers) => string.Join(", ", headers.Select(kvp => $"{{{kvp.Key}: {string.Join(", ", kvp.Value!)}}}"));

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Filter is enabled only when the EnableRequestResponseLogging config value is set.
            if (_isRequestResponseLoggingEnabled)
            {
                var requestBody = (context.HttpContext.Request.Method == "GET") ? "" : context.ActionArguments.First().Value;

                _logger.LogInformation("HTTP request information:\n" +
                        "\tMethod: {@method}\n" +
                        "\tPath: {@path}\n" +
                        "\tQueryString: {@queryString}\n" +
                        "\tHeaders: {@headers}\n" +
                        "\tSchema: {@scheme}\n" +
                        "\tHost: {@host}\n" +
                        "\tRequest Body: {@requestBody}",
                        context.HttpContext.Request.Method,
                        context.HttpContext.Request.Path,
                        context.HttpContext.Request.QueryString,
                        FormatHeaders(context.HttpContext.Request.Headers),
                        context.HttpContext.Request.Scheme,
                        context.HttpContext.Request.Host,
                        requestBody);

                var responseContext = await next();

                var result = responseContext.Result as Microsoft.AspNetCore.Mvc.ObjectResult;
                _logger.LogInformation("HTTP response information:\n" +
                        "\tStatusCode: {@statusCode}\n" +
                        "\tContentType: {@contentType}\n" +
                        "\tHeaders: {@headers}\n" +
                        "\tBody: {@response}",
                        responseContext.HttpContext.Response.StatusCode,
                        responseContext.HttpContext.Response.ContentType,
                        FormatHeaders(responseContext.HttpContext.Response.Headers),
                        result?.Value);
            }
            else
            {
                await next();
            }
        }
    }
}
