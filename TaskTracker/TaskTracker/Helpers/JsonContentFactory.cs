using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace TaskTracker.Helpers
{
    public static class JsonContentFactory
    {
        public static StringContent CreateContent(object obj)
        {
            string jsonObj = JsonConvert.SerializeObject(obj);

            return new StringContent(jsonObj, Encoding.UTF8, "application/json");
        }
    }
}
