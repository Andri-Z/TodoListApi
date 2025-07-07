using TodoListApi.Models;

namespace TodoListApi.DTOs
{
    public class TasksDTOs
    {
        public string Title { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        //public TasksDTOs(Tasks task)
        //{
        //    Title = task.Title;
        //    Descripcion = task.Descripcion;
        //    Status = task.Status;
        //}
    }
}
