using System;
using System.Collections.Generic;
using System.Linq;
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
    class MainPageViewModel : BaseViewModel
    {
        private readonly RestManager _manager;

        public BoardVM SelectedBoard {
            get => _selectedBoard;
            set {
                _selectedBoard = value;
                OnPropertyChanged(nameof(SelectedBoard));
            }
        }
        private BoardVM _selectedBoard;

        public TaskVM SelectedTask {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }
        private TaskVM _selectedTask;

        public string TaskName {
            get => _taskName;
            set {
                _taskName = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }
        private string _taskName;

        public string ColumnName {
            get => _columnName;
            set {
                _columnName = value;
                OnPropertyChanged(nameof(ColumnName));
            }
        }
        private string _columnName;

        public int SelectedColumnPosition {
            get => _selectedColumnPosition;
            set {
                _selectedColumnPosition = value;
                OnPropertyChanged(nameof(SelectedColumnPosition));
            }
        }
        private int _selectedColumnPosition;

        public string NewTaskName {
            get => _newTaskName;
            set {
                _newTaskName = value;
                OnPropertyChanged(nameof(NewTaskName));
            }
        }
        private string _newTaskName;

        public ICommand RefreshCommand { get; set; }
        public ICommand AddColumnButtonCommand { get; set; }
        public ICommand AddNewColumnCommand { get; set; }
        public ICommand AddTaskCommand { get; set; }
        public ICommand RenameColumnCommand { get; set; }
        public ICommand TaskSelectedCommand { get; set; }
        public ICommand RenameColumnButtonCommand { get; set; }
        public ICommand AddTaskButtonCommand { get; set; }
        public ICommand MoveTaskButtonCommand { get; set; }
        public ICommand MoveTaskCommand { get; set; }

        public Action DisplayAddColumn;
        public Action DisplayRenameColumnPopup;
        public Action DisplayAddTask;
        public Action DisplayMoveTaskPopup;
        public Action CloseMoveTaskPopup;
        public Action<TaskVM> DisplayTaskPage;

        public MainPageViewModel(BoardVM selectedBoard)
        {
            _manager = new RestManager(new RestService());

            RefreshCommand = new Command(OnRefresh);
            AddColumnButtonCommand = new Command(OnAddColumnButton);
            AddTaskCommand = new Command(OnAddTask);
            RenameColumnCommand = new Command(OnRenameColumn);
            AddNewColumnCommand = new Command(OnAddColumn);
            TaskSelectedCommand = new Command(OnTaskSelected);
            RenameColumnButtonCommand = new Command(OnRenameColumnButton);
            AddTaskButtonCommand = new Command(OnAddTaskButton);
            MoveTaskButtonCommand = new Command(OnMoveTaskButton);
            MoveTaskCommand = new Command(OnMoveTask);

            GetDetailedInfoAboutBoard(selectedBoard);
        }

        #region Commands

        private void OnRefresh()
        {
            GetDetailedInfoAboutBoard(SelectedBoard);
        }

        private void OnAddColumnButton()
        {
            DisplayAddColumn?.Invoke();
        }

        private void OnAddColumn()
        {
            if (string.IsNullOrEmpty(ColumnName))
                DisplayExceptionMessage?.Invoke(AppResources.NameCannotBeEmpty);
            else
                AddColumn();

            ColumnName = string.Empty;
        }

        private void OnAddTask()
        {
            AddTask();
        }

        private void OnRenameColumn()
        {
            SelectedBoard.ColumnsCollection[SelectedColumnPosition].Name = ColumnName;
            ColumnName = string.Empty;

            RenameColumn();
        }

        private void OnTaskSelected(object obj)
        {
            Syncfusion.ListView.XForms.ItemTappedEventArgs arg = obj as Syncfusion.ListView.XForms.ItemTappedEventArgs;

            TaskVM task = arg?.ItemData as TaskVM;

            if (task == null)
                return;

            DisplayTaskPage?.Invoke(task);
        }

        private void OnRenameColumnButton(object obj)
        {
            ColumnVM column = obj as ColumnVM;

            if (column == null)
                return;

            ColumnName = column.Name;

            DisplayRenameColumnPopup?.Invoke();
        }

        private void OnAddTaskButton()
        {
            DisplayAddTask?.Invoke();
        }

        private void OnMoveTaskButton(object obj)
        {
            SelectedTask = obj as TaskVM;
            if (SelectedTask == null)
                return;

            DisplayMoveTaskPopup?.Invoke();
        }

        private void OnMoveTask(object obj)
        {
            CloseMoveTaskPopup?.Invoke();

            Syncfusion.ListView.XForms.ItemTappedEventArgs args = obj as Syncfusion.ListView.XForms.ItemTappedEventArgs;

            ColumnVM selectedColumn = args?.ItemData as ColumnVM;

            if (selectedColumn == null)
                return;

            MoveTask(SelectedTask.Id, selectedColumn.Id, 0);
            GetDetailedInfoAboutBoard(SelectedBoard);
        }

        #endregion

        #region Methods

        private async void GetDetailedInfoAboutBoard(BoardVM board)
        {
            try
            {
                ShowWaitForm = true;

                BoardVM brd = await _manager.GetBoard(board.Id);

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

        private async void AddColumn()
        {
            try
            {
                ShowWaitForm = true;

                ColumnVM newColumn = await _manager.AddNewColumn(new Column(SelectedBoard.Id, ColumnName));

                if (newColumn == null)
                    return;

                SelectedBoard.ColumnsCollection.Add(newColumn);
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

        internal async void MoveTask(int taskId, int destinationColumn, int destinationPosition)
        {
            try
            {
                ShowWaitForm = true;

                await _manager.MoveTask(taskId, destinationColumn, destinationPosition);
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

        private void MoveColumn(int boardId, int columnId, int sourcePosition, int destinationPosition)
        {

        }

        internal async void RenameColumn()
        {
            try
            {
                await _manager.EditColumn(SelectedBoard.ColumnsCollection[SelectedColumnPosition].Base);
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
                TaskVM returnedTask = await _manager.AddNewTask(new Task(SelectedBoard.Id, SelectedBoard.ColumnsCollection[SelectedColumnPosition].Id, NewTaskName));

                returnedTask.AssignedUser = await _manager.GetUser(returnedTask.AssignedUserId);

                SelectedBoard.ColumnsCollection[SelectedColumnPosition].TaskCollection.Add(returnedTask);
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

        #endregion
    }
}
