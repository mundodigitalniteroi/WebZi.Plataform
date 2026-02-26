using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class DRFAParameters
    {
        [Required(ErrorMessage = "Informe o Id do GRV.")]
        public int GrvId { get; set; }
        [Required(ErrorMessage = "Informe o Tipo de Registro.")]
        public byte TipoRegistroId { get; set; }
        [Required(ErrorMessage = "Informe o Órgão Emissor.")]
        public short OrgaoEmissorId { get; set; }
        [Required(ErrorMessage = "Informe a Divisão da Autoridade.")]
        public byte DivisaoId { get; set; }
        [MaxLength(15, ErrorMessage = "Complemento da Divisão não pode ultrapassar 15 caracteres.")]
        public string? ComplementoDivisao { get; set; }

        [MaxLength(35, ErrorMessage = "Número do Registro não pode ultrapassar 35 caracteres.")]
        public string? NumeroRegistro { get; set; }

        [MaxLength(35, ErrorMessage = "Matrícula do Agente não pode ultrapassar 35 caracteres.")]
        public string? MatriculaAgente { get; set; }

        [MaxLength(100, ErrorMessage = "Nome do Agente não pode ultrapassar 100 caracteres.")]
        public string? NomeAgente { get; set; }

        public byte[]? ArquivoDoRegistroDoRouboFurto { get; set; }

        [MaxLength(15, ErrorMessage = "Longitude não pode ultrapassar 15 caracteres.")]
        public string? Longitude { get; set; }

        [MaxLength(15, ErrorMessage = "Latitude não pode ultrapassar 15 caracteres.")]
        public string? Latitude { get; set; }

        [MaxLength(200, ErrorMessage = "Endereço Completo não pode ultrapassar 200 caracteres.")]
        public string? EnderecoCompleto { get; set; }

        [MaxLength(100, ErrorMessage = "Referência não pode ultrapassar 100 caracteres.")]
        public string? Referencia { get; set; }

        [Required(ErrorMessage = "Informe se há registro de recuperação (S ou N).")]
        [RegularExpression("^[SN]$", ErrorMessage = "FlagRegistroRecuperacao deve ser 'S' ou 'N'.")]
        public char FlagRegistroRecuperacao { get; set; } = 'N';

        [Required(ErrorMessage = "Informe se haverá agendamento de retirada (S ou N).")]
        [RegularExpression("^[SN]$", ErrorMessage = "FlagAgendamento deve ser 'S' ou 'N'.")]
        public char FlagAgendamento { get; set; } = 'N';

        [MaxLength(500, ErrorMessage = "Estado Geral do Veículo não pode ultrapassar 500 caracteres.")]
        public string? EstadoGeralDoVeiculo { get; set; }

        public RegistroRecuperacaoParameters? RegistroRecuperacao { get; set; }

        public AgendamentoRetiradaParameters? AgendamentorRetirada { get; set; }
    }
}
