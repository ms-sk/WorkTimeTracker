using Core.Logging;
using Core.Math;
using Core.Storage;
using Dtos;
using Ninject.Modules;

namespace Core.Modules
{
    public sealed class StandardBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IStorage<WorkTime>>().To<WorkTimeStorage>();
            Bind<IStorage<Settings>>().To<SettingsStorage>();
            Bind<ILogger>().To<Logger>();
        }
    }
}
