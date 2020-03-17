using Caliburn.Micro;
using Weather.Core.Navigation;

namespace Weather.Core.ViewModels
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel(IAppNavigationService navigationService)
        {
            navigationService.NavigateToViewModel<HomeViewModel>();
        }
    }
}
