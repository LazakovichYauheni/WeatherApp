using Caliburn.Micro;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Weather.UWP.Navigation
{
    public class FrameAdapterWrapper : FrameAdapter
    {
        private readonly Frame _frame;
        private object _viewModel;

        public FrameAdapterWrapper(Frame frame, bool treatViewAsLoaded = false) : base(frame, treatViewAsLoaded)
        {
            _frame = frame;
        }

        public void NavigateToViewModel(Type viewModelType, string key)
        {
            NavigateToViewModel(IoC.GetInstance(viewModelType, key));
        }

        public void NavigateToViewModel<TViewModel>(string key)
        {
            NavigateToViewModel(IoC.GetInstance(typeof(TViewModel), key));
        }

        public void NavigateToViewModel(object viewModel)
        {
            NavigateToViewModel(viewModel, null);
        }

        public override bool Navigate(Type sourcePageType, object parameter)
        {
            return _frame.Navigate(sourcePageType, parameter, new EntranceNavigationTransitionInfo());
        }

        public override bool Navigate(Type sourcePageType)
        {
            return _frame.Navigate(sourcePageType, null, new EntranceNavigationTransitionInfo());
        }

        protected override bool CanCloseOnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            return true;
        }

        protected override void BindViewModel(DependencyObject view, object viewModel = null)
        {
            var actualViewModel = _viewModel ?? viewModel;
            base.BindViewModel(view, actualViewModel);
        }

        private void NavigateToViewModel(object viewModel, object parameter)
        {
            _viewModel = viewModel;

            var viewType = ViewLocator.LocateTypeForModelType(_viewModel.GetType(), null, null);

            BackStack.Clear();

            base.Navigate(viewType, parameter);

            _viewModel = null;
        }
    }
}
