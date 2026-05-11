namespace WebZi.Plataform.Domain.DTO.Faturamento.Servico
{
    public class ServicoAssociadoGrvDTO
    {
        public int identificadorServicoAssociadoTipoVeiculo { get; set; }
        public int GrvId { get; set; }
        public decimal? Valor { get; set; }
        public int Quantidade { get; set; }
        public string FlagRealizarCobranca { get; set; }
    }
}