using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GGV
{
    public class AtualizarServicoAoGgvParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorProcesso { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorServicoGrv { get; set; }

        public string ValorTipoCobrancaInformado { get; set; }

        [StringLength(5, ErrorMessage = "Tamanho máximo de 5 caracteres (HH:MM)")]
        public string HoraMinuto { get; set; }

        public int? Quantidade { get; set; } = 1;
    }
}
