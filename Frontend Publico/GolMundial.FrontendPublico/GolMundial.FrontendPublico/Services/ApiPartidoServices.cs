using System.Net.Http.Json;
using GolMundial.FrontendPublico.Models;

namespace GolMundial.FrontendPublico.Services
{
    public class ApiPartidoService : IPartidoService
    {
        private readonly HttpClient _http;

        public ApiPartidoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Partido>> ObtenerTodosAsync()
        {
            var dtos = await _http.GetFromJsonAsync<List<CalendarioPartidoDto>>(
                "api/partidos/calendario");

            if (dtos is null)
                return new List<Partido>();

            return dtos.Select(APartido).ToList();
        }

        public async Task<Partido?> ObtenerPorIdAsync(int id)
        {
            var partidos = await ObtenerTodosAsync();
            return partidos.FirstOrDefault(p => p.Id == id);
        }

        private static Partido APartido(CalendarioPartidoDto d) => new Partido
        {
            Id = d.PartidoId,
            NumeroPartidoFifa = d.NumeroPartidoFifa,
            EquipoLocal = d.LocalNombre,
            EquipoVisitante = d.VisitanteNombre,
            CodigoFifaLocal = d.LocalCodigoFifa,
            CodigoFifaVisitante = d.VisitanteCodigoFifa,
            FechaHora = d.FechaPartido,
            Sede = d.SedeNombre,
            GrupoCodigo = d.GrupoCodigo ?? "",
            Fase = d.FaseNombre,
            Estado = d.Estado,
            GolesLocal = d.GolesLocal,
            GolesVisitante = d.GolesVisitante
        };
    }
}