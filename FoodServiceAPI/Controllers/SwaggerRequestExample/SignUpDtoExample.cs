using FoodService.Models.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace FoodServiceAPI.Controllers.SwaggerRequestExample
{
    public class SignUpDtoExample : IExamplesProvider<SignUpDto>
    {
        public SignUpDto GetExamples()
        {
            return new SignUpDto
            {
                CpfCnpj = "12345678901",
                Username = "exampleuser",
                Email = "exampleuser@example.com",
                Password = "Ab!123",
                ConfirmPassword = "Ab!123",
                PhoneNumber = "1234567890"
            };
        }
    }
}
