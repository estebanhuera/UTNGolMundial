namespace GolMundial.FrontendPublico.Services
{
    public static class ValoresApuesta
    {
        public const string Local = "LOCAL";
        public const string Empate = "EMPATE";
        public const string Visitante = "VISITANTE";

        public const string Pendiente = "PENDIENTE";
        public const string Ganada = "GANADA";
        public const string Perdida = "PERDIDA";
        public static string ADominio(string opcionUi) => opcionUi.ToUpperInvariant() switch
        {
            "LOCAL" => Local,
            "EMPATE" => Empate,
            "VISITANTE" => Visitante,
            _ => opcionUi.ToUpperInvariant()
        };

        public static string AEstadoUi(string estadoApi) => estadoApi.ToUpperInvariant() switch
        {
            Ganada => "Ganada",
            Perdida => "Perdida",
            _ => "Pendiente"
        };

        public static string AOpcionUi(string tipoApi) => tipoApi.ToUpperInvariant() switch
        {
            Local => "Local",
            Empate => "Empate",
            Visitante => "Visitante",
            _ => tipoApi
        };
    }
}