using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prometheus.DTO;

namespace Prometheus.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreetingController : ControllerBase
    {
        private static readonly Counter _accessCounter = Metrics.CreateCounter("api_greeting_access_counter", "Greeting counter");

        private static List<string> Greetings = new ()
        {
            "Hi!", "Hello!", "Hey!", "Hey hoy!"
        };

        private readonly ILogger<GreetingController> _logger;

        public GreetingController(ILogger<GreetingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Greeting>> Get()
        {
            _accessCounter.Inc();

            return Ok(Greetings);
        }
    }
}
