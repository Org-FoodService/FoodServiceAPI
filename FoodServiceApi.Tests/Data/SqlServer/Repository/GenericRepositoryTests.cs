using FoodServiceAPI.Data.SqlServer.Context;
using FoodServiceAPI.Data.SqlServer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FoodServiceApi.Tests.Data.SqlServer.Repository
{
    public class GenericRepositoryTests
    {
        private readonly Mock<DbSet<TestEntity>> _mockSet;
        private readonly Mock<TestDbContext> _mockContext;
        private readonly Mock<ILogger<TestGenericRepository>> _mockLogger;
        private readonly TestGenericRepository _repository;

        public GenericRepositoryTests()
        {
            _mockSet = new Mock<DbSet<TestEntity>>();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _mockContext = new Mock<TestDbContext>(options);
            _mockLogger = new Mock<ILogger<TestGenericRepository>>();
            _repository = new TestGenericRepository(_mockContext.Object, _mockLogger.Object);

            _mockContext.Setup(m => m.Set<TestEntity>()).Returns(_mockSet.Object);
        }

        [Fact(DisplayName = "CreateAsync - Success")]
        public async Task CreateAsync_Success()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_CreateAsync_Success")
                .Options;

            using (var context = new TestDbContext(options))
            {
                var repository = new TestGenericRepository(context, new LoggerFactory().CreateLogger<TestGenericRepository>());

                var entity = new TestEntity { Id = 1 };

                // Act
                var result = await repository.CreateAsync(entity);

                // Assert
                var addedEntity = await context.TestEntities.FindAsync(entity.Id);
                Assert.Equal(entity, addedEntity);
                Assert.Equal(entity, result);
            }
        }


        [Fact(DisplayName = "GetById - Success")]
        public void GetById_Success()
        {
            // Arrange
            var entity = new TestEntity { Id = 1 };
            _mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns(entity);

            // Act
            var result = _repository.GetById(1);

            // Assert
            _mockSet.Verify(m => m.Find(1), Times.Once);
            Assert.Equal(entity, result);
        }

        [Fact(DisplayName = "GetByIdAsync - Success")]
        public async Task GetByIdAsync_Success()
        {
            // Arrange
            var entity = new TestEntity { Id = 1 };
            _mockSet.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            _mockSet.Verify(m => m.FindAsync(1), Times.Once);
            Assert.Equal(entity, result);
        }

        [Fact(DisplayName = "ListAll - Success")]
        public void ListAll_Success()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new TestDbContext(options))
            {
                var data = new List<TestEntity>
                {
                    new TestEntity { Id = 1 },
                    new TestEntity { Id = 2 }
                };

                context.TestEntities.AddRange(data);
                context.SaveChanges();

                var repository = new TestGenericRepository(context, new LoggerFactory().CreateLogger<TestGenericRepository>());

                // Act
                var result = repository.ListAll().ToList();

                // Assert
                Assert.Equal(2, result.Count);
                Assert.Contains(result, r => r.Id == 1);
                Assert.Contains(result, r => r.Id == 2);
            }
        }

        public class TestGenericRepository : GenericRepository<TestEntity, int>
        {
            public TestGenericRepository(AppDbContext context, ILogger<GenericRepository<TestEntity, int>> logger)
                : base(context, logger)
            {
            }
        }

        public class TestEntity
        {
            public int Id { get; set; }
        }

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
}
