using Ninject.Modules;
using WorkTimeTracker.Core.Logging;
using WorkTimeTracker.Core.Models;
using WorkTimeTracker.Core.Storage;

namespace WorkTimeTracker.Core.Modules;

public sealed class CoreBindings : NinjectModule
{
    public override void Load()
    {
        Bind<IStorage<List<Day>>>().To<DayStorage>().InSingletonScope();
        Bind<IStorage<WorkTime>>().To<WorkTimeStorage>().InSingletonScope();
        Bind<IStorage<Settings>>().To<SettingsStorage>().InSingletonScope();
        Bind<ILogger>().To<Logger>().InSingletonScope();
    }
}