namespace GolMundial.FrontendPublico.Models
{
    public class Posicion
    {
        public string Grupo { get; set; }
        public string Seleccion { get; set; }
        public int PartidosJugados { get; set; }
        public int Ganados { get; set; }
        public int Empatados { get; set; }
        public int Perdidos { get; set; }
        public int GolesFavor { get; set; }
        public int GolesContra { get; set; }
        public int Puntos { get; set; }
    }
}
