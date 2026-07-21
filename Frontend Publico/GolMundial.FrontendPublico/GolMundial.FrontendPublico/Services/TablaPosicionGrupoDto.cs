namespace GolMundial.FrontendPublico.Services
{
    public class TablaPosicionGrupoDto
    {
        public string? GrupoCodigo { get; set; }
        public string GrupoNombre { get; set; } = "";
        public List<PosicionSeleccionDto> Posiciones { get; set; } = new();
    }

    public class PosicionSeleccionDto
    {
        public int SeleccionId { get; set; }
        public string Nombre { get; set; } = "";
        public string CodigoFifa { get; set; } = "";
        public int PJ { get; set; }
        public int PG { get; set; }
        public int PE { get; set; }
        public int PP { get; set; }
        public int GF { get; set; }
        public int GC { get; set; }
        public int DG { get; set; }
        public int Pts { get; set; }
    }
}