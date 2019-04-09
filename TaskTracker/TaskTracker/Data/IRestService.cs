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
        Task<User> GetUser(int userId);
        
        Task<List<Board>> GetLoggedUserBoards();
        Task<Board> AddNewBoard(string name);
        Task<Board> EditBoard(Board board);
        Task DeleteBoard(int id);
        Task<List<User>> GetUsersAssignedToBoard(int boardId);
        Task<Board> GetBoard(int boardId);

        Task<Column> AddNewColumn(Column column);
        Task<Column> EditColumn(Column column);
        Task DeleteColumn(int columnId);

        Task<Models.Task> AddNewTask(Models.Task task);
        Task<Models.Task> EditTask(Models.Task task);
        Task DeleteTask(int taskId);
    }
}
