using System.ComponentModel.DataAnnotations;
using TodoListApi.Models;

namespace TodoListApi.DTOs
{
    public class TasksDTO
    {
        [Required]
        [MinLength(2),MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(2)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
