using Caliburn.Micro;
using Weather.Core.ViewModels;
using Weather.UWP.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Weather.UWP.Views
{
    public sealed partial class ShellView : Page
    {
        private readonly AppNavigationService _appNavigationService;
        private ShellViewModel _viewModel;

        public ShellView()
        {
            this.InitializeComponent();

            _appNavigationService = IoC.Get<AppNavigationService>();
        }

        private void OnRootFrameLoaded(object sender, RoutedEventArgs e)
        {
            _appNavigationService.SetShellFrame(RootFrame);
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            _viewModel = DataContext as ShellViewModel;
        }
    }
}
