namespace GolMundial.FrontendPublico.Services
{
    public class CalendarioPartidoDto
    {
        public int PartidoId { get; set; }
        public int NumeroPartidoFifa { get; set; }
        public DateTime FechaPartido { get; set; }
        public string Estado { get; set; } = "";
        public string FaseNombre { get; set; } = "";
        public string? GrupoCodigo { get; set; }
        public string SedeNombre { get; set; } = "";
        public string LocalNombre { get; set; } = "";
        public string LocalCodigoFifa { get; set; } = "";
        public string VisitanteNombre { get; set; } = "";
        public string VisitanteCodigoFifa { get; set; } = "";
        public int? GolesLocal { get; set; }
        public int? GolesVisitante { get; set; }
    }
}