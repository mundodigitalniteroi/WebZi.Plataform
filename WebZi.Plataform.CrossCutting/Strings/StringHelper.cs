using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace WebZi.Plataform.CrossCutting.Strings
{
    public static class StringHelper
    {
        public static string GetNumbersFromString(this string input) => string.IsNullOrWhiteSpace(input) ? input : Regex.Replace(input, @"[^\d]", string.Empty);

        public static string Left(this string input, int lengh)
        {
            return input[..lengh];
        }

        public static string Right(this string input, int lengh)
        {
            return input.Substring(input.Length - lengh, lengh);
        }

        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static string Mid(string input, int index, int lengh)
        {
            return input.Substring(index, lengh);
        }

        public static string Mid(string input, int index)
        {
            return input.Substring(index);
        }

        public static string AddStringLeft(string input, char Caracter, int lenght)
        {
            if (input.Length >= lenght)
            {
                return input;
            }

            return input.PadLeft(lenght, Caracter);
        }

        #region Extension Methods
        /// <summary>
        /// Remove acentuações
        /// </summary>
        public static string Normalize(this string input)
        {
            if (input == null)
            {
                return input;
            }

            return Regex.Replace(input.Normalize(NormalizationForm.FormD), @"\p{Mn}", string.Empty);
        }

        public static string TitleCase(this string input)
        {
            if (input == null)
            {
                return input;
            }

            string[] words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length == 0)
                {
                    continue;
                }

                char firstChar = char.ToUpper(words[i][0]);

                string rest = string.Empty;

                if (words[i].Length > 1)
                {
                    rest = words[i][1..].ToLower(CultureInfo.CurrentCulture);
                }

                words[i] = firstChar + rest;
            }

            return string.Join(" ", words);
        }

        public static int ToInt(this string input)
        {
            return int.Parse(input);
        }

        public static string RemoveString(this string input, string oldValue)
        {
            return input?.Replace(oldValue, string.Empty);
        }

        public static string RemoveStrings(this string input, string[] oldValues)
        {
            if (string.IsNullOrWhiteSpace(input))
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

        public static string ToLowerTrim(this string input)
        {
            return input?.ToLower().Trim();
        }

        public static string ToUpperTrim(this string input)
        {
            return input?.ToUpper().Trim();
        }

        public static string ToNullIfEmpty(this string input)
        {
            return !string.IsNullOrWhiteSpace(input) ? input.Trim() : null;
        }
        #endregion Extension Methods
    }
}