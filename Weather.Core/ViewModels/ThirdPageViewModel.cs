using Caliburn.Micro;
using System.Collections.Generic;
using Weather.Core.Navigation;
using Weather.Models;
using Weather.Services.Contracts;

namespace Weather.Core.ViewModels
{
    public class ThirdPageViewModel : Screen
    {
        private const string _celsius = "Celsius";
        private const string _fahrengeit = "Fahrengeit";
        private readonly IWeatherProvider _weatherProvider;
        private readonly IAppNavigationService _navigationService;
        private List<WeatherDailyInfo> _weatherCollection;

        public WeatherParams Parameter { get; set; }

        public List<WeatherDailyInfo> WeatherCollection
        {
            get => _weatherCollection;
            set
            {
                _weatherCollection = value;
                NotifyOfPropertyChange(nameof(WeatherCollection));
            }
        }

        public string CityName
        {
            get => Parameter?.CityName;
        }

        public ThirdPageViewModel(IWeatherProvider weatherProvider, IAppNavigationService navigationService)
        {
            _weatherProvider = weatherProvider;
            _navigationService = navigationService;
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }

        private void InitWeatherDayCollection()
        {
            var getWeatherAtDate = _weatherProvider.GetDailyWeatherAtDate(Parameter.WeatherDailyInfo.DateFutureString);
            if (getWeatherAtDate != null)
            {
                WeatherCollection = new List<WeatherDailyInfo>(getWeatherAtDate);
            }
            SetMeasure(Parameter.Measure);
        }

        private void SetMeasure(string value)
        {
            switch (value)
            {
                case _celsius: SetTemperatureMeasure(true, false); break;
                case _fahrengeit: SetTemperatureMeasure(false, true); break;
            }
        }

        private void SetTemperatureMeasure(bool isCelsiusClicked, bool isFahrengeitClicked)
        {
            if (WeatherCollection != null)
            {
                foreach (var weather in WeatherCollection)
                {
                    weather.IsCelsiusClicked = isCelsiusClicked;
                    weather.IsFahrengeitClicked = isFahrengeitClicked;
                }
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            InitWeatherDayCollection();
        }
    }
}
