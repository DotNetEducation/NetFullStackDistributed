using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IReadOnlyWeatherForecastRepository
    {
        Task<IEnumerable<WeatherForecast>> LookupWeatherForecasts(Expression<Func<WeatherForecast, bool>> filter = null);
    }
}
