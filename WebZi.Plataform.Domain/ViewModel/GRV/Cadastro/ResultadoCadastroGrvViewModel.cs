namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class ResultadoCadastroGrvViewModel
    {
        public int IdentificadorGrv { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new();
    }
}