using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.GRV.DRFA
{
    public class AgendamentoRetiradaModel
    {
        public int GrvDRFAAgendamentoRetiradaId { get; set; }
        public int DRFAId { get; set; }
        public int UsuarioRegistroAgendamentoId { get; set; }
        public string NomeResponsavelAgendamento { get; set; }
        public string CpfResponsavelAgendamento { get; set; }
        public DateTime DataRegistroAgendamento { get; set; }
        public DateTime DataAgendamento { get; set; }

        public DRFAModel DRFA { get; set; }
        public UsuarioModel UsuarioRegistroAgendamento { get; set; }
    }
}
