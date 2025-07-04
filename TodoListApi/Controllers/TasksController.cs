using Microsoft.AspNetCore.Mvc;
using Serilog.Events;
using System.Reflection.Metadata.Ecma335;
using TodoListApi.DTOs;
using TodoListApi.Interface;
using TodoListApi.Models;

namespace TodoListApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITasks _services;
        public TasksController(ITasks services) =>
            (_services) = (services);

        [HttpGet]
        public async Task<ActionResult<List<Tasks>>> GetTask(int page, int limit)
        {
            var task = await _services.GetTaskAsync(page,limit);

            if (task == null || task.data.Count == 0)
                return NoContent();

            return Ok(task);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Tasks>> GetTaskById(int id)
        {
            var result = await _services.GetTasksByIdAsync(id);

            if (result != null)
                return Ok(result);
            else
                return NotFound(new { mensaje = "Esta tarea no existe." });
        }
        [HttpPost]
        public async Task<ActionResult<Tasks>> CreateTask(TasksDTOs task)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { mensaje = "Modelo de datos invalido." });
            
            var newTask = await _services.CreateTaskAsync(task);
            
            return CreatedAtAction(nameof(GetTaskById), new { newTask.id }, newTask);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tasks>> DeleteTask(int id)
        {
            var result = await _services.DeleteTaskAsync(id);

            if (!result)
                return NotFound(new { mensaje = "La tarea no existe." });

            return Ok(new { mensaje = "Tarea eliminada satisfactoriamente." });
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Tasks>> UpdateTask(int id, TasksDTOs task)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { mensaje = "Modelo invalido." });

            var result = await _services.UpdateTaskAsync(id, task);
            if (result == null)
                return NotFound(new { mensaje = "Verifique que los datos ingresados sean correctos." });
            
            return Ok(result);
        }
        [HttpPatch]
        public async Task<ActionResult<Tasks>> UpdateStatus(int id, string status)
        {
            var result = await _services.UpdateStatusAsync(id, status);

            if (result == null)
                return NotFound(new { mensaje = "Verifique que los datos ingresados sean correctos." });

            return Ok(result);
        }
    }
}
