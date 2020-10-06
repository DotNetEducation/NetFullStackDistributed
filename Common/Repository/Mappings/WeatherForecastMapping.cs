using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Repository.Entities;

namespace Repository.Mappings
{
    public class WeatherForecastMapping : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> builder)
        {
          
            builder.HasKey(x => x.Id).Metadata.IsPrimaryKey();
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Date);
            builder.Property(x => x.TemperatureC);
            builder.Property(x => x.TemperatureF);
            builder.Property(x => x.Summary);
        }
    }
}
