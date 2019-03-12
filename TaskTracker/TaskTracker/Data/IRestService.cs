using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskTracker.Data
{
    interface IRestService
    {
        Task LogIn(User user);
        Task Register(User user);
        Task<List<User>> GetUsers();
        Task<List<Board>> GetLoggedUserBoards();
    }
}
