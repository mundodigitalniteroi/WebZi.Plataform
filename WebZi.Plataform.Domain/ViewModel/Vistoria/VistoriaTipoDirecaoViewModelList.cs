﻿namespace WebZi.Plataform.Domain.ViewModel.Vistoria
{
    public class VistoriaTipoDirecaoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<VistoriaTipoDirecaoViewModel> Listagem { get; set; } = new();
    }
}