using GolMundial.FrontendPublico.Models;

namespace GolMundial.FrontendPublico.Services
{
    public class FakePrediccionService : IPrediccionService
    {
        private const int BonoBienvenida = 10;

        // Cada apuesta guardada: quién, a qué partido, qué eligió y cuánto puso.
        private record Apuesta(int Id, int UsuarioId, int PartidoId, string Opcion, int Monto);

        // static para que las apuestas sobrevivan entre peticiones.
        private static readonly List<Apuesta> _apuestas = new();
        private static int _siguienteId = 1;

        private readonly IPartidoService _partidoService;
        private readonly IUsuarioService _usuarioService;

        public FakePrediccionService(IPartidoService partidoService, IUsuarioService usuarioService)
        {
            _partidoService = partidoService;
            _usuarioService = usuarioService;
        }

        public async Task<MisPredicciones?> ObtenerDelPartidoAsync(int usuarioId, int partidoId)
        {
            var mias = await ObtenerPorUsuarioAsync(usuarioId);
            return mias.FirstOrDefault(p => p.PartidoId == partidoId);
        }

        public async Task<List<MisPredicciones>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            var partidos = await _partidoService.ObtenerTodosAsync();

            return _apuestas
                .Where(a => a.UsuarioId == usuarioId)
                .Select(a =>
                {
                    var partido = partidos.FirstOrDefault(p => p.Id == a.PartidoId);
                    return ADto(a, partido);
                })
                .OrderByDescending(p => p.FechaPartido)
                .ToList();
        }

        public async Task<ResultadoOperacion> CrearAsync(int usuarioId, PrediccionInput input)
        {
            var partido = await _partidoService.ObtenerPorIdAsync(input.PartidoId);

            if (partido is null)
                return ResultadoOperacion.Falla("El partido no existe.");

            if (partido.Estado != "PROGRAMADO")
                return ResultadoOperacion.Falla("Este partido ya no admite predicciones.");

            if (_apuestas.Any(a => a.UsuarioId == usuarioId && a.PartidoId == input.PartidoId))
                return ResultadoOperacion.Falla("Ya hiciste una predicción para este partido.");

            var saldo = await ObtenerSaldoAsync(usuarioId);

            if (input.Monto > saldo)
                return ResultadoOperacion.Falla($"No te alcanza el saldo. Tienes {saldo} UTNGolCoin.");

            _apuestas.Add(new Apuesta(_siguienteId++, usuarioId, input.PartidoId,
                                      input.ResultadoPredicho, input.Monto));

            return ResultadoOperacion.Ok();
        }

        private static MisPredicciones ADto(Apuesta a, Partido? partido)
        {
            var dto = new MisPredicciones
            {
                Id = a.Id,
                PartidoId = a.PartidoId,
                ResultadoPredicho = a.Opcion,
                Monto = a.Monto,
                PartidoDescripcion = partido is null
                    ? $"Partido #{a.PartidoId}"
                    : $"{partido.EquipoLocal} vs {partido.EquipoVisitante}",
                FechaPartido = partido?.FechaHora ?? DateTime.MinValue,
                Estado = "Pendiente",
                Ganancia = 0
            };

            if (partido is null || partido.Estado != "FINALIZADO")
                return dto;

            var real = partido.GolesLocal > partido.GolesVisitante ? "Local"
                     : partido.GolesLocal < partido.GolesVisitante ? "Visitante"
                     : "Empate";

            var acerto = string.Equals(a.Opcion, real, StringComparison.OrdinalIgnoreCase);

            dto.Estado = acerto ? "Ganada" : "Perdida";
            dto.Ganancia = acerto ? a.Monto : -a.Monto;

            return dto;
        }
        private static int CalcularSaldo(List<MisPredicciones> predicciones)
        {
            var apostado = predicciones.Sum(p => p.Monto);
            var devuelto = predicciones.Where(p => p.Estado == "Ganada").Sum(p => p.Monto * 2);

            return BonoBienvenida - apostado + devuelto;
        }
        public Task<ResultadoOperacion> RegistrarUsuarioAsync(Usuario usuario, string email)
        {
            return Task.FromResult(ResultadoOperacion.Ok());
        }
        public async Task<int> ObtenerSaldoAsync(int usuarioId)
        {
            return CalcularSaldo(await ObtenerPorUsuarioAsync(usuarioId));
        }
        public async Task<List<RankingItem>> ObtenerRankingAsync()
        {
            var usuarios = (await _usuarioService.ObtenerTodosAsync())
                .Where(u => Roles.EsPublico(u.RolNombre))
                .ToList();
            var partidos = await _partidoService.ObtenerTodosAsync();

            var lista = usuarios
                .Select(u =>
                {
                    var suyas = _apuestas
                        .Where(a => a.UsuarioId == u.Id)
                        .Select(a => ADto(a, partidos.FirstOrDefault(p => p.Id == a.PartidoId)))
                        .ToList();

                    return new RankingItem
                    {
                        UsuarioId = u.Id,
                        Username = u.Username,
                        Nombre = u.Nombre,
                        Saldo = CalcularSaldo(suyas),
                        Apuestas = suyas.Count,
                        Aciertos = suyas.Count(p => p.Estado == "Ganada")
                    };
                })
                .OrderByDescending(r => r.Saldo)
                .ThenByDescending(r => r.Aciertos)
                .ThenBy(r => r.Username)
                .ToList();

            for (var i = 0; i < lista.Count; i++)
                lista[i].Posicion = i + 1;

            return lista;
        }
    }
}
