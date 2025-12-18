using Collaborative_Tasks.Domain.Enums;

namespace Collaborative_Tasks.DTOs
{
    public record CreateTaskDto(string Title, string Description);
    public record UpdateTaskDto(
    string Title,
    string Description,
    ItemStatus Status
);

}
