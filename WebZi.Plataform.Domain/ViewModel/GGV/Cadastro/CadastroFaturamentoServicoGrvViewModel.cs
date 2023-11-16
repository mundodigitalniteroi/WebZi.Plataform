using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GGV.Cadastro
{
    public class CadastroFaturamentoServicoGrvViewModel
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorServicoAssociadoTipoVeiculo { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [MaxLength(12)]
        public string ValorTipoCobrancaInformado { get; set; }
    }
}