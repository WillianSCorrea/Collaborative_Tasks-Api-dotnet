using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Collaborative_Tasks.Services;
using Collaborative_Tasks.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
namespace Collaborative_Tasks.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _service;

        public TasksController(TaskService service)
        {
            _service = service;
        }

        private string UserId =>
            User.FindFirstValue(ClaimTypes.NameIdentifier);

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDto dto)
        {
            var task = await _service.CreateAsync(dto, UserId);
            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateTaskDto dto)
        {
            await _service.UpdateAsync(id, dto, UserId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id, UserId);
            return NoContent();
        }
    }

}
