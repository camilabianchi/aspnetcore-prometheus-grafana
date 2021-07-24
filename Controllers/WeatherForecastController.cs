using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prometheus.DTO;

namespace Prometheus.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly Gauge _amountControl = Metrics.CreateGauge("api_weatherforecast_amount_control", "Weather Forecast amount control");
        private static readonly Counter _accessCounter = Metrics.CreateCounter("api_weatherforecast_access_counter", "Weather Forecast counter");

        private static List<string> Summaries = new ()
        {
            "Freezing", "Warm", "Hot", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            _accessCounter.Inc();

            return Ok(Summaries);
        }

        [HttpPost]
        public ActionResult<IEnumerable<WeatherForecast>> Add(WeatherForecast weatherForecast)
        {
            _amountControl.Inc();
           
            Summaries.Add(weatherForecast.Summary);

            return Ok(Summaries);
        }

        [HttpDelete]
        public ActionResult<IEnumerable<WeatherForecast>> Delete(WeatherForecast weatherForecast)
        {
            var entryToDelete = Summaries.FirstOrDefault(x => x == weatherForecast.Summary);
            if (entryToDelete is not null)
            {
                Summaries.Remove(entryToDelete);

                _amountControl.Dec();
            }

            return Ok(Summaries);
        }
    }
}
