using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.GRV.SolicitacaoReboque;
using WebZi.Plataform.Domain.Models.Veiculo;

namespace WebZi.Plataform.Domain.Models.Condutor
{
    public class SolicitacaoEquipamentoOpcionalModel
    {
        public int SolicitacaoEquipamentoOpcionalId { get; set; }

        public int SolicitacaoReboqueId { get; set; }

        public decimal EquipamentoOpcionalId { get; set; }

        public int? UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public int? CodigoAvaria { get; set; }

        public string FlagEquipamentoAvariado { get; set; } = "N";

        public DateTime? DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public string FlagPossuiEquipamento { get; set; } = "S";

        public virtual EquipamentoOpcionalModel EquipamentoOpcional { get; set; }

        public virtual SolicitacaoReboqueModel Grv { get; set; }

        //public virtual UsuarioModel UsuarioCadastro { get; set; }

        //public virtual UsuarioModel UsuarioAlteracao { get; set; }

        public virtual ICollection<CondutorEquipamentoOpcionalNaoConformidadeModel> ListagemCondutorEquipamentoOpcionalNaoConformidade { get; set; }
    }
}