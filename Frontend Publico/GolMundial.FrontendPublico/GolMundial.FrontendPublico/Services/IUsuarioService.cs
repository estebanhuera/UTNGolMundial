using GolMundial.FrontendPublico.Models;

namespace GolMundial.FrontendPublico.Services
{
    public interface IUsuarioService
    {
        Usuario? ValidarCredenciales(string email, string password);
        bool ExisteEmail(string email);
        Usuario Registrar(RegistroInput input);
    }
}
