using Caliburn.Micro;
using System;
using System.Linq;
using System.Threading.Tasks;
using Weather.Core.Navigation;
using Windows.UI.Xaml.Controls;

namespace Weather.UWP.Navigation
{
    public class AppNavigationService : IAppNavigationService
    {
        private readonly TaskCompletionSource<bool> _taskCompletion;

        private FrameAdapterWrapper _shellFrame;
        private Frame _frame;

        public Type PreviousPage { get; private set; }
        public Type CurrentPage { get; private set; }

        public AppNavigationService()
        {
            _taskCompletion = new TaskCompletionSource<bool>();
        }

        public void SetShellFrame(Frame frame)
        {
            _frame = frame;
            _shellFrame = new FrameAdapterWrapper(_frame);

            _taskCompletion.TrySetResult(true);
        }

        public async void NavigateToViewModel<TViewModel>(object parameter = null)
        {
            if (_shellFrame == null)
            {
                await _taskCompletion.Task;
            }

            PreviousPage = CurrentPage;
            CurrentPage = typeof(TViewModel);

            _shellFrame.NavigateToViewModel<TViewModel>(parameter);
        }

        public async void NavigateToViewModel(Type viewModelType, string key)
        {
            if (_shellFrame == null)
            {
                await _taskCompletion.Task;
            }

            PreviousPage = CurrentPage;
            CurrentPage = viewModelType;

            _shellFrame.NavigateToViewModel(viewModelType, key);
        }

        public async void NavigateToViewModel(object viewModel)
        {
            if (_shellFrame == null)
            {
                await _taskCompletion.Task;
            }

            PreviousPage = CurrentPage;
            CurrentPage = viewModel.GetType();

            _shellFrame.NavigateToViewModel(viewModel);
        }

        public void GoBack()
        {
            CurrentPage = PreviousPage;
            PreviousPage = null;

            if (_shellFrame.CanGoBack)
            {
                _shellFrame.GoBack();
            }
        }

        public void ReloadViewModel()
        {
            var type = _shellFrame.CurrentSourcePageType;
            var param = new object();

            if (_shellFrame.BackStack.Any())
            {
                type = _shellFrame.BackStack.Last().SourcePageType;
                param = _shellFrame.BackStack.Last().Parameter;
            }
            try
            {
                _shellFrame.Navigate(type, param);
            }
            finally
            {
                var view = _shellFrame.BackStack.Last();
                _shellFrame.BackStack.Remove(view);
                DropFrameCache();
            }
        }

        private void DropFrameCache()
        {
            if (_frame == null)
            {
                return;
            }

            var cacheSize = _frame.CacheSize;
            _frame.CacheSize = 0;
            _frame.CacheSize = cacheSize;
        }
    }

}
