using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.Condutor
{
    public class EquipamentoOpcionalLocalizacaoModel
    {
        public byte EquipamentoOpcionalLocalizacaoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string Descricao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public virtual UsuarioModel UsuarioAlteracao { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual ICollection<EquipamentoOpcionalModel> EquipamentosOpcionais { get; set; }
    }
}