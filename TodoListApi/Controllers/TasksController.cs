using Microsoft.AspNetCore.Mvc;
using Serilog.Events;
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
        private readonly ILogger<TasksController> _logger;
        public TasksController(ITasks services, ILogger<TasksController> logger) =>
            (_services, _logger) = (services, logger);

        [HttpGet]
        public async Task<ActionResult<List<Tasks>>> GetTask()
        {
            _logger.LogInformation("Iniciando solicitud de tareas.");

            var task = await _services.GetTaskAsync();

            if (task == null || task.Count == 0)
            {
                _logger.LogInformation("No existen tareas para mostrar.");
                return NoContent();
            }
            _logger.LogInformation("Mostrando tareas...");
            return Ok(task);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Tasks>> GetTaskById(int id)

        {
            _logger.LogInformation("Realizando peticion a la base de datos.");
            var result = await _services.GetTasksByIdAsync(id);

            if (result != null)
            {
                _logger.LogInformation($"Mostrando tarea con el id: {id}");
                return Ok(result);
            }
            else
            {
                return NotFound(new { mensaje = "Esta tarea no existe." });
            }
        }
        [HttpPost]
        public async Task<ActionResult<Tasks>> CreateTask(TasksDTOs task)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("El modelo de los datos no es valido");
                return BadRequest(new { mensaje = "Modelo de datos invalido." });
            }

            _logger.LogInformation("Solicitando Servicio para crear tarea.");

            try
            {
                var newTask = await _services.CreateTaskAsync(task);

                _logger.LogInformation("Mostrando nueva tarea.");

                return CreatedAtAction(nameof(GetTaskById), new { id = newTask.id }, newTask);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inesperado: {ex.Message}");
                return StatusCode(500, new { mensaje = "Ha ocurrido un error inesperado." });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tasks>> DeleteTask(int id)
        {
            _logger.LogInformation($"Solicitando eliminar tarea con el id: {id}");

            var result = await _services.DeleteTaskAsync(id);

            if (!result)
            {
                _logger.LogError($"No se pudo eliminar la tarea, tarea no encontrada con id: {id}");
                return NotFound(new { mensaje = "La tarea no existe." });
            }

            _logger.LogInformation($"Tarea con id: {id} ha sido eliminada");
            return Ok(new { mensaje = "Tarea eliminada satisfactoriamente." });
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<Tasks>> UpdateTask(TasksDTOs task)
        {
            _logger.LogInformation("Solicitando actualizar tarea.");
            return await _services.UpdateTaskAsync(task);
        }
    }
}
