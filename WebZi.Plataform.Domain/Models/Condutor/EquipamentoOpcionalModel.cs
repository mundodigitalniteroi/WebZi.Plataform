using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.Condutor
{
    public class EquipamentoOpcionalModel
    {
        public decimal EquipamentoOpcionalId { get; set; }

        public byte? EquipamentoOpcionalLocalizacaoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public int? OrdemVistoria { get; set; } = 0;

        public string Descricao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string ItemObrigatorio { get; set; } = "N";

        public string Status { get; set; } = "S";

        public string ItemOcorrenciaDetranBa { get; set; }

        public virtual EquipamentoOpcionalLocalizacaoModel EquipamentoOpcionalLocalizacao { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual UsuarioModel UsuarioAlteracao { get; set; }

        // public virtual ICollection<TipoVeiculoEquipamentoAssociacaoModel> TiposVeiculosEquipamentosAssociacoes { get; set; }
    }
}