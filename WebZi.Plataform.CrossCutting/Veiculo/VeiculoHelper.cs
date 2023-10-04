using System.Text.RegularExpressions;

namespace WebZi.Plataform.CrossCutting.Veiculo
{
    public abstract class VeiculoHelper
    {
        public static bool IsPlaca(string input)
        {
            input = input.Trim();

            var aux = input.Replace("-", "");

            if (aux.EndsWith("0000"))
            {
                return false;
            }

            if (Regex.IsMatch(input, @"^[a-zA-Z]{3}\d{4}$") || Regex.IsMatch(input, @"^[a-zA-Z]{3}\d{1}[a-zA-Z]{1}\d{2}$"))
            {
                return true;
            }

            return false;
        }

        public static bool IsChassi(string input)
        {
            input = input.Trim();

            // Não permite receber as letras I, O e Q e os 6 últimos caracteres devem ser números
            return Regex.IsMatch(input, @"^[a-hj-npr-zA-HJ-NPR-Z0-9]{12}[0-9]{5}$");
        }
    }
}