using Core.Modules;
using Core.Wpf.Loading;
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
            kernel.Bind<LoaderViewModel>().ToSelf().InSingletonScope();

            Startup += (_, _) =>
            {
                MainWindow = kernel.Get<MainWindow>();
                MainWindow.Show();
            };
        }
    }
}
