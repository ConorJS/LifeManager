using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LifeManager.Server.Model.Domain {
    public class ToDoTask : ITask {
        public long Id { get; set; }

        public DateTime? DateTimeCreated { get; set; }

        public DateTime? DateTimeLastModified { get; set; }

        public long OwnedByUserId { get; set; }

        public bool Active { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ToDoTaskStatus Status { get; set; }

        public string Comments { get; set; }

        public int RelativeSize { get; set; }

        public int Priority { get; set; }
    }
}