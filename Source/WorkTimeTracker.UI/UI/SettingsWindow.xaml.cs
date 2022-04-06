using System.Windows;
using WorkTimeTracker.UI.ViewModels;

namespace WorkTimeTracker.UI.UI
{
    public partial class SettingsWindow : Window
    {
        readonly SettingsViewModel settingsViewModel;

        public SettingsWindow(SettingsViewModel settingsViewModel)
        {
            InitializeComponent();
            DataContext = this.settingsViewModel = settingsViewModel ?? throw new System.ArgumentNullException(nameof(settingsViewModel));

            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            Loaded += LoadSettings;
            settingsViewModel.Cancelled += CloseOnCancelled;
        }

        void CloseOnCancelled(object? sender, System.EventArgs e)
        {
            Close();
        }

        async void LoadSettings(object sender, RoutedEventArgs e)
        {
            await settingsViewModel.LoadSettings();
        }
    }
}
