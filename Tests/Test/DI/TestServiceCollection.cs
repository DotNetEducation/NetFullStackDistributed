using Domain.Services;
using Lookup;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.DbContexts;
using Repository.Interfaces;
using Repository.Repositories;
using System.Data.Common;

namespace Test.DI
{
    public static class TestServiceCollection
    {
        public static ServiceProvider ConfigureCollection()
        {
            var services = new ServiceCollection();

            services.AddDbContext<WeatherDbContext>(options =>
            {
                options.UseSqlite(CreateInMemoryDatabase());
            });

            services.AddScoped<IReadOnlyWeatherForecastRepository, WeatherRespository>();
            services.AddScoped<IWeatherForecastRepository, WeatherRespository>();

            services.AddScoped<WeatherService>();

            services.AddScoped<WeatherLookupService>();

            services.AddScoped<WeatherRespository>();

            return services.BuildServiceProvider();
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }
    }
}
