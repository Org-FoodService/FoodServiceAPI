using FoodService.Models.Entities;
using FoodServiceAPI.Core.Interface.Service;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace FoodServiceAPI.Core.Service
{
    public class SiteSettingsService : ISiteSettingsService
    {
        private readonly ISiteSettingsRepository _repository;
        private readonly ILogger<SiteSettingsService> _logger;

        /// <summary>
        /// Initializes a new instance of the SiteSettingsService class.
        /// </summary>
        /// <param name="repository">The siteSettings repository.</param>
        /// <param name="logger">The logger instance.</param>
        public SiteSettingsService(ISiteSettingsRepository repository, ILogger<SiteSettingsService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a siteSettings by its ID asynchronously.
        /// </summary>
        /// <returns>The retrieved siteSettings.</returns>
        public async Task<SiteSettings> GetSiteSettingsAsync()
        {
            _logger.LogInformation("Attempting to retrieve site settings.");
            var siteSettings = await _repository.GetByIdAsync(1);
            if (siteSettings == null)
            {
                _logger.LogWarning("Site settings not found.");
            }
            else
            {
                _logger.LogInformation("Site settings retrieved successfully.");
            }
            return siteSettings;
        }

        /// <summary>
        /// Updates an existing siteSettings asynchronously.
        /// </summary>
        /// <param name="siteSettings">The siteSettings to update.</param>
        /// <returns>The updated siteSettings, or null if the siteSettings does not exist.</returns>
        public async Task<SiteSettings?> UpdateSiteSettingsAsync(SiteSettings siteSettings)
        {
            _logger.LogInformation("Attempting to update site settings.");

            var existingSiteSettings = await GetSiteSettingsAsync();
            if (existingSiteSettings == null)
            {
                _logger.LogWarning("Site settings to update not found.");
                return null;
            }

            _logger.LogInformation("Updating site settings with new values.");
            existingSiteSettings.LastUpdate = DateTime.Now;
            existingSiteSettings.ServiceName = siteSettings.ServiceName;
            existingSiteSettings.PrimaryColor = siteSettings.PrimaryColor;
            existingSiteSettings.SecondaryColor = siteSettings.SecondaryColor;
            existingSiteSettings.BackgroundColor = siteSettings.BackgroundColor;

            await _repository.UpdateAsync(existingSiteSettings);
            _logger.LogInformation("Site settings updated successfully.");
            return existingSiteSettings;
        }
    }
}
