using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.ViewModels.VM.Base;

namespace TaskTracker.ViewModels.VM
{
    internal class TaskVM : BaseVM
    {
        public Task Base { get; private set; }

        #region ModelProperties

        public int Id => Base.Id;

        public int BoardId => Base.BoardId;

        public int ColumnId
        {
            get => Base.ColumnId;
            set
            {
                Base.ColumnId = value;
                OnPropertyChanged(nameof(ColumnId));
            }
        }

        public string Name {
            get => Base.Name;
            set {
                Base.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int AssignedUserId
        {
            get => Base.AssignedUserId;
            set
            {
                Base.AssignedUserId = value;
                OnPropertyChanged(nameof(AssignedUserId));
            }
        }

        public int Position
        {
            get => Base.Position;
            set
            {
                Base.Position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        public bool IsDone
        {
            get => Base.IsDone;
            set
            {
                Base.IsDone = value;
                OnPropertyChanged(nameof(IsDone));
            }
        }

        public string Description {
            get => Base.Description;
            set
            {
                Base.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        #endregion

        #region ViewModelProperties

        public UserVM AssignedUser
        {
            get => _assignedUser;
            set
            {
                _assignedUser = value;
                OnPropertyChanged(nameof(AssignedUser));
            }
        }
        private UserVM _assignedUser;

        #endregion

        public static implicit operator TaskVM(Task task)
        {
            return new TaskVM()
            {
                Base = task
            };
        }
    }
}
