﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public Action<TaskVM> DisplayTaskPage;
        public Action DisplayRenamePopup;

        private readonly RestManager _manager;

        public ICommand AddTaskButtonCommand { get; set; }
        public ICommand AddTaskCommand { get; set; }
        public ICommand RenameColumnCommand { get; set; }
        public ICommand RenameColumnButtonCommand { get; set; }
        public ICommand TaskSelectedCommand { get; set; }

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

            AddTaskButtonCommand = new Command(OnAddTaskButton);
            AddTaskCommand = new Command(OnAddTask);
            TaskSelectedCommand = new Command(OnTaskSelected);
            RenameColumnCommand = new Command(OnRenameColumn);
            RenameColumnButtonCommand = new Command(OnRenameColumnButton);
        }

        public static implicit operator ColumnVM(Column column)
        {
            return new ColumnVM
            {
                Base = column
            };
        }

        #region Commands

        private void OnAddTask()
        {
            AddTask();
        }

        private void OnAddTaskButton()
        {
            DisplayAddTask?.Invoke();
        }

        private void OnTaskSelected(object obj)
        {
            Syncfusion.ListView.XForms.ItemTappedEventArgs arg = obj as Syncfusion.ListView.XForms.ItemTappedEventArgs;

            TaskVM task = arg?.ItemData as TaskVM;

            if(task == null)
                return;

            DisplayTaskPage?.Invoke(task);
        }

        private void OnRenameColumn()
        {
            RenameColumn();
        }

        private void OnRenameColumnButton()
        {
            DisplayRenamePopup?.Invoke();
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
                TaskVM returnedTask = await _manager.AddNewTask(new Task(BoardId, this.Id, NewTaskName));

                returnedTask.AssignedUser = await _manager.GetUser(returnedTask.AssignedUserId);

                TaskCollection.Add(returnedTask);
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage?.Invoke(ex.CompleteMessage);
            }
            finally
            {
                NewTaskName = string.Empty;
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

        internal async void RenameColumn()
        {
            try
            {

                await _manager.EditColumn(Base);
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage?.Invoke(ex.CompleteMessage);
            }
        }

        #endregion
    }
}
