using System.Text.RegularExpressions;

namespace WebZi.Plataform.CrossCutting.Local
{
    public abstract class LocalHelper
    {
        public static bool IsCEP(string cep)
        {
            return Regex.IsMatch(cep, @"^\d{5}-\d{3}|(\d{8})$");
        }

        public static bool IsUF(string uf)
        {
            uf = uf.Trim().ToUpper();

            return uf.Equals("AC") ||
                   uf.Equals("AL") ||
                   uf.Equals("AP") ||
                   uf.Equals("AM") ||
                   uf.Equals("BA") ||
                   uf.Equals("CE") ||
                   uf.Equals("ES") ||
                   uf.Equals("GO") ||
                   uf.Equals("MA") ||
                   uf.Equals("MT") ||
                   uf.Equals("MS") ||
                   uf.Equals("MG") ||
                   uf.Equals("PA") ||
                   uf.Equals("PB") ||
                   uf.Equals("PR") ||
                   uf.Equals("PE") ||
                   uf.Equals("PI") ||
                   uf.Equals("RJ") ||
                   uf.Equals("RN") ||
                   uf.Equals("RS") ||
                   uf.Equals("RO") ||
                   uf.Equals("RR") ||
                   uf.Equals("SC") ||
                   uf.Equals("SP") ||
                   uf.Equals("SE") ||
                   uf.Equals("TO");
        }
    }
}