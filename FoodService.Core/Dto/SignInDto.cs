using System.ComponentModel.DataAnnotations;

namespace FoodService.Core.Dto
{
    /// <summary>
    /// Data transfer object for sign-in operation.
    /// </summary>
    public class SignInDto
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignInDto"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public SignInDto(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
