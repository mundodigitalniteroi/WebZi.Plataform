using System.Text.RegularExpressions;

namespace WebZi.Plataform.CrossCutting.Number
{
    public abstract class NumberHelper
    {
        public static decimal GetPercentage(decimal value, decimal percentage)
        {
            if (percentage > 100)
            {
                percentage = 100;
            }
            else if (percentage <= 0)
            {
                return value;
            }

            percentage = (100 - percentage) / 100;

            return value * percentage;
        }

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