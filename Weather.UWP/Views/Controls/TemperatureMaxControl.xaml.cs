using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Weather.UWP.Views.Controls
{
    public sealed partial class TemperatureMaxControl : UserControl
    {
        public static readonly DependencyProperty PressureValueProperty = 
            DependencyProperty.Register("PressureValue", 
                typeof(string),
                typeof(TemperatureMaxControl),
                new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnPressureValueChanged)));

        public string PressureValue
        {
            get { return (string)GetValue(PressureValueProperty); }
            set { SetValue(PressureValueProperty, value); }
        }

        public TemperatureMaxControl()
        {
            this.InitializeComponent();
        }

        private static void OnPressureValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pressureControl = (TemperatureMaxControl)d;
            pressureControl.UpdateMaxTemp();
        }

        private void UpdateMaxTemp()
        {
            PressureTitle.Text = PressureValue;
        }
    }
}
