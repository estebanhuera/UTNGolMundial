using GolMundial.FrontendPublico.Models;

namespace GolMundial.FrontendPublico.Services
{
    public interface IPosicionService
    {
        Task<List<Posicion>> ObtenerTodasAsync();
    }
}