using TaskTracker.Models;
using TaskTracker.ViewModels.VM.Base;
using Xamarin.Forms;

namespace TaskTracker.ViewModels.VM
{
    internal class UserVM : BaseVM
    {
        public User Base { get; set; }

        public ImageSource Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }
        private ImageSource _image;

        public static implicit operator UserVM(User user)
        {
            return new UserVM()
            {
                Base = user
            };
        }
    }
}
