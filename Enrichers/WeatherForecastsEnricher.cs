using System.Threading.Tasks;
using LinksExample.Models;

namespace LinksExample.Enrichers
{
    /// <summary>
    /// An enricher for our Weather#Index respponse
    /// </summary>
    public class WeatherForecastsEnricher : Enricher<WeatherForecasts>
    {
        private readonly WeatherForecastEnricher enricher;

        public WeatherForecastsEnricher(WeatherForecastEnricher enricher)
        {
            this.enricher = enricher;
        }

        public override async Task Process(WeatherForecasts representation)
        {
            foreach (var forecast in representation.Results)
            {
                await enricher.Process(forecast);
            }
        }
    }
}