namespace GolMundial.FrontendPublico.Services
{
    public class UsuarioApiDto
    {


        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
        public string RolNombre { get; set; } = "";
        public bool Activo { get; set; }
    }

    public class LoginApiDto
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class NuevoUsuarioApiDto
    {
        public string Username { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Mail { get; set; } = "";
        public string Password { get; set; } = "";
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
        public short RolId { get; set; }
    }
}

