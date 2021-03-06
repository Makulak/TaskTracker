﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using Xamarin.Forms;
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

        public async Task LogOut()
        {
            await _restService.LogOut();
        }

        public async Task Register(User user)
        {
            await _restService.Register(user);
        }

        public async Task<User> GetUser(int? userId = null)
        {
            return await _restService.GetUser(userId);
        }

        public async Task<List<User>> GetUserListByLogin(string pattern)
        {
            return await _restService.GetUserListByLogin(pattern);
        }

        public async Task UploadImage(Stream stream)
        {
            await _restService.UploadImage(stream);
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

        public async Task<Board> GetBoard(int boardId)
        {
            return await _restService.GetBoard(boardId);
        }

        #endregion

        #region Columns

        public async Task<Column> AddNewColumn(Column column)
        {
            try
            {
                return await _restService.AddNewColumn(column);
            }
            catch (ServerResponseException)
            {
                throw;
            }
            catch (RestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServerResponseException(ex.Message);
            }
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

        public async Task MoveTask(int taskId, int destinationColumn, int destinationPosition)
        {
            await _restService.MoveTask(taskId, destinationColumn, destinationPosition);
        }

        public async Task<Models.Task> GetTask(int taskId)
        {
            return await _restService.GetTask(taskId);
        }

        #endregion
    }
}
