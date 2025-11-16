using System.Text.Json;
using WeatherBE.Models;
using WeatherBE.Services.Interfaces;

namespace WeatherBE.Services
{
    public class CityService : ICityService 
    {
        private readonly List<City> _cities;

        public CityService(IWebHostEnvironment env)
        {
            var path = Path.Combine(env.ContentRootPath, "Data", "cities.json");
            var json = File.ReadAllText(path);

            using var doc = JsonDocument.Parse(json);

            var listElement = doc.RootElement.GetProperty("List");

            _cities = JsonSerializer.Deserialize<List<City>>(listElement.GetRawText())
                      ?? new List<City>();

        }

        public IReadOnlyList<City> GetAll()
        {
            return _cities;
        }

        public City? GetByCode(int code)
        {
            return _cities.FirstOrDefault(c => c.CityCode == code.ToString());
        }       
    }
}
