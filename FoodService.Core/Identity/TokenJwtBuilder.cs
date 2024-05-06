using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FoodService.Core.Identity
{
    /// <summary>
    /// Builder class for creating JWT tokens.
    /// </summary>
    public class TokenJwtBuilder
    {
        private SecurityKey? securityKey = null;
        private string subject = "";
        private string issuer = "";
        private string audience = "";
        private readonly Dictionary<string, string> claims = [];
        private int expiryInMinutes = 5;

        /// <summary>
        /// Adds the security key used to sign the token.
        /// </summary>
        /// <param name="securityKey">The security key.</param>
        /// <returns>The token builder instance.</returns>
        public TokenJwtBuilder AddSecurityKey(SecurityKey securityKey)
        {
            this.securityKey = securityKey;
            return this;
        }

        /// <summary>
        /// Adds the subject of the token.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <returns>The token builder instance.</returns>
        public TokenJwtBuilder AddSubject(string subject)
        {
            this.subject = subject;
            return this;
        }

        /// <summary>
        /// Adds the issuer of the token.
        /// </summary>
        /// <param name="issuer">The issuer.</param>
        /// <returns>The token builder instance.</returns>
        public TokenJwtBuilder AddIssuer(string issuer)
        {
            this.issuer = issuer;
            return this;
        }

        /// <summary>
        /// Adds the audience of the token.
        /// </summary>
        /// <param name="audience">The audience.</param>
        /// <returns>The token builder instance.</returns>
        public TokenJwtBuilder AddAudience(string audience)
        {
            this.audience = audience;
            return this;
        }

        /// <summary>
        /// Adds a claim to the token.
        /// </summary>
        /// <param name="type">The type of the claim.</param>
        /// <param name="value">The value of the claim.</param>
        /// <returns>The token builder instance.</returns>
        public TokenJwtBuilder AddClaim(string type, string value)
        {
            claims.Add(type, value);
            return this;
        }

        /// <summary>
        /// Adds multiple claims to the token.
        /// </summary>
        /// <param name="claims">The dictionary containing claims.</param>
        /// <returns>The token builder instance.</returns>
        public TokenJwtBuilder AddClaims(Dictionary<string, string> claims)
        {
            _ = this.claims.Union(claims);
            return this;
        }

        /// <summary>
        /// Adds expiry duration to the token.
        /// </summary>
        /// <param name="expiryInMinutes">The expiry duration in minutes.</param>
        /// <returns>The token builder instance.</returns>
        public TokenJwtBuilder AddExpiry(int expiryInMinutes)
        {
            this.expiryInMinutes = expiryInMinutes;
            return this;
        }

        private void EnsureArguments()
        {
            if (securityKey == null)
                throw new ArgumentException("Security Key");

            if (string.IsNullOrEmpty(subject))
                throw new ArgumentException("Subject");

            if (string.IsNullOrEmpty(issuer))
                throw new ArgumentException("Issuer");

            if (string.IsNullOrEmpty(audience))
                throw new ArgumentException("Audience");
        }

        /// <summary>
        /// Builds the JWT token.
        /// </summary>
        /// <returns>The JWT token.</returns>
        public TokenJwt Build()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, subject),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }.Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256)
            );

            return new TokenJwt(token);
        }
    }
}
