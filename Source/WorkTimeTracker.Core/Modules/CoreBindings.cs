using Ninject.Modules;
using WorkTimeTracker.Core.Logging;
using WorkTimeTracker.Core.Storage;

namespace WorkTimeTracker.Core.Modules;

public sealed class CoreBindings : NinjectModule
{
    public override void Load()
    {
        Bind<IDayStorage>().To<DayStorage>().InSingletonScope();
        Bind<ISettingsStorage>().To<SettingsStorage>().InSingletonScope();
        Bind<ITaskStorage>().To<TaskStorage>().InSingletonScope();
        Bind<ILogger>().To<Logger>().InSingletonScope();
    }
}