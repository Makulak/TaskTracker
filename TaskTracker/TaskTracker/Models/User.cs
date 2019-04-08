using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace TaskTracker.Models
{
    [Serializable]
    internal class User
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "mail")]
        public string Mail { get; set; }

        [JsonProperty(PropertyName = "boardIds")]
        public int[] BoardIds { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        public User()
        { }

        public User(int id, string login)
        {
            Id = id;
            Login = login;
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
