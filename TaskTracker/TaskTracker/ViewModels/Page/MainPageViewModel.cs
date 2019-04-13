using System;
using System.Windows.Input;
using TaskTracker.Data;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.ViewModels.Page.Base;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;

namespace TaskTracker.ViewModels.Page
{
    class MainPageViewModel : BaseViewModel
    {
        private readonly RestManager _manager;

        public BoardVM SelectedBoard
        {
            get => _selectedBoard;
            set
            {
                _selectedBoard = value;
                OnPropertyChanged(nameof(SelectedBoard));
            } }
        private BoardVM _selectedBoard;

        public ICommand RefreshCommand { get; set; }
        public ICommand AddColumnCommand { get; set; }
        public ICommand AddTaskCommand { get; set; }

        public Action DisplayAddTask;
        public Action DisplayAddColumn;

        public MainPageViewModel(BoardVM selectedBoard)
        {
            _manager = new RestManager(new RestService());

            RefreshCommand = new Command(OnRefresh);
            AddColumnCommand = new Command(OnAddColumn);
            AddTaskCommand = new Command(OnAddTask);

            GetDetailedInfoAboutBoard(selectedBoard);
        }

        #region Commands

        private void OnRefresh()
        {
            GetDetailedInfoAboutBoard(SelectedBoard);
        }

        private void OnAddColumn()
        {
        }

        private void OnAddTask()
        {
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

        private async void AddColumn(string columnName)
        {
            try
            {
                ShowWaitForm = true;

                ColumnVM newColumn = await _manager.AddNewColumn(new Column(SelectedBoard.Id, columnName));

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

        void MoveTask(int boardId, int taskId, int sourceColumn, int destinationColumn)
        {

        }

        void MoveColumn(int boardId, int columnId, int sourcePosition, int destinationPosition)
        {

        }

        #endregion
    }
}
