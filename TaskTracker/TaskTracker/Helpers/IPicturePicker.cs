using System.IO;
using System.Threading.Tasks;

namespace TaskTracker.Helpers
{
    interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}
