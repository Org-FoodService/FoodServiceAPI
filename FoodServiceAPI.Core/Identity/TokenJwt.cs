using System.IdentityModel.Tokens.Jwt;

namespace FoodServiceAPI.Core.Identity
{
    /// <summary>
    /// Represents a JWT token.
    /// </summary>
    public class TokenJwt
    {
        private readonly JwtSecurityToken token;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenJwt"/> class with the specified JWT security token.
        /// </summary>
        /// <param name="token">The JWT security token.</param>
        internal TokenJwt(JwtSecurityToken token)
        {
            this.token = token;
        }

        /// <summary>
        /// Gets the expiration date and time of the token.
        /// </summary>
        public DateTime ValidTo => token.ValidTo;

        /// <summary>
        /// Gets the string value of the token.
        /// </summary>
        public string Value => new JwtSecurityTokenHandler().WriteToken(token);
    }
}
