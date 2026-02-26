using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class AgendamentoRetiradaParameters
    {
        [Required(ErrorMessage = "Informe o Identificador do DRFA que está agendando.")]
        public int GrvDRFAId { get; set; }
        [Required(ErrorMessage = "Informe o Identificador do Usuário que está agendando.")]
        public int UsuarioId { get; set; }

        [MaxLength(100, ErrorMessage = "Nome do Responsável não pode ultrapassar 150 caracteres.")]
        public string NomeResponsavel { get; set; }

        [MaxLength(11, ErrorMessage = "CPF não pode ultrapassar 11 caracteres.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Informe a Data do Registro do Agendamento.")]
        public DateTime DataDoRegistro { get; set; }

        [Required(ErrorMessage = "Informe a Data do Agendamento.")]
        public DateTime DataDoAgendamento { get; set; }
    }
}
