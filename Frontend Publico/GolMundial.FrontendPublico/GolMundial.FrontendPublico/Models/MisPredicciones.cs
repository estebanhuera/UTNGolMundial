namespace GolMundial.FrontendPublico.Models
{
    public class MisPredicciones
    {
        public int Id { get; set; }
        public int PartidoId { get; set; }
        public string PartidoDescripcion { get; set; } = "";
        public DateTime FechaPartido { get; set; }
        public string ResultadoPredicho { get; set; } = "";
        public int Monto { get; set; }
        public string Estado { get; set; } = "";   // "Pendiente", "Ganada", "Perdida"
        public int Ganancia { get; set; }
    }
}
