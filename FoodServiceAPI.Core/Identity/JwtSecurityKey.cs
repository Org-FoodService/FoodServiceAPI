using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FoodServiceAPI.Core.Identity
{
    /// <summary>
    /// Class for creating JWT security key.
    /// </summary>
    public class JwtSecurityKey
    {
        /// <summary>
        /// Creates a symmetric security key using the specified secret.
        /// </summary>
        /// <param name="secret">The secret used to create the security key.</param>
        /// <returns>The symmetric security key.</returns>
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
