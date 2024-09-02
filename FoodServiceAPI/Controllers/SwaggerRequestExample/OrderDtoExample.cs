using FoodService.Models.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace FoodServiceAPI.Controllers.SwaggerRequestExample
{
    public class OrderDtoExample : IExamplesProvider<OrderDto>
    {
        public OrderDto GetExamples()
        {
            return new OrderDto
            {
                OrderItems =
                [
                    new OrderItemDto    
                    {
                        ProductId = 1,
                        Quantity = 2,
                        Comment = ""

                    },
                    new OrderItemDto
                    {
                        ProductId = 3,
                        Quantity = 1,
                        Comment = ""

                    }
                ]
            };
        }
    }
}
