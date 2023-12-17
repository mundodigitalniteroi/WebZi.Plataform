namespace WebZi.Plataform.Domain.Enums
{
    public static class TipoCobrancaFaturamentoEnum
    {
        public static readonly string Diárias = "D";
        
        /// <summary>
        /// Quantidade de HH:MM vezes o Preço
        /// </summary>
        public static readonly string Horas = "H";
        
        public static readonly string Porcentagem = "P";
        
        public static readonly string Quantidade = "Q";

        /// <summary>
        /// Tempo entre duas Datas
        /// </summary>
        public static readonly string Tempo = "T";
 
        public static readonly string Valor = "V";
    }
}