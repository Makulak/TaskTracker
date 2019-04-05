using System;
using System.Collections.ObjectModel;
using TaskTracker.Data;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.ViewModels.VM.Base;

namespace TaskTracker.ViewModels.VM
{
    internal class ColumnVM : BaseVM
    {
        public Column Base { get; set; }

        public Action<string> DisplayExceptionMessage;

        public string Name => Base.Name;

        public ObservableCollection<TaskVM> TaskCollection => new ObservableCollection<TaskVM>(Base.Tasks.ConvertAll<TaskVM>(x => x));

        private readonly RestManager _manager;

        public ColumnVM()
        {
            _manager = new RestManager(new RestService());
        }

        public static implicit operator ColumnVM(Column column)
        {
            return new ColumnVM
            {
                Base = column
            };
        }

        internal async void RemoveTask(TaskVM task)
        {
            if (task == null)
                return;
            try
            {
                await _manager.DeleteTask(task.Base.Id);

                Base.Tasks.Remove(task.Base);
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage(ex.CompleteMessage);
            }
        }
    }
}
