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
        private readonly HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task LogIn(User user)
        {
            var uri = UriFactory.CreateEndpointUri("users/login");
            var param = JsonContentFactory.CreateContent(user);

            var response = await _client.PostAsync(uri, param);

            if (response.IsSuccessStatusCode)
            {
                var authData = $"{user.Login}:{user.Password}";
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
                _client.MaxResponseContentBufferSize = 256000;
            }
            else
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task<User> Register(User user)
        {
            var uri = UriFactory.CreateEndpointUri("users/register");
            var param = JsonContentFactory.CreateContent(user);

            var response = await _client.PostAsync(uri, param);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(content);
            }
            else
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task<List<User>> GetUsers()
        {
            List<User> List = new List<User>();
            var uri = new Uri(GlobalValues.RestUrl + @"users/all");

            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                List = JsonConvert.DeserializeObject<List<User>>(content);
            }

            return List;
        }
    }
}
