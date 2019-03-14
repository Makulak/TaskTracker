using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskTracker.Data;
using TaskTracker.Exceptions;
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
        public ICommand AddBoardCommand { get; set; }
        public ICommand DeleteBoardCommand { get; set; }
        public ICommand EditBoardCommand { get; set; }

        public Action<Board> DisplayMainPage;
        public Action<Board> DisplayEditBoard;
        public Action DisplayAddBoard;

        private readonly RestManager _manager;

        public BoardPageViewModel()
        {
            _manager = new RestManager(new RestService());

            RefreshCommand = new Command(OnRefresh);
            AddBoardCommand = new Command(OnAddBoard);
            DeleteBoardCommand = new Command(OnDeleteBoard);
            EditBoardCommand = new Command(OnEditBoard);

            SelectedBoard = null;

            GetUserBoards();
        }

        private void OnEditBoard(object obj)
        {
            if (obj is Board board && DisplayAddBoard != null)
                DisplayEditBoard?.Invoke(board);
        }

        private void OnDeleteBoard(object obj)
        {
            if (obj is Board board)
            {
                DeleteSelectedBoard(board);
                GetUserBoards();
            }
        }

        private void OnAddBoard()
        {
            DisplayAddBoard?.Invoke();
        }

        private void OnBoardSelected()
        {
            if (SelectedBoard != null)
                DisplayMainPage?.Invoke(SelectedBoard);
        }

        private void OnRefresh()
        {
            GetUserBoards();

            IsRefreshing = false;
        }

        private async void GetUserBoards()
        {
            try
            {
                List<Board> boards = await _manager.GetLoggedUserBoards();

                UserBoards = new ObservableCollection<Board>(boards);
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage(ex.CompleteMessage);
            }
        }

        private async void DeleteSelectedBoard(Board board)
        {
            try
            {
                await _manager.DeleteBoard(board.Id);
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage(ex.CompleteMessage);
            }
        }
    }
}
