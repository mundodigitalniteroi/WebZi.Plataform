using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.ViewModel.Atendimento
{
    public class GerarFaturamentoSaidaReparoParameters
    {
        public GrvModel Grv { get; set; } = null!;

        public FaturamentoModel UltimoFaturamento { get; set; } = null!;

        public DateTime DataInicialParaCalculo { get; set; }

        public DateTime DataFinalParaCalculo { get; set; }

        public int IdentificadorUsuario { get; set; }

        public bool IsAtualizacaoPrevisao { get; set; } = false;

        public DateTime? DataPrevisaoAntiga { get; set; }
    }
}
