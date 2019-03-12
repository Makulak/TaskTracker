using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.ViewModels.Page
{
    class MainPageViewModel : BaseViewModel
    {
        public Board SelectedBoard { get; set; }

        private readonly RestManager _manager;

        public MainPageViewModel()
        {
            _manager = new RestManager(new RestService());
        }
    }
}
