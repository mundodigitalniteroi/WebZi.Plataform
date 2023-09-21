using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.Models.Usuario;

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

        public string TipoPix { get; set; }

        public string FlagPossuiPix { get; set; }

        public virtual TipoMeioCobrancaModel TipoMeioCobranca { get; set; }

        //public virtual AgenciasBancaria AgenciaBancaria { get; set; }

        //public virtual OrgaoExecutivoTransito OrgaoExecutivoTransito { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual UsuarioModel UsuarioAlteracao { get; set; }

        //public virtual ICollection<AlterdataContaBancarium> AlterdataContaBancaria { get; set; }

        //public virtual ICollection<ClienteRegra> ClienteRegras { get; set; }

        //public virtual ICollection<ClientesDeposito> ClientesDepositos { get; set; }

        public virtual ICollection<FaturamentoRegraModel> FaturamentoRegras { get; set; }

        public virtual ICollection<FaturamentoServicoAssociadoModel> FaturamentoServicosAssociados { get; set; }

        public virtual ICollection<GrvModel> Grvs { get; set; }

        //public virtual ICollection<Gtv> GtvIdClienteEnvioNavigations { get; set; }

        //public virtual ICollection<Gtv> GtvIdClienteRecebimentoNavigations { get; set; }

        //public virtual ICollection<PixDinamicoConfiguracao> PixDinamicoConfiguracaos { get; set; }

        public virtual ICollection<ReboqueModel> Reboques { get; set; }

        public virtual ICollection<ReboquistaModel> Reboquistas { get; set; }

        //public virtual ICollection<UsuariosCliente> UsuariosClientes { get; set; }
    }
}