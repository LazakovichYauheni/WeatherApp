using System;

namespace Weather.Core.Navigation
{
    public interface IAppNavigationService
    {
        Type PreviousPage { get; }
        Type CurrentPage { get; }

        void NavigateToViewModel<TViewModel>(object parameter = null);
        void NavigateToViewModel(Type viewModelType, string key);
        void NavigateToViewModel(object viewModel);
        void GoBack();
        void ReloadViewModel();
    }
}
