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

        #region UpdateAsync

        [Fact(DisplayName = "UpdateAsync - Success")]
        public async Task UpdateAsync_Success()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_UpdateAsync_Success")
                .Options;

            using (var context = new TestDbContext(options))
            {
                var repository = new TestGenericRepository(context, new LoggerFactory().CreateLogger<TestGenericRepository>());

                var entity = new TestEntity { Id = 1, SomeProperty = "Initial Value" };
                context.TestEntities.Add(entity);
                await context.SaveChangesAsync();

                // Modify the entity
                entity.SomeProperty = "Updated Value";

                // Act
                var result = await repository.UpdateAsync(entity, entity.Id);

                // Assert
                Assert.Equal(1, result); // Assuming 1 entity was updated

                var updatedEntity = await context.TestEntities.FindAsync(entity.Id);
                Assert.Equal(entity.SomeProperty, updatedEntity!.SomeProperty);
            }
        }

        [Fact(DisplayName = "UpdateAsync - Entity Not Found")]
        public async Task UpdateAsync_EntityNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_UpdateAsync_EntityNotFound")
                .Options;

            using (var context = new TestDbContext(options))
            {
                var repository = new TestGenericRepository(context, new LoggerFactory().CreateLogger<TestGenericRepository>());

                var entity = new TestEntity { Id = 1 };

                // Act & Assert
                await Assert.ThrowsAsync<KeyNotFoundException>(async () => await repository.UpdateAsync(entity, entity.Id));
            }
        }

        #endregion

        #region DeleteAsync

        [Fact(DisplayName = "DeleteAsync - Success")]
        public async Task DeleteAsync_Success()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_DeleteAsync_Success")
                .Options;

            using (var context = new TestDbContext(options))
            {
                var entity = new TestEntity { Id = 1 };
                context.TestEntities.Add(entity);
                context.SaveChanges();

                var repository = new TestGenericRepository(context, new LoggerFactory().CreateLogger<TestGenericRepository>());

                // Act
                var result = await repository.DeleteAsync(entity, entity.Id);

                // Assert
                Assert.True(result); // Assuming the deletion was successful

                var deletedEntity = await context.TestEntities.FindAsync(entity.Id);
                Assert.Null(deletedEntity); // Ensure the entity was actually deleted
            }
        }

        [Fact(DisplayName = "DeleteAsync - Entity Not Found")]
        public async Task DeleteAsync_EntityNotFound()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_DeleteAsync_EntityNotFound")
                .Options;

            using (var context = new TestDbContext(options))
            {
                var repository = new TestGenericRepository(context, new LoggerFactory().CreateLogger<TestGenericRepository>());

                var entity = new TestEntity { Id = 1 };

                // Act & Assert
                var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () => await repository.DeleteAsync(entity, entity.Id));
                Assert.Equal($"Entity not found", exception.Message);
            }
        }

        #endregion

        #region GetById and GetByIdAsync

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

        [Fact(DisplayName = "GetById - Entity Not Found")]
        public void GetById_EntityNotFound()
        {
            // Arrange
            var id = 1;
            _mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns((TestEntity)null);

            // Act & Assert
            var exception = Assert.Throws<KeyNotFoundException>(() => _repository.GetById(id));
            Assert.Equal($"Entity not found for ID: {id}", exception.Message);
            _mockSet.Verify(m => m.Find(id), Times.Once);
        }

        [Fact(DisplayName = "GetByIdAsync - Entity Not Found")]
        public async Task GetByIdAsync_EntityNotFound()
        {
            // Arrange
            var id = 1;
            _mockSet.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync((TestEntity)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.GetByIdAsync(id));
            Assert.Equal($"Entity not found for ID: {id}", exception.Message);
            _mockSet.Verify(m => m.FindAsync(id), Times.Once);
        }

        #endregion

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
            public string? SomeProperty { get; set; }
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
