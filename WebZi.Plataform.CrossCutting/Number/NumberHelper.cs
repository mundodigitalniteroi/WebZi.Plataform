using System.Globalization;
using System.Text.RegularExpressions;

namespace WebZi.Plataform.CrossCutting.Number
{
    public static partial class NumberHelper
    {
        public static string FormatMoney(this decimal input) => input.ToString("C2", CultureInfo.GetCultureInfo("pt-br"));

        public static string FormatNumber(this decimal input) => input.ToString("0.00", CultureInfo.GetCultureInfo("pt-br"));

        [GeneratedRegex("^\\d+$")]
        private static partial Regex RegexNumber();
        public static bool IsNumber(this string input)
        {
            input = input.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            else if (!RegexNumber().IsMatch(input))
            {
                return false;
            }

            return true;
        }

        private static HashSet<Type> GetNumericTypes()
        {
            return new()
            {
                typeof(decimal),
                typeof(byte),
                typeof(decimal),
                typeof(int),
                typeof(long),
                typeof(sbyte),
                typeof(short),
                typeof(uint),
                typeof(ulong),
                typeof(short),
                typeof(ushort)
            };
        }

        public static bool IsNumericType<T>(this List<T> value)
        {
            return GetNumericTypes().Contains(typeof(T));
        }

        public static bool IsNumericType<T>(this T value)
        {
            return GetNumericTypes().Contains(typeof(T));
        }

        public static decimal GetPercentage(this decimal input, decimal percentage)
        {
            if (percentage > 100)
            {
                percentage = 100;
            }
            else if (percentage <= 0)
            {
                return input;
            }

            percentage = (100 - percentage) / 100;

            return input * percentage;
        }

        public static byte ToByte(this string input)
        {
            return byte.Parse(input);
        }

        public static decimal ToDecimal(this string input)
        {
            return decimal.Parse(input);
        }

        public static int ToInt(this string input)
        {
            return int.Parse(input);
        }

        public static long ToLong(this string input)
        {
            return long.Parse(input);
        }

        public static sbyte ToSByte(this string input)
        {
            return sbyte.Parse(input);
        }

        public static short ToShort(this string input)
        {
            return short.Parse(input);
        }

        public static uint ToUInt(this string input)
        {
            return uint.Parse(input);
        }

        public static ulong ToULong(this string input)
        {
            return ulong.Parse(input);
        }

        public static ushort ToUShort(this string input)
        {
            return ushort.Parse(input);
        }
    }
}