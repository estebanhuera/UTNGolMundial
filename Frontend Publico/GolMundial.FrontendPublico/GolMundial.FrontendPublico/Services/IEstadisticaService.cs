using GolMundial.FrontendPublico.Models;

namespace GolMundial.FrontendPublico.Services
{
    public interface IEstadisticaService
    {
        Task<List<EstadisticaSeleccion>> ObtenerTodasAsync();
    }
}
