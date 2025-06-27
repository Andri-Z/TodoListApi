using TodoListApi.Models;

namespace TodoListApi.Interface
{
    public interface ITasks
    {
        Task<List<Tasks>> GetTaskAsync();
    }
}
