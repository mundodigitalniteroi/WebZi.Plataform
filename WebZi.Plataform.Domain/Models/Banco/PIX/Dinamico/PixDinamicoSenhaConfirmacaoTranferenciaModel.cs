using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico
{
    public class PixDinamicoSenhaConfirmacaoTranferenciaModel
    {
        public int PixDinamicoSenhaConfirmacaoTranferenciaId { get; set; }

        public int FaturamentoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioFinanceiroId { get; set; }

        public string Senha { get; set; }

        public string SenhaFinanceiro { get; set; }

        public string FlagConfirmado { get; set; } = "N";

        public DateTime? DataHoraAutorizacaoFinanceiro { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public virtual FaturamentoModel Faturamento { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual UsuarioModel UsuarioFinanceiro { get; set; }
    }
}