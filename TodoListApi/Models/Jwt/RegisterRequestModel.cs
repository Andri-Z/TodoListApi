using System.ComponentModel.DataAnnotations;

namespace TodoListApi.Models.Jwt
{
    public class RegisterRequestModel
    {
        [Required]
        [MinLength(3),MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
