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

            Loaded += LoadSettings;
        }

        async void LoadSettings(object sender, RoutedEventArgs e)
        {
            await settingsViewModel.LoadSettings();
        }
    }
}
