﻿using FoodService.Models.Entities;
using FoodServiceAPI.Data.SqlServer.Context;
using FoodServiceAPI.Data.SqlServer.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace FoodServiceAPI.Data.SqlServer.Repository
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
