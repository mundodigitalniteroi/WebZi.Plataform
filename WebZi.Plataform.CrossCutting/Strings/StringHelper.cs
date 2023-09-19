using System.Text;
using System.Text.RegularExpressions;

namespace WebZi.Plataform.CrossCutting.Strings
{
    public abstract class StringHelper
    {
        public static bool IsNumber(string input)
        {
            input = input.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            else if (!Regex.IsMatch(input, @"^\d+$"))
            {
                return false;
            }

            return true;
        }

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

        public static string Normalize(string input)
        {
            return Regex.Replace(input.Normalize(NormalizationForm.FormD), @"\p{Mn}", string.Empty);
        }
    }
}