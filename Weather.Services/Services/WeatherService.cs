using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Services.Contracts;
using Weather.Services.DataModels;

namespace Weather.Services.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private const string _urlFormat = "http://api.openweathermap.org/data/2.5/weather?q={0}&APPID=c5cd9601de7f68f23e20332977d214d8&units=metric";
        private const string _dailyUrlFormat = "http://api.openweathermap.org/data/2.5/forecast?q={0}&APPID=c5cd9601de7f68f23e20332977d214d8&units=metric";
        public WeatherService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<WeatherModelRoot> GetWeatherAsync(string cityName)
        {
            var url = string.Format(_urlFormat, cityName);
            var data = await GetJsonDataAsync(url);
            if (data != null)
            {
                return JsonConvert.DeserializeObject<WeatherModelRoot>(data);
            }
            return null;
        }

        public async Task<IEnumerable<List>> GetDailyWeatherAsync(string cityName)
        {
            var url = string.Format(_dailyUrlFormat, cityName);
            var data = await GetJsonDataAsync(url);
            if (data != null)
            {
                return JsonConvert.DeserializeObject<WeatherDailyInfoDataModelRoot>(data).List;
            }
            return null;
        }

        private async Task<string> GetJsonDataAsync(string url)
        {
            using (HttpRequestMessage _httpRequest = new HttpRequestMessage())
            {
                _httpRequest.Method = HttpMethod.Get;
                _httpRequest.RequestUri = new Uri(url);

                var response = await _httpClient.SendAsync(_httpRequest);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return result;
                }
            }
            return null;
        }
    }
}
