using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Exceptions;
using TaskTracker.Helper;
using TaskTracker.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskTracker.Data
{
    internal class RestService : IRestService
    {
        private static readonly HttpClient Client;

        static RestService()
        {
            Client = new HttpClient();
        }

        public async Task LogIn(User user)
        {
            var uri = UriFactory.CreateEndpointUri("users/login");
            var param = JsonContentFactory.CreateContent(user);

            var response = await Client.PostAsync(uri, param);

            if (response.IsSuccessStatusCode)
            {
                var authData = $"{user.Login}:{user.Password}";
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
                Client.MaxResponseContentBufferSize = 256000;

                //var content = await response.Content.ReadAsStringAsync();
                //GlobalValues.LoggedUser = JsonConvert.DeserializeObject<User>(content);

                GlobalValues.LoggedUser = new User(2,"admin");
            }
            else
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task Register(User user)
        {
            var uri = UriFactory.CreateEndpointUri("users/register");
            var param = JsonContentFactory.CreateContent(user);

            var response = await Client.PostAsync(uri, param);

            if (!response.IsSuccessStatusCode)
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        [Obsolete]
        public async Task<List<User>> GetUsers()
        {
            List<User> list = new List<User>();
            var uri = UriFactory.CreateEndpointUri("users/all");

            var response = await Client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<User>>(content);
            }

            return list;
        }

        public async Task<List<Board>> GetLoggedUserBoards()
        {
            var uri = UriFactory.CreateEndpointUri("boards/all");

            var response = await Client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<Board>>(content);

                return list;
            }
            else
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
