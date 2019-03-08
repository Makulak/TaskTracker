using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker.Data
{
    internal class RestService
    {
        private HttpClient _client;

        public RestService(string login, string password)
        {
            var authData = string.Format("{0}:{1}", login, password);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = 256000;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        public async Task<List<User>> GetUsers()
        {
            List<User> List = new List<User>();
            var uri = new Uri(string.Format(GlobalValues.RestUrl, string.Empty));

            try
            {
                var response = await _client.GetAsync(uri);
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    List = JsonConvert.DeserializeObject<List<User>>(content);
                }
            }
            catch(Exception)
            {
                throw;
            }

            return List;
        }
    }
}
