using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Models.Veiculo;

namespace WebZi.Plataform.Domain.Models.ClienteDeposito
{
    public class ClienteDepositoTipoVeiculoModel
    {
        public short ClienteDepositoTipoVeiculoId { get; set; }

        public int ClienteDepositoId { get; set; }

        public byte TipoVeiculoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string FlagAtivo { get; set; } = "S";

        public virtual ClienteDepositoModel ClienteDeposito { get; set; }

        public virtual TipoVeiculoModel TipoVeiculo { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual UsuarioModel UsuarioAlteracao { get; set; }
    }
}