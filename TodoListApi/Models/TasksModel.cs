using System.ComponentModel.DataAnnotations;
using TodoListApi.DTOs;

namespace TodoListApi.Models
{
    public class TasksModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public StatusModel Status { get; set; }
    }
}