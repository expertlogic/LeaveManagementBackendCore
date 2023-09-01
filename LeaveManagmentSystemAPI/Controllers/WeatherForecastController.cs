using LeaveManagmentSystemAPI.Core;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagmentSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserContext _userContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger
            , IUserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }

        [HttpGet("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            var user = _userContext.Profile;


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet("error")]
        public IEnumerable<WeatherForecast> error()
        {
            throw new Exception();
        }
    }
}