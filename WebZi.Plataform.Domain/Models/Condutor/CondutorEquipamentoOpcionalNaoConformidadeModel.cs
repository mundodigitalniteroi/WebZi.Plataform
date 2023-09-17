using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.Condutor
{
    public class CondutorEquipamentoOpcionalNaoConformidadeModel
    {
        public int CondutorEquipamentoOpcionalNaoConformidadeId { get; set; }

        public int CondutorEquipamentoOpcionalId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public string Explicacao { get; set; }

        public DateTime DataCadastro { get; set; }

        public virtual CondutorEquipamentoOpcionalModel CondutorEquipamentoOpcional { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }
    }
}