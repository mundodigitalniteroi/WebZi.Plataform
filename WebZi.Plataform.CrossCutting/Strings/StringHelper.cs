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
    }
}