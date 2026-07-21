using System.ComponentModel.DataAnnotations;

namespace GolMundial.FrontendPublico.Models
{
    public class RegistroInput
    {
        [Required(ErrorMessage = "Elige un usuario")]
        [StringLength(40, MinimumLength = 3)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Ingresa tu nombre completo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingresa un email")]
        [EmailAddress(ErrorMessage = "El email no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Elige una contraseña")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirma tu contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarPassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}
