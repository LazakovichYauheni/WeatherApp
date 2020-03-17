using Newtonsoft.Json;
using System.Collections.Generic;

namespace Weather.Services.DataModels
{
    public class WeatherInfoDataModel
    {
        [JsonProperty("temp")]
        public double Temp { get; set; }
        [JsonProperty("feels_like")]
        public double Feels_like { get; set; }
        [JsonProperty("temp_min")]
        public double Temp_min { get; set; }
        [JsonProperty("temp_max")]
        public double Temp_max { get; set; }
        [JsonProperty("pressure")]
        public string Pressure { get; set; }
        [JsonProperty("hudimity")]
        public int Hudimity { get; set; }
    }

    public class WeatherCoordinateDataModel
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }
        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }

    public class WeatherDataModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("main")]
        public string Main { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public class WeatherModelRoot
    {
        [JsonProperty("main")]
        public WeatherInfoDataModel WeatherModel { get; set; }
        [JsonProperty("weather")]
        public List<WeatherDataModel> Weather { get; set; }
        [JsonProperty("coord")]
        public WeatherCoordinateDataModel WeatherCoordinate { get; set; }
    }
}
