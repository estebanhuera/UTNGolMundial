using GolMundial.FrontendPublico.Models;

namespace GolMundial.FrontendPublico.Services
{
    public interface IPrediccionService
    {
        Task<int> ObtenerSaldoAsync(int usuarioId);
        Task<MisPredicciones?> ObtenerDelPartidoAsync(int usuarioId, int partidoId);
        Task<List<MisPredicciones>> ObtenerPorUsuarioAsync(int usuarioId);
        Task<ResultadoOperacion> CrearAsync(int usuarioId, PrediccionInput input);
        Task<List<RankingItem>> ObtenerRankingAsync();

    }
}
