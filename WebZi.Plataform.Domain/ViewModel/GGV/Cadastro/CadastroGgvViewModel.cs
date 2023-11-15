using System.ComponentModel.DataAnnotations;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Domain.ViewModel.GGV.Cadastro
{
    public class CadastroGgvViewModel
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorGrv { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public DateTime DataHoraGuarda { get; set; }

        public DateTime? DataTransbordo { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagChaveDeposito { get; set; }

        public string NumeroChave { get; set; }

        public string EstacionamentoSetor { get; set; }

        public string EstacionamentoNumeroVaga { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagTransbordo { get; set; }

        public CadastroVistoriaViewModel Vistoria { get; set; }

        public List<CadastroEquipamentoOpcionalViewModel> ListagemEquipamentoOpcional { get; set; }

        public List<CadastroFotoTipoCadastroViewModel> ListagemFotos { get; set; }

        public List<CadastroFaturamentoServicoGrvViewModel> ListagemFaturamentoServicoGrv { get; set; }
    }
}