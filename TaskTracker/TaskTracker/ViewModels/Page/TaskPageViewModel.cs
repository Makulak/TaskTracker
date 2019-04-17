using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using TaskTracker.Data;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.ViewModels.Page.Base;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;

namespace TaskTracker.ViewModels.Page
{
    class TaskPageViewModel : BaseViewModel
    {
        private readonly RestManager _manager;

        public TaskVM SelectedTask {
            get => _selectedTask;
            set {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }
        private TaskVM _selectedTask;

        public ObservableCollection<UserVM> AvailableUsers {
            get => _availableUsers;
            set {
                _availableUsers = value;
                OnPropertyChanged(nameof(AvailableUsers));
            }
        }
        private ObservableCollection<UserVM> _availableUsers;

        public Action ShowUserListPopup { get; set; }
        public Action CloseUserListPopup { get; set; }
        public Action ClosePage { get; set; }

        public ICommand SaveTaskCommand { get; set; }
        public ICommand MoveTaskButtonCommand { get; set; }
        public ICommand MoveTaskCommand { get; set; }
        public ICommand DeleteTaskButtonCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand EditAssignedUserButtonCommand { get; set; }
        public ICommand EditAssignedUserCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public TaskPageViewModel(TaskVM task)
        {
            SelectedTask = task;

            SaveTaskCommand = new Command(OnSaveTask);
            MoveTaskButtonCommand = new Command(OnMoveTaskButton);
            MoveTaskCommand = new Command(OnMoveTask);
            DeleteTaskButtonCommand = new Command(OnDeleteTaskButton);
            DeleteTaskCommand = new Command(OnDeleteTask);
            EditAssignedUserButtonCommand = new Command(OnEditAssignedUserButton);
            EditAssignedUserCommand = new Command(OnEditAssignedUser);
            RefreshCommand = new Command(OnRefresh);

            _manager = new RestManager(new RestService());

            GetAvailableUsers();
        }

        #region Commands

        private void OnSaveTask()
        {
            SaveTask();
        }

        private void OnMoveTaskButton()
        {

        }

        private void OnMoveTask()
        {

        }

        private void OnDeleteTaskButton()
        {

        }

        private void OnDeleteTask()
        {

        }

        private void OnEditAssignedUserButton()
        {
            ShowUserListPopup?.Invoke();
        }

        private void OnEditAssignedUser(object obj)
        {
            CloseUserListPopup?.Invoke();

            ItemTappedEventArgs args = obj as ItemTappedEventArgs;

            UserVM selectedUser = args?.ItemData as UserVM;
            
            if (selectedUser == null)
                return;

            SelectedTask.AssignedUser = selectedUser;
        }

        private void OnRefresh()
        {
            GetTask();
            GetAvailableUsers();
        }

        #endregion


        #region Methods

        private async void GetAvailableUsers()
        {
            try
            {
                ShowWaitForm = true;

                List<User> users = await _manager.GetUsersAssignedToBoard(SelectedTask.BoardId);
                AvailableUsers = new ObservableCollection<UserVM>(users.ConvertAll<UserVM>(x => x));
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

        private async void SaveTask()
        {
            try
            {
                await _manager.EditTask(SelectedTask.Base);
                ClosePage();
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

        private async void GetTask()
        {
            try
            {
                ShowWaitForm = true;

                SelectedTask = await _manager.GetTask(SelectedTask.Id);
                SelectedTask.AssignedUser = await _manager.GetUser(SelectedTask.AssignedUserId);
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

        #endregion
    }
}
