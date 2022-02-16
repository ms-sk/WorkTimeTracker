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
        private readonly MainViewModel _mainViewModel;
        private readonly ILogger _logger;

        public MainWindow(MainViewModel mainViewModel, ILogger logger)
        {
            InitializeComponent();
            
            _mainViewModel = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            DataContext = mainViewModel;
            
            AppDomain.CurrentDomain.UnhandledException += HandleException;

            Loaded += LoadWorkTimes;
        }

        void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.Error((Exception)e.ExceptionObject);
        }

        async void LoadWorkTimes(object sender, RoutedEventArgs e)
        {
            await _mainViewModel.MasterViewModel.LoadWorkTimes();
            await _mainViewModel.MasterViewModel.LoadSettings();
        }
    }
}
