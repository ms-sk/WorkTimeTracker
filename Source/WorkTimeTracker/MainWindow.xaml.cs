using Core.Logging;
using Core.Modules;
using Ninject;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WorkTimeTracker.ViewModels;

namespace WorkTimeTracker
{
    public partial class MainWindow
    {
        readonly MainViewModel _mainViewModel;
        readonly ILogger _logger;

        public MainWindow(MainViewModel mainViewModel, ILogger logger)
        {
            InitializeComponent();

            _mainViewModel = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            DataContext = mainViewModel ?? throw new ArgumentNullException(nameof(mainViewModel));

            AppDomain.CurrentDomain.UnhandledException += HandleException;

            Loaded += LoadWorkTimes;
        }

        void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.Error((Exception)e.ExceptionObject);
        }

        async void LoadWorkTimes(object sender, RoutedEventArgs e)
        {
            await Refresh();
        }

        async void OnRefresh(object sender, ExecutedRoutedEventArgs e)
        {
            await Refresh();
        }

        async Task Refresh()
        {
            await _mainViewModel.MasterViewModel.Init();
            await _mainViewModel.MasterViewModel.LoadWorkTimes();
            await _mainViewModel.MasterViewModel.InitDay();
            await _mainViewModel.MasterViewModel.LoadSettings();
        }
    }
}
