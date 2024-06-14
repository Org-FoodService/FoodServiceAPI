using FoodService.Models.Auth.User;
using FoodService.Models.Dto;

namespace FoodServiceApi.Tests.TestsBase
{
    public class AuthTestsBase
    {
        protected readonly SsoDto SsoDto;
        protected readonly SignUpDto SignUpDto;
        protected readonly SignInDto SignInDto;
        protected readonly UserDto UserDto;

        protected readonly UserBase UserBase;
        protected readonly ClientUser ClientUser;
        protected readonly ClientUser ExistingClientUser;

        protected AuthTestsBase()
        {
            var commonPassword = "Password123!";
            var commonCpfCnpj = "12345678901";
            var commonEmailDomain = "@example.com";

            SsoDto = new SsoDto("eytoken", DateTime.Now.AddHours(1), new List<string> { "User" });

            SignUpDto = new SignUpDto
            {
                Username = "newuser",
                Password = commonPassword,
                ConfirmPassword = commonPassword,
                PhoneNumber = "11911112222",
                Email = $"newuser{commonEmailDomain}",
                CpfCnpj = commonCpfCnpj
            };
            SignInDto = new SignInDto
            {
                Username = "user1",
                Password = commonPassword
            };
            UserDto = new UserDto 
            { 
                UserName = "userdto", 
                Email = $"userdto{commonEmailDomain}" 
            };

            UserBase = new UserBase
            {
                Id = 1,
                UserName = SignInDto.Username,
                Email = $"user1{commonEmailDomain}",
                CpfCnpj = commonCpfCnpj
            };

            ClientUser = new ClientUser
            {
                Id = 2,
                UserName = SignInDto.Username,
                Email = $"user2{commonEmailDomain}",
                CpfCnpj = commonCpfCnpj
            };

            ExistingClientUser = new ClientUser
            {
                Id = 2,
                UserName = "existinguser",
                Email = $"existinguser{commonEmailDomain}",
                CpfCnpj = commonCpfCnpj
            };
        }
    }
}
