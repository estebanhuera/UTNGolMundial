using System.Net.Http.Json;
using GolMundial.FrontendPublico.Models;

namespace GolMundial.FrontendPublico.Services
{
    public class ApiPrediccionService : IPrediccionService
    {
        private const short RolIdUsuario = 2;

        private readonly HttpClient _http;
        private readonly IPartidoService _partidoService;
        private readonly IUsuarioService _usuarioService;

        public ApiPrediccionService(
            HttpClient http,
            IPartidoService partidoService,
            IUsuarioService usuarioService)
        {
            _http = http;
            _partidoService = partidoService;
            _usuarioService = usuarioService;
        }

        public async Task<ResultadoOperacion> RegistrarUsuarioAsync(Usuario usuario, string email)
        {
            try
            {
                var dto = new UsuarioRegistroDto
                {
                    Id = usuario.Id,
                    Username = usuario.Username,
                    Nombre = usuario.Nombre,
                    Email = email,
                    RolId = RolIdUsuario
                };

                var respuesta = await _http.PostAsJsonAsync("usuarios/registrar", dto);
                var mensaje = await LeerMensajeAsync(respuesta);

                return respuesta.IsSuccessStatusCode && mensaje?.Exitoso != false
                    ? ResultadoOperacion.Ok()
                    : ResultadoOperacion.Falla(mensaje?.Mensaje ?? "No se pudo crear la billetera.");
            }
            catch (HttpRequestException)
            {
                return ResultadoOperacion.Falla("El servicio de UTNGolCoin no está disponible.");
            }
        }

        public async Task<int> ObtenerSaldoAsync(int usuarioId)
        {
            try
            {
                var billetera = await _http.GetFromJsonAsync<BilleteraResponseDto>(
                    $"billeteras/usuario/{usuarioId}");

                return billetera is null ? 0 : (int)Math.Round(billetera.Saldo);
            }
            catch (HttpRequestException)
            {
                return 0;
            }
        }

        public async Task<List<MisPredicciones>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            var partidos = await _partidoService.ObtenerTodosAsync();
            return await ObtenerPorUsuarioAsync(usuarioId, partidos);
        }

        private async Task<List<MisPredicciones>> ObtenerPorUsuarioAsync(int usuarioId, List<Partido> partidos)
        {
            var crudas = await ObtenerCrudasAsync(usuarioId);

            return crudas
                .Select(p => ADto(p, partidos.FirstOrDefault(x => x.Id == p.PartidoId)))
                .OrderByDescending(p => p.FechaPartido)
                .ToList();
        }

        public async Task<MisPredicciones?> ObtenerDelPartidoAsync(int usuarioId, int partidoId)
        {
            var mias = await ObtenerPorUsuarioAsync(usuarioId);
            return mias.FirstOrDefault(p => p.PartidoId == partidoId);
        }

        public async Task<ResultadoOperacion> CrearAsync(int usuarioId, PrediccionInput input)
        {
            var partido = await _partidoService.ObtenerPorIdAsync(input.PartidoId);

            if (partido is null)
                return ResultadoOperacion.Falla("El partido no existe.");

            if (partido.Estado != "PROGRAMADO")
                return ResultadoOperacion.Falla("Este partido ya no admite predicciones.");

            if (await ObtenerDelPartidoAsync(usuarioId, input.PartidoId) is not null)
                return ResultadoOperacion.Falla("Ya hiciste una predicción para este partido.");

            var saldo = await ObtenerSaldoAsync(usuarioId);

            if (input.Monto > saldo)
                return ResultadoOperacion.Falla($"No te alcanza el saldo. Tienes {saldo} UTNGolCoin.");

            try
            {
                var dto = new PrediccionRequestDto
                {
                    UsuarioId = usuarioId,
                    PartidoId = input.PartidoId,
                    TipoResultado = ValoresApuesta.ADominio(input.ResultadoPredicho),
                    Monto = input.Monto
                };

                var respuesta = await _http.PostAsJsonAsync("predicciones/registrar", dto);
                var mensaje = await LeerMensajeAsync(respuesta);

                return respuesta.IsSuccessStatusCode && mensaje?.Exitoso != false
                    ? ResultadoOperacion.Ok()
                    : ResultadoOperacion.Falla(mensaje?.Mensaje ?? "No se pudo registrar la predicción.");
            }
            catch (HttpRequestException)
            {
                return ResultadoOperacion.Falla("El servicio de UTNGolCoin no está disponible.");
            }
        }

        public async Task<List<RankingItem>> ObtenerRankingAsync()
        {
            var usuarios = (await _usuarioService.ObtenerTodosAsync())
                .Where(u => Roles.EsPublico(u.RolNombre))
                .ToList();

            var billeteras = await ObtenerBilleterasAsync();
            var partidos = await _partidoService.ObtenerTodosAsync();

            var tareas = usuarios.Select(async u =>
            {
                var suyas = await ObtenerPorUsuarioAsync(u.Id, partidos);
                var billetera = billeteras.FirstOrDefault(b => b.UsuarioId == u.Id);

                return new RankingItem
                {
                    UsuarioId = u.Id,
                    Username = u.Username,
                    Nombre = u.Nombre,
                    Saldo = billetera is null ? 0 : (int)Math.Round(billetera.Saldo),
                    Apuestas = suyas.Count,
                    Aciertos = suyas.Count(p => p.Estado == "Ganada")
                };
            });

            var lista = (await Task.WhenAll(tareas))
                .OrderByDescending(r => r.Saldo)
                .ThenByDescending(r => r.Aciertos)
                .ThenBy(r => r.Username)
                .ToList();

            for (var i = 0; i < lista.Count; i++)
                lista[i].Posicion = i + 1;

            return lista;
        }


        private async Task<List<PrediccionResponseDto>> ObtenerCrudasAsync(int usuarioId)
        {
            try
            {
                return await _http.GetFromJsonAsync<List<PrediccionResponseDto>>(
                    $"predicciones/usuario/{usuarioId}") ?? new List<PrediccionResponseDto>();
            }
            catch (HttpRequestException)
            {
                return new List<PrediccionResponseDto>();
            }
        }

        private static async Task<MensajeResponseDto?> LeerMensajeAsync(HttpResponseMessage respuesta)
        {
            try
            {
                return await respuesta.Content.ReadFromJsonAsync<MensajeResponseDto>();
            }
            catch
            {
                return null;
            }
        }

        private static MisPredicciones ADto(PrediccionResponseDto p, Partido? partido)
        {
            var estado = ValoresApuesta.AEstadoUi(p.Estado);
            var monto = (int)Math.Round(p.Monto);

            var ganancia = estado switch
            {
                "Ganada" => (int)Math.Round(p.Monto * p.CuotaAplicada) - monto,
                "Perdida" => -monto,
                _ => 0
            };

            return new MisPredicciones
            {
                Id = p.Id,
                PartidoId = p.PartidoId,
                PartidoDescripcion = partido is null
                    ? $"Partido #{p.PartidoId}"
                    : $"{partido.EquipoLocal} vs {partido.EquipoVisitante}",
                FechaPartido = partido?.FechaHora ?? DateTime.MinValue,
                ResultadoPredicho = ValoresApuesta.AOpcionUi(p.TipoResultado),
                Monto = monto,
                Estado = estado,
                Ganancia = ganancia
            };
        }
        private async Task<List<BilleteraResponseDto>> ObtenerBilleterasAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<BilleteraResponseDto>>("billeteras")
                       ?? new List<BilleteraResponseDto>();
            }
            catch (HttpRequestException)
            {
                return new List<BilleteraResponseDto>();
            }
        }
    }
}