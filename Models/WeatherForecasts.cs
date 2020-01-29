using System.Collections.Generic;

namespace LinksExample.Models
{
    public class WeatherForecasts : Representation
    {
        public List<WeatherForecast> Results { get; set; }
            = new List<WeatherForecast>();
    }
}