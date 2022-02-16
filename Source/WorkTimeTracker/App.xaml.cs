using System.Windows;
using Core.Modules;
using Ninject;
using WorkTimeTracker.ViewModels;

namespace WorkTimeTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var kernel = new StandardKernel(new StandardBindings());
            kernel.Bind<WorkTimeUpdater>().To<WorkTimeUpdater>();
            kernel.Bind<WorkTimeTodayUpdater>().To<WorkTimeTodayUpdater>();
            kernel.Bind<MasterViewModel>().To<MasterViewModel>();
            kernel.Bind<DetailsViewModel>().To<DetailsViewModel>();

            Properties["kernel"] = kernel;
        }
    }
}
