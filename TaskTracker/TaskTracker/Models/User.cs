using Newtonsoft.Json;

namespace TaskTracker.Models
{
    internal class User
    {
        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "mail")]
        public string Mail { get; set; }

        [JsonProperty(PropertyName = "boards")]
        public Board[] Boards { get; set; }

        public User()
        {
        }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public User(string login, string password, string mail)
        {
            Login = login;
            Password = password;
            Mail = mail;
        }
    }
}
