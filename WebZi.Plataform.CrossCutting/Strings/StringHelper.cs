using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace WebZi.Plataform.CrossCutting.Strings
{
    public static class StringHelper
    {
        public static string Left(string input, int lengh)
        {
            return input[..lengh];
        }

        public static string Right(string input, int lengh)
        {
            return input.Substring(input.Length - lengh, lengh);
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

        public static string ToLowerTrim(this string input)
        {
            return input.ToLower().Trim();
        }

        public static string ToUpperTrim(this string input)
        {
            return input.ToUpper().Trim();
        }
        #endregion Extension Methods
    }
}