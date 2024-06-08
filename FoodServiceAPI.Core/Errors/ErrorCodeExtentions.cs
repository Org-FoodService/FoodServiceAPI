using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FoodServiceAPI.Core.Errors
{
    /// <summary>
    /// Error code extensions.
    /// </summary>
    public static class ErrorCodeExtensions
    {
        private static readonly ReadOnlyDictionary<ErrorCode, string> ErrorDescriptions = InitErrorDescriptions();

        /// <summary>
        /// Get description of an ErrorCode.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static string GetDescription(this ErrorCode error)
        {
            return ErrorDescriptions[error];
        }

        private static ReadOnlyDictionary<ErrorCode, string> InitErrorDescriptions()
        {
            var lookup = Enum.GetValues(typeof(ErrorCode))
                .Cast<ErrorCode>()
                .ToDictionary(
                    errorCode => errorCode,
                    errorCode =>
                    {
                        var field = errorCode.GetType().GetField(errorCode.ToString()) ?? throw new InvalidOperationException($"{nameof(ErrorCode)}.{errorCode} is not a field of {errorCode.GetType()}");
                        var descriptionAttribute = field.GetCustomAttribute<DescriptionAttribute>() ?? throw new InvalidOperationException($"{nameof(ErrorCode)}.{errorCode} is missing a {nameof(DescriptionAttribute)}");
                        return descriptionAttribute.Description;
                    });

            return new ReadOnlyDictionary<ErrorCode, string>(lookup);
        }
    }
}
