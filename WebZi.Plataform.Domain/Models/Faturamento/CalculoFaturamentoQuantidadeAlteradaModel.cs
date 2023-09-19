namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class CalculoFaturamentoQuantidadeAlteradaModel
    {
        public int FaturamentoTipoComposicaoId { get; set; }

        public int FaturamentoServicoTipoVeiculoId { get; set; }

        public char TipoComposicao { get; set; }

        public int UsuarioAlteracaoQuantidadeId { get; set; }

        public int QuantidadeAlterada { get; set; }

        public string ObservacaoQuantidadeAlterada { get; set; }
    }
}