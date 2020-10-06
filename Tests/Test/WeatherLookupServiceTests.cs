using Lookup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Protos.Server;
using System.Threading.Tasks;
using Test.DI;
using Test.Mock;

namespace Test
{
    [TestClass]
    public class WeatherLookupServiceTests
    {
        private readonly WeatherLookupService _weatherLookupService;

        public WeatherLookupServiceTests()
        {
            _weatherLookupService = TestServiceCollection.ConfigureCollection().GetService<WeatherLookupService>();
        }
        [TestMethod]
        public async Task GetWeatherForecastTest()
        {
            var forecasts = await _weatherLookupService.GetWeatherForecast(new WeatherForecastRequest(), new MockServerCallContext());

            Assert.IsNotNull(forecasts);
        }
    }
}
