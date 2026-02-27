using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class RegistroRecuperacaoParameters
    {
        [Required(ErrorMessage = "Informe o indereço do DRFA para o registro de recuperação.")]
        public int GrvDRFAId { get; set; }

        [Required(ErrorMessage = "Informe a Divisão da Autoridade para o registro de recuperação.")]
        public byte DivisaoId { get; set; }


        [MaxLength(15, ErrorMessage = "Número do Registro não pode ultrapassar 35 caracteres.")]
        public string? NumeroRegistro { get; set; }

        [MaxLength(15, ErrorMessage = "Matrícula do Agente não pode ultrapassar 35 caracteres.")]
        public string? MatriculaAgente { get; set; }

        [MaxLength(100, ErrorMessage = "Nome do Agente não pode ultrapassar 100 caracteres.")]
        public string? NomeAgente { get; set; }
        public byte[]? ArquivoDeRecuperacao { get; set; }

        [Required(ErrorMessage = "Informe a Data da Recuperação.")]
        public DateTime DataDeRecuperacao { get; set; }

    }
}
