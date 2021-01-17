using System.Runtime.Serialization;

namespace LifeManager.Server.Model {
    public enum ToDoTaskStatus {
        Ready,
        
        [EnumMember(Value = "In Progress")]
        InProgress,
        
        Complete,
        
        Cancelled
    }
}