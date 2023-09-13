using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class VwDepAtendimentoLiberado
{
    public int AtendimentoId { get; set; }

    public int GrvId { get; set; }

    public string NumeroFormularioGrv { get; set; }

    public int ClienteDepositoId { get; set; }

    public byte QualificacaoResponsavelId { get; set; }

    public string ResponsavelNome { get; set; }

    public string ResponsavelDocumento { get; set; }

    public string ResponsavelCnh { get; set; }

    public string ResponsavelEndereco { get; set; }

    public string ResponsavelNumero { get; set; }

    public string ResponsavelComplemento { get; set; }

    public string ResponsavelBairro { get; set; }

    public string ResponsavelMunicipio { get; set; }

    public string ResponsavelUf { get; set; }

    public string ResponsavelCep { get; set; }

    public string ResponsavelDdd { get; set; }

    public string ResponsavelTelefone { get; set; }

    public string ProprietarioNome { get; set; }

    public string ProprietarioTipoDocumento { get; set; }

    public byte? ProprietarioIdTipoDocumento { get; set; }

    public string ProprietarioDocumento { get; set; }

    public string FormaLiberacao { get; set; }

    public string FormaLiberacaoNome { get; set; }

    public string FormaLiberacaoCnh { get; set; }

    public string FormaLiberacaoCpf { get; set; }

    public string FormaLiberacaoPlaca { get; set; }

    public string ProprietarioEndereco { get; set; }

    public string ProprietarioNumero { get; set; }

    public string ProprietarioComplemento { get; set; }

    public string ProprietarioBairro { get; set; }

    public string ProprietarioMunicipio { get; set; }

    public string ProprietarioUf { get; set; }

    public string ProprietarioCep { get; set; }

    public string ProprietarioDdd { get; set; }

    public string ProprietarioTelefone { get; set; }

    public string NotaFiscalNome { get; set; }

    public string NotaFiscalCpf { get; set; }

    public string NotaFiscalTipoLogradouro { get; set; }

    public string NotaFiscalEndereco { get; set; }

    public string NotaFiscalNumero { get; set; }

    public string NotaFiscalComplemento { get; set; }

    public string NotaFiscalBairro { get; set; }

    public string NotaFiscalMunicipio { get; set; }

    public string NotaFiscalUf { get; set; }

    public string NotaFiscalCep { get; set; }

    public string NotaFiscalDdd { get; set; }

    public string NotaFiscalTelefone { get; set; }

    public string NotaFiscalEmail { get; set; }

    public int TotalImpressoes { get; set; }

    public string StatusCadastroSap { get; set; }

    public string StatusCadastroOrdensVendaSap { get; set; }

    public DateTime? DataHoraInicioAtendimento { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public string FlagPagamentoFinanciado { get; set; }

    public string FlagAtendimentoWs { get; set; }

    public int ClienteId { get; set; }

    public string Cliente { get; set; }

    public int DepositoId { get; set; }

    public string Deposito { get; set; }

    public int? UsuarioEntregaId { get; set; }

    public DateTime? DataEntrega { get; set; }
}
