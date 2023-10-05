using WebZi.Plataform.Domain.Models.Banco;
using WebZi.Plataform.Domain.Models.Empresa;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Views.Localizacao;

namespace WebZi.Plataform.Domain.Models.Cliente
{
    public class ClienteModel
    {
        public int ClienteId { get; set; }

        public short AgenciaBancariaId { get; set; }

        public int CEPId { get; set; }

        public byte? TipoLogradouroId { get; set; }

        public int? BairroId { get; set; }

        public byte? TipoMeioCobrancaId { get; set; }

        public int? EmpresaId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string Nome { get; set; }

        public string CNPJ { get; set; }

        public string Logradouro { get; set; }

        public string NumeroEndereco { get; set; }

        public string ComplementoEndereco { get; set; }

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

        public string FlagUsarHoraDiaria { get; set; } = "S";

        public string FlagEmissaoNotaFiscal { get; set; } = "S";

        public string FlagCadastrarQuilometragem { get; set; } = "S";

        public string FlagCobrarDiariasDiasCorridos { get; set; } = "N";

        public string FlagClienteRealizaFaturamentoArrecadacao { get; set; } = "N";

        public string FlagEnderecoCadastroManual { get; set; } = "N";

        public string FlagPermiteAlteracaoTipoVeiculo { get; set; } = "N";

        public string FlagLancarIpvaMultas { get; set; } = "N";

        public string FlagPossuiClienteCodigoIdentificacao { get; set; } = "N";

        public string FlagAtivo { get; set; } = "S";

        public int? OrgaoExecutivoTransitoId { get; set; }

        public string CodigoOrgao { get; set; }

        public string FlagPossuiPixEstatico { get; set; } = "N";

        public byte? PixTipoChaveId { get; set; }

        public string PixChave { get; set; }

        public string FlagPossuiPixDinamico { get; set; } = "N";

        public virtual AgenciaBancariaModel AgenciaBancaria { get; set; }

        public virtual EmpresaModel Empresa { get; set; }

        public virtual ViewEnderecoCompletoModel Endereco { get; set; }

        public virtual TipoMeioCobrancaModel TipoMeioCobranca { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual UsuarioModel UsuarioAlteracao { get; set; }

        public virtual ICollection<FaturamentoRegraModel> FaturamentoRegras { get; set; }

        public virtual ICollection<FaturamentoServicoAssociadoModel> FaturamentoServicosAssociados { get; set; }

        public virtual ICollection<GrvModel> Grvs { get; set; }

        public virtual ICollection<ReboqueModel> Reboques { get; set; }

        public virtual ICollection<ReboquistaModel> Reboquistas { get; set; }

        public virtual ICollection<UsuarioClienteModel> UsuariosClientes { get; set; }

        //public virtual OrgaoExecutivoTransito OrgaoExecutivoTransito { get; set; }

        //public virtual ICollection<AlterdataContaBancaria> AlterdataContaBancaria { get; set; }

        //public virtual ICollection<ClienteRegra> ClienteRegras { get; set; }

        //public virtual ICollection<ClientesDeposito> ClientesDepositos { get; set; }

        //public virtual ICollection<Gtv> GtvIdClienteEnvio { get; set; }

        //public virtual ICollection<Gtv> GtvIdClienteRecebimento { get; set; }

        //public virtual ICollection<PixDinamicoConfiguracao> PixDinamicoConfiguracaos { get; set; }
    }
}