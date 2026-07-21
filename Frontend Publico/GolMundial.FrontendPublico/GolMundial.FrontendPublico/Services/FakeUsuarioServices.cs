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

        public Usuario? ValidarCredenciales(string email, string password)
        {
            var encontrado = _usuarios.FirstOrDefault(u =>
                string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);

            return encontrado is null ? null : AUsuario(encontrado);
        }

        public bool ExisteEmail(string email)
        {
            return _usuarios.Any(u =>
                string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));
        }

        public Usuario Registrar(RegistroInput input)
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
            return AUsuario(nuevo);
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
