using GolMundial.FrontendPublico.Models;
using GolMundial.FrontendPublico.Services;
using Microsoft.AspNetCore.Mvc;

namespace GolMundial.FrontendPublico.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPartidoService _partidoService;

        public HomeController(IPartidoService partidoService)
        {
            _partidoService = partidoService;
        }

        // Calendario del torneo (página de inicio)
        public async Task<IActionResult> Index()
        {
            var partidos = await _partidoService.ObtenerTodosAsync();
            return View(partidos);
        }

        public IActionResult Posiciones()
        {
            var posiciones = new List<Posicion>
            {
                new Posicion {
                    GrupoCodigo = "A", SeleccionId = 1,
                    CodigoFifa = "ARG", Nombre = "Argentina",
                    Pj = 3, Pg = 2, Pe = 1, Pp = 0,
                    Gf = 5, Gc = 1, Dif = 4, Puntos = 7
                },
                new Posicion {
                    GrupoCodigo = "A", SeleccionId = 2,
                    CodigoFifa = "MEX", Nombre = "México",
                    Pj = 3, Pg = 1, Pe = 1, Pp = 1,
                    Gf = 3, Gc = 3, Dif = 0, Puntos = 4
                }
            };

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