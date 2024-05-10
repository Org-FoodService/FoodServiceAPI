using FoodService.Models.Auth.User;
using FoodService.Models.Dto;
using FoodService.Models.Responses;

namespace FoodServiceAPI.Core.Interface.Command
{
    public interface IAuthCommand
    {
        Task<ResponseCommon<bool>> AddUserToAdminRole(int userId);
        Task<ResponseCommon<UserBase>> GetCurrentUser();
        Task<ResponseCommon<UserDto>> GetUserDto(int id);
        Task<ResponseCommon<List<ClientUser>>> ListUsers();
        Task<ResponseCommon<SsoDto>> SignIn(SignInDto signInDTO);
        Task<ResponseCommon<bool>> SignUp(SignUpDto signUpDto);
    }
}