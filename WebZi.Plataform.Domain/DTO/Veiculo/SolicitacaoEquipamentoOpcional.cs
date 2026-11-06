namespace WebZi.Plataform.Domain.DTO.Veiculo;

public class SolicitacaoEquipamentoOpcional
{

    public int SolicitacaoId { get; set; }

    public decimal EquipamentoOpcionalId { get; set; }

    public int? UsuarioCadastroId { get; set; }

    public int? UsuarioAlteracaoId { get; set; }

    public int? CodigoAvaria { get; set; }

    public string FlagEquipamentoAvariado { get; set; } = "N";

    public DateTime? DataCadastro { get; set; }

    public DateTime? DataAtualizacao { get; set; }

    public string FlagPossuiEquipamento { get; set; } = "S";

    public virtual EquipamentoOpcionalDTO EquipamentoOpcional { get; set; }

}
