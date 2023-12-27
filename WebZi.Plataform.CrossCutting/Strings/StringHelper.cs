using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace WebZi.Plataform.CrossCutting.Strings
{
    public static partial class StringHelper
    {
        public static string AddCharToLeft(string input, char Caracter, int lenght)
        {
            return !input.IsNullOrWhiteSpace() && input.Length < lenght ? input.PadLeft(lenght, Caracter) : input;
        }

        public static string AddCharToRight(string input, char Caracter, int lenght)
        {
            return !input.IsNullOrWhiteSpace() && input.Length < lenght ? input.PadRight(lenght, Caracter) : input;
        }

        [GeneratedRegex("[^\\d]")]
        private static partial Regex RegexGetNumbers();
        public static string GetNumbers(this string input) => !input.IsNullOrWhiteSpace() ? RegexGetNumbers().Replace(input, string.Empty) : input;

        public static bool IsNull(this string input)
        {
            return input == null;
        }

        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static string Left(this string input, int lengh)
        {
            return !input.IsNull() ? input[..lengh] : input;
        }

        public static string Mid(this string input, int position)
        {
            return !input.IsNull() ? input.Substring(position - 1) : input;
        }

        public static string Mid(this string input, int position, int lengh)
        {
            return !input.IsNull() ? input.Substring(position - 1, lengh) : input;
        }

        [GeneratedRegex("\\p{Mn}")]
        private static partial Regex RegexNormalize();

        public static string Normalize(this string input)
        {
            return !input.IsNull() ? RegexNormalize().Replace(input.Normalize(NormalizationForm.FormD), string.Empty) : input;
        }

        public static string RemoveString(this string input, string oldValue)
        {
            return !input.IsNull() ? input.Replace(oldValue, string.Empty) : input;
        }

        public static string RemoveStrings(this string input, string[] oldValues)
        {
            if (input.IsNullOrWhiteSpace())
            {
                return input;
            }

            foreach (string item in oldValues)
            {
                input = input.Replace(item, string.Empty);
            }

            return input;
        }

        public static string ReplaceSecure(this string input, string oldValue, string newValue)
        {
            return input?.Replace(oldValue, newValue);
        }

        public static string Right(this string input, int lengh)
        {
            return !input.IsNull() ? input.Substring(input.Length - lengh, lengh) : input;
        }

        public static string ToCamelCase(this string input) // toCamelCase
        {
            if (input.IsNullOrWhiteSpace())
            {
                return input;
            }
            else if (input.Length == 1)
            {
                return input.ToLower();
            }

            return input.Left(1).ToLower() + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input)[1..];
        }

        public static string ToEmptyIfNull(this string input)
        {
            return input == null ? string.Empty : input;
        }

        public static string ToLowerTrim(this string input)
        {
            return !input.IsNull() ? input.ToLower().Trim() : input;
        }

        public static string ToNullIfEmpty(this string input)
        {
            return !string.IsNullOrWhiteSpace(input) ? input.Trim() : null;
        }

        public static string ToTitleCase(this string input)
        {
            return !input.IsNull() ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input) : input;
        }

        public static string ToUpperTrim(this string input)
        {
            return !input.IsNull() ? input.ToUpper().Trim() : input;
        }
    }
}