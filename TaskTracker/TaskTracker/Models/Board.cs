using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaskTracker.ViewModels.VM;

namespace TaskTracker.Models
{
    [Serializable]
    internal class Board
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "columns")]
        public List<Column>Columns { get; set; }

        [JsonProperty(PropertyName = "assignedUserIds")]
        public int[] AssignedUsersIds { get; set; }

        public Board(string name)
        {
            Name = name;
        }
    }
}
