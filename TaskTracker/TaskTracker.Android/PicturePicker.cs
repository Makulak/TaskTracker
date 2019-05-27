using System.IO;
using System.Threading.Tasks;
using Android.Content;
using TaskTracker.Droid;
using TaskTracker.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(PicturePicker))]
namespace TaskTracker.Droid
{
    class PicturePicker : IPicturePicker
    {
        public Task<Stream> GetImageStreamAsync()
        {
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);

            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
    }
}