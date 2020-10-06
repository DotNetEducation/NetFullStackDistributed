using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Entities;
using Repository.Repositories;
using System;
using System.Threading.Tasks;
using Test.DI;

namespace Test
{
    [TestClass]
    public class WeatherRepositoryTests
    {
        private readonly WeatherRespository _weatherRespository;

        public WeatherRepositoryTests()
        {
            _weatherRespository = TestServiceCollection.ConfigureCollection().GetService<WeatherRespository>();
        }
        [TestMethod]
        public async Task AddWeatherForecastTest()
        {
            await _weatherRespository.AddWeatherForecast(new WeatherForecast 
            {
                Date = DateTime.Now,
                Summary = "Test",
                TemperatureC = 30,
                TemperatureF = 200
            });
        }

        [TestMethod]
        public async Task LookupWeatherForecastsTest()
        {
           var result = await _weatherRespository.LookupWeatherForecasts(x => x.Id == 1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task RemoveWeatherForecastTest()
        {
            await _weatherRespository.RemoveWeatherForecast(1);
        }
    }
}
