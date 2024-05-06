using FoodService.Nuget.Models;

namespace FoodService.Core.Interface.Command
{
    public interface IProductCommand
    {
        Task<ResponseCommon<Product>> CreateProduct(Product Product);
        Task<ResponseCommon<bool>> DeleteProduct(int id);
        Task<ResponseCommon<List<Product>>> GetAllProducts();
        Task<ResponseCommon<Product>> GetProductById(int id);
        Task<ResponseCommon<Product?>> UpdateProduct(int id, Product Product);
    }
}