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

        public ICommand AddBoardCommand { get; set; }
        public ICommand DeleteBoardCommand { get; set; }
        public ICommand EditBoardCommand { get; set; }

        public Action<BoardVM> DisplayMainPage;
        public Action<BoardVM> DisplayEditBoard;
        public Action HideKeyboard;

        private readonly RestManager _manager;

        public string NewBoardName
        {
            get => _newBoardName;
            set
            {
                _newBoardName = value;
                OnPropertyChanged("BoardName");
            }
        }
        private string _newBoardName;

        public BoardPageViewModel()
        {
            _manager = new RestManager(new RestService());

            DeleteBoardCommand = new Command(OnDeleteBoard);
            AddBoardCommand = new Command(OnAddBoard);

            GetUserBoards();
        }

        #region Commands

        private void OnDeleteBoard(object obj)
        {
            if (obj is Board board)
            {
                DeleteSelectedBoard(board);

                GetUserBoards();
            }
        }

        private void OnAddBoard(object obj)
        {
            AddNewBoard(NewBoardName);
            NewBoardName = string.Empty;
            HideKeyboard?.Invoke();
        }

        #endregion

        #region Methods

        internal async void GetUserBoards()
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
