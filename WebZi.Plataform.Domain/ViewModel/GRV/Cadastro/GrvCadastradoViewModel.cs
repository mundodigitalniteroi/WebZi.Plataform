namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class GrvCadastradoViewModel
    {
        public int GrvId { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new();
    }
}