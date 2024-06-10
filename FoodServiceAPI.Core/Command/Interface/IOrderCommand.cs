using FoodService.Models.Entities;
using FoodService.Models.Responses;

namespace FoodServiceAPI.Core.Command.Interface
{
    public interface IOrderCommand
    {
        Task<ResponseCommon<Order>> CreateOrder(Order Order);
        Task<ResponseCommon<bool>> DeleteOrder(int id);
        Task<ResponseCommon<List<Order>>> GetAllOrders();
        Task<ResponseCommon<Order>> GetOrderById(int id);
        Task<ResponseCommon<Order?>> UpdateOrder(int id, Order Order);
    }
}
