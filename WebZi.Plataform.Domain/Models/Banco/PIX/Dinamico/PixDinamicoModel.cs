using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico
{
    public class PixDinamicoModel
    {
        public int PixDinamicoId { get; set; }

        public byte PixDinamicoTipoStatusGeracaoId { get; set; }

        public int FaturamentoId { get; set; }

        public string TxId { get; set; }

        public int? Revisao { get; set; }

        public string Pix { get; set; }

        public string QrString { get; set; }

        public string QrCode { get; set; }

        public DateTime? CalendarioCriacao { get; set; }

        public int? CalendarioExpiracao { get; set; }

        public string Devedor { get; set; }

        public string Location { get; set; }

        public int? LocationId { get; set; }

        public string TipoCobranca { get; set; }

        public string Chave { get; set; }

        public string SolicitacaoPagador { get; set; }

        public string InfoAdicionais { get; set; }

        public decimal? ValorOriginal { get; set; }

        public string Json { get; set; }

        public DateTime? PixHorario { get; set; }

        public string PagadorNome { get; set; }

        public string PagadorCpf { get; set; }

        public string PagadorCnpj { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public virtual FaturamentoModel Faturamento { get; set; }

        public virtual PixDinamicoTipoStatusGeracaoModel PixDinamicoTipoStatusGeracao { get; set; }

        public virtual ICollection<PixDinamicoConsultaModel> PixDinamicoConsultas { get; set; }
    }
}