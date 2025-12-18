using Collaborative_Tasks.Domain.Entities;
using Collaborative_Tasks.DTOs;
using System.Security;
using Collaborative_Tasks.Domain.Enums;

namespace Collaborative_Tasks.Services
{
    public class TaskService
    {
        private readonly FirestoreService _firestore;

        public TaskService(FirestoreService firestore)
        {
            _firestore = firestore;
        }

        public async Task<TaskItem> CreateAsync(CreateTaskDto dto, string userId)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new Exception("Título é obrigatório.");

            var task = new TaskItem
            {
                Id = Guid.NewGuid().ToString(),
                Title = dto.Title,
                Description = dto.Description,
                Status = ItemStatus.Pending,
                OwnerUserId = userId,
                CreatedAt = DateTime.UtcNow,
                History =
            {
                new TaskStatusHistory
                {
                    Status = ItemStatus.Pending,
                    ChangedAt = DateTime.UtcNow
                }
            }
            };

            await _firestore.AddAsync("tasks", task.Id, task);
            return task;
        }

        public async Task UpdateAsync(string taskId, UpdateTaskDto dto, string userId)
        {
            var task = await _firestore.GetAsync<TaskItem>("tasks", taskId)
                ?? throw new Exception("Tarefa não encontrada.");

            if (task.OwnerUserId != userId)
                throw new SecurityException("Acesso negado.");

            if (dto.Status == ItemStatus.Completed &&
                string.IsNullOrWhiteSpace(dto.Title))
                throw new Exception("Não pode concluir sem título.");

            if (task.Status != dto.Status)
            {
                task.History.Add(new TaskStatusHistory
                {
                    Status = dto.Status,
                    ChangedAt = DateTime.UtcNow
                });
            }

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = dto.Status;

            await _firestore.UpdateAsync("tasks", task.Id, task);
        }

        public async Task DeleteAsync(string taskId, string userId)
        {
            var task = await _firestore.GetAsync<TaskItem>("tasks", taskId)
                ?? throw new Exception("Tarefa não encontrada.");

            if (task.OwnerUserId != userId)
                throw new SecurityException("Acesso negado.");

            await _firestore.DeleteAsync("tasks", task.Id);
        }
    }

}
