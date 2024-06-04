using FoodService.Models.Entities;
using FoodService.Models.Responses;

namespace FoodServiceAPI.Core.Interface.Command
{
    public interface ISiteSettingsCommand
    {
        Task<ResponseCommon<SiteSettings>> GetSiteSettings();
        Task<ResponseCommon<SiteSettings?>> UpdateSiteSettings(int id, SiteSettings siteSettings);
    }
}