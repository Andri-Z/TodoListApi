namespace TodoListApi.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}