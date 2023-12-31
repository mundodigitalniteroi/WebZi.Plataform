using System.Text.RegularExpressions;
using WebZi.Plataform.CrossCutting.Strings;

namespace WebZi.Plataform.CrossCutting.Veiculo
{
    public static partial class VeiculoHelper
    {
        #region Chassi
        public static string FormatChassi(this string input)
        {
            if (input.IsNullOrWhiteSpace())
            {
                return input;
            }

            input = input.NormalizeChassi();

            if (input.Length != 17)
            {
                return input;
            }

            return input.Left(3) + " " + input.Mid(4, 5) + " " + input.Mid(9, 1) + " " + input.Mid(10, 2) + " " + input.Mid(12, 5);
        }

        [GeneratedRegex("^[A-Za-z0-9]{3,3}[A-Za-z0-9]{6,6}[A-Za-z0-9]{2,2}[A-Za-z0-9]{6,6}$")]
        private static partial Regex RegexIsChassi();

        public static bool IsChassi(this string input)
        {
            if (input.IsNullOrWhiteSpace())
            {
                return false;
            }

            return RegexIsChassi().IsMatch(input.NormalizeChassi());
        }

        public static string NormalizeChassi(this string input)
        {
            return !input.IsNullOrWhiteSpace() ? input.Replace(" ", "").ToUpperTrim() : input;
        }
        #endregion Chassi

        #region Placa
        public static string FormatPlaca(this string input)
        {
            if (input.IsNullOrWhiteSpace())
            {
                return input;
            }

            input = input.NormalizePlaca();

            return input.IsPlaca() ? input.Left(3) + "-" + input.Right(4) : input;
        }

        [GeneratedRegex("^[a-zA-Z]{3}\\d{4}$")]
        private static partial Regex RegexPlacaAntiga();

        [GeneratedRegex("^[a-zA-Z]{3}\\d{1}[a-zA-Z]{1}\\d{2}$")]
        private static partial Regex RegexPlacaNova();

        public static bool IsPlaca(this string input)
        {
            if (input.IsNullOrWhiteSpace())
            {
                return false;
            }

            input = input.NormalizePlaca();

            return !input.EndsWith("0000") && (RegexPlacaAntiga().IsMatch(input) || RegexPlacaNova().IsMatch(input));
        }

        public static string NormalizePlaca(this string input)
        {
            return !input.IsNullOrWhiteSpace() ? input.Replace("-", "").ToUpperTrim() : input;
        }
        #endregion Placa

        #region Renavan
        public static bool IsRenavan(string input)
        {
            if (input.Length != 11)
            {
                input = input.PadRight(11, '0');
            }
            if (!Regex.IsMatch(input, "^[0-9]{11}$")) return false;


            int[] digitos = GetDigitosInvertidos(input);
            int verificador = GetDigitoVerificador(input);
            int soma = GetSoma(digitos);
            int verificadorCalculado = GetVerificador(soma);
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
            int soma = 0;
            for (int i = 0; i < digitos.Length; i++)
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
            int valor = 11 - (soma % 11);
            if (valor >= 10) return 0;
            return valor;
        }
        #endregion Renavan
    }
}