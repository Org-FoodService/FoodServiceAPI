using FoodService.Nuget.Models.Auth.User;

namespace FoodService.Core.Dto
{
    /// <summary>
    /// Data transfer object for user information.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDto"/> class.
        /// </summary>
        public UserDto() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDto"/> class with specified user data.
        /// </summary>
        /// <param name="user">The user data.</param>
        public UserDto(ClientUser user)
        {
            UserName = user.UserName!;
            Email = user.Email!;
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; } = null!;
    }
}
