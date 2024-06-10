using FoodService.Models.Entities;

namespace FoodServiceAPI.Core.Service.Interface
{
    public interface ISiteSettingsService
    {
        Task<SiteSettings> GetSiteSettingsAsync();
        Task<SiteSettings?> UpdateSiteSettingsAsync(SiteSettings siteSettings);
    }
}