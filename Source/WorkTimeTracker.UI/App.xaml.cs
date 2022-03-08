using Ninject;
using WorkTimeTracker.Core.Modules;

namespace WorkTimeTracker.UI
{
    public partial class App
    {
        public App()
        {
            var kernel = new StandardKernel (
                new CoreBindings(),
                new WorkTimeTrackerBindings()
            );

            Startup += (_, _) =>
            {
                MainWindow = kernel.Get<MainWindow>();
                MainWindow.Show();
            };
        }
    }
}
