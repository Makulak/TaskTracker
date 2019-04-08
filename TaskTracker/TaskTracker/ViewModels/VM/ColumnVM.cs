using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using TaskTracker.Data;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.ViewModels.VM.Base;

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

        private readonly RestManager _manager;

        public ColumnVM()
        {
            _manager = new RestManager(new RestService());
            TaskCollection = new ObservableCollection<TaskVM>();

        }

        public static implicit operator ColumnVM(Column column)
        {
            return new ColumnVM
            {
                Base = column
            };
        }

        internal async void RemoveTask(TaskVM task)
        {
            if (task == null)
                return;
            try
            {
                await _manager.DeleteTask(task.Base.Id);

                Base.Tasks.Remove(task.Base);
            }
            catch (RestException ex)
            {
                DisplayExceptionMessage(ex.CompleteMessage);
            }
        }
    }
}
