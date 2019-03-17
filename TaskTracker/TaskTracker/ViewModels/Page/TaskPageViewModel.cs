using TaskTracker.ViewModels.Page.Base;
using TaskTracker.ViewModels.VM;

namespace TaskTracker.ViewModels.Page
{
    class TaskPageViewModel : BaseViewModel
    {
        public TaskVM SelectedTask { get; set; }

        public TaskPageViewModel(TaskVM selectedTask)
        {
            SelectedTask = selectedTask;
        }
    }
}
