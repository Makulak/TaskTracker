using System.Collections.ObjectModel;
using TaskTracker.Models;
using TaskTracker.ViewModels.VM.Base;

namespace TaskTracker.ViewModels.VM
{
    internal class BoardVM : BaseVM
    {
        public Board Base { get; set; }

        public string Name => Base.Name;

        public ObservableCollection<ColumnVM> ColumnsCollection => new ObservableCollection<ColumnVM>(Base.Columns.ConvertAll<ColumnVM>(x => x));

        public static implicit operator BoardVM(Board board)
        {
            return new BoardVM
            {
                Base = board
            };
        }
    }
}
