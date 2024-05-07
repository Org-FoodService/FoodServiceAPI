namespace FoodServiceAPI.Core.Identity
{
    /// <summary>
    /// Defines custom claim types used in the application.
    /// </summary>
    public class CustomClaimTypes
    {
        /// <summary>
        /// Client Id claim.
        /// </summary>
        public const string ClientId = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/clientId";

        /// <summary>
        /// Allowed origins claim.
        /// </summary>
        public const string AllowedOrigins = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/allowedorigins";

        /// <summary>
        /// User login claim.
        /// </summary>
        public const string UserLogin = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/userid";

        /// <summary>
        /// Security stamp claim.
        /// </summary>
        public const string SecurityStamp = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/securityStamp";

        /// <summary>
        /// User Id claim.
        /// </summary>
        public const string UserId = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/usuarioid";

        /// <summary>
        /// Email claim.
        /// </summary>
        public const string Email = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/email";

        /// <summary>
        /// Person Id claim.
        /// </summary>
        public const string PersonId = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/pessoaid";

        /// <summary>
        /// Person document claim.
        /// </summary>
        public const string PersonDocument = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/pessoadocumento";

        /// <summary>
        /// Tenant Id claim.
        /// </summary>
        public const string TenantId = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/tenantid";

        /// <summary>
        /// Hierarchy claim.
        /// </summary>
        public const string Hierarchy = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/hierarchy";

        /// <summary>
        /// Permission claim.
        /// </summary>
        public const string Permission = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/permission";

        /// <summary>
        /// Permission block claim.
        /// </summary>
        public const string PermissionBlock = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/permission.block";

        /// <summary>
        /// Force password change claim.
        /// </summary>
        public const string ForcePasswordChange =
            "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/force.password.change";
    }
}
