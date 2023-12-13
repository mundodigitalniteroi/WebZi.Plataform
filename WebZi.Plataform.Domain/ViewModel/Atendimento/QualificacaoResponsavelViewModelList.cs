namespace WebZi.Plataform.Domain.ViewModel.Atendimento
{
    public class QualificacaoResponsavelViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        // Não precisa criar uma ViewModel para esta entidade
        // porque são retornados todos os campos do Banco de Dados
        public List<QualificacaoResponsavelViewModel> Listagem { get; set; } = new();
    }
}