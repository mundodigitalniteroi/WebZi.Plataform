namespace WebZi.Plataform.Domain.Models.Veiculo
{
    public class TipoVeiculoClassificacaoModel
    {
        public byte TipoVeiculoClassificacaoId { get; set; }

        public byte TipoVeiculoClassificacaoNomeId { get; set; }

        public byte TipoVeiculoId { get; set; }

        public virtual TipoVeiculoClassificacaoNomeModel TipoVeiculoClassificacaoNome { get; set; }

        public virtual TipoVeiculoModel TipoVeiculo { get; set; }
    }
}