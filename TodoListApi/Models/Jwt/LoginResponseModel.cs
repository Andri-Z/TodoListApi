using Azure.Core;
using System.ComponentModel.DataAnnotations;

namespace TodoListApi.Models.Jwt
{
    public class LoginResponseModel
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
    }
}
