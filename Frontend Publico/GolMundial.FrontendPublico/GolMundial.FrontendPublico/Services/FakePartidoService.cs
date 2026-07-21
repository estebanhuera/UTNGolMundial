using GolMundial.FrontendPublico.Models;

namespace GolMundial.FrontendPublico.Services
{
    public class FakePartidoService : IPartidoService
    {
        private readonly List<Partido> _partidos = new()
        {
            new Partido {
                Id = 1, NumeroPartidoFifa = 1,
                EquipoLocal = "Argentina", EquipoVisitante = "Brasil",
                CodigoFifaLocal = "ARG", CodigoFifaVisitante = "BRA",
                FechaHora = DateTime.UtcNow.AddDays(2),
                Sede = "Estadio Azteca", GrupoCodigo = "A", Fase = "GRUPOS",
                Estado = "PROGRAMADO"
            },
            new Partido {
                Id = 2, NumeroPartidoFifa = 2,
                EquipoLocal = "España", EquipoVisitante = "Francia",
                CodigoFifaLocal = "ESP", CodigoFifaVisitante = "FRA",
                FechaHora = DateTime.UtcNow.AddDays(3),
                Sede = "MetLife Stadium", GrupoCodigo = "B", Fase = "GRUPOS",
                Estado = "PROGRAMADO"
            },
            new Partido {
                Id = 3, NumeroPartidoFifa = 3,
                EquipoLocal = "Alemania", EquipoVisitante = "Portugal",
                CodigoFifaLocal = "GER", CodigoFifaVisitante = "POR",
                FechaHora = DateTime.UtcNow.AddDays(-1),
                Sede = "Estadio BC Place", GrupoCodigo = "C", Fase = "GRUPOS",
                Estado = "FINALIZADO",
                GolesLocal = 2, GolesVisitante = 1
            }
        };

        public Task<List<Partido>> ObtenerTodosAsync() => Task.FromResult(_partidos);

        public Task<Partido?> ObtenerPorIdAsync(int id) =>
            Task.FromResult(_partidos.FirstOrDefault(p => p.Id == id));
    }
}
