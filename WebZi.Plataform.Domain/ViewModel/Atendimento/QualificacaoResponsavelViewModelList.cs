using WebZi.Plataform.Domain.Models.Atendimento;

namespace WebZi.Plataform.Domain.ViewModel.Atendimento
{
    public class QualificacaoResponsavelViewModelList
    {
        // Não precisa criar uma ViewModel para esta entidade
        // porque são retornados todos os campos do Banco de Dados
        public List<QualificacaoResponsavelModel> QualificacoesResponsaveis { get; set; }

        public MensagemViewModel Mensagem { get; set; } = new();
    }
}