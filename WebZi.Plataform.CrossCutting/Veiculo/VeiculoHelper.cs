using WebZi.Plataform.CrossCutting.Strings;
using System.Text.RegularExpressions;

namespace WebZi.Plataform.CrossCutting.Veiculo
{
    public static class VeiculoHelper
    {
        public static string FormatPlaca(string placa)
        {
                return placa.Left(3) + "-" + placa.Right(4);
        }

        public static bool IsPlaca(string input)
        {
            string aux = input.Replace("-", "").Trim();

            return !aux.EndsWith("0000") && (Regex.IsMatch(aux, @"^[a-zA-Z]{3}\d{4}$") || Regex.IsMatch(aux, @"^[a-zA-Z]{3}\d{1}[a-zA-Z]{1}\d{2}$"));
        }

        public static bool IsChassi(string input)
        {
            // Não permite receber as letras I, O e Q e os 6 últimos caracteres devem ser números
            return Regex.IsMatch(input.Trim(), @"^[a-hj-npr-zA-HJ-NPR-Z0-9]{12}[0-9]{5}$");
        }
    }
}