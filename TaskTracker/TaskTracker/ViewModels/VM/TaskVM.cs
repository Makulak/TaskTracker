using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.ViewModels.VM.Base;

namespace TaskTracker.ViewModels.VM
{
    internal class TaskVM : BaseVM
    {
        public Task Base { get; set; }

        public string Name => Base.Name;

        public UserVM AssignedUser
        {
            get => _assignedUser;
            set
            {
                _assignedUser = value;
                OnPropertyChanged("AssignedUser");
            }
        }
        private UserVM _assignedUser;

        public static implicit operator TaskVM(Task task)
        {
            return new TaskVM()
            {
                Base = task
            };
        }
    }
}
