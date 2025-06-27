using System.ComponentModel.DataAnnotations;

namespace TodoListApi.Models
{
    public class Users
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Email es obligatorio.")]
        public string email { get; set; } = string.Empty;
        [Required(ErrorMessage = "La contraseña no puede estar vacia.")]
        public string password { get; set; } = string.Empty;
    }
}
