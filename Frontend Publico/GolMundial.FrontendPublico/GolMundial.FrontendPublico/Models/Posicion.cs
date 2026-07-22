namespace GolMundial.FrontendPublico.Models
{
    public class Posicion
    {
        public string GrupoCodigo { get; set; } = "";
        public int SeleccionId { get; set; }
        public string CodigoFifa { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int Pj { get; set; }
        public int Pg { get; set; }
        public int Pe { get; set; }
        public int Pp { get; set; }
        public int Gf { get; set; }
        public int Gc { get; set; }
        public int Dif { get; set; }
        public int Puntos { get; set; }
    }
}
