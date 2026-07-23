using System.Security.Claims;
using GolMundial.FrontendPublico.Models;
using GolMundial.FrontendPublico.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace GolMundial.FrontendPublico.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public AccountController(
            IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            return View(new LoginInput { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInput input)
        {
            if (!ModelState.IsValid)
                return View(input);

            var usuario = await _usuarioService.ValidarCredencialesAsync(input.UsuarioOEmail, input.Password);

            if (usuario is null)
            {
                ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos");
                return View(input);
            }

            if (!Roles.EsPublico(usuario.RolNombre))
            {
                ModelState.AddModelError(string.Empty, "Esta cuenta no tiene acceso al sitio público.");
                return View(input);
            }

            await IniciarSesion(usuario);
            return RedirigirA(input.ReturnUrl);
        }

        [HttpGet]
        public IActionResult Registro(string? returnUrl = null)
        {
            return View(new RegistroInput { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroInput input)
        {
            if (!ModelState.IsValid)
                return View(input);

            if (await _usuarioService.ExisteUsernameAsync(input.Username))
            {
                ModelState.AddModelError(nameof(input.Username), "Ese nombre de usuario ya está en uso");
                return View(input);
            }

            if (await _usuarioService.ExisteEmailAsync(input.Email))
            {
                ModelState.AddModelError(nameof(input.Email), "Ya existe una cuenta con ese email");
                return View(input);
            }

            var usuario = await _usuarioService.RegistrarAsync(input);

            if (usuario is null)
            {
                ModelState.AddModelError(string.Empty, "No se pudo crear la cuenta. Intenta de nuevo.");
                return View(input);
            }

            await IniciarSesion(usuario);
            return RedirigirA(input.ReturnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task IniciarSesion(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Role, usuario.RolNombre.ToUpperInvariant()),
                new Claim("NombreCompleto", usuario.Nombre)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        private IActionResult RedirigirA(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }
    }
}