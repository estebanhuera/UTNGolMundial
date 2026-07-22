namespace GolMundial.FrontendPublico.Models
{
    public class EstadisticaSeleccion
    {
        public int SeleccionId { get; set; }
        public string CodigoFifa { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Confederacion { get; set; } = "";
        public bool EsAnfitrion { get; set; }
        public int PartidosJugados { get; set; }
        public int Ganados { get; set; }
        public int Empatados { get; set; }
        public int Perdidos { get; set; }
        public int GolesFavor { get; set; }
        public int GolesContra { get; set; }
    }
}
