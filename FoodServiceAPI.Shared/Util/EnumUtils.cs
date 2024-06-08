using System.ComponentModel;
using System.Reflection;

namespace FoodServiceAPI.Shared.Util
{
    /// <summary>
    /// Utility class for working with enums.
    /// </summary>
    public static class EnumUtils
    {
        /// <summary>
        /// Gets the description of an enum value.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>The description of the enum value, or null if not found.</returns>
        public static string? GetEnumDescription(this Enum value)
        {
            FieldInfo? field = value.GetType().GetField(value.ToString());

            // If the field is not found, return null
            if (field == null) return null;

            // Get the DescriptionAttribute associated with the enum value
            DescriptionAttribute? attribute = (DescriptionAttribute?)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            // If DescriptionAttribute is not found, return the enum value's string representation
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
