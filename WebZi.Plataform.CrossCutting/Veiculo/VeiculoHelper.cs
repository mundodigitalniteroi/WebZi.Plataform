using System.Text.RegularExpressions;
using WebZi.Plataform.CrossCutting.Strings;

namespace WebZi.Plataform.CrossCutting.Veiculo
{
    public static partial class VeiculoHelper
    {
        public static string FormatChassi(this string input)
        {
            input = input.Trim();

            if (input.Length != 17)
            {
                return input;
            }

            return input.Left(3) + " " + input.Mid(4, 5) + " " + input.Mid(9, 1) + " " + input.Mid(10, 2) + " " + input.Mid(12, 5);
        }

        public static string FormatPlaca(this string input)
        {
            return IsPlaca(input) ? input.Left(3) + "-" + input.Right(4) : input;
        }

        [GeneratedRegex("^[A-Za-z0-9]{3,3}[A-Za-z0-9]{6,6}[A-Za-z0-9]{2,2}[A-Za-z0-9]{6,6}$")]
        private static partial Regex RegexIsChassi();

        public static bool IsChassi(this string input)
        {
            return RegexIsChassi().IsMatch(input.Trim());
        }

        [GeneratedRegex("^[a-zA-Z]{3}\\d{4}$")]
        private static partial Regex RegexPlacaAntiga();

        [GeneratedRegex("^[a-zA-Z]{3}\\d{1}[a-zA-Z]{1}\\d{2}$")]
        private static partial Regex RegexPlacaNova();

        public static bool IsPlaca(this string input)
        {
            string aux = input.Replace("-", "").Trim();

            return !aux.EndsWith("0000") && (RegexPlacaAntiga().IsMatch(aux) || RegexPlacaNova().IsMatch(aux));
        }

        #region Renavan
        public static bool IsRenavan(string input)
        {
            if (input.Length != 11)
            {
                input = input.PadRight(11, '0');
            }
            if (!Regex.IsMatch(input, "^[0-9]{11}$")) return false;


            int[] digitos = GetDigitosInvertidos(input);
            var verificador = GetDigitoVerificador(input);
            var soma = GetSoma(digitos);
            var verificadorCalculado = GetVerificador(soma);
            return verificadorCalculado == verificador;
        }

        private static int[] GetDigitosInvertidos(string digitos)
        {
            char[] digitosChar = digitos.ToCharArray();
            Array.Reverse(digitosChar);
            return Array.ConvertAll(digitosChar, c => (int)Char.GetNumericValue(c));
        }

        private static int GetDigitoVerificador(string digitos)
        {
            string digito = digitos[-1].ToString();
            return int.Parse(digito);
        }

        private static int GetSoma(int[] digitos)
        {
            var soma = 0;
            for (var i = 0; i < digitos.Length; i++)
            {
                soma += digitos[i] * GetFactor(i);
            }
            return soma;
        }

        private static int GetFactor(int num)
        {
            int[] digits = { 2, 3, 4, 5, 6, 7, 8, 9 };
            int index;
            if (num >= digits.Length)
            {
                index = num % digits.Length;
            }
            else
            {
                index = num;
            }
            return digits[index];
        }

        private static int GetVerificador(int soma)
        {
            var valor = 11 - (soma % 11);
            if (valor >= 10) return 0;
            return valor;
        }
        #endregion
    }
}