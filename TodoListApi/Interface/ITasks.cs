using TodoListApi.DTOs;
using TodoListApi.Models;

namespace TodoListApi.Interface
{
    public interface ITasks
    {
        Task<ApiResponseModel> GetTaskAsync(PaginationModel pagModel);
        Task<TasksModel?> GetTaskByIdAsync(int id);
        Task<TasksModel?> CreateTaskAsync(TasksDTO task);
        Task<TasksModel?> UpdateTaskAsync(int id, TasksDTO task);
        Task<bool> DeleteTaskAsync(int id);
        Task<TasksModel?> UpdateStatusAsync(int id, Enum status);
    }
}
