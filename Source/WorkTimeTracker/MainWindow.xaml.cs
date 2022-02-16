using Core.Logging;
using Core.Modules;
using Ninject;
using System;
using System.Windows;
using WorkTimeTracker.ViewModels;

namespace WorkTimeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly StandardKernel _kernel;
        
        public MainWindow()
        {
            InitializeComponent();
            
            _kernel = Application.Current.Properties["kernel"] as StandardKernel ?? throw new InvalidOperationException();
            AppDomain.CurrentDomain.UnhandledException += HandleException;

            Loaded += LoadWorkTimes;
        }

        void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            _kernel.Get<ILogger>().Error((Exception)e.ExceptionObject);
        }

        async void LoadWorkTimes(object sender, RoutedEventArgs e)
        {
            var mainViewModel = _kernel.Get<MainViewModel>();
            DataContext = mainViewModel;
            await mainViewModel.MasterViewModel.LoadWorkTimes();
            await mainViewModel.MasterViewModel.LoadSettings();
        }
    }
}
