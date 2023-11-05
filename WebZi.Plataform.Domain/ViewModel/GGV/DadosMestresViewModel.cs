using WebZi.Plataform.Domain.ViewModel.Empresa;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.Sistema;
using WebZi.Plataform.Domain.ViewModel.Veiculo;
using WebZi.Plataform.Domain.ViewModel.Vistoria;

namespace WebZi.Plataform.Domain.ViewModel.GGV
{
    public class DadosMestresViewModel
    {
        public MensagemViewModel Mensagem { get; set; }

        public CorViewModelList ListagemCorOstentada { get; set; }

        public EmpresaViewModelList ListagemEmpresa { get; set; }

        public TabelaGenericaViewModelList ListagemEstadoGeralVeiculo { get; set; }

        public VistoriaSituacaoChassiViewModelList ListagemSituacaoChassi { get; set; }

        public VistoriaStatusViewModelList ListagemStatusVistoria { get; set; }

        public TipoAvariaViewModelList ListagemTipoAvaria { get; set; }

        public TabelaGenericaViewModelList ListagemTipoCadastroFotoGGV { get; set; }

        public TabelaGenericaViewModelList ListagemTipoDirecao { get; set; }
    }
}