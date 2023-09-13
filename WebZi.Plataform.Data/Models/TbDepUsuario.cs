using System;
using System.Collections.Generic;

namespace WebZi.Plataform.Data.Models;

public partial class TbDepUsuario
{
    public int IdUsuario { get; set; }

    public int IdUsuarioCadastro { get; set; }

    public int? IdUsuarioAlteracao { get; set; }

    public string Login { get; set; }

    public string Senha1 { get; set; }

    public string Senha2 { get; set; }

    public string Senha3 { get; set; }

    public string Senha4 { get; set; }

    public string Senha5 { get; set; }

    public string SenhaAndroid { get; set; }

    public string Email { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public DateTime DataCadastroSenha { get; set; }

    public DateTime? DataUltimoAcesso { get; set; }

    public string FlagPermissaoDesconto { get; set; }

    public string FlagPermissaoDataRetroativaFaturamento { get; set; }

    public string FlagReceberEmailErro { get; set; }

    public string FlagAtivo { get; set; }

    public long? PessoaId { get; set; }

    public string Matricula { get; set; }

    public byte? IdTipoOperador { get; set; }

    public int? IdFuncionario { get; set; }

    public string Dummy { get; set; }

    public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

    public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

    public virtual ICollection<TbDepUsuario> InverseIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepUsuario>();

    public virtual ICollection<TbDepUsuario> InverseIdUsuarioCadastroNavigation { get; set; } = new List<TbDepUsuario>();

    public virtual ICollection<TbDepAgenciasBancaria> TbDepAgenciasBancariaIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepAgenciasBancaria>();

    public virtual ICollection<TbDepAgenciasBancaria> TbDepAgenciasBancariaIdUsuarioCadastroNavigations { get; set; } = new List<TbDepAgenciasBancaria>();

    public virtual ICollection<TbDepAgente> TbDepAgenteUsuarioAlteracaos { get; set; } = new List<TbDepAgente>();

    public virtual ICollection<TbDepAgente> TbDepAgenteUsuarioCadastros { get; set; } = new List<TbDepAgente>();

    public virtual ICollection<TbDepAlterdatum> TbDepAlterdata { get; set; } = new List<TbDepAlterdatum>();

    public virtual ICollection<TbDepAlterdataConfiguracao> TbDepAlterdataConfiguracaoUsuarioAlteracaos { get; set; } = new List<TbDepAlterdataConfiguracao>();

    public virtual ICollection<TbDepAlterdataConfiguracao> TbDepAlterdataConfiguracaoUsuarioCadastros { get; set; } = new List<TbDepAlterdataConfiguracao>();

    public virtual ICollection<TbDepAlterdataOperacao> TbDepAlterdataOperacaoUsuarioAlteracaos { get; set; } = new List<TbDepAlterdataOperacao>();

    public virtual ICollection<TbDepAlterdataOperacao> TbDepAlterdataOperacaoUsuarioCadastros { get; set; } = new List<TbDepAlterdataOperacao>();

    public virtual ICollection<TbDepAtendimento> TbDepAtendimentoIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepAtendimento>();

    public virtual ICollection<TbDepAtendimento> TbDepAtendimentoIdUsuarioCadastroNavigations { get; set; } = new List<TbDepAtendimento>();

    public virtual ICollection<TbDepAutoridadesResponsavei> TbDepAutoridadesResponsaveiIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepAutoridadesResponsavei>();

    public virtual ICollection<TbDepAutoridadesResponsavei> TbDepAutoridadesResponsaveiIdUsuarioCadastroNavigations { get; set; } = new List<TbDepAutoridadesResponsavei>();

    public virtual ICollection<TbDepBanco> TbDepBancoIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepBanco>();

    public virtual ICollection<TbDepBanco> TbDepBancoIdUsuarioCadastroNavigations { get; set; } = new List<TbDepBanco>();

    public virtual ICollection<TbDepClassificaco> TbDepClassificacos { get; set; } = new List<TbDepClassificaco>();

    public virtual ICollection<TbDepClienteDepositoTiposVeiculo> TbDepClienteDepositoTiposVeiculoIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepClienteDepositoTiposVeiculo>();

    public virtual ICollection<TbDepClienteDepositoTiposVeiculo> TbDepClienteDepositoTiposVeiculoIdUsuarioCadastroNavigations { get; set; } = new List<TbDepClienteDepositoTiposVeiculo>();

    public virtual ICollection<TbDepCliente> TbDepClienteIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepCliente>();

    public virtual ICollection<TbDepCliente> TbDepClienteIdUsuarioCadastroNavigations { get; set; } = new List<TbDepCliente>();

    public virtual ICollection<TbDepClienteRegra> TbDepClienteRegraUsuarioAlteracaos { get; set; } = new List<TbDepClienteRegra>();

    public virtual ICollection<TbDepClienteRegra> TbDepClienteRegraUsuarioCadastros { get; set; } = new List<TbDepClienteRegra>();

    public virtual ICollection<TbDepClientesDeposito> TbDepClientesDepositoIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepClientesDeposito>();

    public virtual ICollection<TbDepClientesDeposito> TbDepClientesDepositoIdUsuarioCadastroNavigations { get; set; } = new List<TbDepClientesDeposito>();

    public virtual ICollection<TbDepComunicacaoEmail> TbDepComunicacaoEmails { get; set; } = new List<TbDepComunicacaoEmail>();

    public virtual ICollection<TbDepCondutorEquipamentosOpcionai> TbDepCondutorEquipamentosOpcionaiIdUsuarioAtualizacaoNavigations { get; set; } = new List<TbDepCondutorEquipamentosOpcionai>();

    public virtual ICollection<TbDepCondutorEquipamentosOpcionai> TbDepCondutorEquipamentosOpcionaiIdUsuarioCadastroNavigations { get; set; } = new List<TbDepCondutorEquipamentosOpcionai>();

    public virtual ICollection<TbDepCondutorEquipamentosOpcionaisNaoConformidade> TbDepCondutorEquipamentosOpcionaisNaoConformidades { get; set; } = new List<TbDepCondutorEquipamentosOpcionaisNaoConformidade>();

    public virtual ICollection<TbDepDeposito> TbDepDepositoIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepDeposito>();

    public virtual ICollection<TbDepDeposito> TbDepDepositoIdUsuarioCadastroNavigations { get; set; } = new List<TbDepDeposito>();

    public virtual ICollection<TbDepDetranGrvStatusTransacao> TbDepDetranGrvStatusTransacaos { get; set; } = new List<TbDepDetranGrvStatusTransacao>();

    public virtual ICollection<TbDepDetroGrv> TbDepDetroGrvIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepDetroGrv>();

    public virtual ICollection<TbDepDetroGrv> TbDepDetroGrvIdUsuarioCadastroNavigations { get; set; } = new List<TbDepDetroGrv>();

    public virtual ICollection<TbDepDetroGrvMotivoNaoAutorizado> TbDepDetroGrvMotivoNaoAutorizados { get; set; } = new List<TbDepDetroGrvMotivoNaoAutorizado>();

    public virtual ICollection<TbDepEnquadramentoInfraco> TbDepEnquadramentoInfracos { get; set; } = new List<TbDepEnquadramentoInfraco>();

    public virtual ICollection<TbDepEquipamentosOpcionai> TbDepEquipamentosOpcionaiIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepEquipamentosOpcionai>();

    public virtual ICollection<TbDepEquipamentosOpcionai> TbDepEquipamentosOpcionaiIdUsuarioNavigations { get; set; } = new List<TbDepEquipamentosOpcionai>();

    public virtual ICollection<TbDepEquipamentosOpcionaisLocalizacao> TbDepEquipamentosOpcionaisLocalizacaoIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepEquipamentosOpcionaisLocalizacao>();

    public virtual ICollection<TbDepEquipamentosOpcionaisLocalizacao> TbDepEquipamentosOpcionaisLocalizacaoIdUsuarioCadastroNavigations { get; set; } = new List<TbDepEquipamentosOpcionaisLocalizacao>();

    public virtual ICollection<TbDepFaturamentoBoleto> TbDepFaturamentoBoletos { get; set; } = new List<TbDepFaturamentoBoleto>();

    public virtual ICollection<TbDepFaturamentoComposicao> TbDepFaturamentoComposicaoIdUsuarioAlteracaoQuantidadeNavigations { get; set; } = new List<TbDepFaturamentoComposicao>();

    public virtual ICollection<TbDepFaturamentoComposicao> TbDepFaturamentoComposicaoIdUsuarioDescontoNavigations { get; set; } = new List<TbDepFaturamentoComposicao>();

    public virtual ICollection<TbDepFaturamento> TbDepFaturamentoIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepFaturamento>();

    public virtual ICollection<TbDepFaturamento> TbDepFaturamentoIdUsuarioCadastroNavigations { get; set; } = new List<TbDepFaturamento>();

    public virtual ICollection<TbDepFaturamentoRegra> TbDepFaturamentoRegraIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepFaturamentoRegra>();

    public virtual ICollection<TbDepFaturamentoRegra> TbDepFaturamentoRegraIdUsuarioCadastroNavigations { get; set; } = new List<TbDepFaturamentoRegra>();

    public virtual ICollection<TbDepFaturamentoServicosAssociado> TbDepFaturamentoServicosAssociadoIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepFaturamentoServicosAssociado>();

    public virtual ICollection<TbDepFaturamentoServicosAssociado> TbDepFaturamentoServicosAssociadoIdUsuarioCadastroNavigations { get; set; } = new List<TbDepFaturamentoServicosAssociado>();

    public virtual ICollection<TbDepFaturamentoServicosTipo> TbDepFaturamentoServicosTipoIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepFaturamentoServicosTipo>();

    public virtual ICollection<TbDepFaturamentoServicosTipo> TbDepFaturamentoServicosTipoIdUsuarioCadastroNavigations { get; set; } = new List<TbDepFaturamentoServicosTipo>();

    public virtual ICollection<TbDepGrvBloqueioMotivo> TbDepGrvBloqueioMotivos { get; set; } = new List<TbDepGrvBloqueioMotivo>();

    public virtual ICollection<TbDepGrvBloqueio> TbDepGrvBloqueios { get; set; } = new List<TbDepGrvBloqueio>();

    public virtual ICollection<TbDepGrvClientesCodigoIdentificacao> TbDepGrvClientesCodigoIdentificacaoIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepGrvClientesCodigoIdentificacao>();

    public virtual ICollection<TbDepGrvClientesCodigoIdentificacao> TbDepGrvClientesCodigoIdentificacaoIdUsuarioCadastroNavigations { get; set; } = new List<TbDepGrvClientesCodigoIdentificacao>();

    public virtual ICollection<TbDepGrvCobrancasLegai> TbDepGrvCobrancasLegais { get; set; } = new List<TbDepGrvCobrancasLegai>();

    public virtual ICollection<TbDepGrvDocumento> TbDepGrvDocumentoUsuarioCadastros { get; set; } = new List<TbDepGrvDocumento>();

    public virtual ICollection<TbDepGrvDocumento> TbDepGrvDocumentoUsuarioExclusaos { get; set; } = new List<TbDepGrvDocumento>();

    public virtual ICollection<TbDepGrvDrfaAgendamentoRetiradum> TbDepGrvDrfaAgendamentoRetirada { get; set; } = new List<TbDepGrvDrfaAgendamentoRetiradum>();

    public virtual ICollection<TbDepGrvDrfa> TbDepGrvDrfaIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepGrvDrfa>();

    public virtual ICollection<TbDepGrvDrfa> TbDepGrvDrfaIdUsuarioCadastroNavigations { get; set; } = new List<TbDepGrvDrfa>();

    public virtual ICollection<TbDepGrvFoto> TbDepGrvFotos { get; set; } = new List<TbDepGrvFoto>();

    public virtual ICollection<TbDepGrv> TbDepGrvIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepGrv>();

    public virtual ICollection<TbDepGrv> TbDepGrvIdUsuarioCadastroGgvNavigations { get; set; } = new List<TbDepGrv>();

    public virtual ICollection<TbDepGrv> TbDepGrvIdUsuarioCadastroNavigations { get; set; } = new List<TbDepGrv>();

    public virtual ICollection<TbDepGrv> TbDepGrvIdUsuarioEdicaoNavigations { get; set; } = new List<TbDepGrv>();

    public virtual ICollection<TbDepGrvLacre> TbDepGrvLacreIdUsuarioAtualizacaoNavigations { get; set; } = new List<TbDepGrvLacre>();

    public virtual ICollection<TbDepGrvLacre> TbDepGrvLacreIdUsuarioCadastroNavigations { get; set; } = new List<TbDepGrvLacre>();

    public virtual ICollection<TbDepGrvVistorium> TbDepGrvVistoriumIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepGrvVistorium>();

    public virtual ICollection<TbDepGrvVistorium> TbDepGrvVistoriumIdUsuarioCadastroNavigations { get; set; } = new List<TbDepGrvVistorium>();

    public virtual ICollection<TbDepGtv> TbDepGtvIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepGtv>();

    public virtual ICollection<TbDepGtv> TbDepGtvIdUsuarioCadastroNavigations { get; set; } = new List<TbDepGtv>();

    public virtual ICollection<TbDepGtv> TbDepGtvIdUsuarioSeparacaoVeiculosNavigations { get; set; } = new List<TbDepGtv>();

    public virtual ICollection<TbDepLiberacaoEspecial> TbDepLiberacaoEspecials { get; set; } = new List<TbDepLiberacaoEspecial>();

    public virtual ICollection<TbDepLiberacaoLeilao> TbDepLiberacaoLeilaos { get; set; } = new List<TbDepLiberacaoLeilao>();

    public virtual ICollection<TbDepLiberacao> TbDepLiberacaos { get; set; } = new List<TbDepLiberacao>();

    public virtual ICollection<TbDepMarcasModelo> TbDepMarcasModelos { get; set; } = new List<TbDepMarcasModelo>();

    public virtual ICollection<TbDepNfeConfiguracaoImagem> TbDepNfeConfiguracaoImagemUsuarioAlteracaos { get; set; } = new List<TbDepNfeConfiguracaoImagem>();

    public virtual ICollection<TbDepNfeConfiguracaoImagem> TbDepNfeConfiguracaoImagemUsuarioCadastros { get; set; } = new List<TbDepNfeConfiguracaoImagem>();

    public virtual ICollection<TbDepNfeNotaFiscal> TbDepNfeNotaFiscals { get; set; } = new List<TbDepNfeNotaFiscal>();

    public virtual ICollection<TbDepNfeRegra> TbDepNfeRegraUsuarioAlteracaos { get; set; } = new List<TbDepNfeRegra>();

    public virtual ICollection<TbDepNfeRegra> TbDepNfeRegraUsuarioCadastros { get; set; } = new List<TbDepNfeRegra>();

    public virtual ICollection<TbDepNfeWsErro> TbDepNfeWsErros { get; set; } = new List<TbDepNfeWsErro>();

    public virtual ICollection<TbDepNfe> TbDepNves { get; set; } = new List<TbDepNfe>();

    public virtual ICollection<TbDepPixDinamicoConfiguracao> TbDepPixDinamicoConfiguracaoUsuarioAlteracaos { get; set; } = new List<TbDepPixDinamicoConfiguracao>();

    public virtual ICollection<TbDepPixDinamicoConfiguracao> TbDepPixDinamicoConfiguracaoUsuarioCadastros { get; set; } = new List<TbDepPixDinamicoConfiguracao>();

    public virtual ICollection<TbDepPixDinamicoSenhaConfirmacaoTranferencium> TbDepPixDinamicoSenhaConfirmacaoTranferenciumUsuarioCadastros { get; set; } = new List<TbDepPixDinamicoSenhaConfirmacaoTranferencium>();

    public virtual ICollection<TbDepPixDinamicoSenhaConfirmacaoTranferencium> TbDepPixDinamicoSenhaConfirmacaoTranferenciumUsuarioFinanceiros { get; set; } = new List<TbDepPixDinamicoSenhaConfirmacaoTranferencium>();

    public virtual ICollection<TbDepPreGrv> TbDepPreGrvs { get; set; } = new List<TbDepPreGrv>();

    public virtual ICollection<TbDepReboquista> TbDepReboquistaIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepReboquista>();

    public virtual ICollection<TbDepReboquista> TbDepReboquistaIdUsuarioCadastroNavigations { get; set; } = new List<TbDepReboquista>();

    public virtual ICollection<TbDepRepositorioArquivo> TbDepRepositorioArquivos { get; set; } = new List<TbDepRepositorioArquivo>();

    public virtual ICollection<TbDepSistemaAcesso> TbDepSistemaAcessoIdUsuarioAcessoNavigations { get; set; } = new List<TbDepSistemaAcesso>();

    public virtual ICollection<TbDepSistemaAcesso> TbDepSistemaAcessoIdUsuarioNavigations { get; set; } = new List<TbDepSistemaAcesso>();

    public virtual ICollection<TbDepSistemaPerfilAcesso> TbDepSistemaPerfilAcessoIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepSistemaPerfilAcesso>();

    public virtual ICollection<TbDepSistemaPerfilAcesso> TbDepSistemaPerfilAcessoIdUsuarioCadastroNavigations { get; set; } = new List<TbDepSistemaPerfilAcesso>();

    public virtual ICollection<TbDepSistemaPerfilAcessoUsuario> TbDepSistemaPerfilAcessoUsuarios { get; set; } = new List<TbDepSistemaPerfilAcessoUsuario>();

    public virtual ICollection<TbDepSolicitacaoReboque> TbDepSolicitacaoReboqueIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepSolicitacaoReboque>();

    public virtual ICollection<TbDepSolicitacaoReboque> TbDepSolicitacaoReboqueIdUsuarioCadastroNavigations { get; set; } = new List<TbDepSolicitacaoReboque>();

    public virtual ICollection<TbDepTarifa> TbDepTarifaIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepTarifa>();

    public virtual ICollection<TbDepTarifa> TbDepTarifaIdUsuarioCadastroNavigations { get; set; } = new List<TbDepTarifa>();

    public virtual ICollection<TbDepTipoVeiculo> TbDepTipoVeiculoIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepTipoVeiculo>();

    public virtual ICollection<TbDepTipoVeiculo> TbDepTipoVeiculoIdUsuarioCadastroNavigations { get; set; } = new List<TbDepTipoVeiculo>();

    public virtual ICollection<TbDepTipoVeiculosEquipamentosAssociacao> TbDepTipoVeiculosEquipamentosAssociacaos { get; set; } = new List<TbDepTipoVeiculosEquipamentosAssociacao>();

    public virtual ICollection<TbDepTiposCombustivei> TbDepTiposCombustiveis { get; set; } = new List<TbDepTiposCombustivei>();

    public virtual ICollection<TbDepUsuariosCliente> TbDepUsuariosClienteIdUsuarioCadastroNavigations { get; set; } = new List<TbDepUsuariosCliente>();

    public virtual ICollection<TbDepUsuariosCliente> TbDepUsuariosClienteIdUsuarioNavigations { get; set; } = new List<TbDepUsuariosCliente>();

    public virtual ICollection<TbDepUsuariosDeposito> TbDepUsuariosDepositoIdUsuarioCadastroNavigations { get; set; } = new List<TbDepUsuariosDeposito>();

    public virtual ICollection<TbDepUsuariosDeposito> TbDepUsuariosDepositoIdUsuarioNavigations { get; set; } = new List<TbDepUsuariosDeposito>();

    public virtual ICollection<TbDepUsuariosPermisso> TbDepUsuariosPermissoIdUsuarioAlteracaoNavigations { get; set; } = new List<TbDepUsuariosPermisso>();

    public virtual ICollection<TbDepUsuariosPermisso> TbDepUsuariosPermissoIdUsuarioCadastroNavigations { get; set; } = new List<TbDepUsuariosPermisso>();

    public virtual ICollection<TbDepUsuariosPermisso> TbDepUsuariosPermissoIdUsuarioNavigations { get; set; } = new List<TbDepUsuariosPermisso>();

    public virtual ICollection<TbLogClienteDepositoTiposVeiculo> TbLogClienteDepositoTiposVeiculos { get; set; } = new List<TbLogClienteDepositoTiposVeiculo>();

    public virtual ICollection<TbLogDetroGrv> TbLogDetroGrvs { get; set; } = new List<TbLogDetroGrv>();
}
