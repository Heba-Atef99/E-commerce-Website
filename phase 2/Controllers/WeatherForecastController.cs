using E_commerce.Models;
using E_commerce.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IAccountRepository _logger;
        private readonly IUserRepository _user;
        private readonly IItemRepository _item;
        public WeatherForecastController(IAccountRepository logger, IUserRepository user, IItemRepository item)
        {
            _logger = logger;
            _user = user;
            _item = item;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ACCOUNT1>> signin()
        {
          // var x= _logger.GetAllAccountEmailsAndPass();
            //IEnumerable<ACCOUNT1> a = _logger.GetAllAccountEmailsAndPass();
            return _logger.GetAllAccountEmailsAndPass().ToArray();
        }
        /*private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }*/

    }
}
