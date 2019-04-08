using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using TaskTracker.Data;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.Resources;
using TaskTracker.ViewModels.Page.Base;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;

namespace TaskTracker.ViewModels.Page
{
    class BoardPageViewModel : BaseViewModel
    {
        private bool _editBoard; //TODO: Change that
        private BoardVM _selectedBoard;

        public ObservableCollection<BoardVM> UserBoards {
            get => _userBoards;
            set {
                _userBoards = value;
                OnPropertyChanged(nameof(UserBoards));
            }
        }
        private ObservableCollection<BoardVM> _userBoards;

        public ICommand ConfirmPopupCommand { get; set; }
        public ICommand DeleteBoardCommand { get; set; }
        public ICommand EditBoardCommand { get; set; }
        public ICommand AddBoardButtonCommand { get; set; }

        public Action<BoardVM> DisplayMainPage;
        public Action DisplayAddBoard;

        private readonly RestManager _manager;

        public string NewBoardName {
            get => _newBoardName;
            set {
                _newBoardName = value;
                OnPropertyChanged(nameof(NewBoardName));
            }
        }
        private string _newBoardName;

        public BoardPageViewModel()
        {
            _manager = new RestManager(new RestService());

            EditBoardCommand = new Command(OnEditBoard);
            DeleteBoardCommand = new Command(OnDeleteBoard);
            ConfirmPopupCommand = new Command(OnConfirmPopup);
            AddBoardButtonCommand = new Command(OnAddBoardButton);

            GetUserBoards();
        }

        #region Commands 

        private void OnDeleteBoard(object obj)
        {
            if (obj is BoardVM board) //TODO: Add confirmation popup
            {
                DeleteSelectedBoard(board.Base);

                ShowWaitForm = true;

                GetUserBoards();
            }
        }

        private void OnConfirmPopup(object obj)
        {
            if (!string.IsNullOrEmpty(NewBoardName))
            {
                if (_editBoard)
                {
                    _selectedBoard.Base.Name = NewBoardName;
                    EditSelectedBoard(_selectedBoard.Base);

                    GetUserBoards();
                }
                else
                {
                    AddNewBoard(NewBoardName);
                }

                NewBoardName = string.Empty;
            }
            else
            {
                DisplayExceptionMessage(AppResources.NameCannotBeEmpty);
            }
        }

        private void OnAddBoardButton(object obj)
        {
            _editBoard = false;
            DisplayAddBoard();
        }

        private void OnEditBoard(object obj)
        {
            _editBoard = true;

            if (obj is BoardVM board)
            {
                _selectedBoard = board;

                NewBoardName = board.Name;
                DisplayAddBoard();

                GetUserBoards();
            }
        }

        #endregion

        #region Methods

        internal async void GetUserBoards()
        {
            try
            {
                List<Board> boards = await _manager.GetLoggedUserBoards();

                UserBoards = new ObservableCollection<BoardVM>(boards.ConvertAll<BoardVM>(x => x));

                foreach (BoardVM board in UserBoards)
                {
                    Thread.Sleep(30);

                    //List<User> users = await _manager.GetUsersAssignedToBoard(board.Base.Id);
                    //board.AssignedUsers = new ObservableCollection<UserVM>(users.ConvertAll<UserVM>(x => x));

                    board.AssignedUsers = new ObservableCollection<UserVM>();
                    board.AssignedUsers.Add(new UserVM());
                }
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage(ex.CompleteMessage);
            }
            finally
            {
                ShowWaitForm = false;
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

                if (newBoard != null)
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
