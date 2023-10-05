namespace WebZi.Plataform.Domain.ViewModel.Deposito
{
    public class DepositoViewModel
    {
        public int DepositoId { get; set; }

        public int? EmpresaId { get; set; }

        public int? CEPId { get; set; }

        public byte? TipoLogradouroId { get; set; }

        public int? BairroId { get; set; }

        public int? SistemaExternoId { get; set; }

        public string Descricao { get; set; }

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