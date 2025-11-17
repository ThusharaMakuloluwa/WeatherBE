namespace WeatherBE.DTOs
{
    public class WeatherResultDTO
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string DateTime { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int Visibility { get; set; }
        public double WindSpeed { get; set; }
        public int WindDegree { get; set; }
        public string Sunrise { get; set; } = string.Empty;
        public string Sunset { get; set; } = string.Empty;
    }
}
