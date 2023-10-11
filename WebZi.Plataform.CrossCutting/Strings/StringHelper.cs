using System.Text;
using System.Text.RegularExpressions;

namespace WebZi.Plataform.CrossCutting.Strings
{
    public abstract class StringHelper
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

        /// <summary>
        /// Remove acentuações
        /// </summary>
        public static string Normalize(string input)
        {
            return Regex.Replace(input.Normalize(NormalizationForm.FormD), @"\p{Mn}", string.Empty);
        }

        public static string AddStringLeft(string input, char Caracter, int lenght)
        {
            if (input.Length >= lenght)
            {
                return input;
            }

            return input.PadLeft(lenght, Caracter);
        }
        
        public static string TitleCase(string input)
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
                    rest = words[i].Substring(1).ToLower();
                }

                words[i] = firstChar + rest;
            }

            return string.Join(" ", words);
        }
    }
}