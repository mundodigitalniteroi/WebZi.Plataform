using WebZi.Plataform.Domain.DTO.Usuario;
using WebZi.Plataform.Domain.Models.GRV.DRFA;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.DTO.DRFA
{
    public class AgendamentoRetiradaDTO
    {
        public int IdentificadorAgendamentoRetirada { get; set; }
        public int IdentificadorDRFA { get; set; }
        public int IdentificadorUsuarioRegistroAgendamento { get; set; }
        public string NomeResponsavelAgendamento { get; set; }
        public string CpfResponsavelAgendamento { get; set; }
        public string DataRegistroAgendamento { get; set; }
        public string DataAgendamento { get; set; }

        public UsuarioDTO UsuarioRegistroAgendamento { get; set; }
    }
}
