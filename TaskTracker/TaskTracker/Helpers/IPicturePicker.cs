using System.IO;
using System.Threading.Tasks;

namespace TaskTracker.Helpers
{
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}
