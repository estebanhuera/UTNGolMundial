namespace GolMundial.FrontendPublico.Services
{
    public class MensajeResponseDto
    {
        public string Mensaje { get; set; } = "";
        public bool Exitoso { get; set; }
    }

    public class BilleteraResponseDto
    {
        public int BilleteraId { get; set; }
        public int UsuarioId { get; set; }
        public string Username { get; set; } = "";
        public decimal Saldo { get; set; }
    }

    public class PrediccionRequestDto
    {
        public int UsuarioId { get; set; }
        public int PartidoId { get; set; }
        public string TipoResultado { get; set; } = "";
        public decimal Monto { get; set; }
    }

    public class PrediccionResponseDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PartidoId { get; set; }
        public string TipoResultado { get; set; } = "";
        public decimal Monto { get; set; }
        public decimal CuotaAplicada { get; set; }
        public string Estado { get; set; } = "";
    }

    public class UsuarioRegistroDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
        public short RolId { get; set; }
    }
}