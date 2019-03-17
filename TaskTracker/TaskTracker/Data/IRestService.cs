using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskTracker.Data
{
    interface IRestService
    {
        Task LogIn(User user);
        Task Register(User user);
        Task<List<Board>> GetLoggedUserBoards();
        Task DeleteBoard(int id);
        Task<Board> AddNewBoard(string name);
        Task EditBoard(Board board);
        Task<Column> AddNewColumn(Column column);
        Task<Models.Task> AddNewTask(Models.Task task);
    }
}
