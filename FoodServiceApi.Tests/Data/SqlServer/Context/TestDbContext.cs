using FoodServiceAPI.Data.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodServiceApi.Tests.Data.SqlServer.Context
{
    public class TestDbContext : AppDbContext
    {
        public TestDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TestEntity> TestEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ensure the primary key is configured
            modelBuilder.Entity<TestEntity>()
                .HasKey(te => te.Id);
        }
    }
}
