using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedModels;
using Task_Management_Web_APIs.Services.Interface;

namespace Task_Management_Web_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        public ITaskRepo _taskRepoService;
        public TaskController(ITaskRepo taskService)
        {
            _taskRepoService = taskService;
        }

        [HttpGet("/tasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = _taskRepoService.GetAllProducts();
            return result.Count > 0 ? Ok(result) : NotFound();
        }

        [HttpGet("/taskById")]
        public async Task<IActionResult> GetTaskById([FromQuery] Guid taskId)
        {
            var task = await _taskRepoService.GetProduct(taskId);
            return task != null ? Ok(task) : NotFound(new { Success = false, Message = "task did not found." });
        }

        [HttpPost("/addTask")]
        public async Task<IActionResult> AddTask([FromForm] TaskModel model)
        {
            var status = await _taskRepoService.AddTask(model);
            return status > 0 ? Ok(status) : StatusCode(500, "Error adding a task");
        }

        [HttpDelete("/DeleteTaskById")]
        public async Task<IActionResult> DeleteTask([FromQuery] Guid taskId)
        {
            var status = await _taskRepoService.DeleteProduct(taskId);
            return status ? Ok(new { Success = true, Message = "Task Deleted successfully." }) :
                StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "task did not found" });
        }

        [HttpPut("/UpdateTaskById")]
        public async Task<IActionResult> UpdateTask([FromBody] TaskModel model)
        {
            var status = await _taskRepoService.Update(model);
            return status ? Ok(new { Success = true, Message = "Task updated successfully." }) :
                StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = "task did not found" });
        }
    }
}
