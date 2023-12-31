using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GGV
{
    public class GgvParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorProcesso { get; set; }

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

        public VistoriaParameters Vistoria { get; set; }

        public List<EquipamentoOpcionalParameters> ListagemEquipamentoOpcional { get; set; }

        public List<FotoTipoCadastroParameters> ListagemFotos { get; set; }

        public List<FaturamentoServicoGrvParameters> ListagemFaturamentoServicoGrv { get; set; }
    }
}