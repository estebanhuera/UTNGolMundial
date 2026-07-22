using GolMundial.FrontendPublico.Models;

namespace GolMundial.FrontendPublico.Services
{
    public class FakeUsuarioServices : IUsuarioService
    {
        private readonly List<UsuarioFake> _usuarios = new()
        {
            new UsuarioFake { Id = 1, Username = "messi10", Nombre = "Lionel Messi",
                              RolNombre = "usuario", Email = "messi@utn.com",  Password = "123456" },
            new UsuarioFake { Id = 2, Username = "admin",   Nombre = "Administrador",
                              RolNombre = "admin",   Email = "admin@utn.com",  Password = "admin123" },
            new UsuarioFake { Id = 3, Username = "juanpe",  Nombre = "Juan Pérez",
                              RolNombre = "usuario", Email = "juan@utn.com",   Password = "123456" }
        };

        public Task<Usuario?> ValidarCredencialesAsync(string usuarioOEmail, string password)
        {
            var encontrado = _usuarios.FirstOrDefault(u =>
                (string.Equals(u.Username, usuarioOEmail, StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(u.Email, usuarioOEmail, StringComparison.OrdinalIgnoreCase)) &&
                u.Password == password);

            return Task.FromResult<Usuario?>(encontrado is null ? null : AUsuario(encontrado));
        }
        public Task<List<Usuario>> ObtenerTodosAsync()
        {
            return Task.FromResult(_usuarios.Select(AUsuario).ToList());
        }
        public Task<bool> ExisteEmailAsync(string email)
        {
            return Task.FromResult(_usuarios.Any(u =>
                string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase)));
        }

        public Task<bool> ExisteUsernameAsync(string username)
        {
            return Task.FromResult(_usuarios.Any(u =>
                string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase)));
        }

        public Task<Usuario?> RegistrarAsync(RegistroInput input)
        {
            var nuevo = new UsuarioFake
            {
                Id = _usuarios.Count == 0 ? 1 : _usuarios.Max(u => u.Id) + 1,
                Username = input.Username,
                Nombre = input.Nombre,
                Email = input.Email,
                Password = input.Password,
                RolNombre = "usuario"
            };

            _usuarios.Add(nuevo);
            return Task.FromResult<Usuario?>(AUsuario(nuevo));
        }

        private static Usuario AUsuario(UsuarioFake f) => new Usuario
        {
            Id = f.Id,
            Username = f.Username,
            Nombre = f.Nombre,
            RolNombre = f.RolNombre
        };
    }
}