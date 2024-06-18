using FoodService.Models.Entities;
using FoodService.Models.Responses;
using FoodServiceAPI.Core.Command.Interface;
using FoodServiceAPI.Core.Service.Interface;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using FoodServiceApi.Tests.Utility;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace FoodServiceApi.Tests.TestHelper
{
    [ExcludeFromCodeCoverage]
    public static class ProductTestHelper
    {
        public static readonly Product Product = new Product
        {
            Id = 1,
            Active = true,
            Description = "Test Description",
            Name = "Test Product",
            Price = 10.50,
            Brand = "Test Brand",
            Image = "Test Image",
            //ProductIngredients = new List<ProductIngredient>() { new() { IngredientId = 1, ProductId = 1} },
            ShortDescription = "Test Short Description",
            Type = FoodService.Models.Enum.ProductTypeEnum.Beverage
        };

        #region Setup Methods Command

        public static void SetupGetAllProductsCommand(this Mock<IProductCommand> mockProductCommand, ResponseCommon<List<Product>> response)
        {
            mockProductCommand.Setup(x => x.GetAllProducts()).ReturnsAsync(response);
        }

        public static void SetupGetProductByIdCommand(this Mock<IProductCommand> mockProductCommand, int id, ResponseCommon<Product> response)
        {
            mockProductCommand.Setup(x => x.GetProductById(id)).ReturnsAsync(response);
        }

        public static void SetupCreateProductCommand(this Mock<IProductCommand> mockProductCommand, Product product, ResponseCommon<Product> response)
        {
            mockProductCommand.Setup(x => x.CreateProduct(product)).ReturnsAsync(response);
        }

        public static void SetupUpdateProductCommand(this Mock<IProductCommand> mockProductCommand, int id, Product product, ResponseCommon<Product?> response)
        {
            mockProductCommand.Setup(x => x.UpdateProduct(id, product)).ReturnsAsync(response);
        }

        public static void SetupDeleteProductCommand(this Mock<IProductCommand> mockProductCommand, int id, ResponseCommon<bool> response)
        {
            mockProductCommand.Setup(x => x.DeleteProduct(id)).ReturnsAsync(response);
        }

        #endregion

        #region Setup Methods Service

        public static void SetupGetAllProductService(this Mock<IProductService> mockProductService, List<Product> products)
        {
            mockProductService.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products);
        }

        public static void SetupGetProductByIdService(this Mock<IProductService> mockProductService, int id, Product? product)
        {
            mockProductService.Setup(x => x.GetProductByIdAsync(id))!.ReturnsAsync(product);
        }

        public static void SetupCreateProductService(this Mock<IProductService> mockProductService, Product createdProduct)
        {
            mockProductService.Setup(x => x.CreateProductAsync(It.IsAny<Product>())).ReturnsAsync(createdProduct);
        }

        public static void SetupUpdateProductService(this Mock<IProductService> mockProductService, Product updatedProduct)
        {
            mockProductService.Setup(x => x.UpdateProductAsync(It.IsAny<Product>())).ReturnsAsync(updatedProduct);
        }

        public static void SetupDeleteProductService(this Mock<IProductService> mockProductService, int id, bool success)
        {
            mockProductService.Setup(x => x.DeleteProductAsync(id)).ReturnsAsync(success);
        }

        #endregion

        #region Setup Methods Repository

        public static void SetupListAllProductsRepository(this Mock<IProductRepository> mockProductRepository, List<Product> products)
        {
            var productDbSet = MockDbSet(products);
            mockProductRepository.Setup(x => x.ListAll()).Returns(productDbSet);
        }

        public static void SetupGetByIdProductRepository(this Mock<IProductRepository> mockProductRepository, int id, Product? product)
        {
            mockProductRepository.Setup(x => x.GetByIdAsync(id))!.ReturnsAsync(product);
        }

        public static void SetupCreateProductRepository(this Mock<IProductRepository> mockProductRepository, Product product, Product createdProduct)
        {
            mockProductRepository.Setup(x => x.CreateAsync(product)).ReturnsAsync(createdProduct);
        }

        public static void SetupUpdateProductRepository(this Mock<IProductRepository> mockProductRepository, Product product, int result)
        {
            mockProductRepository.Setup(x => x.UpdateAsync(product, product.Id)).ReturnsAsync(result);
        }

        public static void SetupDeleteProductRepository(this Mock<IProductRepository> mockProductRepository, Product product, bool result)
        {
            mockProductRepository.Setup(x => x.DeleteAsync(product, product.Id)).ReturnsAsync(result);
        }

        private static DbSet<T> MockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            dbSet.As<IAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<T>(queryable.GetEnumerator()));

            return dbSet.Object;
        }

        #endregion
    }
}
