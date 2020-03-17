using Weather.Services.Contracts;
using Caliburn.Micro;
using System.Threading.Tasks;
using Weather.Models;
using Weather.Core.Navigation;

namespace Weather.Core.ViewModels
{
    public class HomeViewModel : Screen
    {
        private readonly IWeatherProvider _weatherProvider;
        private readonly IAppNavigationService _navigationService;


        private WeatherInfo _weather;
        private string _searchText;
        private string _errorText;
        private bool _isEnabledNextButton;
        private bool _isEnabledFindButton;

        public WeatherInfo Weather
        {
            get { return _weather; }
            set
            {
                _weather = value;
                NotifyOfPropertyChange(() => Weather);
            }
        }

        public bool IsEnabledNextButton
        {
            get { return _isEnabledNextButton; }
            set
            {
                _isEnabledNextButton = value;
                NotifyOfPropertyChange(() => IsEnabledNextButton);
            }
        }

        public bool IsEnabledFindButton
        {
            get { return _isEnabledFindButton; }
            set
            {
                _isEnabledFindButton = value;
                NotifyOfPropertyChange(() => IsEnabledFindButton);
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                CheckIsEnabledFindButton(value);
                NotifyOfPropertyChange(() => SearchText);
            }
        }

        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                NotifyOfPropertyChange(() => ErrorText);
            }
        }

        private WeatherInfo ErrorWeather { get; set; } = new WeatherInfo()
        {
            Description = "",
            FeelsLike = 0,
            Hudimity = 0,
            Icon = "",
            Pressure = "",
            Temp = 0
        };

        public HomeViewModel(IWeatherProvider weatherProvider, IAppNavigationService navigationService)
        {
            _weatherProvider = weatherProvider;
            _navigationService = navigationService;
        }

        public async Task PerformSearch()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                var data = await _weatherProvider.GetWeatherAsync(SearchText);

                Weather = data ?? ErrorWeather;
                ErrorText = data != null ? null : "Data is null";
            }

            IsEnabledNextButton = !string.IsNullOrEmpty(ErrorText) ? false : true;
        }

        public void NavigateToSecondPage()
        {
            if (!string.IsNullOrEmpty(SearchText) && Weather != null)
            {
                _navigationService.NavigateToViewModel<SecondPageViewModel>(SearchText);
            }

        }

        private void CheckIsEnabledFindButton(string value)
        {
            IsEnabledFindButton = !string.IsNullOrEmpty(value) ? true : false;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
        }
    }
}
