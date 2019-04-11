using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Input;
using TaskTracker.Data;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.ViewModels.VM.Base;
using Xamarin.Forms;

namespace TaskTracker.ViewModels.VM
{
    internal class ColumnVM : BaseVM
    {
        public Column Base {
            get => _base;
            private set {
                _base = value;

                if (_base.Tasks != null)
                    TaskCollection = new ObservableCollection<TaskVM>(Base.Tasks.ConvertAll<TaskVM>(x => x));

                OnPropertyChanged(nameof(Base));
            }
        }
        private Column _base;

        #region ModelProperties

        public int Id => Base.Id;

        public int BoardId => Base.BoardId;

        public string Name {
            get => Base.Name;
            set {
                Base.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int Position {
            get => Base.Position;
            set {
                Base.Position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        public List<Task> Tasks {
            get => Base.Tasks;
            set {
                Base.Tasks = value;

                if (_base.Tasks != null)
                    TaskCollection = new ObservableCollection<TaskVM>(Base.Tasks.ConvertAll<TaskVM>(x => x));

                OnPropertyChanged(nameof(Tasks));
            }
        }

        #endregion

        #region ViewModelProperties

        public ObservableCollection<TaskVM> TaskCollection {
            get => _taskCollection;
            set {
                _taskCollection = value;
                OnPropertyChanged(nameof(TaskCollection));
            }
        }
        private ObservableCollection<TaskVM> _taskCollection;

        #endregion

        public Action<string> DisplayExceptionMessage;
        public Action DisplayAddTask;

        private readonly RestManager _manager;

        public ICommand AddTaskCommand { get; set; }
        public ICommand ConfirmPopupCommand { get; set; }

        public string NewTaskName {
            get => _newTaskName;
            set {
                _newTaskName = value;
                OnPropertyChanged(nameof(NewTaskName));
            }
        }
        private string _newTaskName;

        public ColumnVM()
        {
            _manager = new RestManager(new RestService());
            TaskCollection = new ObservableCollection<TaskVM>();

            AddTaskCommand = new Command(OnAddTaskButton);
            ConfirmPopupCommand = new Command(OnConfirmPopup);
        }

        public static implicit operator ColumnVM(Column column)
        {
            return new ColumnVM
            {
                Base = column
            };
        }

        #region Commands

        private void OnConfirmPopup()
        {
            AddTask();
        }

        private void OnAddTaskButton()
        {
            DisplayAddTask?.Invoke();
        }

        #endregion

        #region Methods

        internal async void RemoveTask(TaskVM task)
        {
            if (task == null)
                return;
            try
            {
                await _manager.DeleteTask(task.Base.Id);

                TaskCollection.Remove(TaskCollection.SingleOrDefault(t => t.Id == task.Id));
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage?.Invoke(ex.CompleteMessage);
            }
        }

        private async void AddTask()
        {
            try
            {
                Task newTask = new Task(BoardId, this.Id, NewTaskName);

                await _manager.AddNewTask(newTask);

                TaskCollection.Add(newTask);
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage?.Invoke(ex.CompleteMessage);
            }
        }

        internal async void MoveTask(int taskId, int position)
        {
            try
            {
                await _manager.MoveTask(taskId, position);
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage?.Invoke(ex.CompleteMessage);
            }
        }

        #endregion
    }
}
