using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using TodoListApi.Context;
using TodoListApi.DTOs;
using TodoListApi.Interface;
using TodoListApi.Models;

namespace TodoListApi.Services
{
    public class TasksServices : ITasks
    {
        private readonly TodoListContext _context;
        private readonly ILogger<TodoListContext> _logger;
        public TasksServices(TodoListContext context, ILogger<TodoListContext> logger) =>
            (_context, _logger) = (context, logger);
        public async Task<List<Tasks>> GetTaskAsync()
        {
            try
            {
                _logger.LogInformation("Obteniendo todas las tareas desde la base de datos...");

                return await _context.Tasks.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al solicitar a la base de datos: {Mensaje}", ex.Message);
                throw;
            }
        }
        public async Task<Tasks?> GetTasksByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Consultando tarea con id: {id} a la base de datos.");

                var task = await _context.Tasks.FindAsync(id);

                if (task == null)
                {
                    _logger.LogWarning($"No se encontro la tarea con id: {id}");
                    return null;
                }

                _logger.LogInformation("Retornando la tarea.");
                return task;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error con la tarea {id}, durante la operacion: {ex.Message}");
                throw;
            }
        }
        public async Task<Tasks> CreateTaskAsync(TasksDTOs task)
        {
            try 
            {
                var newTask = new Tasks
                {
                    title = task.title,
                    descripcion = task.descripcion
                };
                _logger.LogInformation("Guardando tarea en la base de datos...");
                
                _context.Add(newTask);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Tarea creada exitosamente con el id: {newTask.id}.");
                return newTask;
            }
            catch(Exception ex)
            {
                _logger.LogError("Ha ocurrido un error al crear la tarea: {Mensaje}",ex.Message);
                throw;
            }
        }
        public async Task<bool> DeleteTaskAsync(int id)
        {
            _logger.LogInformation("Buscando tarea para confirmar que existe.");

            try
            {
                var task = await GetTasksByIdAsync(id);
                if(task == null)
                {
                    return false;
                }

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Tarea con el id:{id} ha sido eliminada.");
                return true;

            }
            catch(Exception ex)
            {
                _logger.LogError($"Error al eliminar la tarea con el id: {id}, se ha producido la siguiente excepcion: {ex.Message}");
                return false;
            }
        }
        public async Task<Tasks> UpdateTaskAsync(TasksDTOs task)
        {
            _logger.LogInformation("Comprobando que la tarea exista.");

            var newTask = new Tasks
            {
                title = task.title,
                descripcion = task.descripcion
            };

            var result = await _context.Tasks.FindAsync(newTask.id);
            if (result == null)
            {
                return new Tasks();
            }
            
            _logger.LogInformation("Actualizando tarea.");

            _context.Tasks.Update(newTask);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Mostrando tarea actualizada.");
            return newTask;
        }
    }
}