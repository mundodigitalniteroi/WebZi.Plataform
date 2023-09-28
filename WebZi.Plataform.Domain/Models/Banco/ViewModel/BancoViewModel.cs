﻿namespace WebZi.Plataform.Domain.Models.Banco.ViewModel
{
    public class BancoViewModel
    {
        public short BancoId { get; set; }

        public string CodigoFebraban { get; set; }

        public string Nome { get; set; }

        public string FlagAtivo { get; set; } = "S";
    }
}