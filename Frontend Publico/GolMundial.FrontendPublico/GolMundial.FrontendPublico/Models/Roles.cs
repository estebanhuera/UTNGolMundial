namespace GolMundial.FrontendPublico.Models
{
    public class Roles
    {
        public const string Publico = "USUARIO";

        public static bool EsPublico(string? rolNombre) =>
            string.Equals(rolNombre, Publico, StringComparison.OrdinalIgnoreCase);
    }
}
