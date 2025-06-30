using TodoListApi.Models;

namespace TodoListApi.DTOs
{
    public class TasksDTOs
    {
        public string title { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public Status status { get; set; }
    }
}
