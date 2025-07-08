using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
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
        private readonly ILogger<TasksServices> _logger;
        public TasksServices(TodoListContext context, ILogger<TasksServices> logger) =>
            (_context, _logger) = (context, logger);
        public async Task<ApiResponseModel> GetTaskAsync(PaginationModel pagModel)
        {
            try
            {
                var total = await _context.Tasks.CountAsync();
                var data = await _context.Tasks
                                   .Skip((pagModel.Page - 1) * pagModel.Limit)
                                   .Take(pagModel.Limit)
                                   .ToListAsync();

                return new ApiResponseModel
                {
                    Data = data,
                    Page = pagModel.Page,
                    Limit = pagModel.Limit,
                    Total = total
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al solicitar las tareas a la base de datos: {ex.Message}");
                throw;
            }
        }
        public async Task<TasksModel?> GetTaskByIdAsync(int id)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);

                if (task == null)
                    return null;

                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error al consultar tarea con id:{id}.\n Excepcion: {ex.Message}");
                throw;
            }
        }
        public async Task<TasksModel?> CreateTaskAsync(TasksDTO task)
        {
            try
            {
                var _status = ParseStatus(task.Status.ToString());
                if (_status is null)
                    return null;

                var newTask = new TasksModel
                {
                    Title = task.Title,
                    Description = task.Description,
                    Status = _status.Value
                };

                _context.Add(newTask);
                await _context.SaveChangesAsync();

                return newTask;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error al crear la tarea:{task}.\n Excepcion:{ex.Message}");
                throw;
            }
        }
        public async Task<bool> DeleteTaskAsync(int id)
        {
            try
            {
                var task = await GetTaskByIdAsync(id);
                if (task == null)
                    return false;

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error eliminando la tarea:{id}.\n Excepcion: {ex.Message}");
                throw;
            }
        }
        public async Task<TasksModel?> UpdateTaskAsync(int id, TasksDTO task)
        {
            try
            {
                var newTask = await _context.Tasks.FindAsync(id);
                var _status = ParseStatus(task.Status.ToString());

                if (newTask is null || _status is null)
                    return null;

                newTask.Title = task.Title;
                newTask.Description = task.Description;
                newTask.Status = _status.Value;

                await _context.SaveChangesAsync();

                return newTask;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error actualizando la tarea con el id:{id}.\n Excepcion: {ex.Message}");
                throw;
            }
        }
        public async Task<TasksModel?> UpdateStatusAsync(int id, string status)
        {
            try
            {
                var _status = ParseStatus(status.ToString());
                var task = await _context.Tasks.FindAsync(id);

                if (task is null || _status is null)
                    return null;

                task.Status = _status.Value;

                await _context.SaveChangesAsync();
                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error actualizando el estado de la tarea con el id:{id}.\n Excepcion: {ex.Message}");
                throw;
            }
        }
        public static StatusModel? ParseStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return null;

            var _status = status.ToLower().Replace(" ", "").Trim();

            return _status switch
            {
                "pendiente" => StatusModel.pendiente,
                "enproceso" => StatusModel.enproceso,
                "completada" => StatusModel.completada,
                _ => null
            };
        }
    }
}