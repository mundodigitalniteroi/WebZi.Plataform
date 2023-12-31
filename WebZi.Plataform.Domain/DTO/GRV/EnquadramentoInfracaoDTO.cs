namespace WebZi.Plataform.Domain.DTO.GRV
{
    public class EnquadramentoInfracaoDTO
    {
        public decimal IdentificadorEnquadramentoInfracao { get; set; }

        public string CodigoInfracao { get; set; }

        public short? Artigo { get; set; }

        public string Inciso { get; set; }

        public string Descricao { get; set; }

        public string FlagAtivo { get; set; }
    }
}