using WebZi.Plataform.Domain.Models.Condutor;

namespace WebZi.Plataform.Domain.Models.Veiculo
{
    public class TipoVeiculoEquipamentoAssociacaoModel
    {
        public int TipoVeiculoEquipamentoAssociacaoId { get; set; }

        public byte TipoVeiculoId { get; set; }

        public decimal EquipamentoOpcionalId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public DateTime DataCadastro { get; set; }

        public virtual EquipamentoOpcionalModel EquipamentoOpcional { get; set; }

        public virtual TipoVeiculoModel TipoVeiculo { get; set; }

        // public virtual UsuarioModel Usuario { get; set; }
    }
}