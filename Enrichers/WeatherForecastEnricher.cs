using System.Threading.Tasks;
using LinksExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace LinksExample.Enrichers
{
    /// <summary>
    /// An enricher for our Weather#Show response
    /// </summary>
    public class WeatherForecastEnricher : Enricher<WeatherForecast>
    {
        private readonly IHttpContextAccessor accessor;
        private readonly LinkGenerator linkGenerator;

        public WeatherForecastEnricher(IHttpContextAccessor accessor, LinkGenerator linkGenerator)
        {
            this.accessor = accessor;
            this.linkGenerator = linkGenerator;
        }

        public override Task Process(WeatherForecast representation)
        {
            var httpContext = accessor.HttpContext;

            representation
                .AddLink(new Link
                {
                    Id = representation.Id.ToString(),
                    Label = $"Weather #{representation.Id}",
                    Url = linkGenerator.GetUriByName(
                        httpContext,
                        "weather#show",
                        new {id = representation.Id},
                        scheme: "https"
                    )
                });

            return Task.CompletedTask;
        }
    }
}