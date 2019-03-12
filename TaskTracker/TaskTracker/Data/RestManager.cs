using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskTracker.Data
{
    internal class RestManager : IRestService
    {
        private readonly RestService _restService;

        public RestManager(RestService service)
        {
            _restService = service;
        }

        public async Task LogIn(User user)
        {
            await _restService.LogIn(user);
        }

        public Task Register(User user)
        {
            return _restService.Register(user);
        }

        [Obsolete]
        public Task<List<User>> GetUsers()
        {
            return _restService.GetUsers();
        }

        public Task<List<Board>> GetLoggedUserBoards()
        {
            return _restService.GetLoggedUserBoards();
        }
    }
}
