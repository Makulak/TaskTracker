using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskTracker.Data;
using TaskTracker.Models;
using Xamarin.Forms;

namespace TaskTracker.ViewModels.Page
{
    class BoardPageViewModel : BaseViewModel
    {
        public ObservableCollection<Board> UserBoards
        {
            get => _userBoards;
            set
            {
                _userBoards = value;
                OnPropertyChanged("UserBoards");
            }
        }
        private ObservableCollection<Board> _userBoards;

        public Board SelectedBoard
        {
            get => _selectedBoard;
            set
            {
                _selectedBoard = value;
                OnPropertyChanged("SelectedBoard");
                OnBoardSelected();
            }
        }
        private Board _selectedBoard;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = false;
                OnPropertyChanged("IsRefreshing");
            }
        }
        private bool _isRefreshing;

        public ICommand RefreshCommand { get; set; }

        public Action<Board> DisplayMainPage;

        private readonly RestManager _manager;

        public BoardPageViewModel()
        {
            _manager = new RestManager(new RestService());

            RefreshCommand = new Command(OnRefresh);

            SelectedBoard = null;

            GetUserBoards();
        }

        private void OnRefresh(object obj)
        {
            GetUserBoards();

            IsRefreshing = false;
        }

        private async void GetUserBoards()
        {
            List<Board> boards = await _manager.GetLoggedUserBoards();

            UserBoards = new ObservableCollection<Board>(boards);
        }

        private void OnBoardSelected()
        {
            if (SelectedBoard != null)
                DisplayMainPage(SelectedBoard);
        }
    }
}
