using GolMundial.FrontendPublico.Models;
using GolMundial.FrontendPublico.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GolMundial.FrontendPublico.Controllers
{
    [Authorize(Roles = "USUARIO")]
    public class PrediccionesController : Controller
    {
        private readonly IPartidoService _partidoService;
        private readonly IPrediccionService _prediccionService;

        public PrediccionesController(
            IPartidoService partidoService, 
            IPrediccionService prediccionService)
        {
            _partidoService = partidoService;
            _prediccionService = prediccionService;
        }

        private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> Nueva(int partidoId)
        {
            var partido = await _partidoService.ObtenerPorIdAsync(partidoId);

            if (partido is null)
                return NotFound();

            if (partido.Estado != "PROGRAMADO")
                return RedirectToAction("Index", "Home");

            if (await _prediccionService.ObtenerDelPartidoAsync(UsuarioId, partidoId) is not null)
            {
                TempData["Aviso"] = "Ya hiciste una predicción para ese partido.";
                return RedirectToAction(nameof(Mias));
            }

            return View(new PrediccionFormViewModel
            {
                Partido = partido,
                Saldo = await _prediccionService.ObtenerSaldoAsync(UsuarioId),
                Input = new PrediccionInput { PartidoId = partidoId }
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Nueva(PrediccionFormViewModel vm)
        {
            var partido = await _partidoService.ObtenerPorIdAsync(vm.Input.PartidoId);

            if (partido is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                var resultado = await _prediccionService.CrearAsync(UsuarioId, vm.Input);

                if (resultado.Exito)
                {
                    TempData["Aviso"] = "¡Predicción registrada!";
                    return RedirectToAction(nameof(Mias));
                }

                ModelState.AddModelError(string.Empty, resultado.Error!);
            }

            vm.Partido = partido;
            vm.Saldo = await _prediccionService.ObtenerSaldoAsync(UsuarioId);
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Mias()
        {
            ViewData["Saldo"] = await _prediccionService.ObtenerSaldoAsync(UsuarioId);
            return View(await _prediccionService.ObtenerPorUsuarioAsync(UsuarioId));
        }
    }
}