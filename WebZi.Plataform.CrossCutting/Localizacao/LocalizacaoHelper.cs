using System.Text.RegularExpressions;
using WebZi.Plataform.CrossCutting.Strings;

namespace WebZi.Plataform.CrossCutting.Localizacao
{
    public static partial class LocalizacaoHelper
    {
        [GeneratedRegex("^\\d{5}-\\d{3}|(\\d{8})$")]
        private static partial Regex RegexCEP();
        public static bool IsCEP(this string cep)
        {
            return !cep.IsNullOrWhiteSpace() && RegexCEP().IsMatch(cep.Trim());
        }

        public static bool IsUF(this string uf)
        {
            uf = uf.ToUpperTrim();

            return uf == "AC" ||
                   uf == "AL" ||
                   uf == "AP" ||
                   uf == "AM" ||
                   uf == "BA" ||
                   uf == "CE" ||
                   uf == "ES" ||
                   uf == "GO" ||
                   uf == "MA" ||
                   uf == "MT" ||
                   uf == "MS" ||
                   uf == "MG" ||
                   uf == "PA" ||
                   uf == "PB" ||
                   uf == "PR" ||
                   uf == "PE" ||
                   uf == "PI" ||
                   uf == "RJ" ||
                   uf == "RN" ||
                   uf == "RS" ||
                   uf == "RO" ||
                   uf == "RR" ||
                   uf == "SC" ||
                   uf == "SP" ||
                   uf == "SE" ||
                   uf == "TO";
        }
    }
}