﻿namespace WebZi.Plataform.Domain.ViewModel.Vistoria
{
    public class VistoriaTipoDirecaoViewModelList
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public List<VistoriaTipoDirecaoViewModel> TipoDirecao { get; set; } = new();
    }
}