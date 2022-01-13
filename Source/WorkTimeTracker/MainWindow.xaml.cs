using Core.Logging;
using Core.Modules;
using Ninject;
using System;
using System.Windows;
using System.Windows.Input;
using WorkTimeTracker.ViewModels;

namespace WorkTimeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly StandardKernel _kernel;

        public MainWindow()
        {
            InitializeComponent();

            _kernel = new StandardKernel(new StandardBindings());
            AppDomain.CurrentDomain.UnhandledException += HandleException;

            Loaded += LoadWorkTimes;
        }

        void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            _kernel.Get<ILogger>().Error((Exception)e.ExceptionObject);
        }

        async void LoadWorkTimes(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(FilterComboBox);

            _kernel.Bind<WorkTimeUpdater>().To<WorkTimeUpdater>();
            _kernel.Bind<WorkTimeTodayUpdater>().To<WorkTimeTodayUpdater>();

            var mainViewModel = _kernel.Get<MainViewModel>();
            DataContext = mainViewModel;
            await mainViewModel.LoadWorkTimes();
            await mainViewModel.LoadSettings();
        }

        void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.Filter();
            }
        }
    }
}
