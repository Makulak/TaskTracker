using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TaskTracker.Data;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.ViewModels.Page.Base;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;

namespace TaskTracker.ViewModels.Page
{
    class BoardManagementPageViewModel : BaseViewModel
    {
        public BoardVM SelectedBoard {
            get => _selectedBoard;
            set {
                _selectedBoard = value;
                OnPropertyChanged(nameof(SelectedBoard));
            }
        }
        private BoardVM _selectedBoard;

        public string SearchText {
            get => _searchText;
            set {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                if (_searchText.Length >= 3)
                    GetUserListByLogin();
            }
        }
        private string _searchText;

        public ObservableCollection<UserVM> UserList
        {
            get => _userList;
            set
            {
                _userList = value;
                OnPropertyChanged(nameof(UserList));
            }
        }
        private ObservableCollection<UserVM> _userList;

        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteBoardButtonCommand { get; set; }
        public ICommand DeleteBoardCommand { get; set; }
        public ICommand SaveBoardCommand { get; set; }

        public Action ShowConfirmDeletePopup;
        public Action ShowBoardPage;

        private readonly RestManager _manager;

        public BoardManagementPageViewModel(BoardVM board)
        {
            SelectedBoard = board;

            RefreshCommand = new Command(OnRefresh);
            DeleteBoardButtonCommand = new Command(OnDeleteBoardButton);
            DeleteBoardCommand = new Command(OnDeleteBoard);
            SaveBoardCommand = new Command(OnSaveBoard);

            _manager = new RestManager(new RestService());

            SearchText = string.Empty;

            UserList = new ObservableCollection<UserVM>();
        }

        #region Commands

        private void OnRefresh()
        {
            GetBoard();
        }

        private void OnDeleteBoardButton()
        {
            ShowConfirmDeletePopup?.Invoke();
        }

        private void OnDeleteBoard()
        {
            DeleteBoard();
        }

        private void OnSaveBoard()
        {
            SaveBoard();
        }

        #endregion

        #region Methods

        private async void GetBoard()
        {
            try
            {
                ShowWaitForm = true;

                BoardVM brd = await _manager.GetBoard(SelectedBoard.Id);

                foreach (ColumnVM column in brd.ColumnsCollection)
                {
                    foreach (TaskVM task in column.TaskCollection)
                    {
                        User x = await _manager.GetUser(task.Base.AssignedUserId);
                        task.AssignedUser = x;
                    }
                }

                SelectedBoard = brd;
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

        private async void DeleteBoard()
        {
            try
            {
                ShowWaitForm = true;
                await _manager.DeleteBoard(SelectedBoard.Id);

                ShowBoardPage();
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

        private async void SaveBoard()
        {
            try
            {
                ShowWaitForm = true;
                await _manager.EditBoard(SelectedBoard.Base);

                ShowBoardPage();
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

        private async void GetUserListByLogin()
        {
            try
            {
                List<User> list = await _manager.GetUserListByLogin(SearchText);

                UserList = new ObservableCollection<UserVM>(list.ConvertAll<UserVM>(x => x));
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

        internal void AssignUserToBoard(UserVM user)
        {
            if (user == null)
                return;

            SelectedBoard.AssignedUsers.Add(user);
            SelectedBoard.AssignedUserIds = SelectedBoard.AssignedUsers.Select(usr => usr.Id).ToArray();
            SearchText = string.Empty;
        }

        #endregion
    }
}
