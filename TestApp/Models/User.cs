namespace TaskTracker.Models
{
    internal class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public Table Tables { get; set; }
    }
}
