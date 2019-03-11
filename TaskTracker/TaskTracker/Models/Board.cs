namespace TaskTracker.Models
{
    internal class Board
    {
        public string Name { get; set; }
        public Column Columns { get; set; }
        public User[] AssignedUser { get; set; }
    }
}
