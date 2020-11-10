using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Protos.Server;
using Repository.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Lookup
{
    public class WeatherLookupService : Weather.WeatherBase
    {
      
        private readonly IReadOnlyWeatherForecastRepository _readOnlyWeatherForecastRepository;

        public WeatherLookupService(IReadOnlyWeatherForecastRepository readOnlyWeatherForecastRepository)
        {
            _readOnlyWeatherForecastRepository = readOnlyWeatherForecastRepository;
        }

        public override async Task<WeatherForecastReplies> GetWeatherForecast(WeatherForecastRequest request, ServerCallContext context)
        {
            var weatherForeCasts = await _readOnlyWeatherForecastRepository.LookupWeatherForecasts();
            var result = new WeatherForecastReplies();
            result.WeatherForecasts.AddRange(weatherForeCasts.Select(x =>
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
            return result;
        }
    }
}
