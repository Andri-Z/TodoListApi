using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger<TodoListContext> _logger;
        public TasksServices(TodoListContext context, ILogger<TodoListContext> logger) =>
            (_context, _logger) = (context, logger);
        public async Task<ApiResponse> GetTaskAsync(int page, int limit)
        {
            try
            {
                var result = await _context.Tasks.ToListAsync();

                var tasksPerPage = Response(page,limit,result);

                return tasksPerPage;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al solicitar las tareas a la base de datos: {ex.Message}");
                throw;
            }
        }
        public async Task<Tasks?> GetTasksByIdAsync(int id)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);

                if (task == null)
                    return null;

                return task;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error al consultar tarea con id:{id}.\n Excepcion: {ex.Message}");
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
                    descripcion = task.descripcion,
                    status = task.status
                };
                
                _context.Add(newTask);
                await _context.SaveChangesAsync();

                return newTask;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error al crear la tarea:{task}.\n Excepcion:{ex.Message}");
                throw;
            }
        }
        public async Task<bool> DeleteTaskAsync(int id)
        {
            try
            {
                var task = await GetTasksByIdAsync(id);
                if(task == null)
                    return false;

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error eliminando la tarea:{id}.\n Excepcion: {ex.Message}");
                throw;
            }
        }
        public async Task<Tasks?> UpdateTaskAsync(int id, TasksDTOs task)
        {
            try
            {
                var _status = StatusValid(task.status);
                var newTask = await _context.Tasks.FindAsync(id);

                if (_status == null || newTask == null)
                    return null;

                newTask.title = task.title;
                newTask.descripcion = task.descripcion;
                newTask.status = _status;

                await _context.SaveChangesAsync();

                return newTask;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error actualizando la tarea con el id:{id}.\n Excepcion: {ex.Message}");
                throw;
            }
        }
        public async Task<Tasks?> UpdateStatusAsync(int id, string status)
        {
            try
            {
                var _status = StatusValid(status);
                var task = await _context.Tasks.FindAsync(id);

                if (_status == null || task == null)
                    return null;

                task.status = _status;

                await _context.SaveChangesAsync();
                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error actualizando el estado de la tarea con el id:{id}.\n Excepcion: {ex.Message}");
                throw;
            }
        }
        public string? StatusValid(string status)
        {
            var _status = status.ToLower().Replace(" ", "");

            if (!Enum.TryParse<Status>(_status, true, out var parsedStatus))
                return null;

            return _status;
        }
        public ApiResponse Response(int page, int limit, List<Tasks> tasks)
        { 
            var totalTask = tasks.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalTask / limit);
            var tasksPerPage = tasks
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToList();

            var response = new ApiResponse
            {
                data = tasksPerPage,
                page = page,
                limit = limit,
                total = totalTask
            };

            return response;
        }
    }
}