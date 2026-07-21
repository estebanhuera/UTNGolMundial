using GolMundial.FrontendPublico.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GolMundial.FrontendPublico.Controllers
{
    [Authorize]
    public class PrediccionesController : Controller
    {
        private readonly IPartidoService _partidoService;

        public PrediccionesController(IPartidoService partidoService)
        {
            _partidoService = partidoService;
        }

        [HttpGet]
        public async Task<IActionResult> Nueva(int partidoId)
        {
            var partido = await _partidoService.ObtenerPorIdAsync(partidoId);

            if (partido is null)
                return NotFound();

            if (partido.Estado != "PROGRAMADO")
                return RedirectToAction("Index", "Home");

            return View(partido);
        }
    }
}