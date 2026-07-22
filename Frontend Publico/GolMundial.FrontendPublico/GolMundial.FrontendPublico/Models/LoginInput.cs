using System.ComponentModel.DataAnnotations;

namespace GolMundial.FrontendPublico.Models
{
    public class LoginInput
    {
        [Required(ErrorMessage = "Ingresa tu usuario o correo")]
        [Display(Name = "Usuario o correo")]
        public string UsuarioOEmail { get; set; } = "";

        [Required(ErrorMessage = "Ingresa tu contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        public string? ReturnUrl { get; set; }
    }
}
