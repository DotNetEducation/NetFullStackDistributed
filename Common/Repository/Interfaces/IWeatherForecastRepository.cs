using Repository.Entities;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IWeatherForecastRepository: IReadOnlyWeatherForecastRepository
    {
        Task AddWeatherForecast(WeatherForecast weatherForecast);

        Task RemoveWeatherForecast(long weatherForecastId);
    }
}
