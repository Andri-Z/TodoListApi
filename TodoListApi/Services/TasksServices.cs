using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TodoListApi.Context;
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
                return await _context.Tasks.ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogCritical("Error al solicitar a la base de datos: {Mensaje}", ex.Message);
                return null;
            }
        }
    }
}
