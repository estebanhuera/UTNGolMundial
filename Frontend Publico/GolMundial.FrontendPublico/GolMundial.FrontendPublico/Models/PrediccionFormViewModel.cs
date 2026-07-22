namespace GolMundial.FrontendPublico.Models
{
    public class PrediccionFormViewModel
    {
        public Partido Partido { get; set; } = null!;
        public int Saldo { get; set; }
        public PrediccionInput Input { get; set; } = new();
    }
}