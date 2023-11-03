using WebZi.Plataform.Domain.ViewModel.GGV.Cadastro;

namespace WebZi.Plataform.Domain.ViewModel.GGV
{
    public class GgvPersistenciaViewModel
    {
        public int IdentificadorGrv { get; set; }

        public int IdentificadorUsuario { get; set; }

        public DateTime DataHoraGuarda { get; set; }

        public DateTime? DataTransbordo { get; set; }

        public string FlagChaveDeposito { get; set; }

        public string NumeroChave { get; set; }

        public string EstacionamentoSetor { get; set; }

        public string EstacionamentoNumeroVaga { get; set; }

        public string FlagTransbordo { get; set; }

        public List<CadastroEquipamentoOpcionalViewModel> ListagemEquipamentoOpcional { get; set; }

        public CadastroVistoriaViewModel Vistoria { get; set; }

        public List<CadastroFotoTipoCadastroViewModel> Fotos { get; set; }
    }
}