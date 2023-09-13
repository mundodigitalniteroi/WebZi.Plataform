using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Models.Cliente
{
    public class ClienteModel
    {
        public int ClienteId { get; set; }

        public short AgenciaBancariaId { get; set; }

        public int CepId { get; set; }

        public byte? TipoLogradouroId { get; set; }

        public int? BairroId { get; set; }

        public byte? TipoMeioCobrancaId { get; set; }

        public int? EmpresaId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

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

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public char FlagUsarHoraDiaria { get; set; }

        public char FlagEmissaoNotaFiscalSap { get; set; }

        public char FlagCadastrarQuilometragem { get; set; }

        public char FlagCobrarDiariasDiasCorridos { get; set; }

        public char FlagClienteRealizaFaturamentoArrecadacao { get; set; }

        public char FlagEnderecoCadastroManual { get; set; }

        public char FlagPermiteAlteracaoTipoVeiculo { get; set; }

        public char FlagLancarIpvaMultas { get; set; }

        public char FlagPossuiClienteCodigoIdentificacao { get; set; }

        public char FlagAtivo { get; set; }

        public int? IdOrgaoExecutivoTransito { get; set; }

        public string CodigoOrgao { get; set; }

        public char FlagPossuiPixEstatico { get; set; }

        public byte? PixTipoChaveId { get; set; }

        public string PixChave { get; set; }

        public char FlagPossuiPixDinamico { get; set; }

        public string TipoPix { get; set; }

        public char FlagPossuiPix { get; set; }

        //public virtual TbDepAgenciasBancaria IdAgenciaBancariaNavigation { get; set; }

        //public virtual TbDepOrgaoExecutivoTransito IdOrgaoExecutivoTransitoNavigation { get; set; }

        //public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

        //public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

        //public virtual ICollection<TbDepAlterdataContaBancarium> TbDepAlterdataContaBancaria { get; set; } = new List<TbDepAlterdataContaBancarium>();

        //public virtual ICollection<TbDepClienteRegra> TbDepClienteRegras { get; set; } = new List<TbDepClienteRegra>();

        //public virtual ICollection<TbDepClientesDeposito> TbDepClientesDepositos { get; set; } = new List<TbDepClientesDeposito>();

        public virtual ICollection<FaturamentoRegraModel> FaturamentoRegras { get; set; }

        //public virtual ICollection<TbDepFaturamentoServicosAssociado> TbDepFaturamentoServicosAssociados { get; set; } = new List<TbDepFaturamentoServicosAssociado>();

        public virtual ICollection<GrvModel> Grvs { get; set; }

        //public virtual ICollection<TbDepGtv> TbDepGtvIdClienteEnvioNavigations { get; set; } = new List<TbDepGtv>();

        //public virtual ICollection<TbDepGtv> TbDepGtvIdClienteRecebimentoNavigations { get; set; } = new List<TbDepGtv>();

        //public virtual ICollection<TbDepPixDinamicoConfiguracao> TbDepPixDinamicoConfiguracaos { get; set; } = new List<TbDepPixDinamicoConfiguracao>();

        //public virtual ICollection<TbDepReboque> TbDepReboques { get; set; } = new List<TbDepReboque>();

        //public virtual ICollection<TbDepReboquista> TbDepReboquista { get; set; } = new List<TbDepReboquista>();

        //public virtual ICollection<TbDepUsuariosCliente> TbDepUsuariosClientes { get; set; } = new List<TbDepUsuariosCliente>();
    }
}