namespace GolMundial.FrontendPublico.Models
{
    public static class Banderas
    {
        private static readonly Dictionary<string, string> _iso = new()
        {
            ["MEX"] = "mx",
            ["KOR"] = "kr",
            ["RSA"] = "za",
            ["CZE"] = "cz",
            ["CAN"] = "ca",
            ["BIH"] = "ba",
            ["QAT"] = "qa",
            ["SUI"] = "ch",
            ["BRA"] = "br",
            ["MAR"] = "ma",
            ["HAI"] = "ht",
            ["SCO"] = "gb-sct",
            ["USA"] = "us",
            ["AUS"] = "au",
            ["PAR"] = "py",
            ["TUR"] = "tr",
            ["GER"] = "de",
            ["ECU"] = "ec",
            ["CIV"] = "ci",
            ["CUW"] = "cw",
            ["NED"] = "nl",
            ["JPN"] = "jp",
            ["TUN"] = "tn",
            ["SWE"] = "se",
            ["BEL"] = "be",
            ["IRN"] = "ir",
            ["EGY"] = "eg",
            ["NZL"] = "nz",
            ["ESP"] = "es",
            ["URU"] = "uy",
            ["KSA"] = "sa",
            ["CPV"] = "cv",
            ["FRA"] = "fr",
            ["SEN"] = "sn",
            ["NOR"] = "no",
            ["IRQ"] = "iq",
            ["ARG"] = "ar",
            ["AUT"] = "at",
            ["ALG"] = "dz",
            ["JOR"] = "jo",
            ["POR"] = "pt",
            ["COL"] = "co",
            ["UZB"] = "uz",
            ["COD"] = "cd",
            ["ENG"] = "gb-eng",
            ["CRO"] = "hr",
            ["PAN"] = "pa",
            ["GHA"] = "gh"
        };

        public static string Iso(string? codigoFifa)
        {
            if (string.IsNullOrWhiteSpace(codigoFifa))
                return "";

            return _iso.TryGetValue(codigoFifa.ToUpperInvariant(), out var iso) ? iso : "";
        }
    }
}