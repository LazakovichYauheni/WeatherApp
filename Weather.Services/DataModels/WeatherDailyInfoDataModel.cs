using Newtonsoft.Json;
using System.Collections.Generic;

namespace Weather.Services.DataModels
{
    public class WeatherDailyInfoDataModel
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }
    }

    public class WeatherIcons
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public class List
    {
        [JsonProperty("dt_txt")]
        public string Date { get; set; }
        [JsonProperty("main")]
        public WeatherDailyInfoDataModel WeatherDailyInfoDataModel { get; set; }
        [JsonProperty("weather")]
        public List<WeatherIcons> WeatherIcons { get; set; }
    }

    public class WeatherDailyInfoDataModelRoot
    {
        [JsonProperty("list")]
        public List<List> List { get; set; }
    }
}
