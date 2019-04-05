using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaskTracker.Models
{
    [Serializable]
    public class Column
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "boardId")]
        public int BoardId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "position")]
        public int Position { get; set; }

        [JsonProperty(PropertyName = "tasks")]
        public List<Task> Tasks { get; set; }

        public Column(int boardId, string name)
        {
            BoardId = boardId;
            Name = name;
        }
    }
}
