using System;
using Newtonsoft.Json;

namespace TaskTracker.Models
{
    [Serializable]
    internal class Column
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "tasks")]
        public Task[] Tasks { get; set; }
    }
}
