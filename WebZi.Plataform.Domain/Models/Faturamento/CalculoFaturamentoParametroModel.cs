using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class CalculoFaturamentoParametroModel
    {
        public int TipoMeioCobrancaId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public char origemTipoComposicao { get; set; } = 'P'; // P = PÁTIO, L = LEILÃO

        public string StatusOperacaoId { get; set; }

        public bool flag_cadastrar_faturamento { get; set; } = true;

        public bool flag_permissao_data_retroativa_faturamento { get; set; }

        public int Diarias { get; set; }

        public DateTime DataHoraGuarda { get; set; }

        public DateTime DataLiberacao { get; set; }

        public DateTime DataPrazoRetiradaVeiculo { get; set; }

        public DateTime DataHoraAtualPorDeposito { get; set; }

        public GrvModel Grv { get; set; }

        public AtendimentoModel Atendimento { get; set; }

        public List<CalculoFaturamentoQuantidadeAlteradaModel> FaturamentoQuantidadeAlteradaList { get; set; }

        public List<CalculoFaturamentoDescontoModel> FaturamentoDescontoList { get; set; }
    }
}