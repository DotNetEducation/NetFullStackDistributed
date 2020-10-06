using MassTransit;
using Messages;
using Repository.Interfaces;
using System.Threading.Tasks;

namespace Domain.Services
{

    public class WeatherService :
        IConsumer<AddWeatherForecast>,
        IConsumer<RemoveWeatherForecast>
    {
        private readonly IWeatherForecastRepository _weatherForecastRepository;

        public WeatherService(IWeatherForecastRepository weatherForecastRepository)
        {
            _weatherForecastRepository = weatherForecastRepository;
        }
        public async Task Consume(ConsumeContext<AddWeatherForecast> context)
        {
            await _weatherForecastRepository.AddWeatherForecast(context.Message.WeatherForecast);

            await context.RespondAsync(new Result());
        }

        public async Task Consume(ConsumeContext<RemoveWeatherForecast> context)
        {
            await _weatherForecastRepository.RemoveWeatherForecast(context.Message.WeatherForecastId);

            await context.RespondAsync(new Result());
        }
    }
}
