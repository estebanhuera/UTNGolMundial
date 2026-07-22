using GolMundial.FrontendPublico.Models;

namespace GolMundial.FrontendPublico.Services
{
    public interface IUsuarioService
    {
        Task<Usuario?> ValidarCredencialesAsync(string usuarioOEmail, string password);
        Task<bool> ExisteEmailAsync(string email);
        Task<bool> ExisteUsernameAsync(string username);
        Task<Usuario?> RegistrarAsync(RegistroInput input);
    }
}