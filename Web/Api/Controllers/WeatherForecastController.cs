using MassTransit;
using Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Protos;
using Protos.Client;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       

        private readonly ILogger<WeatherForecastController> _logger;

        // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
        static readonly string[] _scopeRequiredByApi = new string[] { "access_as_user" };

        private readonly Weather.WeatherClient _weatherClient;
        private readonly IRequestClient<AddWeatherForecast> _addRequestClient;
        private readonly IRequestClient<RemoveWeatherForecast> _removeRequestClient;


        public WeatherForecastController(
            ILogger<WeatherForecastController> logger, 
            Weather.WeatherClient weatherClient,
            IRequestClient<AddWeatherForecast> addRequestClient,
            IRequestClient<RemoveWeatherForecast> removeRequestClient)
        {
            _logger = logger;
            _weatherClient = weatherClient;
            _addRequestClient = addRequestClient;
            _removeRequestClient = removeRequestClient;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecastReply>> GetAsync()
        {
            var reply = await _weatherClient.GetWeatherForecastAsync(
                              new WeatherForecastRequest());
            return reply.WeatherForecasts;
        }

        [HttpPut]
        public async Task AddAsync(WeatherForecast weatherForecast)
        {
            await _addRequestClient.GetResponse<Result>(new AddWeatherForecast
            {
                WeatherForecast = weatherForecast
            });
      
        }

        [HttpDelete]
        public async Task RemoveAsync(long id)
        {
           await _removeRequestClient.GetResponse<Result>(new RemoveWeatherForecast
            {
                WeatherForecastId = id
            });

        }

    }
}
