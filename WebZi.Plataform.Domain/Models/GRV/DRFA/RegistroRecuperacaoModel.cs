namespace WebZi.Plataform.Domain.Models.GRV.DRFA
{
    public class RegistroRecuperacaoModel
    {
        public int GrvDRFARegistroRecuperacaoId { get; set; }
        public int DRFAId { get; set; }
        public byte AutoridadeDivisaoId{ get; set; }
        public string NumeroRegistroRecuperacao { get; set; }
        public string MatriculaAgente { get; set; }
        public string NomeAgente { get; set; }
        public DateTime DataRegistroRecuperacao { get; set; }
    }
}
