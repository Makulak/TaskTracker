using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TaskTracker.Models;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace TaskTracker.Data
{
    interface IRestService
    {
        Task LogIn(User user);
        Task LogOut();
        Task Register(User user);
        Task<User> GetUser(int? userId = null);
        Task<List<User>> GetUserListByLogin(string pattern);
        Task UploadImage(Stream stream);

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
        Task MoveTask(int taskId, int destinationColumn, int destinationPosition);
        Task<Models.Task> GetTask(int taskId);
    }
}
