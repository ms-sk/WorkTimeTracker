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
            kernel.Bind<MainWindow>().ToSelf().InSingletonScope();
            kernel.Bind<WorkTimeUpdater>().ToSelf().InSingletonScope();
            kernel.Bind<WorkTimeTodayUpdater>().ToSelf().InSingletonScope();
            kernel.Bind<MasterViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<DetailsViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<LoaderViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<FooterViewModel>().ToSelf().InSingletonScope();

            Startup += (_, _) =>
            {
                MainWindow = kernel.Get<MainWindow>();
                MainWindow.Show();
            };
        }
    }
}
