namespace GolMundial.FrontendPublico.Models
{
    public class RankingItem
    {
        public int Posicion { get; set; }
        public int UsuarioId { get; set; }
        public string Username { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int Saldo { get; set; }
        public int Apuestas { get; set; }
        public int Aciertos { get; set; }
    }
}
