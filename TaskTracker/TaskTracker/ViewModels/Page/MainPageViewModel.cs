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

        public BoardVM SelectedBoard { get; set; }

        public int CarouselSelectedIndex
        {
            get => _carouselSelectedIndex;
            set
            {
                _carouselSelectedIndex = value;
                OnPropertyChanged("CarouselSelectedIndex");
            }
        }
        private int _carouselSelectedIndex;


        public MainPageViewModel(BoardVM selectedBoard)
        {
            SelectedBoard = selectedBoard;

            _manager = new RestManager(new RestService());

        }

        #region Commands

        #endregion

        #region Methods

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

        #endregion
    }
}
