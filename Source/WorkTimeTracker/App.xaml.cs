using Core.Modules;
using Ninject;
using WorkTimeTracker.ViewModels;

namespace WorkTimeTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            var kernel = new StandardKernel(new CoreBindings());
            kernel.Bind<MainWindow>().To<MainWindow>();
            kernel.Bind<WorkTimeUpdater>().To<WorkTimeUpdater>();
            kernel.Bind<WorkTimeTodayUpdater>().To<WorkTimeTodayUpdater>();
            kernel.Bind<MasterViewModel>().To<MasterViewModel>();
            kernel.Bind<DetailsViewModel>().To<DetailsViewModel>();
            
            Startup += (_, _) =>
            {
                MainWindow = kernel.Get<MainWindow>();
                MainWindow.Show();
            };
        }
    }
}
