using System;

namespace Repository.Entities
{
    public class WeatherForecast
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string Summary { get; set; }

    }
}
