using FoodService.Models.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace FoodServiceAPI.Controllers.SwaggerRequestExample
{
    public class SignInDtoExample : IExamplesProvider<SignInDto>
    {
        public SignInDto GetExamples()
        {
            return new SignInDto
            {
                Username = "exampleuser",
                Password = "Ab!123"
            };
        }
    }
}
