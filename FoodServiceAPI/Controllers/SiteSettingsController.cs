using FoodService.Models.Entities;
using FoodServiceAPI.Core.Interface.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodServiceAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SiteSettingsController : ControllerBase
    {
        private readonly ISiteSettingsCommand _siteSettingsCommand;
        private readonly ILogger<SiteSettingsController> _logger;

        public SiteSettingsController(ISiteSettingsCommand siteSettingsCommand, ILogger<SiteSettingsController> logger)
        {
            _siteSettingsCommand = siteSettingsCommand;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the site settings.
        /// </summary>
        /// <returns>A response containing the site settings.</returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetSiteSettings()
        {
            _logger.LogInformation("Received request to get site settings.");

            var response = await _siteSettingsCommand.GetSiteSettings();
            if (!response.IsSuccess)
            {
                _logger.LogWarning("Failed to retrieve site settings: {Message}", response.Message);
                return StatusCode(response.StatusCode, response.Message);
            }

            _logger.LogInformation("Site settings retrieved successfully.");
            return Ok(response);
        }

        /// <summary>
        /// Updates the site settings.
        /// </summary>
        /// <param name="id">The ID of the site settings to update.</param>
        /// <param name="siteSettings">The updated site settings data.</param>
        /// <returns>A response containing the updated site settings.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSiteSettings(int id, [FromBody] SiteSettings siteSettings)
        {
            _logger.LogInformation("Received request to update site settings with ID {Id}.", id);

            if (id != siteSettings.Id)
            {
                _logger.LogWarning("ID mismatch: URL ID {UrlId} does not match body ID {BodyId}.", id, siteSettings.Id);
                return BadRequest("The SiteSettings ID in the URL does not match the SiteSettings ID in the request body.");
            }

            var response = await _siteSettingsCommand.UpdateSiteSettings(id, siteSettings);
            if (!response.IsSuccess)
            {
                _logger.LogWarning("Failed to update site settings: {Message}", response.Message);
                return StatusCode(response.StatusCode, response.Message);
            }

            _logger.LogInformation("Site settings updated successfully.");
            return Ok(response);
        }
    }
}
