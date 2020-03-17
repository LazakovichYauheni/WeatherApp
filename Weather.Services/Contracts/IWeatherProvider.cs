using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Services.Contracts
{
    public interface IWeatherProvider
    {
        Task<WeatherInfo> GetWeatherAsync(string cityName);
        Task<IEnumerable<WeatherDailyInfo>> GetDailyWeatherAtHoursAsync(string cityName);
        IEnumerable<WeatherDailyInfo> GetDailyWeatherAtDate(string date);
    }
}
