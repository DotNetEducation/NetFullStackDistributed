using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Mappings;

namespace Repository.DbContexts
{
    public class WeatherDbContext: DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public WeatherDbContext(DbContextOptions<WeatherDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WeatherForecastMapping());
        }
        
  
    }
}
