
using WebZi.Plataform.Domain.DTO.GRV;

namespace WebZi.Plataform.Data.Services.DRFA
{
    public class RegistroRecuperacaoDTO
    {
        public int IdentificadorRegistroRecuperacao { get; set; }
        public int IdentificadorDRFA { get; set; }
        public byte IdentificadorAutoridadeDivisao { get; set; }
        public string NumeroRegistroRecuperacao { get; set; }
        public string MatriculaAgente { get; set; }
        public string NomeAgente { get; set; }
        public string DataRegistroRecuperacao { get; set; }
        public AutoridadesDivisoesDTO AutoridadeDivisao { get; set; }
    }
}
