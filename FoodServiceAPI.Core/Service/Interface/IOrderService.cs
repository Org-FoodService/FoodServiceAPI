using FoodService.Models.Entities;

namespace FoodServiceAPI.Core.Service.Interface
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order Order);
        Task<bool> DeleteOrder(int id);
        Task<List<Order>> GetAllOrder();
        Task<Order> GetOrderById(int id);
        Task<Order?> UpdateOrder(Order Order);
    }
}
