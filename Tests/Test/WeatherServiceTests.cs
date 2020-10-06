using Domain.Services;
using Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Entities;
using System;
using System.Threading.Tasks;
using Test.DI;
using Test.Mock;

namespace Test
{
    [TestClass]
    public class WeatherServiceTests
    {
        private readonly WeatherService _weatherService;

        public WeatherServiceTests()
        {
            _weatherService = TestServiceCollection.ConfigureCollection().GetService<WeatherService>();
        }

        [TestMethod]
        public async Task AddWeatherForecastTest()
        {
            var context = new MockConsumeContext<AddWeatherForecast, Result>(new AddWeatherForecast 
            {
                WeatherForecast = new WeatherForecast
                {
                    Date = DateTime.Now,
                    Summary = "Test",
                    TemperatureC = 30,
                    TemperatureF = 200
                }
            });
            await _weatherService.Consume(context);
            Assert.IsNotNull(context.Response);
        }

        [TestMethod]
        public async Task RemoveWeatherForecastTest()
        {
            var context = new MockConsumeContext<RemoveWeatherForecast, Result>(new RemoveWeatherForecast 
            {
                WeatherForecastId = 1
            });
            await _weatherService.Consume(context);
            Assert.IsNotNull(context.Response);
        }
    }
}
