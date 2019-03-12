using Newtonsoft.Json;

namespace TaskTracker.Models
{
    internal class Board
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "columns")]
        public Column Columns { get; set; }

        [JsonProperty(PropertyName = "assignedUserIds")]
        public int[] AssignedUsersIds { get; set; }
    }
}
