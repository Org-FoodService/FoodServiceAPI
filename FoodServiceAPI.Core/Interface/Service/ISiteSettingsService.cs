using FoodService.Models.Entities;

namespace FoodServiceAPI.Core.Interface.Service
{
    public interface ISiteSettingsService
    {
        Task<SiteSettings> GetSiteSettingsAsync();
        Task<SiteSettings?> UpdateSiteSettingsAsync(SiteSettings siteSettings);
    }
}