namespace GolMundial.FrontendPublico.Models
{
    public class Partido
    {
        public int Id { get; set; }
        public int NumeroPartidoFifa { get; set; }
        public string EquipoLocal { get; set; }
        public string EquipoVisitante { get; set; }
        public string CodigoFifaLocal { get; set; }
        public string CodigoFifaVisitante { get; set; }
        public DateTime FechaHora { get; set; }
        public string Sede { get; set; }
        public string GrupoCodigo { get; set; }
        public string Fase { get; set; }
        public string Estado { get; set; } // PROGRAMADO, EN_JUEGO, FINALIZADO, SUSPENDIDO, CANCELADO
        public int? GolesLocal { get; set; }
        public int? GolesVisitante { get; set; }
    }
}
