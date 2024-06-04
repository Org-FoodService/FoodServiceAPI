using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodService.Models.Entities
{
    public class SiteSettingsConfiguration : IEntityTypeConfiguration<SiteSettings>
    {
        public void Configure(EntityTypeBuilder<SiteSettings> builder)
        {
            builder.HasData(
                new SiteSettings
                {
                    Id = 1,
                    PrimaryColor = "#3498db",
                    SecondaryColor = "#2ecc71",
                    BackgroundColor = "#ecf0f1",
                    ServiceName = "FoodService",
                    LastUpdate = DateTime.Now
                }
            );
        }
    }
}