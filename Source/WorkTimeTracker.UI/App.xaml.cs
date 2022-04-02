using Ninject;
using System;
using WorkTimeTracker.Core.Configuration;
using WorkTimeTracker.Core.Modules;
using System.IO;
using System.Text.Json;
using WorkTimeTracker.Core.Models;
using System.Collections.Generic;

namespace WorkTimeTracker.UI
{
    public partial class App
    {
        public App()
        {
            var kernel = new StandardKernel (
                new CoreBindings(),
                new WorkTimeTrackerBindings()
            );

            MigrateWorkTime(kernel.Get<Paths>());

            Startup += (_, _) =>
            {
                MainWindow = kernel.Get<MainWindow>();
                MainWindow.Show();
            };
        }

        void MigrateWorkTime(Paths paths)
        {
            if (paths is null)
            {
                throw new ArgumentNullException(nameof(paths));
            }

            if(File.Exists(paths.WorkTime))
            {
                var json = File.ReadAllText(paths.WorkTime);
                if(string.IsNullOrEmpty(json))
                {
                    return;
                }

                WorkTime workTime = null;
                try
                {
                    workTime = JsonSerializer.Deserialize<WorkTime>(json);
                } catch
                {
                    return;
                }

                json = JsonSerializer.Serialize(workTime?.Days, new JsonSerializerOptions() { WriteIndented = true });
                File.WriteAllText(paths.WorkTime, json);
            }
        }
    }
}
