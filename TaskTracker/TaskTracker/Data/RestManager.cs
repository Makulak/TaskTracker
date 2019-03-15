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

        #region User

        public async Task LogIn(User user)
        {
            await _restService.LogIn(user);
        }

        public Task Register(User user)
        {
            return _restService.Register(user);
        }

        public Task<List<Board>> GetLoggedUserBoards()
        {
            return _restService.GetLoggedUserBoards();
        }

        #endregion

        #region Boards

        public Task DeleteBoard(int id)
        {
            return _restService.DeleteBoard(id);
        }

        public Task<Board> AddNewBoard(string name)
        {
            return _restService.AddNewBoard(name);
        }

        public Task EditBoard(Board board)
        {
            return _restService.EditBoard(board);
        }

        #endregion
    }
}
