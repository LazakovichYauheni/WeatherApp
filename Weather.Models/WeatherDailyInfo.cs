using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Weather.Models
{
    public class WeatherDailyInfo : INotifyPropertyChanged
    {
        private bool _isCelsiusClicked;
        private bool _isFahrengeitClicked;

        public double TemperatureCelsius { get; set; }
        public double TemperatureFahrenheit { get; set; }
        public DateTime Date { get; set; }
        public string DatePreviousString => Date.ToString("dd MMM", CultureInfo.InvariantCulture);
        public string DateFutureString => Date.ToString("dd MMM", CultureInfo.InvariantCulture);
        public string DayTime => Date.ToString("HH:mm");
        public string Icon { get; set; }

        public bool IsCelsiusClicked
        {
            get { return _isCelsiusClicked; }
            set
            {
                _isCelsiusClicked = value;
                NotifyPropertyChanged(nameof(IsCelsiusClicked));
            }
        }
        public bool IsFahrengeitClicked
        {
            get { return _isFahrengeitClicked; }
            set
            {
                _isFahrengeitClicked = value;
                NotifyPropertyChanged(nameof(IsFahrengeitClicked));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
