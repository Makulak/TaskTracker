using TaskTracker.Data;
using TaskTracker.Models;
using TaskTracker.ViewModels.Page.Base;
using TaskTracker.ViewModels.VM;

namespace TaskTracker.ViewModels.Page
{
    class MainPageViewModel : BaseViewModel
    {
        public BoardVM SelectedBoard { get; set; }

        private readonly RestManager _manager;

        public MainPageViewModel(BoardVM selectedBoard)
        {
            SelectedBoard = selectedBoard;

            TaskVM t = new TaskVM();
            Task y = new Task();

            t = (TaskVM)y;

            _manager = new RestManager(new RestService());
        }
    }
}
