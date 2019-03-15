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

            _manager = new RestManager(new RestService());
        }
    }
}
