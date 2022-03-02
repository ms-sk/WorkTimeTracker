using Core.Modules;
using Ninject;

namespace WorkTimeTracker
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
