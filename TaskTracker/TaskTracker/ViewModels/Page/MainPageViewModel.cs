using System.Dynamic;
using System.Linq;
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

        public MainPageViewModel(BoardVM selectedBoard)
        {
            _manager = new RestManager(new RestService());

            GetDetailedInfoAboutBoard(selectedBoard);
        }

        #region Commands

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

        void MoveTask(int boardId, int taskId, int sourceColumn, int destinationColumn)
        {

        }

        void MoveColumn(int boardId, int columnId, int sourcePosition, int destinationPosition)
        {

        }

        #endregion
    }
}
