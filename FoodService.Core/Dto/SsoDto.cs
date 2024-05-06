using FoodService.Nuget.Models.Auth.User;

namespace FoodService.Core.Dto
{
    /// <summary>
    /// Data transfer object for single sign-on (SSO) operation.
    /// </summary>
    public class SsoDto
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the expiration day of the access token.
        /// </summary>
        public DayOfWeek Expiration { get; set; }

        /// <summary>
        /// Gets or sets the user information.
        /// </summary>
        public UserBase User { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SsoDto"/> class.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="user">The user information.</param>
        public SsoDto(string accessToken, UserBase user)
        {
            AccessToken = accessToken;
            User = user;
            Expiration = DateTime.UtcNow.DayOfWeek;
        }
    }
}
