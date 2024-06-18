using FoodServiceAPI.Data.SqlServer.Context;
using FoodServiceAPI.Data.SqlServer.Repository;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace FoodServiceApi.Tests.Data.SqlServer.Repository
{
    [ExcludeFromCodeCoverage]
    public class TestGenericRepository : GenericRepository<TestEntity, int>
    {
        public TestGenericRepository(AppDbContext context, ILogger<GenericRepository<TestEntity, int>> logger)
            : base(context, logger)
        {
        }
    }
}
