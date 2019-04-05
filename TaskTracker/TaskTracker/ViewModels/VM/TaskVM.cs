using TaskTracker.Models;
using TaskTracker.ViewModels.VM.Base;

namespace TaskTracker.ViewModels.VM
{
    public class TaskVM : BaseVM
    {
        public Task Base { get; set; }

        public string Name => Base.Name;

        public static implicit operator TaskVM(Task task)
        {
            return new TaskVM()
            {
                Base = task
            };
        }
    }
}
