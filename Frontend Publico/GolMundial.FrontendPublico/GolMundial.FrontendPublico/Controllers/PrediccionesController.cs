using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GolMundial.FrontendPublico.Controllers
{
    [Authorize]
    public class PrediccionesController : Controller
    {
        [HttpGet]
        public IActionResult Nueva(int partidoId)
        {
            ViewData["PartidoId"] = partidoId;
            return View();
        }
    }
}
