using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebZi.Plataform.Domain.Models.Condutor;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Veiculo;

namespace WebZi.Plataform.Domain.DTO.Veiculo
{
    public class CondutorEquipamentoOpcionalDTO
    {
        public int GrvId { get; set; }

        public decimal EquipamentoOpcionalId { get; set; }

        public int? UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public int? CodigoAvaria { get; set; }

        public string FlagEquipamentoAvariado { get; set; } = "N";

        public DateTime? DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public string FlagPossuiEquipamento { get; set; } = "S";

        public virtual EquipamentoOpcionalDTO EquipamentoOpcional { get; set; }

        public virtual ICollection<CondutorEquipamentoOpcionalNaoConformidadeDTO> ListagemCondutorEquipamentoOpcionalNaoConformidade { get; set; }
    }
}
