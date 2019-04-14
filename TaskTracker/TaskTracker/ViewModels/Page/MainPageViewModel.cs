using System;
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

        public ICommand RefreshCommand { get; set; }
        public ICommand AddColumnButtonCommand { get; set; }
        public ICommand AddTaskButtonCommand { get; set; }
        public ICommand AddNewTaskCommand { get; set; }
        public ICommand AddNewColumnCommand { get; set; }

        public Action DisplayAddTask;
        public Action DisplayAddColumn;

        public MainPageViewModel(BoardVM selectedBoard)
        {
            _manager = new RestManager(new RestService());

            RefreshCommand = new Command(OnRefresh);
            AddColumnButtonCommand = new Command(OnAddColumnButton);
            AddTaskButtonCommand = new Command(OnAddTaskButton);

            AddNewColumnCommand = new Command(OnAddColumn);
            AddNewTaskCommand = new Command(OnAddTask);

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

        private void OnAddTaskButton()
        {
            DisplayAddTask?.Invoke();
        }

        private void OnAddTask()
        {
            if (string.IsNullOrEmpty(ColumnName))
                DisplayExceptionMessage?.Invoke(AppResources.NameCannotBeEmpty);
            else
                AddTask();

            TaskName = string.Empty;
        }

        private void OnAddColumn()
        {
            if (string.IsNullOrEmpty(ColumnName))
                DisplayExceptionMessage?.Invoke(AppResources.NameCannotBeEmpty);
            else
                AddColumn();

            ColumnName = string.Empty;
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

        private async void AddTask()
        {
            try
            {
                ShowWaitForm = true;

                ColumnVM column =
                    SelectedBoard.ColumnsCollection.SingleOrDefault(col => col.Position == SelectedColumnPosition);

                if (column == null)
                    return;

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

        void MoveTask(int boardId, int taskId, int sourceColumn, int destinationColumn)
        {

        }

        void MoveColumn(int boardId, int columnId, int sourcePosition, int destinationPosition)
        {

        }

        #endregion
    }
}
