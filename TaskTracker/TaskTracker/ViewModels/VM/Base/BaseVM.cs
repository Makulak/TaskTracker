using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TaskTracker.ViewModels.VM.Base
{
    class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
