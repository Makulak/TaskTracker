using System;
using System.ComponentModel;

namespace TaskTracker.ViewModels.Page.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Action<string> DisplayExceptionMessage;

        public bool ShowWaitForm
        {
            get => _showWaitForm;
            set
            {
                _showWaitForm = value;
                if (value)
                    ControlsOpacity = 0.25f;
                else
                    ControlsOpacity = 1;

                OnPropertyChanged(nameof(ShowWaitForm));
            }
        }
        private bool _showWaitForm;

        public float ControlsOpacity
        {
            get => _controlsOpacity;
            set
            {
                _controlsOpacity = value;
                OnPropertyChanged(nameof(ControlsOpacity));
            }
        }
        private float _controlsOpacity = 1;
    }
}
