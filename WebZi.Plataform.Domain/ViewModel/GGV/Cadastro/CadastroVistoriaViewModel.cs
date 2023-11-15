using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GGV.Cadastro
{
    public class CadastroVistoriaViewModel
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagVistoria { get; set; }

        public string MotivoNaoRealizacaoVistoria { get; set;}

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagPossuiRestricoes { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagPossuiVidroEletrico { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagPossuiTravaEletrica { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagPossuiPlaca { get; set; }

        [StringLength(7, MinimumLength = 7)]
        public string PlacaOstentada { get; set; }

        public int IdentificadorEmpresaVistoria { get; set; }

        public int IdentificadorStatusVistoria { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public byte IdentificadorSituacaoChassi { get; set; }

        public int IdentificadorTipoDirecao { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorEstadoGeralVeiculo { get; set; }

        public int IdentificadorCorOstentada { get; set; }

        public string NumeroVistoria { get; set; }

        public string NomeVistoriador { get; set; }

        public string NumeroMotor { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public DateTime DataVistoria { get; set; }

        public string ResumoVistoria { get; set; }
    }
}