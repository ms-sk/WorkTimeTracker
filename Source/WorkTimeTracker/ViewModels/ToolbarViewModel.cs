using Core.Dtos;
using Core.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkTimeTracker.ViewModels
{
    public sealed class ToolbarViewModel
    {
        readonly IStorage<List<Day>> storage;

        public ToolbarViewModel(SumViewModel sum, IStorage<List<Day>> storage)
        {
            Sum = sum ?? throw new System.ArgumentNullException(nameof(sum));
            this.storage = storage;
            Sum.DisplayText = "8.25";

            Save = new AsyncCommand(ExecuteSave, (selectedItem) => selectedItem != null);
            SaveAll = new AsyncCommand(ExecuteSaveAll, CanExecuteAll);
            Settings = new Command(ExecuteSettings, (_) => true);
        }

        void ExecuteSettings(object? obj)
        {
            throw new NotImplementedException();
        }

        public SumViewModel Sum { get; }

        public ICommand Save { get; }

        public ICommand SaveAll { get; }

        public ICommand Settings { get; }

        async Task ExecuteSave(object? parameter)
        {
            if (parameter is DayViewModel dayViewModel)
            {
                await storage.Save(new List<Day>() { dayViewModel?.Dto ?? throw new InvalidOperationException() });
            }
        }

        bool CanExecuteAll(object? arg)
        {
            if (arg is List<DayViewModel> list)
            {
                return list.Count > 0;
            }

            return false;
        }

        Task ExecuteSaveAll(object? arg)
        {
            return Task.CompletedTask;
        }

    }
}