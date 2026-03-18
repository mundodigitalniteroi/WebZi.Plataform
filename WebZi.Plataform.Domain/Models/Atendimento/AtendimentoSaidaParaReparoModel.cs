namespace WebZi.Plataform.Domain.Models.Atendimento
{
    public class AtendimentoSaidaParaReparoModel
    {
        public int Id { get; set; }
        public int AtendimentoId { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime DataSaida { get; set; }
        public DateTime DataPrevisaoRetorno { get; set; }
        public string MotivoSaida { get; set; }
        public DateTime? DataRetorno { get; set; }
        public AtendimentoModel Atendimento { get; set; }
    }
}
