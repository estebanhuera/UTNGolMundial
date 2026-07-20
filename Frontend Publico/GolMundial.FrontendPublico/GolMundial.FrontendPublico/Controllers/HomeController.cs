using GolMundial.FrontendPublico.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GolMundial.FrontendPublico.Controllers
{

    public class HomeController : Controller
    {
        // Calendario del torneo (página de inicio)
        public IActionResult Index()
        {
            var partidos = new List<Partido>
            {
                new Partido {
            Id = 1, NumeroPartidoFifa = 1,
            EquipoLocal = "Argentina", EquipoVisitante = "Brasil",
            FechaHora = DateTime.UtcNow.AddDays(2),
            Sede = "Estadio Azteca", GrupoCodigo = "A", Fase = "GRUPOS",
            Estado = "PROGRAMADO"
        },
        new Partido {
            Id = 2, NumeroPartidoFifa = 2,
            EquipoLocal = "España", EquipoVisitante = "Francia",
            FechaHora = DateTime.UtcNow.AddDays(3),
            Sede = "MetLife Stadium", GrupoCodigo = "B", Fase = "GRUPOS",
            Estado = "PROGRAMADO"
        },
        new Partido {
            Id = 3, NumeroPartidoFifa = 3,
            EquipoLocal = "Alemania", EquipoVisitante = "Portugal",
            FechaHora = DateTime.UtcNow.AddDays(-1),
            Sede = "Estadio BC Place", GrupoCodigo = "C", Fase = "GRUPOS",
            Estado = "FINALIZADO",
            GolesLocal = 2, GolesVisitante = 1
        }
    };

            return View(partidos);
        }

        public IActionResult Posiciones()
        {
            var posiciones = new List<Posicion>
            {
                new Posicion {
                    GrupoCodigo = "A", SeleccionId = 1,
                    CodigoFifa = "ARG", Nombre = "Argentina",
                    Pj = 3, Pg = 2, Pe = 1, Pp = 0,
                    Gf = 5, Gc = 1, Dif = 4, Puntos = 7
                },
                new Posicion {
                    GrupoCodigo = "A", SeleccionId = 2,
                    CodigoFifa = "MEX", Nombre = "México",
                    Pj = 3, Pg = 1, Pe = 1, Pp = 1,
                    Gf = 3, Gc = 3, Dif = 0, Puntos = 4
                }
            };

            return View(posiciones);
        }

        public IActionResult Estadisticas()
        {
            var estadisticas = new List<EstadisticaSeleccion>
            {
                new EstadisticaSeleccion{
                    SeleccionId = 1, CodigoFifa = "ARG", Nombre = "Argentina",
                    Confederacion = "CONMEBOL", EsAnfitrion = false,
                    PartidosJugados = 3, Ganados = 2, Empatados = 1, Perdidos = 0,
                    GolesFavor = 5, GolesContra = 1
                },
                new EstadisticaSeleccion{
                    SeleccionId = 3, CodigoFifa = "BRA", Nombre = "Brasil",
                    Confederacion = "CONMEBOL", EsAnfitrion = false,
                    PartidosJugados = 3, Ganados = 1, Empatados = 2, Perdidos = 0,
                    GolesFavor = 4, GolesContra = 2
                }
                    };

            return View(estadisticas);
        }

        public IActionResult Ranking()
        {
            return View();
        }
    }
}
