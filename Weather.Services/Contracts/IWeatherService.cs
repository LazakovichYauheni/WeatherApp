using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Services.DataModels;

namespace Weather.Services.Contracts
{
    public interface IWeatherService
    {
        Task<WeatherModelRoot> GetWeatherAsync(string cityName);
        Task<IEnumerable<List>> GetDailyWeatherAsync(string cityName);
    }
}
