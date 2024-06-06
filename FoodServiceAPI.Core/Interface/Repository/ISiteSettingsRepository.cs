using FoodService.Models.Entities;
using FoodServiceAPI.Core.Interface.Repository.Generic;

namespace FoodServiceAPI.Core.Interface.Repository
{
    public interface ISiteSettingsRepository : IGenericRepository<SiteSettings, int>
    {
    }
}
