using System.Collections.ObjectModel;
using TaskTracker.Models;
using TaskTracker.ViewModels.VM.Base;

namespace TaskTracker.ViewModels.VM
{
    public class ColumnVM : BaseVM
    {
        public Column Base { get; set; }

        public string Name => Base.Name;

        public ObservableCollection<TaskVM> TaskCollection => new ObservableCollection<TaskVM>(Base.Tasks.ConvertAll<TaskVM>(x => x));

        public static implicit operator ColumnVM(Column column)
        {
            return new ColumnVM
            {
                Base = column
            };
        }
    }
}
