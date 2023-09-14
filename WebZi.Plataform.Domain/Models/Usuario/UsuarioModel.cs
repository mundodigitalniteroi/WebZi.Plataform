using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Veiculo;

namespace WebZi.Plataform.Domain.Models.Usuario
{
    public class UsuarioModel
    {
        public int UsuarioId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

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

        public byte? TipoOperadorId { get; set; }

        public int? FuncionarioId { get; set; }

        //public virtual TbDepUsuario IdUsuarioAlteracaoNavigation { get; set; }

        //public virtual TbDepUsuario IdUsuarioCadastroNavigation { get; set; }

        //public virtual ICollection<TbDepUsuario> InverseIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepUsuario>();

        //public virtual ICollection<TbDepUsuario> InverseIdUsuarioCadastroNavigation { get; set; } = new List<TbDepUsuario>();

        //public virtual ICollection<TbDepAgenciasBancaria> TbDepAgenciasBancariaIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepAgenciasBancaria>();

        //public virtual ICollection<TbDepAgenciasBancaria> TbDepAgenciasBancariaIdUsuarioCadastroNavigation { get; set; } = new List<TbDepAgenciasBancaria>();

        //public virtual ICollection<TbDepAgente> TbDepAgenteUsuarioAlteracaos { get; set; } = new List<TbDepAgente>();

        //public virtual ICollection<TbDepAgente> TbDepAgenteUsuarioCadastros { get; set; } = new List<TbDepAgente>();

        //public virtual ICollection<TbDepAlterdatum> TbDepAlterdata { get; set; } = new List<TbDepAlterdatum>();

        //public virtual ICollection<TbDepAlterdataConfiguracao> TbDepAlterdataConfiguracaoUsuarioAlteracaos { get; set; } = new List<TbDepAlterdataConfiguracao>();

        //public virtual ICollection<TbDepAlterdataConfiguracao> TbDepAlterdataConfiguracaoUsuarioCadastros { get; set; } = new List<TbDepAlterdataConfiguracao>();

        //public virtual ICollection<TbDepAlterdataOperacao> TbDepAlterdataOperacaoUsuarioAlteracaos { get; set; } = new List<TbDepAlterdataOperacao>();

        //public virtual ICollection<TbDepAlterdataOperacao> TbDepAlterdataOperacaoUsuarioCadastros { get; set; } = new List<TbDepAlterdataOperacao>();

        //public virtual ICollection<TbDepAutoridadesResponsavei> TbDepAutoridadesResponsaveiIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepAutoridadesResponsavei>();

        //public virtual ICollection<TbDepAutoridadesResponsavei> TbDepAutoridadesResponsaveiIdUsuarioCadastroNavigation { get; set; } = new List<TbDepAutoridadesResponsavei>();

        //public virtual ICollection<TbDepBanco> TbDepBancoIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepBanco>();

        //public virtual ICollection<TbDepBanco> TbDepBancoIdUsuarioCadastroNavigation { get; set; } = new List<TbDepBanco>();

        //public virtual ICollection<TbDepClassificaco> TbDepClassificacos { get; set; } = new List<TbDepClassificaco>();

        //public virtual ICollection<TbDepClienteDepositoTiposVeiculo> TbDepClienteDepositoTiposVeiculoIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepClienteDepositoTiposVeiculo>();

        //public virtual ICollection<TbDepClienteDepositoTiposVeiculo> TbDepClienteDepositoTiposVeiculoIdUsuarioCadastroNavigation { get; set; } = new List<TbDepClienteDepositoTiposVeiculo>();

        //public virtual ICollection<TbDepCliente> TbDepClienteIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepCliente>();

        //public virtual ICollection<TbDepCliente> TbDepClienteIdUsuarioCadastroNavigation { get; set; } = new List<TbDepCliente>();

        //public virtual ICollection<TbDepClienteRegra> TbDepClienteRegraUsuarioAlteracaos { get; set; } = new List<TbDepClienteRegra>();

        //public virtual ICollection<TbDepClienteRegra> TbDepClienteRegraUsuarioCadastros { get; set; } = new List<TbDepClienteRegra>();

        //public virtual ICollection<TbDepClientesDeposito> TbDepClientesDepositoIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepClientesDeposito>();

        //public virtual ICollection<TbDepClientesDeposito> TbDepClientesDepositoIdUsuarioCadastroNavigation { get; set; } = new List<TbDepClientesDeposito>();

        //public virtual ICollection<TbDepComunicacaoEmail> TbDepComunicacaoEmails { get; set; } = new List<TbDepComunicacaoEmail>();

        //public virtual ICollection<TbDepCondutorEquipamentosOpcionai> TbDepCondutorEquipamentosOpcionaiIdUsuarioAtualizacaoNavigation { get; set; } = new List<TbDepCondutorEquipamentosOpcionai>();

        //public virtual ICollection<TbDepCondutorEquipamentosOpcionai> TbDepCondutorEquipamentosOpcionaiIdUsuarioCadastroNavigation { get; set; } = new List<TbDepCondutorEquipamentosOpcionai>();

        //public virtual ICollection<TbDepCondutorEquipamentosOpcionaisNaoConformidade> TbDepCondutorEquipamentosOpcionaisNaoConformidades { get; set; } = new List<TbDepCondutorEquipamentosOpcionaisNaoConformidade>();

        //public virtual ICollection<TbDepDeposito> TbDepDepositoIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepDeposito>();

        //public virtual ICollection<TbDepDeposito> TbDepDepositoIdUsuarioCadastroNavigation { get; set; } = new List<TbDepDeposito>();

        //public virtual ICollection<TbDepDetranGrvStatusTransacao> TbDepDetranGrvStatusTransacaos { get; set; } = new List<TbDepDetranGrvStatusTransacao>();

        //public virtual ICollection<TbDepDetroGrv> TbDepDetroGrvIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepDetroGrv>();

        //public virtual ICollection<TbDepDetroGrv> TbDepDetroGrvIdUsuarioCadastroNavigation { get; set; } = new List<TbDepDetroGrv>();

        //public virtual ICollection<TbDepDetroGrvMotivoNaoAutorizado> TbDepDetroGrvMotivoNaoAutorizados { get; set; } = new List<TbDepDetroGrvMotivoNaoAutorizado>();

        //public virtual ICollection<TbDepEnquadramentoInfraco> TbDepEnquadramentoInfracos { get; set; } = new List<TbDepEnquadramentoInfraco>();

        //public virtual ICollection<TbDepEquipamentosOpcionai> TbDepEquipamentosOpcionaiIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepEquipamentosOpcionai>();

        //public virtual ICollection<TbDepEquipamentosOpcionai> TbDepEquipamentosOpcionaiIdUsuarioNavigation { get; set; } = new List<TbDepEquipamentosOpcionai>();

        //public virtual ICollection<TbDepEquipamentosOpcionaisLocalizacao> TbDepEquipamentosOpcionaisLocalizacaoIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepEquipamentosOpcionaisLocalizacao>();

        //public virtual ICollection<TbDepEquipamentosOpcionaisLocalizacao> TbDepEquipamentosOpcionaisLocalizacaoIdUsuarioCadastroNavigation { get; set; } = new List<TbDepEquipamentosOpcionaisLocalizacao>();

        //public virtual ICollection<TbDepFaturamentoBoleto> TbDepFaturamentoBoletos { get; set; } = new List<TbDepFaturamentoBoleto>();

        //public virtual ICollection<TbDepFaturamentoComposicao> TbDepFaturamentoComposicaoIdUsuarioAlteracaoQuantidadeNavigation { get; set; } = new List<TbDepFaturamentoComposicao>();

        //public virtual ICollection<TbDepFaturamentoComposicao> TbDepFaturamentoComposicaoIdUsuarioDescontoNavigation { get; set; } = new List<TbDepFaturamentoComposicao>();

        //public virtual ICollection<TbDepFaturamento> TbDepFaturamentoIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepFaturamento>();

        //public virtual ICollection<TbDepFaturamento> TbDepFaturamentoIdUsuarioCadastroNavigation { get; set; } = new List<TbDepFaturamento>();

        //public virtual ICollection<TbDepFaturamentoRegra> TbDepFaturamentoRegraIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepFaturamentoRegra>();

        //public virtual ICollection<TbDepFaturamentoRegra> TbDepFaturamentoRegraIdUsuarioCadastroNavigation { get; set; } = new List<TbDepFaturamentoRegra>();

        //public virtual ICollection<TbDepFaturamentoServicosAssociado> TbDepFaturamentoServicosAssociadoIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepFaturamentoServicosAssociado>();

        //public virtual ICollection<TbDepFaturamentoServicosAssociado> TbDepFaturamentoServicosAssociadoIdUsuarioCadastroNavigation { get; set; } = new List<TbDepFaturamentoServicosAssociado>();

        //public virtual ICollection<TbDepFaturamentoServicosTipo> TbDepFaturamentoServicosTipoIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepFaturamentoServicosTipo>();

        //public virtual ICollection<TbDepFaturamentoServicosTipo> TbDepFaturamentoServicosTipoIdUsuarioCadastroNavigation { get; set; } = new List<TbDepFaturamentoServicosTipo>();

        //public virtual ICollection<GrvBloqueioMotivo> GrvBloqueioMotivos { get; set; } = new List<GrvBloqueioMotivo>();

        //public virtual ICollection<GrvBloqueio> GrvBloqueios { get; set; } = new List<GrvBloqueio>();

        //public virtual ICollection<GrvClientesCodigoIdentificacao> GrvClientesCodigoIdentificacaoIdUsuarioAlteracaoNavigation { get; set; } = new List<GrvClientesCodigoIdentificacao>();

        //public virtual ICollection<GrvClientesCodigoIdentificacao> GrvClientesCodigoIdentificacaoIdUsuarioCadastroNavigation { get; set; } = new List<GrvClientesCodigoIdentificacao>();

        //public virtual ICollection<GrvCobrancasLegai> GrvCobrancasLegais { get; set; } = new List<GrvCobrancasLegai>();

        //public virtual ICollection<GrvDocumento> GrvDocumentoUsuarioCadastros { get; set; } = new List<GrvDocumento>();

        //public virtual ICollection<GrvDocumento> GrvDocumentoUsuarioExclusaos { get; set; } = new List<GrvDocumento>();

        //public virtual ICollection<GrvDrfaAgendamentoRetiradum> GrvDrfaAgendamentoRetirada { get; set; } = new List<GrvDrfaAgendamentoRetiradum>();

        //public virtual ICollection<GrvDrfa> GrvDrfaIdUsuarioAlteracaoNavigation { get; set; } = new List<GrvDrfa>();

        //public virtual ICollection<GrvDrfa> GrvDrfaIdUsuarioCadastroNavigation { get; set; } = new List<GrvDrfa>();

        //public virtual ICollection<GrvFoto> GrvFotos { get; set; } = new List<GrvFoto>();

        public virtual ICollection<GrvModel> UsuarioCadastroGrvs { get; set; }

        public virtual ICollection<GrvModel> UsuarioAlteracaoGrvs { get; set; }

        public virtual ICollection<GrvModel> UsuarioEdicaoGrvs { get; set; }

        public virtual ICollection<GrvModel> UsuarioCadastroGgvs { get; set; }

        public virtual ICollection<Atendimento.AtendimentoModel> UsuarioCadastroAtendimentos { get; set; }
        
        public virtual ICollection<Atendimento.AtendimentoModel> UsuarioAlteracaoAtendimentos { get; set; }

        public virtual ICollection<TipoVeiculoModel> UsuarioCadastroTiposVeiculos { get; set; }

        public virtual ICollection<TipoVeiculoModel> UsuarioAlteracaoTiposVeiculos { get; set; }

        //public virtual ICollection<GrvLacre> GrvLacreIdUsuarioAtualizacaoNavigation { get; set; } = new List<GrvLacre>();

        //public virtual ICollection<GrvLacre> GrvLacreIdUsuarioCadastroNavigation { get; set; } = new List<GrvLacre>();

        //public virtual ICollection<GrvVistorium> GrvVistoriumIdUsuarioAlteracaoNavigation { get; set; } = new List<GrvVistorium>();

        //public virtual ICollection<GrvVistorium> GrvVistoriumIdUsuarioCadastroNavigation { get; set; } = new List<GrvVistorium>();

        //public virtual ICollection<TbDepGtv> TbDepGtvIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepGtv>();

        //public virtual ICollection<TbDepGtv> TbDepGtvIdUsuarioCadastroNavigation { get; set; } = new List<TbDepGtv>();

        //public virtual ICollection<TbDepGtv> TbDepGtvIdUsuarioSeparacaoVeiculosNavigation { get; set; } = new List<TbDepGtv>();

        //public virtual ICollection<TbDepLiberacaoEspecial> TbDepLiberacaoEspecials { get; set; } = new List<TbDepLiberacaoEspecial>();

        //public virtual ICollection<TbDepLiberacaoLeilao> TbDepLiberacaoLeilaos { get; set; } = new List<TbDepLiberacaoLeilao>();

        //public virtual ICollection<TbDepLiberacao> TbDepLiberacaos { get; set; } = new List<TbDepLiberacao>();

        //public virtual ICollection<TbDepMarcasModelo> TbDepMarcasModelos { get; set; } = new List<TbDepMarcasModelo>();

        //public virtual ICollection<TbDepNfeConfiguracaoImagem> TbDepNfeConfiguracaoImagemUsuarioAlteracaos { get; set; } = new List<TbDepNfeConfiguracaoImagem>();

        //public virtual ICollection<TbDepNfeConfiguracaoImagem> TbDepNfeConfiguracaoImagemUsuarioCadastros { get; set; } = new List<TbDepNfeConfiguracaoImagem>();

        //public virtual ICollection<TbDepNfeNotaFiscal> TbDepNfeNotaFiscals { get; set; } = new List<TbDepNfeNotaFiscal>();

        //public virtual ICollection<TbDepNfeRegra> TbDepNfeRegraUsuarioAlteracaos { get; set; } = new List<TbDepNfeRegra>();

        //public virtual ICollection<TbDepNfeRegra> TbDepNfeRegraUsuarioCadastros { get; set; } = new List<TbDepNfeRegra>();

        //public virtual ICollection<TbDepNfeWsErro> TbDepNfeWsErros { get; set; } = new List<TbDepNfeWsErro>();

        //public virtual ICollection<TbDepNfe> TbDepNves { get; set; } = new List<TbDepNfe>();

        //public virtual ICollection<TbDepPixDinamicoConfiguracao> TbDepPixDinamicoConfiguracaoUsuarioAlteracaos { get; set; } = new List<TbDepPixDinamicoConfiguracao>();

        //public virtual ICollection<TbDepPixDinamicoConfiguracao> TbDepPixDinamicoConfiguracaoUsuarioCadastros { get; set; } = new List<TbDepPixDinamicoConfiguracao>();

        //public virtual ICollection<TbDepPixDinamicoSenhaConfirmacaoTranferencium> TbDepPixDinamicoSenhaConfirmacaoTranferenciumUsuarioCadastros { get; set; } = new List<TbDepPixDinamicoSenhaConfirmacaoTranferencium>();

        //public virtual ICollection<TbDepPixDinamicoSenhaConfirmacaoTranferencium> TbDepPixDinamicoSenhaConfirmacaoTranferenciumUsuarioFinanceiros { get; set; } = new List<TbDepPixDinamicoSenhaConfirmacaoTranferencium>();

        //public virtual ICollection<TbDepPreGrv> TbDepPreGrvs { get; set; } = new List<TbDepPreGrv>();

        //public virtual ICollection<TbDepReboquista> TbDepReboquistaIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepReboquista>();

        //public virtual ICollection<TbDepReboquista> TbDepReboquistaIdUsuarioCadastroNavigation { get; set; } = new List<TbDepReboquista>();

        //public virtual ICollection<TbDepRepositorioArquivo> TbDepRepositorioArquivos { get; set; } = new List<TbDepRepositorioArquivo>();

        //public virtual ICollection<TbDepSistemaAcesso> TbDepSistemaAcessoIdUsuarioAcessoNavigation { get; set; } = new List<TbDepSistemaAcesso>();

        //public virtual ICollection<TbDepSistemaAcesso> TbDepSistemaAcessoIdUsuarioNavigation { get; set; } = new List<TbDepSistemaAcesso>();

        //public virtual ICollection<TbDepSistemaPerfilAcesso> TbDepSistemaPerfilAcessoIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepSistemaPerfilAcesso>();

        //public virtual ICollection<TbDepSistemaPerfilAcesso> TbDepSistemaPerfilAcessoIdUsuarioCadastroNavigation { get; set; } = new List<TbDepSistemaPerfilAcesso>();

        //public virtual ICollection<TbDepSistemaPerfilAcessoUsuario> TbDepSistemaPerfilAcessoUsuarios { get; set; } = new List<TbDepSistemaPerfilAcessoUsuario>();

        //public virtual ICollection<TbDepSolicitacaoReboque> TbDepSolicitacaoReboqueIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepSolicitacaoReboque>();

        //public virtual ICollection<TbDepSolicitacaoReboque> TbDepSolicitacaoReboqueIdUsuarioCadastroNavigation { get; set; } = new List<TbDepSolicitacaoReboque>();

        //public virtual ICollection<TbDepTarifa> TbDepTarifaIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepTarifa>();

        //public virtual ICollection<TbDepTarifa> TbDepTarifaIdUsuarioCadastroNavigation { get; set; } = new List<TbDepTarifa>();

        //public virtual ICollection<TbDepTipoVeiculosEquipamentosAssociacao> TbDepTipoVeiculosEquipamentosAssociacaos { get; set; } = new List<TbDepTipoVeiculosEquipamentosAssociacao>();

        //public virtual ICollection<TbDepTiposCombustivei> TbDepTiposCombustiveis { get; set; } = new List<TbDepTiposCombustivei>();

        //public virtual ICollection<TbDepUsuariosCliente> TbDepUsuariosClienteIdUsuarioCadastroNavigation { get; set; } = new List<TbDepUsuariosCliente>();

        //public virtual ICollection<TbDepUsuariosCliente> TbDepUsuariosClienteIdUsuarioNavigation { get; set; } = new List<TbDepUsuariosCliente>();

        //public virtual ICollection<TbDepUsuariosDeposito> TbDepUsuariosDepositoIdUsuarioCadastroNavigation { get; set; } = new List<TbDepUsuariosDeposito>();

        //public virtual ICollection<TbDepUsuariosDeposito> TbDepUsuariosDepositoIdUsuarioNavigation { get; set; } = new List<TbDepUsuariosDeposito>();

        //public virtual ICollection<TbDepUsuariosPermisso> TbDepUsuariosPermissoIdUsuarioAlteracaoNavigation { get; set; } = new List<TbDepUsuariosPermisso>();

        //public virtual ICollection<TbDepUsuariosPermisso> TbDepUsuariosPermissoIdUsuarioCadastroNavigation { get; set; } = new List<TbDepUsuariosPermisso>();

        //public virtual ICollection<TbDepUsuariosPermisso> TbDepUsuariosPermissoIdUsuarioNavigation { get; set; } = new List<TbDepUsuariosPermisso>();
    }
}