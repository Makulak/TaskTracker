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

        public async Task Register(User user)
        {
            await _restService.Register(user);
        }

        public async Task<User> GetUser(int userId)
        {
            return await _restService.GetUser(userId);
        }

        #endregion

        #region Boards

        public async Task<List<Board>> GetLoggedUserBoards()
        {
            return await _restService.GetLoggedUserBoards();
        }

        public async Task<Board> AddNewBoard(string name)
        {
            return await _restService.AddNewBoard(name);
        }

        public async Task<Board> EditBoard(Board board)
        {
            return await _restService.EditBoard(board);
        }

        public async Task DeleteBoard(int id)
        {
            await _restService.DeleteBoard(id);
        }

        public async Task<List<User>> GetUsersAssignedToBoard(int boardId)
        {
            return await _restService.GetUsersAssignedToBoard(boardId);
        }

        #endregion

        #region Columns

        public async Task<Column> AddNewColumn(Column column)
        {
            return await _restService.AddNewColumn(column);
        }

        public async Task<Column> EditColumn(Column column)
        {
            return await _restService.EditColumn(column);
        }

        public async Task DeleteColumn(int columnId)
        {
            await _restService.DeleteColumn(columnId);
        }

        #endregion

        #region Tasks

        public async Task<Models.Task> AddNewTask(Models.Task task)
        {
            return await _restService.AddNewTask(task);
        }

        public async Task<Models.Task> EditTask(Models.Task task)
        {
            return await _restService.EditTask(task);
        }

        public async Task DeleteTask(int taskId)
        {
            await _restService.DeleteTask(taskId);
        }

        #endregion
    }
}
