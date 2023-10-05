using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.Banco
{
    public class AgenciaBancariaModel
    {
        public short AgenciaBancariaId { get; set; }

        public short BancoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string CodigoAgencia { get; set; }

        public string ContaCorrente { get; set; }

        public string DigitoVerificador { get; set; }

        public string CodigoCedente { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string FlagAtivo { get; set; } = "S";

        public string SacadoCarteira { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual UsuarioModel UsuarioAlteracao { get; set; }

        public virtual BancoModel Banco { get; set; }

        public virtual ICollection<ClienteModel> Clientes { get; set; }

        //public virtual ICollection<ContasTemporaria> ContasTemporaria { get; set; } = new List<ContasTemporaria>();
    }
}