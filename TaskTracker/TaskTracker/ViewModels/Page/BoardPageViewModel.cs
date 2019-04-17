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
        private BoardVM _selectedBoard;

        public ObservableCollection<BoardVM> UserBoards {
            get => _userBoards;
            set {
                _userBoards = value;
                OnPropertyChanged(nameof(UserBoards));
            }
        }
        private ObservableCollection<BoardVM> _userBoards;

        public ICommand BoardSelectedCommand { get; set; }

        public ICommand AddBoardButtonCommand { get; set; }
        public ICommand EditBoardButtonCommand { get; set; }
        public ICommand DeleteBoardButtonCommand { get; set; }

        public ICommand AddNewBoardCommand { get; set; }
        public ICommand EditBoardCommand { get; set; }
        public ICommand DeleteBoardCommand { get; set; }

        public Action<BoardVM> DisplayMainPage;
        public Action<BoardVM> DisplayEditBoardPage;
        public Action DisplayAddBoardPopup;
        public Action DisplayDeleteBoardPopup;

        private readonly RestManager _manager;

        public string BoardName {
            get => _newBoardName;
            set {
                _newBoardName = value;
                OnPropertyChanged(nameof(BoardName));
            }
        }
        private string _newBoardName;

        public BoardPageViewModel()
        {
            _manager = new RestManager(new RestService());

            BoardSelectedCommand = new Command(OnBoardSelected);

            AddBoardButtonCommand = new Command(OnAddBoardButton);
            EditBoardButtonCommand = new Command(OnEditBoardButton);
            DeleteBoardButtonCommand = new Command(OnDeleteBoardButton);

            AddNewBoardCommand = new Command(OnAddNewBoard);
            EditBoardCommand = new Command(OnEditBoard);
            DeleteBoardCommand = new Command(OnDeleteBoard);

            ShowWaitForm = true;
            GetUserBoards();
        }

        #region Commands 

        private void OnBoardSelected(object obj)
        {
            Syncfusion.ListView.XForms.ItemTappedEventArgs arg = obj as Syncfusion.ListView.XForms.ItemTappedEventArgs;

            BoardVM board = arg?.ItemData as BoardVM;

            if (board == null)
                return;

            DisplayMainPage(board);
        }

        private void OnAddBoardButton()
        {
            DisplayAddBoardPopup();
        }

        private void OnEditBoardButton(object obj)
        {
            if (obj is BoardVM board)
            {
                _selectedBoard = board;

                BoardName = board.Name;
                DisplayEditBoardPage(obj as BoardVM);

                GetUserBoards();
            }
        }

        private void OnDeleteBoardButton(object obj)
        {
            if (obj is BoardVM board)
            {
                _selectedBoard = board;

                BoardName = board.Name;
                DisplayDeleteBoardPopup();

                GetUserBoards();
            }
        }


        private void OnAddNewBoard()
        {
            if (string.IsNullOrEmpty(BoardName))
                DisplayExceptionMessage?.Invoke(AppResources.NameCannotBeEmpty);
            else
                AddNewBoard(BoardName);

            BoardName = string.Empty;
        }

        private void OnEditBoard()
        {
            if (string.IsNullOrEmpty(BoardName))
                DisplayExceptionMessage?.Invoke(AppResources.NameCannotBeEmpty);
            else
            {
                _selectedBoard.Name = BoardName;
                EditSelectedBoard(_selectedBoard.Base);
            }

            BoardName = string.Empty;
        }

        private void OnDeleteBoard()
        {
            if (string.IsNullOrEmpty(BoardName))
                DisplayExceptionMessage?.Invoke(AppResources.NameCannotBeEmpty);
            else
            {
                _selectedBoard.Name = BoardName;
                DeleteSelectedBoard(_selectedBoard.Base);
            }

            BoardName = string.Empty;
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
                    List<User> users = await _manager.GetUsersAssignedToBoard(board.Base.Id);
                    board.AssignedUsers = new ObservableCollection<UserVM>(users.ConvertAll<UserVM>(x => x));
                }
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage?.Invoke(ex.CompleteMessage);
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
                DisplayExceptionMessage?.Invoke(ex.CompleteMessage);
            }
            finally
            {
                GetUserBoards();
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
                DisplayExceptionMessage?.Invoke(ex.CompleteMessage);
            }
            finally
            {
                GetUserBoards();
            }
        }

        private async void AddNewBoard(string boardName)
        {
            try
            {
                await _manager.AddNewBoard(boardName);
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage?.Invoke(ex.CompleteMessage);
            }
            finally
            {
                GetUserBoards();
            }
        }

        #endregion
    }
}
