namespace GolMundial.FrontendPublico.Models
{
    public class Partido
    {
        public int Id { get; set; }
        public string EquipoLocal { get; set; }
        public string EquipoVisitante { get; set; }
        public DateTime FechaHora { get; set; }
        public string Sede { get; set; }
        public string Fase { get; set; }
        public string Estado { get; set; } // "Programado", "En curso", "Finalizado"
        public int? GolesLocal { get; set; }
        public int? GolesVisitante { get; set; }
    }
}
