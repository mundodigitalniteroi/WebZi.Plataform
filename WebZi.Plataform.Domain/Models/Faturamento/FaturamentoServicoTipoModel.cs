using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.Faturamento
{
    public class FaturamentoServicoTipoModel
    {
        public int FaturamentoServicoTipoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string Descricao { get; set; }

        /// <summary>
        /// TIPOS DE COBRANÇA:
        /// D = Diárias;
        /// H = Quantidade de HH:MM vezes o Preço;
        /// P = Porcentagem;
        /// Q = Quantidade;
        /// T = Tempo entre duas Datas;
        /// V = Valor.
        /// </summary>
        public string TipoCobranca { get; set; }

        public string FaturamentoProdutoId { get; set; } = "DEP";

        public byte OrdemImpressao { get; set; } = 1;

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string FlagCobrarTelaGrv { get; set; } = "N";

        public string FlagNaoCobrarSeNaoUsouReboque { get; set; } = "N";

        public string FlagServicoObrigatorio { get; set; } = "N";

        public string FlagRebocada { get; set; } = "N";

        public string FlagImpressaoAgrupada { get; set; } = "N";

        public string FlagTributacao { get; set; } = "N";

        public string FlagCobrancaPorHora { get; set; } = "N";

        public string FlagAtivo { get; set; } = "S";

        public virtual FaturamentoProdutoModel FaturamentoProduto { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual UsuarioModel UsuarioAlteracao { get; set; }

        public virtual ICollection<FaturamentoServicoAssociadoModel> FaturamentoServicosAssociados { get; set; }
    }
}