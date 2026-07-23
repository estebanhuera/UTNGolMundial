namespace GolMundial.FrontendPublico.Models
{
    public static class EstadoPartido
    {
        public const string Programado = "PROGRAMADO";
        public const string EnJuego = "EN_JUEGO";
        public const string Finalizado = "FINALIZADO";
        public const string Suspendido = "SUSPENDIDO";
        public const string Cancelado = "CANCELADO";

        private static string Normalizar(string? estado) =>
            (estado ?? "").Trim().ToUpperInvariant();

        public static bool SePuedePredecir(string? estado) =>
            Normalizar(estado) == Programado;

        public static bool EstaFinalizado(string? estado) =>
            Normalizar(estado) == Finalizado;

        public static string Etiqueta(string? estado) => Normalizar(estado) switch
        {
            Programado => "Programado",
            EnJuego => "En juego",
            Finalizado => "Finalizado",
            Suspendido => "Suspendido",
            Cancelado => "Cancelado",
            "" => "Sin estado",
            _ => Normalizar(estado)
        };

        public static string Badge(string? estado) => Normalizar(estado) switch
        {
            Programado => "bg-success",
            EnJuego => "bg-danger",
            Finalizado => "bg-secondary",
            Suspendido => "bg-warning text-dark",
            Cancelado => "bg-dark",
            _ => "bg-light text-dark"
        };
    }
}