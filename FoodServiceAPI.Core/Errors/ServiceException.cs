using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodServiceAPI.Core.Errors
{
    /// <summary>
    /// A service exception.
    /// </summary>
    public class ServiceException : Exception
    {
        /// <summary>
        /// Gets the canonical error code that represents the business error.
        /// </summary>
        public ErrorCode ErrorCode { get; }

        /// <summary>
        /// Gets the detail for the given service exception.
        /// </summary>
        public string? Detail { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceException"/> class.
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="detail"></param>
        public ServiceException(ErrorCode errorCode, string? detail = null)
            : this(errorCode, null, detail)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceException"/> class.
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="innerException"></param>
        /// <param name="detail"></param>
        public ServiceException(ErrorCode errorCode, Exception? innerException, string? detail = null)
            : base(GetMessage(errorCode, innerException, detail), innerException)
        {
            ErrorCode = errorCode;
            Detail = detail;
        }

        private static string GetMessage(ErrorCode errorCode, Exception? innerException, string? detail)
        {
            var message = $"Code: {errorCode}";

            if (innerException != null)
            {
                var innerExceptionMessage = string.Empty;
                if (!string.IsNullOrEmpty(innerException.Message))
                {
                    innerExceptionMessage = $" - {innerException.Message}";
                }

                message += $"\nInner Exception: {innerException.GetType()}{innerExceptionMessage}";
            }

            if (detail != null)
            {
                message += $"\nDetail: {detail}";
            }

            return message;
        }
    }
}
