using FoodServiceAPI.Data.SqlServer.Context;
using FoodServiceAPI.Data.SqlServer.Repository;
using Microsoft.Extensions.Logging;

namespace FoodServiceApi.Tests.Data.SqlServer.Repository
{
    public class TestGenericRepository : GenericRepository<TestEntity, int>
    {
        public TestGenericRepository(AppDbContext context, ILogger<GenericRepository<TestEntity, int>> logger)
            : base(context, logger)
        {
        }
    }
}
