using System.Globalization;
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

        public static string FormatMoney(decimal value)
        {
            return value.ToString("C2", CultureInfo.GetCultureInfo("pt-br"));
        }

        public static string FormatNumber(decimal value)
        {
            return value.ToString("0.00", CultureInfo.GetCultureInfo("pt-br"));
        }

        public static bool IsNumber(string value)
        {
            value = value.Trim();

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            else if (!Regex.IsMatch(value, @"^\d+$"))
            {
                return false;
            }

            return true;
        }
    }
}