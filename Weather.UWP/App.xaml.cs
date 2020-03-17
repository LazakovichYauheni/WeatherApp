using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Weather.Core.Contracts;
using Weather.Core.Navigation;
using Weather.Core.ViewModels;
using Weather.Services.Contracts;
using Weather.Services.Services;
using Weather.UWP.Navigation;
using Weather.UWP.Views;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Weather.UWP
{
    public sealed partial class App
    {
        private WinRTContainer _container;
        public App()
        {
            Initialize();
            InitializeComponent();
        }

        protected override void Configure()
        {
            _container = new WinRTContainer();
            _container.RegisterWinRTServices();
           
            _container.Singleton<IWeatherService, WeatherService>();
            _container.Singleton<IWeatherProvider, WeatherProvider>();
            _container.Singleton<AppNavigationService>();
            _container.Singleton<IInteractionService, InteractionService>();
            _container.Handler<IAppNavigationService>(cnt => cnt.GetInstance<AppNavigationService>());

            _container.PerRequest<ShellViewModel>();
            _container.PerRequest<HomeViewModel>();
            _container.PerRequest<SecondPageViewModel>();
            _container.PerRequest<ThirdPageViewModel>();
           

            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "Weather.UWP.Views",
                DefaultSubNamespaceForViewModels = "Weather.Core.ViewModels"
            };

            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            _container.RegisterNavigationService(rootFrame);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                return;

            DisplayRootView<ShellView>();
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.CacheSize = 3;
            Window.Current.Activate();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var assemblies = base.SelectAssemblies().ToList();
            assemblies.Add(typeof(HomeViewModel).GetTypeInfo().Assembly);

            return assemblies;
        }

        protected override void OnUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
        }
    }
}
