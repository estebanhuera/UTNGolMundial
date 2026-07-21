using System.ComponentModel.DataAnnotations;

namespace GolMundial.FrontendPublico.Models
{
    public class LoginInput
    {
        [Required(ErrorMessage = "Ingresa tu email")]
        [EmailAddress(ErrorMessage = "El email no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingresa tu contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}

