using Caliburn.Micro;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Core.Contracts;
using Weather.Core.Navigation;
using Weather.Models;
using Weather.Services.Contracts;

namespace Weather.Core.ViewModels
{
    public class SecondPageViewModel : Screen
    {
        private const string _celsius = "Celsius";
        private const string _fahrengeit = "Fahrengeit";
        private readonly IAppNavigationService _appNavigationService;
        private readonly IWeatherProvider _weatherProvider;
        private readonly IInteractionService _interactionService;
        private List<WeatherDailyInfo> _weatherCollection;
        private string _selectedItem;

        public string Parameter { get; set; }

        public List<string> Items { get; set; } = new List<string>
        {
         _celsius,
         _fahrengeit
        };

        public List<WeatherDailyInfo> WeatherCollection
        {
            get => _weatherCollection;
            set
            {
                _weatherCollection = value;
                NotifyOfPropertyChange(nameof(WeatherCollection));
            }
        }

        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _selectedItem = value;
                    SetMeasure(value);
                    NotifyOfPropertyChange(() => SelectedItem);
                }
            }
        }

        private WeatherParams WeatherParams { get; set; } = new WeatherParams();

        public SecondPageViewModel(IAppNavigationService appNavigationService, IWeatherProvider weatherProvider, IInteractionService interactionService)
        {
            _appNavigationService = appNavigationService;
            _weatherProvider = weatherProvider;
            _interactionService = interactionService;
            _interactionService.ItemClicked += OnGoToThirdPage;

            _selectedItem = Items.FirstOrDefault();
        }
        public void GoHomeBack()
        {
            _appNavigationService.GoBack();
        }

        public void OnGoToThirdPage(object sender, WeatherDailyInfo weatherDailyInfo)
        {
            WeatherParams.WeatherDailyInfo = weatherDailyInfo;
            WeatherParams.CityName = Parameter;
            WeatherParams.Measure = SelectedItem;
            _appNavigationService.NavigateToViewModel<ThirdPageViewModel>(WeatherParams);
        }

        private async Task InitWeatherDailyListAsync()
        {
            var getWeatherAtHoursAsync = await _weatherProvider.GetDailyWeatherAtHoursAsync(Parameter);
            if (getWeatherAtHoursAsync != null)
            {
                WeatherCollection = new List<WeatherDailyInfo>(getWeatherAtHoursAsync);
            }
            SetTemperatureMeasure(true, false);
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

        protected override async void OnActivate()
        {
            base.OnActivate();

            if (WeatherCollection == null || !WeatherCollection.Any())
            {
                await InitWeatherDailyListAsync();
            }
        }
    }
}
