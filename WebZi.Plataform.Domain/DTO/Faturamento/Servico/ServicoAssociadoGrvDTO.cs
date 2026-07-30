namespace WebZi.Plataform.Domain.DTO.Faturamento.Servico
{
    public class ServicoAssociadoGrvDTO
    {
        public int IdentificadorServicoGrv { get; set; }
        public int identificadorServicoAssociadoTipoVeiculo { get; set; }
        public string NomeServico { get; set; }
        public int GrvId { get; set; }
        public decimal? Valor { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal {get; set;}
        public string FlagRealizarCobranca { get; set; }
        public string TipoCobranca { get; set; }
        public string TempoTrabalhado { get; set; }
    }
}