using System.Net.Http.Json;
using GolMundial.FrontendPublico.Models;

namespace GolMundial.FrontendPublico.Services
{
    public class ApiUsuarioService : IUsuarioService
    {
        private readonly HttpClient _http;

        public ApiUsuarioService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Usuario?> ValidarCredencialesAsync(string usuarioOEmail, string password)
        {
            try
            {
                var respuesta = await _http.PostAsJsonAsync("api/auth/login",
                    new LoginApiDto { Username = usuarioOEmail, Password = password });

                if (!respuesta.IsSuccessStatusCode)
                    return null;

                var dto = await respuesta.Content.ReadFromJsonAsync<UsuarioApiDto>();
                return dto is null ? null : AUsuario(dto);
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<bool> ExisteEmailAsync(string email)
        {
            var usuarios = await ObtenerTodosAsync();
            return usuarios.Any(u => string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> ExisteUsernameAsync(string username)
        {
            var usuarios = await ObtenerTodosAsync();
            return usuarios.Any(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Usuario?> RegistrarAsync(RegistroInput input)
        {
            try
            {
                var nuevo = new NuevoUsuarioApiDto
                {
                    Username = input.Username,
                    Nombre = input.Nombre,
                    Mail = input.Email,
                    Password = input.Password,
                    FechaRegistro = DateTime.UtcNow,
                    Activo = true,
                    RolId = 2
                };

                var respuesta = await _http.PostAsJsonAsync("api/usuarios", nuevo);

                if (!respuesta.IsSuccessStatusCode)
                    return null;

                // Ya creado, nos logueamos para recibirlo con su rol resuelto.
                return await ValidarCredencialesAsync(input.Username, input.Password);
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        private async Task<List<UsuarioApiDto>> ObtenerTodosAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<UsuarioApiDto>>("api/usuarios")
                       ?? new List<UsuarioApiDto>();
            }
            catch (HttpRequestException)
            {
                return new List<UsuarioApiDto>();
            }
        }

        private static Usuario AUsuario(UsuarioApiDto d) => new Usuario
        {
            Id = d.Id,
            Username = d.Username,
            Nombre = d.Nombre,
            RolNombre = d.RolNombre
        };
    }
}