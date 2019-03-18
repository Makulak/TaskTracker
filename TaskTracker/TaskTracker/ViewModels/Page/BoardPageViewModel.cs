using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TaskTracker.Data;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.ViewModels.Page.Base;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;

namespace TaskTracker.ViewModels.Page
{
    class BoardPageViewModel : BaseViewModel
    {
        public ObservableCollection<BoardVM> UserBoards
        {
            get => _userBoards;
            set
            {
                _userBoards = value;
                OnPropertyChanged("UserBoards");
            }
        }
        private ObservableCollection<BoardVM> _userBoards;

        public BoardVM SelectedBoard
        {
            get => _selectedBoard;
            set
            {
                _selectedBoard = value;
                OnPropertyChanged("SelectedBoard");
                OnSelectedBoard();
            }
        }
        private BoardVM _selectedBoard;

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

        public Action<BoardVM> DisplayMainPage;
        public Action<BoardVM> DisplayEditBoard;
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

        #region Commands

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

        private void OnSelectedBoard()
        {
            if (SelectedBoard != null)
                DisplayMainPage?.Invoke(SelectedBoard.Base);
        }

        private void OnRefresh()
        {
            GetUserBoards();

            IsRefreshing = false;
        }

        #endregion

        #region Methods

        private async void GetUserBoards()
        {
            try
            {
                List<Board> boards = await _manager.GetLoggedUserBoards();

                UserBoards = new ObservableCollection<BoardVM>(boards.ConvertAll<BoardVM>(x => x));
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

        private async void EditSelectedBoard(Board board)
        {
            try
            {
                await _manager.EditBoard(board);
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage(ex.CompleteMessage);
            }
        }

        private async void AddNewBoard(string boardName)
        {
            try
            {
                await _manager.AddNewBoard(boardName);

                Board newBoard = await _manager.AddNewBoard(boardName);

                UserBoards.Add(newBoard);
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage(ex.CompleteMessage);
            }
        }

        #endregion
    }
}
