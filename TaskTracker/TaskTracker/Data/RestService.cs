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

        #region User

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

                var content = await response.Content.ReadAsStringAsync();
                GlobalValues.LoggedUser = JsonConvert.DeserializeObject<User>(content);
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

        public async Task<List<Board>> GetLoggedUserBoards()
        {
            var uri = UriFactory.CreateEndpointUri("boards/show");

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

        #endregion

        #region Boards

        public async Task DeleteBoard(int id)
        {
            var uri = UriFactory.CreateEndpointUri("boards/delete");
            var param = JsonContentFactory.CreateContent(id);

            var response = await Client.PostAsync(uri, param);

            if (!response.IsSuccessStatusCode)
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task<Board> AddNewBoard(string name)
        {
            var uri = UriFactory.CreateEndpointUri("board/add");
            var param = JsonContentFactory.CreateContent(new Board(name));

            var response = await Client.PostAsync(uri, param);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Board>(content);
            }
            else
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task EditBoard(Board board)
        {
            var uri = UriFactory.CreateEndpointUri("board/edit"); //TODO[JM]: Nazwa endpointa
            var param = JsonContentFactory.CreateContent(board);

            var response = await Client.PostAsync(uri, param);

            if (!response.IsSuccessStatusCode)
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        #endregion

        public async Task<Column> AddNewColumn(Column column)
        {
            var uri = UriFactory.CreateEndpointUri("column/add");
            var param = JsonContentFactory.CreateContent(column);

            var response = await Client.PostAsync(uri, param);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Column>(content);
            }
            else
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task<Models.Task> AddNewTask(Models.Task task)
        {
            var uri = UriFactory.CreateEndpointUri("task/add");
            var param = JsonContentFactory.CreateContent(task);

            var response = await Client.PostAsync(uri, param);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Models.Task>(content);
            }
            else
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
