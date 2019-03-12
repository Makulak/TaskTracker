using System.Collections.Generic;
using System.Collections.ObjectModel;
using TaskTracker.Data;
using TaskTracker.Models;

namespace TaskTracker.ViewModels.Page
{
    class BoardPageViewModel
    {
        public ObservableCollection<Board> UserBoards { get; set; }

        private readonly RestManager _manager;

        public BoardPageViewModel()
        {
            _manager = new RestManager(new RestService());

            GetUserBoards();
        }

        private async void GetUserBoards()
        {
            List<Board> boards = await _manager.GetLoggedUserBoards();

            UserBoards = new ObservableCollection<Board>(boards);
        }
    }
}
