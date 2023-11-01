namespace WebZi.Plataform.Domain.ViewModel.GGV.Cadastro
{
    public class CadastroVistoriaViewModel
    {
        public string FlagVistoria { get; set; }

        public string MotivoNaoRealizacaoVistoria { get; set;}

        public string FlagPossuiRestricoes { get; set; }

        public string FlagPossuiVidroEletrico { get; set; }

        public string FlagPossuiTravaEletrica { get; set; }

        public string FlagPossuiPlaca { get; set; }

        public string PlacaOstentada { get; set; }

        public int IdentificadorEmpresaVistoria { get; set; }

        public int IdentificadorStatusVistoria { get; set; }

        public int IdentificadorSituacaoChassi { get; set; }

        public int IdentificadorTipoDirecao { get; set; }

        public int IdentificadorEstadoGeralVeiculo { get; set; }

        public int IdentificadorCorOstentada { get; set; }

        public string NumeroVistoria { get; set; }

        public string NomeVistoriador { get; set; }

        public string NumeroMotor { get; set; }

        public DateTime DataVistoria { get; set; }

        public string ResumoVistoria { get; set; }
    }
}