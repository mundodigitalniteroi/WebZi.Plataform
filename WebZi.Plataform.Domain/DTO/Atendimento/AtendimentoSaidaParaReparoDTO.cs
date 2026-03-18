namespace WebZi.Plataform.Domain.DTO.Atendimento
{
    public class AtendimentoSaidaParaReparoDTO
    {
        public int IdentificadorSaidaReparo { get; set; }
        public int IdentificadorAtendimento { get; set; }
        public DateTime DataSaida { get; set; }
        public DateTime DataPrevisaoRetorno { get; set; }
        public string MotivoSaida { get; set; }
    }
}
