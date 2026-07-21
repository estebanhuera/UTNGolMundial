using System.Net.Http.Json;
using GolMundial.FrontendPublico.Models;

namespace GolMundial.FrontendPublico.Services
{
    public class ApiEstadisticaService : IEstadisticaService
    {
        private readonly HttpClient _http;

        public ApiEstadisticaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<EstadisticaSeleccion>> ObtenerTodasAsync()
        {
            var dtos = await _http.GetFromJsonAsync<List<EstadisticaSeleccionDto>>(
                "api/estadisticas/selecciones");

            if (dtos is null)
                return new List<EstadisticaSeleccion>();

            return dtos.Select(d => new EstadisticaSeleccion
            {
                SeleccionId = d.SeleccionId,
                CodigoFifa = d.CodigoFifa,
                Nombre = d.Nombre,
                Confederacion = d.Confederacion,
                EsAnfitrion = false,
                PartidosJugados = d.PartidosJugados,
                Ganados = d.Ganados,
                Empatados = d.Empatados,
                Perdidos = d.Perdidos,
                GolesFavor = d.GolesAFavor,
                GolesContra = d.GolesEnContra
            }).ToList();
        }
    }
}