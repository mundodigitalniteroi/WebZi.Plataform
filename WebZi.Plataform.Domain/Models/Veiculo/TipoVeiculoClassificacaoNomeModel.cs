namespace WebZi.Plataform.Domain.Models.Veiculo
{
    public class TipoVeiculoClassificacaoNomeModel
    {
        public byte IdTipoVeiculoClassificacaoNome { get; set; }

        public string Descricao { get; set; }

        public string Classificacao { get; set; }

        // public virtual ICollection<TbDepReboquesTerceirizadosTarifa> TbDepReboquesTerceirizadosTarifas { get; set; } = new List<TbDepReboquesTerceirizadosTarifa>();

        public virtual ICollection<TipoVeiculoClassificacaoModel> TiposVeiculosClassificacoes { get; set; }
    }
}