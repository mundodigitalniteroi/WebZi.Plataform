using WebZi.Plataform.Domain.Models.Documento;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.GRV
{
    public class AutoridadeResponsavelModel
    {
        public int AutoridadeResponsavelId { get; set; }

        public short OrgaoEmissorId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string Divisao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string FlagAtivo { get; set; } = "S";

        public int? SistemaExternoId { get; set; }

        public virtual OrgaoEmissorModel OrgaoEmissor { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual UsuarioModel UsuarioAlteracao { get; set; }

        // public virtual ICollection<TbDepAgente> TbDepAgentes { get; set; } = new List<TbDepAgente>();

        // public virtual GrvModel Grv { get; set; }

        // public virtual ICollection<TbDepSolicitacaoReboqueGrv> TbDepSolicitacaoReboqueGrvs { get; set; } = new List<TbDepSolicitacaoReboqueGrv>();
    }
}