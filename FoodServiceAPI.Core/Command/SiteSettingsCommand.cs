using FoodService.Models.Entities;
using FoodService.Models.Responses;
using FoodServiceAPI.Core.Command.Interface;
using FoodServiceAPI.Core.Service.Interface;
using Microsoft.Extensions.Logging;

namespace FoodServiceAPI.Core.Command
{
    public class SiteSettingsCommand : ISiteSettingsCommand
    {
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly ILogger<SiteSettingsCommand> _logger;

        public SiteSettingsCommand(ISiteSettingsService siteSettingsService, ILogger<SiteSettingsCommand> logger)
        {
            _siteSettingsService = siteSettingsService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves site settings.
        /// </summary>
        /// <returns>A response containing the site settings.</returns>
        public async Task<ResponseCommon<SiteSettings>> GetSiteSettings()
        {
            var siteSettings = await _siteSettingsService.GetSiteSettingsAsync();
            if (siteSettings == null)
            {
                _logger.LogWarning("Site settings not found.");
                return ResponseCommon<SiteSettings>.Failure("SiteSettings not found", 404);
            }

            return ResponseCommon<SiteSettings>.Success(siteSettings);
        }

        /// <summary>
        /// Updates an existing site setting.
        /// </summary>
        /// <param name="id">The ID of the site setting to update.</param>
        /// <param name="siteSettings">The updated site settings data.</param>
        /// <returns>A response containing the updated site settings.</returns>
        public async Task<ResponseCommon<SiteSettings?>> UpdateSiteSettings(int id, SiteSettings siteSettings)
        {
            if (id != siteSettings.Id)
            {
                _logger.LogWarning("ID mismatch: URL ID {UrlId} does not match body ID {BodyId}.", id, siteSettings.Id);
                return ResponseCommon<SiteSettings?>.Failure("The SiteSettings ID in the URL does not match the SiteSettings ID in the request body", 400);
            }

            var result = await _siteSettingsService.UpdateSiteSettingsAsync(siteSettings);

            return ResponseCommon<SiteSettings?>.Success(result);
        }
    }
}
