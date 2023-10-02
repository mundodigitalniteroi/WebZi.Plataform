using WebZi.Plataform.Domain.Models.Atendimento;

namespace WebZi.Plataform.Domain.ViewModel.Atendimento
{
    public class QualificacaoResponsavelViewModelList
    {
        public MensagemViewModel Mensagem { get; set; }

        public List<QualificacaoResponsavelModel> QualificacoesResponsaveis { get; set; }
    }
}