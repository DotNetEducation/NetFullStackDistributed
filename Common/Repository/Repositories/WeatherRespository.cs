using Google.Protobuf.WellKnownTypes;
using Protos.Server;
using Repository.DbContexts;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class WeatherRespository : IWeatherForecastRepository
    {
        private readonly WeatherDbContext _weatherDbContext;

        public WeatherRespository(WeatherDbContext weatherDbContext)
        {
            _weatherDbContext = weatherDbContext;
        }

        public async Task AddWeatherForecast(WeatherForecast weatherForecast)
        {
            await _weatherDbContext.AddAsync(weatherForecast);
            await _weatherDbContext.SaveChangesAsync();
        }

        public Task<WeatherForecastReplies> LookupWeatherForecasts(Expression<Func<WeatherForecast, bool>> filter)
        {
            IEnumerable<WeatherForecast> weatherForecasts;
            if (filter == null)
            {
                weatherForecasts = _weatherDbContext.WeatherForecasts;
            }
            else
            {
                weatherForecasts = _weatherDbContext.WeatherForecasts.Where(filter.Compile());
            }

            var result = new WeatherForecastReplies
            {

            };
            result.WeatherForecasts.AddRange(weatherForecasts.Select(x =>
            {
                x.Date = DateTime.SpecifyKind(x.Date, DateTimeKind.Utc);
                return new WeatherForecastReply
                {
                    Id = x.Id,
                    Date = Timestamp.FromDateTime(x.Date),
                    Summary = x.Summary,
                    TemperatureC = x.TemperatureC,
                    TemperatureF = x.TemperatureF
                };
            }));

            return Task.FromResult(result);
        }

        public async Task RemoveWeatherForecast(long weatherForecastId)
        {
            var weatherForecast = _weatherDbContext.WeatherForecasts.First(x => x.Id == weatherForecastId);

            _weatherDbContext.Remove(weatherForecast);

            await _weatherDbContext.SaveChangesAsync();
        }
    }
}
