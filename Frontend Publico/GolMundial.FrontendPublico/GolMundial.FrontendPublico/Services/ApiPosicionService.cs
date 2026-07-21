using System.Net.Http.Json;
using GolMundial.FrontendPublico.Models;

namespace GolMundial.FrontendPublico.Services
{
    public class ApiPosicionService : IPosicionService
    {
        private readonly HttpClient _http;

        public ApiPosicionService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Posicion>> ObtenerTodasAsync()
        {
            var grupos = await _http.GetFromJsonAsync<List<TablaPosicionGrupoDto>>(
                "api/estadisticas/posiciones");

            if (grupos is null)
                return new List<Posicion>();

            return grupos
                .SelectMany(g => g.Posiciones.Select(p => new Posicion
                {
                    GrupoCodigo = g.GrupoCodigo ?? "",
                    SeleccionId = p.SeleccionId,
                    CodigoFifa = p.CodigoFifa,
                    Nombre = p.Nombre,
                    Pj = p.PJ,
                    Pg = p.PG,
                    Pe = p.PE,
                    Pp = p.PP,
                    Gf = p.GF,
                    Gc = p.GC,
                    Dif = p.DG,
                    Puntos = p.Pts
                }))
                .ToList();
        }
    }
}