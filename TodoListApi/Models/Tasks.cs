namespace TodoListApi.Models
{
    public class Tasks
    {
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public Status status { get;set; }
    }
    public enum Status
    {
        Pendiente = 1,
        EnProceso = 2,
        Completada = 3,
    }
}
