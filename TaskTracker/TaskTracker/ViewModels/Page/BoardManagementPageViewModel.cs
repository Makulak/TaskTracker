using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TaskTracker.ViewModels.Page.Base;
using TaskTracker.ViewModels.VM;
using Xamarin.Forms;

namespace TaskTracker.ViewModels.Page
{
    class BoardManagementPageViewModel : BaseViewModel
    {
        public BoardVM SelectedBoard
        {
            get => _selectedBoard;
            set
            {
                _selectedBoard = value;
                OnPropertyChanged(nameof(SelectedBoard));
            }
        }
        private BoardVM _selectedBoard;

        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteBoardButtonCommand { get; set; }
        public ICommand DeleteBoardCommand { get; set; }
        public ICommand SaveBoardCommand { get; set; }

        public Action ShowConfirmDeletePopup;

        public BoardManagementPageViewModel(BoardVM board)
        {
            SelectedBoard = board;

            RefreshCommand = new Command(OnRefresh);
            DeleteBoardButtonCommand = new Command(OnDeleteBoardButton);
            DeleteBoardCommand = new Command(OnDeleteBoard);
            SaveBoardCommand = new Command(OnSaveBoard);
        }

        #region Commands

        private void OnRefresh()
        {
            GetBoard();
        }

        private void OnDeleteBoardButton()
        {
            ShowConfirmDeletePopup?.Invoke();
        }

        private void OnDeleteBoard()
        {
            DeleteBoard();
        }

        private void OnSaveBoard()
        {
            SaveBoard();
        }

        #endregion

        #region Methods

        private async void GetBoard()
        {

        }

        private async void DeleteBoard()
        {

        }

        private async void SaveBoard()
        {

        }

        #endregion
    }
}
