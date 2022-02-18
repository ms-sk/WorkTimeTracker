using Core.Dtos;
using Core.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkTimeTracker.Factories;

namespace WorkTimeTracker.ViewModels
{
    public sealed class ToolbarViewModel
    {
        readonly IStorage<List<Day>> storage;
        readonly DayFactory dayFactory;

        public ToolbarViewModel(SumViewModel sum, IStorage<List<Day>> storage, DayFactory dayFactory)
        {
            Sum = sum ?? throw new System.ArgumentNullException(nameof(sum));
            this.storage = storage;
            this.dayFactory = dayFactory;
            Sum.DisplayText = "8.25";

            Add = new Command(ExecuteAdd, (_) => true);
            Save = new AsyncCommand(ExecuteSave, (_) => true);
            SaveAll = new AsyncCommand(ExecuteSaveAll, CanExecuteAll);
            Settings = new Command(ExecuteSettings, (_) => true);
        }

        public SumViewModel Sum { get; }

        public ICommand Add { get; }

        public ICommand Save { get; }

        public ICommand SaveAll { get; }

        public ICommand Settings { get; }

        async Task ExecuteSave(object? parameter)
        {
            if (parameter is DayViewModel dayViewModel)
            {
                var dto = dayFactory.CreateDay(dayViewModel);
                await storage.Save(new List<Day>() { dto });
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

        void ExecuteAdd(object? arg)
        {
            throw new NotImplementedException();
        }

        void ExecuteSettings(object? obj)
        {
            throw new NotImplementedException();
        }
    }
}