using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinksExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LinksExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("", Name = "weather#index")]
        public ActionResult<WeatherForecasts> Get()
        {
            var results = GetForecasts();
            return Ok(
                new WeatherForecasts
                {
                    Results = GetForecasts()
                });
        }

        [HttpGet]
        [Route("{id:int}", Name = "weather#show")]
        public ActionResult<WeatherForecast> Get(int id)
        {
            var result = GetForecasts().FirstOrDefault(x => x.Id == id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        private List<WeatherForecast> GetForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5)
                .Select(index => new WeatherForecast
                {
                    Id = index,
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToList();
        }
    }
}