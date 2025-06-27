using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var task = await _services.GetTaskAsync();
                if (task != null)
                {
                    return task;
                }
                else
                {
                    _logger.LogInformation("Datos nulos o invalidos");
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Ha ocurrido un error durante la operacion: {Mensaje}",ex.Message);
                return BadRequest();
            }
        }
    }
}
