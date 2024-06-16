using System.Diagnostics.CodeAnalysis;

namespace FoodServiceApi.Tests.Data.SqlServer
{
    [ExcludeFromCodeCoverage]
    public class TestEntity
    {
        public int Id { get; set; }
        public string? SomeProperty { get; set; }
    }
}
