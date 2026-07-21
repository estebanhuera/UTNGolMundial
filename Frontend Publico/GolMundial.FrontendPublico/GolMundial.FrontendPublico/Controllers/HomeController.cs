using GolMundial.FrontendPublico.Models;
using GolMundial.FrontendPublico.Services;
using Microsoft.AspNetCore.Mvc;

namespace GolMundial.FrontendPublico.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPartidoService _partidoService;
        private readonly IPosicionService _posicionService;

        public HomeController(IPartidoService partidoService, IPosicionService posicionService)
        {
            _partidoService = partidoService;
            _posicionService = posicionService;
        }

        // Calendario del torneo (página de inicio)
        public async Task<IActionResult> Index()
        {
            var partidos = await _partidoService.ObtenerTodosAsync();
            return View(partidos);
        }

        public async Task<IActionResult> Posiciones()
        {
            var posiciones = await _posicionService.ObtenerTodasAsync();
            return View(posiciones);
        }

        public IActionResult Estadisticas()
        {
            var estadisticas = new List<EstadisticaSeleccion>
            {
                new EstadisticaSeleccion{
                    SeleccionId = 1, CodigoFifa = "ARG", Nombre = "Argentina",
                    Confederacion = "CONMEBOL", EsAnfitrion = false,
                    PartidosJugados = 3, Ganados = 2, Empatados = 1, Perdidos = 0,
                    GolesFavor = 5, GolesContra = 1
                },
                new EstadisticaSeleccion{
                    SeleccionId = 3, CodigoFifa = "BRA", Nombre = "Brasil",
                    Confederacion = "CONMEBOL", EsAnfitrion = false,
                    PartidosJugados = 3, Ganados = 1, Empatados = 2, Perdidos = 0,
                    GolesFavor = 4, GolesContra = 2
                }
            };

            return View(estadisticas);
        }

        public IActionResult Ranking()
        {
            return View();
        }
    }
}