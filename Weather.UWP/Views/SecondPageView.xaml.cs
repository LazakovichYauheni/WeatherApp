using Caliburn.Micro;
using Weather.Core.Contracts;
using Weather.Models;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Weather.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SecondPageView : Page
    {
        private readonly IInteractionService _interactionService;
        public SecondPageView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            _interactionService = IoC.Get<IInteractionService>();
        }

        private void WeatherItem_Clicked(object sender, ItemClickEventArgs e)
        {
            var item = (WeatherDailyInfo)e.ClickedItem;
            _interactionService.ItemClicked(sender, item);
        }
    }
}
