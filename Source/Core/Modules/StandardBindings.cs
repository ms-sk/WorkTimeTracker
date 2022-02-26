using Core.Dtos;
using Core.Logging;
using Core.Math;
using Core.Storage;
using Ninject.Modules;

namespace Core.Modules
{
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
}
