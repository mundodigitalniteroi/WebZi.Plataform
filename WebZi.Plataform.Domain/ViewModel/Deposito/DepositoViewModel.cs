namespace WebZi.Plataform.Domain.ViewModel.Deposito
{
    public class DepositoViewModel
    {
        public int IdentificadorDeposito { get; set; }

        public int? IdentificadorEmpresa { get; set; }

        public int? IdentificadorCEP { get; set; }

        public byte? IdentificadorTipoLogradouro { get; set; }

        public int? IdentificadorBairro { get; set; }

        public int? IdentificadorSistemaExterno { get; set; }

        public string Nome { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string EmailNfe { get; set; }

        public byte GrvMinimoFotosExigidas { get; set; }

        public byte GrvLimiteMinimoDatahoraGuarda { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string EnderecoMob { get; set; }

        public string TelefoneMob { get; set; }

        public string FlagEnderecoCadastroManual { get; set; }

        public string FlagAtivo { get; set; }

        public string FlagVirtual { get; set; }
    }
}