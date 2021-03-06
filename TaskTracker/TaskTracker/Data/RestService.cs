﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Exceptions;
using TaskTracker.Helpers;
using TaskTracker.Models;
using Xamarin.Forms;
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
            HttpResponseMessage response;

            try
            {
                response = await Client.PostAsync(uri, param);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

            if (response.IsSuccessStatusCode)
            {
                var authData = $"{user.Login}:{user.Password}";
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
                Client.MaxResponseContentBufferSize = 256000;
            }
            else
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task LogOut()
        {
            Client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task Register(User user)
        {
            var uri = UriFactory.CreateEndpointUri("users/register");
            var param = JsonContentFactory.CreateContent(user);
            HttpResponseMessage response;

            try
            {
                response = await Client.PostAsync(uri, param);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task<User> GetUser(int? userId = null)
        {
            var uri = UriFactory.CreateEndpointUri(userId == null ? "users/current" : $"users/id={userId}");
            HttpResponseMessage response;

            try
            {
                response = await Client.GetAsync(uri);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

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

        public async Task<List<User>> GetUserListByLogin(string pattern)
        {
            var uri = UriFactory.CreateEndpointUri($"users/all/query={pattern}");
            HttpResponseMessage response;

            try
            {
                response = await Client.GetAsync(uri);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<User>>(content);

                return list;
            }
            else
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task UploadImage(Stream stream)
        {
            var uri = UriFactory.CreateEndpointUri("images/upload/base64");
            var param = JsonContentFactory.CreateContent(new XamarinImage(stream));
            HttpResponseMessage response;

            try
            {
                response = await Client.PostAsync(uri, param);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        #endregion

        #region Boards

        public async Task<List<Board>> GetLoggedUserBoards()
        {
            var uri = UriFactory.CreateEndpointUri("boards/show");
            HttpResponseMessage response;

            try
            {
                response = await Client.GetAsync(uri);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

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

        public async Task<Board> AddNewBoard(string name)
        {
            var uri = UriFactory.CreateEndpointUri("boards/add");
            var param = JsonContentFactory.CreateContent(new Board(name));
            HttpResponseMessage response;

            try
            {
                response = await Client.PostAsync(uri, param);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

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

        public async Task<Board> EditBoard(Board board)
        {
            var uri = UriFactory.CreateEndpointUri("boards/update");
            var param = JsonContentFactory.CreateContent(board);
            HttpResponseMessage response;

            try
            {
                response = await Client.PutAsync(uri, param);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

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

        public async Task DeleteBoard(int id)
        {
            var uri = UriFactory.CreateEndpointUri($"boards/delete/id={id}");
            HttpResponseMessage response;

            try
            {
                response = await Client.DeleteAsync(uri);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task<List<User>> GetUsersAssignedToBoard(int boardId)
        {
            var uri = UriFactory.CreateEndpointUri($"boards/show/id={boardId}/users");
            HttpResponseMessage response;

            try
            {
                response = await Client.GetAsync(uri);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<User>>(content);
            }
            else
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task<Board> GetBoard(int boardId)
        {
            var uri = UriFactory.CreateEndpointUri($"boards/show/id={boardId}");
            HttpResponseMessage response;

            try
            {
                response = await Client.GetAsync(uri);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

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

        #endregion

        #region Columns

        public async Task<Column> AddNewColumn(Column column)
        {
            var uri = UriFactory.CreateEndpointUri("columns/add");
            var param = JsonContentFactory.CreateContent(column);
            HttpResponseMessage response;

            try
            {
                response = await Client.PostAsync(uri, param);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

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

        public async Task<Column> EditColumn(Column column)
        {
            var uri = UriFactory.CreateEndpointUri("columns/update");
            var param = JsonContentFactory.CreateContent(column);
            HttpResponseMessage response;

            try
            {
                response = await Client.PutAsync(uri, param);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

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

        public async Task DeleteColumn(int columnId)
        {
            var uri = UriFactory.CreateEndpointUri($"columns/delete/id={columnId}");
            var param = JsonContentFactory.CreateContent(columnId);
            HttpResponseMessage response;

            try
            {
                response = await Client.PostAsync(uri, param);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        #endregion

        #region Tasks

        public async Task<Models.Task> AddNewTask(Models.Task task)
        {
            var uri = UriFactory.CreateEndpointUri("tasks/add");
            var param = JsonContentFactory.CreateContent(task);
            HttpResponseMessage response;

            try
            {
                response = await Client.PostAsync(uri, param);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

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

        public async Task<Models.Task> EditTask(Models.Task task)
        {
            var uri = UriFactory.CreateEndpointUri("tasks/update");
            var param = JsonContentFactory.CreateContent(task);
            HttpResponseMessage response;

            try
            {
                response = await Client.PutAsync(uri, param);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

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

        public async Task DeleteTask(int taskId)
        {
            var uri = UriFactory.CreateEndpointUri($"tasks/delete/id={taskId}");
            HttpResponseMessage response;

            try
            {
                response = await Client.DeleteAsync(uri);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task MoveTask(int taskId, int destinationColumn, int destinationPosition)
        {
            var uri = UriFactory.CreateEndpointUri($"tasks/move/id={taskId}/target_column_id={destinationColumn}/position={destinationPosition}");
            var param = JsonContentFactory.CreateContent(null);

            HttpResponseMessage response;

            try
            {
                response = await Client.PostAsync(uri, param);
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task<Models.Task> GetTask(int taskId)
        {
            var uri = UriFactory.CreateEndpointUri($"tasks/show/id={taskId}");

            HttpResponseMessage response;

            try
            {
                response = await Client.GetAsync(uri);

            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var task = JsonConvert.DeserializeObject<Models.Task>(content);

                return task;
            }
            else
            {
                throw new RestException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }
        }

        #endregion
    }
}
