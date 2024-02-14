using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Banco.PIX
{
    public class PixDinamicoDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public int IdentificadorPixDinamico { get; set; }

        public byte IdentificadorPixDinamicoTipoStatusGeracao { get; set; }

        public string TransactionId { get; set; }

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
    }
}