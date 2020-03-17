using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Models;
using Weather.Services.Contracts;
using Weather.Services.DataModels;

namespace Weather.Services.Services
{
    public class WeatherProvider : IWeatherProvider
    {
        private readonly IWeatherService _weatherService;
        private List<WeatherDailyInfo> _dailyWeatherList { get; set; } = new List<WeatherDailyInfo>();

        public WeatherProvider(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<IEnumerable<WeatherDailyInfo>> GetDailyWeatherAtHoursAsync(string cityName)
        {
            _dailyWeatherList.Clear();

            await InitDailyWeatherListAsync(cityName);

            return _dailyWeatherList.Where(x => (DateTime.Now.Hour - x.Date.Hour) <= 3 && (x.Date.Hour - DateTime.Now.Hour) <= 3 && (DateTime.Now.Hour - x.Date.Hour) > 0);
        }

        public IEnumerable<WeatherDailyInfo> GetDailyWeatherAtDate(string date)
        {
            if (_dailyWeatherList == null || !_dailyWeatherList.Any())
            {
                //await InitDailyWeatherListAsync(cityName);
            }

            return _dailyWeatherList?.Where(x => x.DateFutureString.Contains(date));
        }

        public async Task<WeatherInfo> GetWeatherAsync(string cityName)
        {
            var weatherRoot = await _weatherService.GetWeatherAsync(cityName);
            if (weatherRoot == null)
                return null;
            return new WeatherInfo()
            {
                Icon = weatherRoot.Weather[0].Icon,
                Description = weatherRoot.Weather[0].Description,
                Id = weatherRoot.Weather[0].Id,
                Main = weatherRoot.Weather[0].Main,
                Temp = weatherRoot.WeatherModel.Temp,
                FeelsLike = weatherRoot.WeatherModel.Feels_like,
                Temp_max = weatherRoot.WeatherModel.Temp_max,
                Temp_min = weatherRoot.WeatherModel.Temp_min,
                Pressure = weatherRoot.WeatherModel.Pressure,
                Hudimity = weatherRoot.WeatherModel.Hudimity,
                Longitude = weatherRoot.WeatherCoordinate.Longitude,
                Latitude = weatherRoot.WeatherCoordinate.Latitude
            };
        }

        private async Task InitDailyWeatherListAsync(string cityName)
        {
            var dailyWeatherRoot = await _weatherService.GetDailyWeatherAsync(cityName);
            if (dailyWeatherRoot != null && dailyWeatherRoot.Any())
            {
                foreach (var day in dailyWeatherRoot)
                {
                    _dailyWeatherList.Add(GetDailyWeather(day));
                }
            }
        }

        private WeatherDailyInfo GetDailyWeather(List list)
        {
            return new WeatherDailyInfo
            {
                Date = DateTime.Parse(list.Date),
                TemperatureCelsius = list.WeatherDailyInfoDataModel.Temperature,
                TemperatureFahrenheit = ConvertCelsiusToFahrenheit(list.WeatherDailyInfoDataModel.Temperature),
                Icon = list.WeatherIcons[0].Icon
            };
        }

        private double ConvertCelsiusToFahrenheit(double temperatureCelsius)
        {
            return temperatureCelsius * 9 / 5 + 32;
        }
    }
}
