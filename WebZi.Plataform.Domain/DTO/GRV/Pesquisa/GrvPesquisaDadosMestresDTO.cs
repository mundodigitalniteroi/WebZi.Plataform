using WebZi.Plataform.Domain.DTO.Faturamento;

namespace WebZi.Plataform.Domain.DTO.GRV.Pesquisa
{
    public class GrvPesquisaDadosMestresDTO
    {
        public ClienteSimplificadoListDTO ListagemCliente { get; set; }

        public ClienteDepositoSimplificadoListDTO ListagemDeposito { get; set; }

        public FaturamentoProdutoListDTO ListagemProduto { get; set; }

        //public ReboqueSimplificadoViewModelList ListagemReboque { get; set; }

        //public ReboquistaSimplificadoViewModelList ListagemReboquista { get; set; }

        public StatusOperacaoListDTO ListagemStatusOperacao { get; set; }
    }
}