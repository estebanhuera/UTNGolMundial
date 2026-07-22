using GolMundial.FrontendPublico.Models;
using GolMundial.FrontendPublico.Services;
using Microsoft.AspNetCore.Mvc;

namespace GolMundial.FrontendPublico.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPartidoService _partidoService;
        private readonly IPosicionService _posicionService;
        private readonly IEstadisticaService _estadisticaService;
        private readonly IPrediccionService _prediccionService;
        public HomeController(
            IPartidoService partidoService,
            IPosicionService posicionService,
            IEstadisticaService estadisticaService,
            IPrediccionService prediccionService)
        {
            _partidoService = partidoService;
            _posicionService = posicionService;
            _estadisticaService = estadisticaService;
            _prediccionService = prediccionService;
        }

        // Calendario del torneo (página de inicio)
        public async Task<IActionResult> Index()
        {
            var partidos = await _partidoService.ObtenerTodosAsync();

            var faseGrupos = partidos
                .Where(p => p.FaseCodigo == "GRUPOS")
                .OrderBy(p => p.FechaHora)
                .ToList();

            return View(faseGrupos);
        }
        public async Task<IActionResult> Eliminatorias()
        {
            var partidos = await _partidoService.ObtenerTodosAsync();

            var eliminatorias = partidos
                .Where(p => p.FaseCodigo != "GRUPOS")
                .OrderBy(p => p.NumeroPartidoFifa)
                .ToList();

            return View(eliminatorias);
        }

        public async Task<IActionResult> Posiciones()
        {
            var posiciones = await _posicionService.ObtenerTodasAsync();
            return View(posiciones);
        }

        public async Task<IActionResult> Estadisticas()
        {
            var estadisticas = await _estadisticaService.ObtenerTodasAsync();
            return View(estadisticas);
        }

        public async Task<IActionResult> Ranking()
        {
            return View(await _prediccionService.ObtenerRankingAsync());
        }
    }
}