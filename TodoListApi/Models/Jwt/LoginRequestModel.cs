using System.ComponentModel.DataAnnotations;

namespace TodoListApi.Models.Jwt
{
    public class LoginRequestModel
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MinLength(2)]
        [MaxLength(20)]
        public string Password { get; set; } = string.Empty;
    }
}
