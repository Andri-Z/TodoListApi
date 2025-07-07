using System.ComponentModel.DataAnnotations;

namespace TodoListApi.Models.Jwt
{
    public class LoginRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(2), MaxLength(20)]
        public string Password { get; set; } = string.Empty;
    }
}
