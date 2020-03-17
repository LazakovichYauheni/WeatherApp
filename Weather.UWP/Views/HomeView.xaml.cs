using Windows.UI.Xaml.Controls;

namespace Weather.UWP.Views
{
    public sealed partial class HomeView : Page
    {
        public HomeView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }
    }
}
