namespace Weather.Models
{
    public class WeatherInfo
    {
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public string Pressure { get; set; }
        public int Hudimity { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

    }
}
