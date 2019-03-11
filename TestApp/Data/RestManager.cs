using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.Models;

namespace TaskTracker.Data
{
    internal class RestManager
    {
        private RestService _restService;

        public RestManager(RestService service)
        {
            _restService = service;
        }

        public Task<List<User>> GetUsers()
        {
            return _restService.GetUsers();
        }
    }
}
