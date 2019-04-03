using System.Dynamic;
using TaskTracker.Data;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.ViewModels.Page.Base;
using TaskTracker.ViewModels.VM;

namespace TaskTracker.ViewModels.Page
{
    class MainPageViewModel : BaseViewModel
    {
        public BoardVM SelectedBoard { get; set; }

        private readonly RestManager _manager;

        public bool IsDeleteViewVisible
        {
            get => _isDeleteViewVisible;
            set
            {
                _isDeleteViewVisible = value;
                OnPropertyChanged("IsDeleteViewVisible");
            }
        }

        private bool _isDeleteViewVisible;

        public MainPageViewModel(BoardVM selectedBoard)
        {
            SelectedBoard = selectedBoard;

            _manager = new RestManager(new RestService());
        }

        void AddColumn(int boardId, string columnName)
        {
            try
            {
                _manager.AddNewColumn(new Column(boardId, columnName));
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage?.Invoke(ex.CompleteMessage);
            }
        }

        void AddTask(int boardId, int columnId, string taskName)
        {
            try
            {
                _manager.AddNewTask(new Task(boardId, columnId, taskName));
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage?.Invoke(ex.CompleteMessage);
            }
        }

        void MoveTask(int boardId, int taskId, int sourceColumn, int destinationColumn)
        {

        }

        void MoveColumn(int boardId, int columnId, int sourcePosition, int destinationPosition)
        {

        }
    }
}
