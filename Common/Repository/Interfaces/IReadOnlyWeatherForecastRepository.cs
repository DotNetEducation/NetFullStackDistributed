using Protos.Server;
using Repository.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IReadOnlyWeatherForecastRepository
    {
        Task<WeatherForecastReplies> LookupWeatherForecasts(Expression<Func<WeatherForecast, bool>> filter = null);
    }
}
