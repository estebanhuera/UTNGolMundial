namespace GolMundial.FrontendPublico.Services
{
    public class EstadisticaSeleccionDto
    {
        public int SeleccionId { get; set; }
        public string Nombre { get; set; } = "";
        public string CodigoFifa { get; set; } = "";
        public string Confederacion { get; set; } = "";
        public int PartidosJugados { get; set; }
        public int Ganados { get; set; }
        public int Empatados { get; set; }
        public int Perdidos { get; set; }
        public int GolesAFavor { get; set; }
        public int GolesEnContra { get; set; }
        public int DiferenciaDeGoles { get; set; }
    }
}