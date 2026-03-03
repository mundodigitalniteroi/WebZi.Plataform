
using WebZi.Plataform.Domain.DTO.Usuario;
using WebZi.Plataform.Domain.Models.Condutor;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.DTO.Veiculo
{
    public class CondutorEquipamentoOpcionalNaoConformidadeDTO
    {

        public int CondutorEquipamentoOpcionalId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public string Explicacao { get; set; }

        public DateTime DataCadastro { get; set; }

        public virtual UsuarioDTO UsuarioCadastro { get; set; }
    }
}
