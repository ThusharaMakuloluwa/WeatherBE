using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherBE.Services.Interfaces;

namespace WeatherBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IWeatherService _weatherService;

        public WeatherController(ICityService cityService, IWeatherService weatherService)
        {
            _cityService = cityService;
            _weatherService = weatherService;
        }

        [HttpGet("cities")]
        public IActionResult GetCities()
        {
            // Only send minimal info to the front-end
            return Ok(_cityService.GetAll()
                .Select(c => new { id = c.CityCode, name = c.CityName }));
        }

        [HttpGet("{cityId:int}")]
        public async Task<IActionResult> GetWeather(int cityId)
        {
            var city = _cityService.GetByCode(cityId);
            if (city is null) return NotFound("Invalid city id");

            var weather = await _weatherService.GetWeatherAsync(cityId);
            if (weather is null) return StatusCode(502, "Weather API error");

            return Ok(weather);
        }
    }
}
