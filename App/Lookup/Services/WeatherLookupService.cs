using Grpc.Core;
using Protos.Server;
using Repository.Interfaces;
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

        public override Task<WeatherForecastReplies> GetWeatherForecast(WeatherForecastRequest request, ServerCallContext context)
        {
            var weatherForeCasts =_readOnlyWeatherForecastRepository.LookupWeatherForecasts();

            return weatherForeCasts;
        }
    }
}
