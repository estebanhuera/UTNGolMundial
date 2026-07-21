using GolMundial.FrontendPublico.Models;

namespace GolMundial.FrontendPublico.Services
{
    public interface IPartidoService
    {
        Task<List<Partido>> ObtenerTodosAsync();
        Task<Partido?> ObtenerPorIdAsync(int id);
    }
}
