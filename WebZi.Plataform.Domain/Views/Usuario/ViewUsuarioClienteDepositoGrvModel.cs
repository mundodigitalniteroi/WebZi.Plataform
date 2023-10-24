using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Views.Usuario
{
    public class ViewUsuarioClienteDepositoGrvModel
    {
        public long UsuarioClienteId { get; set; }

        public long UsuarioDepositoId { get; set; }

        public int ClienteId { get; set; }

        public int DepositoId { get; set; }

        public int UsuarioId { get; set; }

        public string Login { get; set; }

        public string Matricula { get; set; }

        public string UsuarioFlagAtivo { get; set; }

        public string Cliente { get; set; }

        public string ClienteFlagAtivo { get; set; }

        public string Deposito { get; set; }

        public string DepositoFlagAtivo { get; set; }

        public int GrvId { get; set; }

        public string NumeroFormularioGrv { get; set; }

        public string FaturamentoProdutoCodigo { get; set; }

        public virtual GrvModel Grv { get; set; }
    }
}