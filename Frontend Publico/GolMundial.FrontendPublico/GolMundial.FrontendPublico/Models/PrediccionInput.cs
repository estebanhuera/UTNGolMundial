using System.ComponentModel.DataAnnotations;

namespace GolMundial.FrontendPublico.Models
{
    public class PrediccionInput
    {
        [Required]
        public int PartidoId { get; set; }

        [Required(ErrorMessage = "Elige un resultado")]
        public string ResultadoPredicho { get; set; } = ""; // "Local", "Empate", "Visitante"

        [Required(ErrorMessage = "Ingresa un monto")]
        [Range(1, 1000, ErrorMessage = "El monto debe estar entre 1 y 1000 UTNGolCoin")]
        public int Monto { get; set; }
    }
}
