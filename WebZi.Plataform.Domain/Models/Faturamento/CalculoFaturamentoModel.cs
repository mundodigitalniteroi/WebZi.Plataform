using WebZi.Plataform.Domain.Models.Atendimento;

namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public  class CalculoFaturamentoModel
    {
        public AtendimentoModel AtendimentoModel { get; set; }

        public FaturamentoModel FaturamentoModel { get; set; }

        public List<FaturamentoComposicaoModel> FaturamentoComposicoes { get; set; } = new List<FaturamentoComposicaoModel>();

        public List<CalculoTributacaoModel> Tributacoes { get; set; } = new List<CalculoTributacaoModel>();
    }
}