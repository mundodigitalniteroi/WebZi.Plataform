using System.Text.RegularExpressions;

namespace WebZi.Plataform.CrossCutting.Localizacao
{
    public static class LocalizacaoHelper
    {
        public static bool IsCEP(string cep)
        {
            cep = cep.Replace("-", "").Trim();

            return Regex.IsMatch(cep, @"^\d{5}-\d{3}|(\d{8})$");
        }

        public static bool IsUF(string uf)
        {
            uf = uf.Trim().ToUpper();

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