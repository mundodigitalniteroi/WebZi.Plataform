namespace WebZi.Plataform.Domain.Models.Atendimento
{
    public class AtendimentoFotoResponsavelModel
    {
        public int AtendimentoFotoResponsavelId { get; set; }

        public int AtendimentoId { get; set; }

        public byte[] Foto { get; set; }
    }
}