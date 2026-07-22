namespace GolMundial.FrontendPublico.Services
{
    public class ResultadoOperacion
    {
        public bool Exito { get; set; }
        public string? Error { get; set; }

        public static ResultadoOperacion Ok() => new() { Exito = true };

        public static ResultadoOperacion Falla(string error) =>
            new() { Exito = false, Error = error };
    }
}
