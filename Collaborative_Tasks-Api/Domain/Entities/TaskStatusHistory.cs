using Collaborative_Tasks.Domain.Enums;

namespace Collaborative_Tasks.Domain.Entities
{
    public class TaskStatusHistory
    {
        public ItemStatus Status { get; set; }
        public DateTime ChangedAt { get; set; }
    }

}
