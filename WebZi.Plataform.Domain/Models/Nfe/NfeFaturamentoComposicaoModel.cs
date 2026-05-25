using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Domain.Models.Nfe;

public class NfeFaturamentoComposicaoModel
{
    public int NfeFaturamentoComposicaoId { get; set; }
    public int NfeId { get; set; }
    public int FaturamentoComposicaoId { get; set; }
    /// <summary>
    /// P = PENDENTE DE CADASTRO
    /// F = CADASTRO FINALIZADO
    /// E = ERRO NO CADASTRO
    /// </summary>
    public char StatusCadastroErp { get; set; }

    public NfeModel Nfe { get; set; }
    public FaturamentoComposicaoModel FaturamentoComposicao { get; set; }
}