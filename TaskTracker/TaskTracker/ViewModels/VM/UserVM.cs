using TaskTracker.Models;
using TaskTracker.ViewModels.VM.Base;
using Xamarin.Forms;

namespace TaskTracker.ViewModels.VM
{
    public class UserVM : BaseVM
    {
        private User Base { get; set; }

        public ImageSource Image { get; set; }
    }
}
