using System.Text.RegularExpressions;

namespace WebZi.Plataform.CrossCutting.Local
{
    public abstract class LocalHelper
    {
        public static bool IsCEP(string cep)
        {
            return Regex.IsMatch(cep, @"^\d{5}-\d{3}|(\d{8})$");
        }
    }
}