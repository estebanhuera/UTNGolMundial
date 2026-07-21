using System.ComponentModel.DataAnnotations;

namespace GolMundial.FrontendPublico.Models
{
    public class RegistroInput
    {
        [Required(ErrorMessage = "Elegí un usuario")]
        [StringLength(40, MinimumLength = 3)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Ingresá tu nombre completo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingresá un email")]
        [EmailAddress(ErrorMessage = "El email no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Elegí una contraseña")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmá tu contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarPassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}
