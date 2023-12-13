using System.ComponentModel.DataAnnotations;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Liberacao;
using WebZi.Plataform.Domain.Models.Pessoa;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.Views.Usuario;

namespace WebZi.Plataform.Domain.Models.Usuario
{
    public class UsuarioModel
    {
        public int UsuarioId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string Login { get; set; }

        [DataType(DataType.Password)]
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

        public virtual PessoaModel Pessoa { get; set; }

        public virtual ICollection<AtendimentoModel> ListagemUsuarioAlteracaoAtendimento { get; set; }

        public virtual ICollection<AtendimentoModel> ListagemUsuarioCadastroAtendimento { get; set; }

        public virtual ICollection<GrvModel> ListagemUsuarioAlteracaoGrv { get; set; }

        public virtual ICollection<GrvModel> ListagemUsuarioCadastroGgv { get; set; }

        public virtual ICollection<GrvModel> ListagemUsuarioCadastroGrv { get; set; }

        public virtual ICollection<GrvModel> ListagemUsuarioEdicaoGrv { get; set; }

        public virtual ICollection<TipoVeiculoModel> ListagemUsuarioAlteracaoTipoVeiculo { get; set; }

        public virtual ICollection<TipoVeiculoModel> ListagemUsuarioCadastroTipoVeiculo { get; set; }

        public virtual ICollection<UsuarioClienteModel> ListagemUsuarioCliente { get; set; }

        public virtual ICollection<UsuarioClienteModel> ListagemUsuarioClienteCadastro { get; set; }

        public virtual ICollection<UsuarioDepositoModel> ListagemUsuarioDeposito { get; set; }

        public virtual ICollection<UsuarioDepositoModel> ListagemUsuarioDepositoCadastro { get; set; }

        public virtual ICollection<LiberacaoModel> ListagemUsuarioLiberacao { get; set; }

        public virtual ICollection<UsuarioPermissaoModel> ListagemUsuarioPermissao { get; set; }

        public virtual ICollection<UsuarioPermissaoModel> ListagemUsuarioPermissaoAlteracao { get; set; }

        public virtual ICollection<UsuarioPermissaoModel> ListagemUsuarioPermissaoCadastro { get; set; }

        public virtual ICollection<ViewUsuarioClienteDepositoModel> ListagemUsuarioClienteDeposito { get; set; }

        //public virtual UsuarioModel UsuarioAlteracao { get; set; }

        //public virtual UsuarioModel UsuarioCadastro { get; set; }

        //public virtual ICollection<Usuario> InverseIdUsuarioAlteracaoNavigation { get; set; } = new List<Usuario>();

        //public virtual ICollection<Usuario> InverseIdUsuarioCadastroNavigation { get; set; } = new List<Usuario>();

        //public virtual ICollection<AgenciasBancaria> AgenciasBancariaIdUsuarioAlteracaoNavigation { get; set; } = new List<AgenciasBancaria>();

        //public virtual ICollection<AgenciasBancaria> AgenciasBancariaIdUsuarioCadastroNavigation { get; set; } = new List<AgenciasBancaria>();

        //public virtual ICollection<Agente> AgenteUsuarioAlteracaos { get; set; } = new List<Agente>();

        //public virtual ICollection<Agente> AgenteUsuarioCadastros { get; set; } = new List<Agente>();

        //public virtual ICollection<Alterdatum> Alterdata { get; set; } = new List<Alterdatum>();

        //public virtual ICollection<AlterdataConfiguracao> AlterdataConfiguracaoUsuarioAlteracaos { get; set; } = new List<AlterdataConfiguracao>();

        //public virtual ICollection<AlterdataConfiguracao> AlterdataConfiguracaoUsuarioCadastros { get; set; } = new List<AlterdataConfiguracao>();

        //public virtual ICollection<AlterdataOperacao> AlterdataOperacaoUsuarioAlteracaos { get; set; } = new List<AlterdataOperacao>();

        //public virtual ICollection<AlterdataOperacao> AlterdataOperacaoUsuarioCadastros { get; set; } = new List<AlterdataOperacao>();

        //public virtual ICollection<AutoridadesResponsavei> AutoridadesResponsaveiIdUsuarioAlteracaoNavigation { get; set; } = new List<AutoridadesResponsavei>();

        //public virtual ICollection<AutoridadesResponsavei> AutoridadesResponsaveiIdUsuarioCadastroNavigation { get; set; } = new List<AutoridadesResponsavei>();

        //public virtual ICollection<Banco> BancoIdUsuarioAlteracaoNavigation { get; set; } = new List<Banco>();

        //public virtual ICollection<Banco> BancoIdUsuarioCadastroNavigation { get; set; } = new List<Banco>();

        //public virtual ICollection<Classificaco> Classificacos { get; set; } = new List<Classificaco>();

        //public virtual ICollection<ClienteDepositoTiposVeiculo> ClienteDepositoTiposVeiculoIdUsuarioAlteracaoNavigation { get; set; } = new List<ClienteDepositoTiposVeiculo>();

        //public virtual ICollection<ClienteDepositoTiposVeiculo> ClienteDepositoTiposVeiculoIdUsuarioCadastroNavigation { get; set; } = new List<ClienteDepositoTiposVeiculo>();

        //public virtual ICollection<Cliente> ClienteIdUsuarioAlteracaoNavigation { get; set; } = new List<Cliente>();

        //public virtual ICollection<Cliente> ClienteIdUsuarioCadastroNavigation { get; set; } = new List<Cliente>();

        //public virtual ICollection<ClienteRegra> ClienteRegraUsuarioAlteracaos { get; set; } = new List<ClienteRegra>();

        //public virtual ICollection<ClienteRegra> ClienteRegraUsuarioCadastros { get; set; } = new List<ClienteRegra>();

        //public virtual ICollection<ClientesDeposito> ClientesDepositoIdUsuarioAlteracaoNavigation { get; set; } = new List<ClientesDeposito>();

        //public virtual ICollection<ClientesDeposito> ClientesDepositoIdUsuarioCadastroNavigation { get; set; } = new List<ClientesDeposito>();

        //public virtual ICollection<ComunicacaoEmail> ComunicacaoEmails { get; set; } = new List<ComunicacaoEmail>();

        //public virtual ICollection<CondutorEquipamentosOpcionai> CondutorEquipamentosOpcionaiIdUsuarioAlteracaoNavigation { get; set; } = new List<CondutorEquipamentosOpcionai>();

        //public virtual ICollection<CondutorEquipamentosOpcionai> CondutorEquipamentosOpcionaiIdUsuarioCadastroNavigation { get; set; } = new List<CondutorEquipamentosOpcionai>();

        //public virtual ICollection<CondutorEquipamentosOpcionaisNaoConformidade> CondutorEquipamentosOpcionaisNaoConformidades { get; set; } = new List<CondutorEquipamentosOpcionaisNaoConformidade>();

        //public virtual ICollection<Deposito> DepositoIdUsuarioAlteracaoNavigation { get; set; } = new List<Deposito>();

        //public virtual ICollection<Deposito> DepositoIdUsuarioCadastroNavigation { get; set; } = new List<Deposito>();

        //public virtual ICollection<DetranGrvStatusTransacao> DetranGrvStatusTransacaos { get; set; } = new List<DetranGrvStatusTransacao>();

        //public virtual ICollection<DetroGrv> DetroGrvIdUsuarioAlteracaoNavigation { get; set; } = new List<DetroGrv>();

        //public virtual ICollection<DetroGrv> DetroGrvIdUsuarioCadastroNavigation { get; set; } = new List<DetroGrv>();

        //public virtual ICollection<DetroGrvMotivoNaoAutorizado> DetroGrvMotivoNaoAutorizados { get; set; } = new List<DetroGrvMotivoNaoAutorizado>();

        //public virtual ICollection<EnquadramentoInfraco> EnquadramentoInfracos { get; set; } = new List<EnquadramentoInfraco>();

        //public virtual ICollection<EquipamentosOpcionai> EquipamentosOpcionaiIdUsuarioAlteracaoNavigation { get; set; } = new List<EquipamentosOpcionai>();

        //public virtual ICollection<EquipamentosOpcionai> EquipamentosOpcionaiIdUsuarioNavigation { get; set; } = new List<EquipamentosOpcionai>();

        //public virtual ICollection<EquipamentosOpcionaisLocalizacao> EquipamentosOpcionaisLocalizacaoIdUsuarioAlteracaoNavigation { get; set; } = new List<EquipamentosOpcionaisLocalizacao>();

        //public virtual ICollection<EquipamentosOpcionaisLocalizacao> EquipamentosOpcionaisLocalizacaoIdUsuarioCadastroNavigation { get; set; } = new List<EquipamentosOpcionaisLocalizacao>();

        //public virtual ICollection<FaturamentoBoleto> FaturamentoBoletos { get; set; } = new List<FaturamentoBoleto>();

        //public virtual ICollection<FaturamentoComposicao> FaturamentoComposicaoIdUsuarioAlteracaoQuantidadeNavigation { get; set; } = new List<FaturamentoComposicao>();

        //public virtual ICollection<FaturamentoComposicao> FaturamentoComposicaoIdUsuarioDescontoNavigation { get; set; } = new List<FaturamentoComposicao>();

        //public virtual ICollection<Faturamento> FaturamentoIdUsuarioAlteracaoNavigation { get; set; } = new List<Faturamento>();

        //public virtual ICollection<Faturamento> FaturamentoIdUsuarioCadastroNavigation { get; set; } = new List<Faturamento>();

        //public virtual ICollection<FaturamentoRegra> FaturamentoRegraIdUsuarioAlteracaoNavigation { get; set; } = new List<FaturamentoRegra>();

        //public virtual ICollection<FaturamentoRegra> FaturamentoRegraIdUsuarioCadastroNavigation { get; set; } = new List<FaturamentoRegra>();

        //public virtual ICollection<FaturamentoServicosAssociado> FaturamentoServicosAssociadoIdUsuarioAlteracaoNavigation { get; set; } = new List<FaturamentoServicosAssociado>();

        //public virtual ICollection<FaturamentoServicosAssociado> FaturamentoServicosAssociadoIdUsuarioCadastroNavigation { get; set; } = new List<FaturamentoServicosAssociado>();

        //public virtual ICollection<FaturamentoServicosTipo> FaturamentoServicosTipoIdUsuarioAlteracaoNavigation { get; set; } = new List<FaturamentoServicosTipo>();

        //public virtual ICollection<FaturamentoServicosTipo> FaturamentoServicosTipoIdUsuarioCadastroNavigation { get; set; } = new List<FaturamentoServicosTipo>();

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

        //public virtual ICollection<GrvLacre> GrvLacreIdUsuarioAlteracaoNavigation { get; set; } = new List<GrvLacre>();

        //public virtual ICollection<GrvLacre> GrvLacreIdUsuarioCadastroNavigation { get; set; } = new List<GrvLacre>();

        //public virtual ICollection<GrvVistorium> GrvVistoriumIdUsuarioAlteracaoNavigation { get; set; } = new List<GrvVistorium>();

        //public virtual ICollection<GrvVistorium> GrvVistoriumIdUsuarioCadastroNavigation { get; set; } = new List<GrvVistorium>();

        //public virtual ICollection<Gtv> GtvIdUsuarioAlteracaoNavigation { get; set; } = new List<Gtv>();

        //public virtual ICollection<Gtv> GtvIdUsuarioCadastroNavigation { get; set; } = new List<Gtv>();

        //public virtual ICollection<Gtv> GtvIdUsuarioSeparacaoVeiculosNavigation { get; set; } = new List<Gtv>();

        //public virtual ICollection<LiberacaoEspecial> LiberacaoEspecials { get; set; } = new List<LiberacaoEspecial>();

        //public virtual ICollection<LiberacaoLeilao> LiberacaoLeilaos { get; set; } = new List<LiberacaoLeilao>();

        //public virtual ICollection<Liberacao> Liberacaos { get; set; } = new List<Liberacao>();

        //public virtual ICollection<MarcasModelo> MarcasModelos { get; set; } = new List<MarcasModelo>();

        //public virtual ICollection<NfeConfiguracaoImagem> NfeConfiguracaoImagemUsuarioAlteracaos { get; set; } = new List<NfeConfiguracaoImagem>();

        //public virtual ICollection<NfeConfiguracaoImagem> NfeConfiguracaoImagemUsuarioCadastros { get; set; } = new List<NfeConfiguracaoImagem>();

        //public virtual ICollection<NfeNotaFiscal> NfeNotaFiscals { get; set; } = new List<NfeNotaFiscal>();

        //public virtual ICollection<NfeRegra> NfeRegraUsuarioAlteracaos { get; set; } = new List<NfeRegra>();

        //public virtual ICollection<NfeRegra> NfeRegraUsuarioCadastros { get; set; } = new List<NfeRegra>();

        //public virtual ICollection<NfeWsErro> NfeWsErros { get; set; } = new List<NfeWsErro>();

        //public virtual ICollection<Nfe> Nves { get; set; } = new List<Nfe>();

        //public virtual ICollection<PixDinamicoConfiguracao> PixDinamicoConfiguracaoUsuarioAlteracaos { get; set; } = new List<PixDinamicoConfiguracao>();

        //public virtual ICollection<PixDinamicoConfiguracao> PixDinamicoConfiguracaoUsuarioCadastros { get; set; } = new List<PixDinamicoConfiguracao>();

        //public virtual ICollection<PixDinamicoSenhaConfirmacaoTranferencium> PixDinamicoSenhaConfirmacaoTranferenciumUsuarioCadastros { get; set; } = new List<PixDinamicoSenhaConfirmacaoTranferencium>();

        //public virtual ICollection<PixDinamicoSenhaConfirmacaoTranferencium> PixDinamicoSenhaConfirmacaoTranferenciumUsuarioFinanceiros { get; set; } = new List<PixDinamicoSenhaConfirmacaoTranferencium>();

        //public virtual ICollection<PreGrv> PreGrvs { get; set; } = new List<PreGrv>();

        //public virtual ICollection<Reboquista> ReboquistaIdUsuarioAlteracaoNavigation { get; set; } = new List<Reboquista>();

        //public virtual ICollection<Reboquista> ReboquistaIdUsuarioCadastroNavigation { get; set; } = new List<Reboquista>();

        //public virtual ICollection<RepositorioArquivo> RepositorioArquivos { get; set; } = new List<RepositorioArquivo>();

        //public virtual ICollection<SistemaAcesso> SistemaAcessoIdUsuarioAcessoNavigation { get; set; } = new List<SistemaAcesso>();

        //public virtual ICollection<SistemaAcesso> SistemaAcessoIdUsuarioNavigation { get; set; } = new List<SistemaAcesso>();

        //public virtual ICollection<SistemaPerfilAcesso> SistemaPerfilAcessoIdUsuarioAlteracaoNavigation { get; set; } = new List<SistemaPerfilAcesso>();

        //public virtual ICollection<SistemaPerfilAcesso> SistemaPerfilAcessoIdUsuarioCadastroNavigation { get; set; } = new List<SistemaPerfilAcesso>();

        //public virtual ICollection<SistemaPerfilAcessoUsuario> SistemaPerfilAcessoUsuarios { get; set; } = new List<SistemaPerfilAcessoUsuario>();

        //public virtual ICollection<SolicitacaoReboque> SolicitacaoReboqueIdUsuarioAlteracaoNavigation { get; set; } = new List<SolicitacaoReboque>();

        //public virtual ICollection<SolicitacaoReboque> SolicitacaoReboqueIdUsuarioCadastroNavigation { get; set; } = new List<SolicitacaoReboque>();

        //public virtual ICollection<Tarifa> TarifaIdUsuarioAlteracaoNavigation { get; set; } = new List<Tarifa>();

        //public virtual ICollection<Tarifa> TarifaIdUsuarioCadastroNavigation { get; set; } = new List<Tarifa>();

        //public virtual ICollection<TipoVeiculosEquipamentosAssociacao> TipoVeiculosEquipamentosAssociacaos { get; set; } = new List<TipoVeiculosEquipamentosAssociacao>();

        //public virtual ICollection<TiposCombustivei> TiposCombustiveis { get; set; } = new List<TiposCombustivei>();
    }
}