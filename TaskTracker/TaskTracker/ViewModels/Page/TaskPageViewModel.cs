using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using TaskTracker.ViewModels.Page.Base;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;

namespace TaskTracker.ViewModels.Page
{
    class TaskPageViewModel : BaseViewModel
    {
        public TaskVM SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }
        private TaskVM _selectedTask;

        public ICommand SaveTaskCommand { get; set; }
        public ICommand MoveTaskButtonCommand { get; set; }
        public ICommand MoveTaskCommand { get; set; }
        public ICommand DeleteTaskButtonCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand EditAssignedUserButtonCommand { get; set; }
        public ICommand EditAssignedUserCommand { get; set; }

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
        }

        private void OnSaveTask()
        {

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

        }

        private void OnEditAssignedUser()
        {

        }
    }
}
