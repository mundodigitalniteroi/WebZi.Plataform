using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

/// <summary>
/// Esta coluna informa se o Cliente possui um Código que representa o GRV em seu próprio cadastro, caso positivo o Depósito Público habilita a coluna label_cliente_codigo_identificacao.
/// </summary>
public partial class TbDepCliente
{
    public int IdCliente { get; set; }

    public short IdAgenciaBancaria { get; set; }

    public int IdCep { get; set; }

    public byte? IdTipoLogradouro { get; set; }

    public int? IdBairro { get; set; }

    public byte? IdTipoMeioCobranca { get; set; }

    public int? IdEmpresa { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

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

    public string FlagEmissaoNotaFiscalSap { get; set; }

    public string FlagCadastrarQuilometragem { get; set; }

    public string FlagCobrarDiariasDiasCorridos { get; set; }

    public string FlagClienteRealizaFaturamentoArrecadacao { get; set; }

    public string FlagEnderecoCadastroManual { get; set; }

    public string FlagPermiteAlteracaoTipoVeiculo { get; set; }

    public string FlagLancarIpvaMultas { get; set; }

    public string FlagPossuiClienteCodigoIdentificacao { get; set; }

    public string FlagAtivo { get; set; }

    public int? IdOrgaoExecutivoTransito { get; set; }

    public string CodigoOrgao { get; set; }

    public string FlagPossuiPixEstatico { get; set; }

    public byte? PixTipoChaveId { get; set; }

    public string PixChave { get; set; }

    public string FlagPossuiPixDinamico { get; set; }

    public string TipoPix { get; set; }

    public string FlagPossuiPix { get; set; }

    public virtual TbDepAgenciasBancaria IdAgenciaBancariaNavigation { get; set; }

    public virtual TbDepOrgaoExecutivoTransito IdOrgaoExecutivoTransitoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepAlterdataContaBancarium> TbDepAlterdataContaBancaria { get; set; } = new List<TbDepAlterdataContaBancarium>();

    public virtual ICollection<TbDepClienteRegra> TbDepClienteRegras { get; set; } = new List<TbDepClienteRegra>();

    public virtual ICollection<TbDepClientesDeposito> TbDepClientesDepositos { get; set; } = new List<TbDepClientesDeposito>();

    public virtual ICollection<TbDepFaturamentoRegra> TbDepFaturamentoRegras { get; set; } = new List<TbDepFaturamentoRegra>();

    public virtual ICollection<TbDepFaturamentoServicosAssociado> TbDepFaturamentoServicosAssociados { get; set; } = new List<TbDepFaturamentoServicosAssociado>();

    public virtual ICollection<TbDepGrv> TbDepGrvs { get; set; } = new List<TbDepGrv>();

    public virtual ICollection<TbDepGtv> TbDepGtvIdClienteEnvioNavigations { get; set; } = new List<TbDepGtv>();

    public virtual ICollection<TbDepGtv> TbDepGtvIdClienteRecebimentoNavigations { get; set; } = new List<TbDepGtv>();

    public virtual ICollection<TbDepPixDinamicoConfiguracao> TbDepPixDinamicoConfiguracaos { get; set; } = new List<TbDepPixDinamicoConfiguracao>();

    public virtual ICollection<TbDepReboque> TbDepReboques { get; set; } = new List<TbDepReboque>();

    public virtual ICollection<TbDepReboquista> TbDepReboquista { get; set; } = new List<TbDepReboquista>();

    public virtual ICollection<TbDepUsuariosCliente> TbDepUsuariosClientes { get; set; } = new List<TbDepUsuariosCliente>();
}
