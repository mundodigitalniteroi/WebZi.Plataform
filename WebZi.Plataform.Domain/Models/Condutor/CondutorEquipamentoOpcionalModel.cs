using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Veiculo;

namespace WebZi.Plataform.Domain.Models.Condutor
{
    public class CondutorEquipamentoOpcionalModel
    {
        public int CondutorEquipamentoOpcionalId { get; set; }

        public int GrvId { get; set; }

        public decimal EquipamentoOpcionalId { get; set; }

        public int? UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public int? CodigoAvaria { get; set; }

        public string Avariado { get; set; }

        public DateTime? DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public string FlagPossuiEquipamento { get; set; } = "S";

        public virtual EquipamentoOpcionalModel EquipamentoOpcional { get; set; }

        public virtual GrvModel Grv { get; set; }

        //public virtual UsuarioModel UsuarioCadastro { get; set; }

        //public virtual UsuarioModel UsuarioAlteracao { get; set; }

        public virtual ICollection<CondutorEquipamentoOpcionalNaoConformidadeModel> CondutorEquipamentosOpcionaisNaoConformidades { get; set; }
    }
}