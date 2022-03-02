using Core.Wpf.Loading;
using Ninject.Modules;
using WorkTimeTracker.ViewModels;

namespace WorkTimeTracker
{
    public sealed class WorkTimeTrackerBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<MainWindow>().ToSelf().InSingletonScope();
            Bind<WorkTimeUpdater>().ToSelf().InSingletonScope();
            Bind<WorkTimeTodayUpdater>().ToSelf().InSingletonScope();
            Bind<MasterViewModel>().ToSelf().InSingletonScope();
            Bind<DetailsViewModel>().ToSelf().InSingletonScope();
            Bind<LoaderViewModel>().ToSelf().InSingletonScope();
            Bind<FooterViewModel>().ToSelf().InSingletonScope();
        }
    }
}
