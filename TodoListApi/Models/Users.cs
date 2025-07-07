using System.ComponentModel.DataAnnotations;

namespace TodoListApi.Models
{
    public class Users
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Email es obligatorio.")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña no puede estar vacia.")]
        public string Password { get; set; } = string.Empty;
    }
}
