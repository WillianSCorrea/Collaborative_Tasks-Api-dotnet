using Collaborative_Tasks.Domain.Enums;

namespace Collaborative_Tasks.Domain.Entities
{
    public class TaskItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ItemStatus Status { get; set; }

        public string OwnerUserId { get; set; }

        public List<TaskStatusHistory> History { get; set; } = new();

        public DateTime CreatedAt { get; set; }
    }

}
