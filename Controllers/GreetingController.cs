using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prometheus.DTO;

namespace Prometheus.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreetingController : ControllerBase
    {
        private static readonly string[] Greetings = new[]
        {
            "Hi!", "Hello!", "Hey!", "Hey hoy!"
        };

        private readonly ILogger<GreetingController> _logger;

        public GreetingController(ILogger<GreetingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Greeting> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 3).Select(index => new Greeting
            {
                Text = Greetings[rng.Next(Greetings.Length)]
            })
            .ToArray();
        }
    }
}
