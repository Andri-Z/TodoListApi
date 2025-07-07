using System.ComponentModel.DataAnnotations;

namespace TodoListApi.Models
{
    public class PaginationModel
    {
        [Required]
        [Range(1,100)]
        public int Page { get; set; }

        [Required]
        [Range(1, 100)]
        public int Limit { get; set; }
    }
}
