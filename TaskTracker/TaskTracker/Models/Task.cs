using System;
using Newtonsoft.Json;

namespace TaskTracker.Models
{
    [Serializable]
    internal class Task
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "boardId")]
        public int BoardId { get; set; }

        [JsonProperty(PropertyName = "columnId")]
        public int ColumnId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public int AssignedUserId { get; set; }

        [JsonProperty(PropertyName = "position")]
        public int Position { get; set; }

        public Task(int boardId, int columnId, string name)
        {
            BoardId = boardId;
            ColumnId = columnId;
            Name = name;
        }
    }
}
