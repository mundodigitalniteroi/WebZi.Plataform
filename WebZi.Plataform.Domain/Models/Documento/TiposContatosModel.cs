namespace WebZi.Plataform.Domain.Models.Documento
{
    public class TiposContatosModel
    {
        public int TipoContatoId { get; set; }
        public string Descricao { get; set; }
        public string? Formato { get; set; }
        public byte TamanhoMinimo { get; set; }
        public byte TamanhoMaximo { get; set; }
        public byte OrdemApresentacao { get; set; }
        public char FlagAtivo { get; set; }
    }
}
