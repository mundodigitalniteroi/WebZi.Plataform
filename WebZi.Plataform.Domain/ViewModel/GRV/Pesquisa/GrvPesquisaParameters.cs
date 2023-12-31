﻿using System.ComponentModel.DataAnnotations;

namespace WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa
{
    public class GrvPesquisaParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public List<string> ListagemCodigoProduto { get; set; } = new();

        public List<string> ListagemStatusOperacao { get; set; } = new();

        public string NumeroProcesso { get; set; }

        public string PlacaVeiculo { get; set; }

        public string Chassi { get; set; }

        public string FlagVeiculoNaoIdentificado { get; set; }

        public DateTime? DataInicialRemocao { get; set; }

        public DateTime? DataFinalRemocao { get; set; }

        public int? IdentificadorCliente { get; set; }

        public int? IdentificadorDeposito { get; set; }

        public string PlacaReboque { get; set; }

        public string NomeReboquista { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }
    }
}