using WebZi.Plataform.Domain.ViewModel.Faturamento;

namespace WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa
{
    public class GrvPesquisaDadosMestresViewModel
    {
        public ClienteSimplificadoViewModelList ListagemCliente { get; set; }

        public ClienteDepositoSimplificadoViewModelList ListagemDeposito { get; set; }

        public FaturamentoProdutoViewModelList ListagemProduto { get; set; }

        //public ReboqueSimplificadoViewModelList ListagemReboque { get; set; }

        //public ReboquistaSimplificadoViewModelList ListagemReboquista { get; set; }

        public StatusOperacaoViewModelList ListagemStatusOperacao { get; set; }
    }
}