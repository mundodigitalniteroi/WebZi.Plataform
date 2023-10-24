using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Domain.ViewModel.Faturamento
{
    public class FaturamentoProdutoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<FaturamentoProdutoViewModel> Listagem { get; set; }
    }
}