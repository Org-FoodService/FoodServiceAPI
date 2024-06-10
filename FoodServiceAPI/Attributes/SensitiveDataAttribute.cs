namespace FoodServiceAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class SensitiveDataAttribute : Attribute
    {
    }
}
