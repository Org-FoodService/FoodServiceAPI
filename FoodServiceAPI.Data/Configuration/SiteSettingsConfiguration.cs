using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodService.Models.Entities
{
    /// <summary>
    /// Configuration class for the SiteSettings entity.
    /// </summary>
    public class SiteSettingsConfiguration : IEntityTypeConfiguration<SiteSettings>
    {
        /// <summary>
        /// Configures the SiteSettings entity.
        /// </summary>
        /// <param name="builder">The entity type builder used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<SiteSettings> builder)
        {
            // Seed initial data for SiteSettings
            builder.HasData(
                new SiteSettings
                {
                    Id = 1,
                    PrimaryColor = "#AA2E26",            // Set the primary color
                    SecondaryColor = "#FB9F3A",          // Set the secondary color
                    BackgroundColor = "#fffaf3",         // Set the background color
                    ServiceName = "FoodService",         // Set the service name
                    DarkColor = "#412D2C",               // Set the dark color
                    TertiaryColor = "#2CAB61",           // Set the tertiary color
                    GreenColor = "#376B4C",              // Set the green color
                    SuccessColor = "#02EB62",            // Set the success color
                    DangerColor = "#8E291F",             // Set the danger color
                    LastUpdate = DateTime.Now            // Set the last update time
                }
            );
        }
    }
}
