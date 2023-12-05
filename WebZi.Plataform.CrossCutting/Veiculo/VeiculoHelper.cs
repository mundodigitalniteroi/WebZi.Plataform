using WebZi.Plataform.CrossCutting.Strings;
using System.Text.RegularExpressions;

namespace WebZi.Plataform.CrossCutting.Veiculo
{
    public static partial class VeiculoHelper
    {
        public static string FormatPlaca(string placa)
        {
            return IsPlaca(placa) ? placa.Left(3) + "-" + placa.Right(4) : placa;
        }

        [GeneratedRegex("^[a-zA-Z]{3}\\d{4}$")]
        private static partial Regex RegexIsPlaca1();

        [GeneratedRegex("^[a-zA-Z]{3}\\d{1}[a-zA-Z]{1}\\d{2}$")]
        private static partial Regex RegexIsPlaca2();

        public static bool IsPlaca(string input)
        {
            string aux = input.Replace("-", "").Trim();

            return !aux.EndsWith("0000") && (RegexIsPlaca1().IsMatch(aux) || RegexIsPlaca2().IsMatch(aux));
        }

        [GeneratedRegex("^[a-hj-npr-zA-HJ-NPR-Z0-9]{12}[0-9]{5}$")]
        private static partial Regex RegexIsChassi();

        public static bool IsChassi(string input)
        {
            // Não permite receber as letras I, O e Q e os 6 últimos caracteres devem ser números
            return RegexIsChassi().IsMatch(input.Trim());
        }
    }
}