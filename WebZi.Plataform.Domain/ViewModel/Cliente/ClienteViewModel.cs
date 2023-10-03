namespace WebZi.Plataform.Domain.ViewModel.Cliente
{
    public class ClienteViewModel
    {
        public int ClienteId { get; set; }

        public short AgenciaBancariaId { get; set; }

        public int CepId { get; set; }

        public byte? TipoLogradouroId { get; set; }

        public int? BairroId { get; set; }

        public byte? TipoMeioCobrancaId { get; set; }

        public int? EmpresaId { get; set; }

        public string Nome { get; set; }

        public string Cnpj { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public decimal? GpsLatitude { get; set; }

        public decimal? GpsLongitude { get; set; }

        public decimal? MetragemTotal { get; set; }

        public decimal? MetragemGuarda { get; set; }

        public string HoraDiaria { get; set; }

        public short MaximoDiariasParaCobranca { get; set; }

        public short MaximoDiasVencimento { get; set; }

        public string CodigoSap { get; set; }

        public string LabelClienteCodigoIdentificacao { get; set; }

        public string FlagUsarHoraDiaria { get; set; }

        public string FlagEmissaoNotaFiscal { get; set; }

        public string FlagCadastrarQuilometragem { get; set; }

        public string FlagCobrarDiariasDiasCorridos { get; set; }

        public string FlagClienteRealizaFaturamentoArrecadacao { get; set; }

        public string FlagEnderecoCadastroManual { get; set; }

        public string FlagPermiteAlteracaoTipoVeiculo { get; set; }

        public string FlagLancarIpvaMultas { get; set; }

        public string FlagPossuiClienteCodigoIdentificacao { get; set; }

        public string FlagAtivo { get; set; }

        public int? OrgaoExecutivoTransitoId { get; set; }

        public string CodigoOrgao { get; set; }

        public string FlagPossuiPixEstatico { get; set; }

        public byte? PixTipoChaveId { get; set; }

        public string PixChave { get; set; }

        public string FlagPossuiPixDinamico { get; set; }

        public string FlagPossuiPix { get; set; }
    }
}