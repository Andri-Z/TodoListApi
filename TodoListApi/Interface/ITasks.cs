using TodoListApi.DTOs;
using TodoListApi.Models;

namespace TodoListApi.Interface
{
    public interface ITasks
    {
        Task<ApiResponse> GetTaskAsync(int page, int limit);
        Task<Tasks?> GetTasksByIdAsync(int id);
        Task<Tasks> CreateTaskAsync(TasksDTOs task);
        Task<Tasks?> UpdateTaskAsync(int id, TasksDTOs task);
        Task<bool> DeleteTaskAsync(int id);
        Task<Tasks?> UpdateStatusAsync(int id, string status);
    }
}
