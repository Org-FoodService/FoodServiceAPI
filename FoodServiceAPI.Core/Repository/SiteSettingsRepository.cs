using FoodService.Models.Entities;
using FoodServiceAPI.Core.Interface.Repository;
using FoodServiceAPI.Core.Repository.Generic;
using FoodServiceAPI.Data.Context;
using Microsoft.Extensions.Logging;

namespace FoodServiceAPI.Core.Repository
{
    public class SiteSettingsRepository : GenericRepository<SiteSettings, int>, ISiteSettingsRepository
    {

        /// <summary>
        /// Initializes a new instance of the SiteSettingsRepository class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public SiteSettingsRepository(AppDbContext context, ILogger<SiteSettingsRepository> logger)
            : base(context, logger)
        {
        }
    }
}
