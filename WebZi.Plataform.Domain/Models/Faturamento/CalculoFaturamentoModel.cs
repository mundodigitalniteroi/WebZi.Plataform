using WebZi.Plataform.Domain.Models.Atendimento;

namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public  class CalculoFaturamentoModel
    {
        public AtendimentoModel Atendimento { get; set; }

        public List<CalculoTributacaoModel> Tributacoes { get; set; } = new();

        public AvisoViewModel Mensagens { get; set; } = new();
    }
}