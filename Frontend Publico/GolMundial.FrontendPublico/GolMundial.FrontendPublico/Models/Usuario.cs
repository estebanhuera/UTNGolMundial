namespace GolMundial.FrontendPublico.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Nombre { get; set; }
        public string RolNombre { get; set; } // "admin", "usuario", etc.
    }
}
