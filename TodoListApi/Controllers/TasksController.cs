using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITasks _services;
        public TasksController(ITasks services) =>
            (_services) = (services);
        [HttpGet]
        public async Task<ActionResult<List<TasksModel>>> GetTask([FromQuery]PaginationModel pagModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { mensaje = "Tiene que ingresar valores validos." });

            var task = await _services.GetTaskAsync(pagModel);

            if (task is null || task.Data.Count is 0)
                return NoContent();

            return Ok(task);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TasksModel>> GetTaskById(int id)
        {
            var result = await _services.GetTaskByIdAsync(id);

            if (result is not null)
                return Ok(result);
            else
                return NotFound(new { mensaje = $"La tarea con Id: {id} no existe."});
        }
        [HttpPost]
        public async Task<ActionResult<TasksModel>> CreateTask(TasksDTO task)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { mensaje = "Modelo de datos invalido." });
            
            var newTask = await _services.CreateTaskAsync(task);

            if (newTask is null)
                return BadRequest();

            return CreatedAtAction(nameof(GetTaskById), new { newTask.Id }, newTask);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<TasksModel>> DeleteTask(int id)
        {
            var result = await _services.DeleteTaskAsync(id);

            if (!result)
                return NotFound(new { mensaje = $"La tarea con Id:{id} no existe." });

            return Ok(new { mensaje = "Tarea eliminada satisfactoriamente." });
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TasksModel>> UpdateTask(int id, TasksDTO task)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { mensaje = "Modelo invalido." });

            var result = await _services.UpdateTaskAsync(id, task);
            if (result is null)
                return NotFound(new { mensaje = "Verifique que los datos ingresados sean correctos." });
            
            return Ok(result);
        }
        [HttpPatch]
        public async Task<ActionResult<TasksModel>> UpdateStatus(int id, Enum status)
        {
            var result = await _services.UpdateStatusAsync(id, status);

            if (result is null)
                return NotFound(new { mensaje = "Verifique que los datos ingresados sean correctos." });

            return Ok(result);
        }
    }
}