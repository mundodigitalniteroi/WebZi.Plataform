using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.Atendimento
{
    public class DescontoParameters
    {
        public int FaturamentoServicoTipoVeiculoId { get; set; }

        public char TipoComposicao { get; set; }

        public int FaturamentoTipoComposicaoId { get; set; }

        public int UsuarioDescontoId { get; set; }

        public int QuantidadeAjuste { get; set; } = 0;

        public string TipoDesconto { get; set; } // P = Porcentagem, V = Valor

        public int QuantidadeDesconto { get; set; }

        public decimal ValorDesconto { get; set; }

        public string ObservacaoDesconto { get; set; }
    }
}