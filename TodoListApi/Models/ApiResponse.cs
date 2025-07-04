namespace TodoListApi.Models
{
    public class ApiResponse
    {
        public List<Tasks> data { get; set; } = [];
        public int page { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
    }
}
