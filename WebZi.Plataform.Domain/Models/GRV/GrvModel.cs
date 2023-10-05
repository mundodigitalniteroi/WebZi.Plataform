using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Condutor;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Models.Veiculo;

namespace WebZi.Plataform.Domain.Models.GRV
{
    public class GrvModel
    {
        public int GrvId { get; set; }

        public int ClienteId { get; set; }

        public int DepositoId { get; set; }

        public byte? TipoVeiculoId { get; set; }

        public int? ReboquistaId { get; set; }

        public int? ReboqueId { get; set; }

        public int? AutoridadeResponsavelId { get; set; }

        public int? CorId { get; set; }

        public int? CorOstentadaId { get; set; }

        public int? MarcaModeloId { get; set; }

        public int? AgenteId { get; set; }

        public byte? MotivoApreensaoId { get; set; }

        public string StatusOperacaoId { get; set; }

        public int? LiberacaoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public int? UsuarioEdicaoId { get; set; }

        /// <summary>
        /// ID do Usuário que realizou o cadastro das informações do GGV
        /// </summary>
        public int? UsuarioCadastroGgvId { get; set; }

        public string NumeroFormularioGrv { get; set; }

        public string FaturamentoProdutoId { get; set; }

        public string MatriculaAutoridadeResponsavel { get; set; }

        public string NomeAutoridadeResponsavel { get; set; }

        public string Placa { get; set; }

        public string PlacaOstentada { get; set; }

        public string Chassi { get; set; }

        public string Renavam { get; set; }

        public string Rfid { get; set; }

        public int? EnderecoLocalizacaoVeiculoCEPId { get; set; }

        public string EnderecoLocalizacaoVeiculoLogradouro { get; set; }

        public string EnderecoLocalizacaoVeiculoNumero { get; set; }

        public string EnderecoLocalizacaoVeiculoComplemento { get; set; }

        public string EnderecoLocalizacaoVeiculoBairro { get; set; }

        public string EnderecoLocalizacaoVeiculoMunicipio { get; set; }

        public string EnderecoLocalizacaoVeiculoUF { get; set; }

        public string EnderecoLocalizacaoVeiculoReferencia { get; set; }

        public string EnderecoLocalizacaoVeiculoPontoReferencia { get; set; }

        public string NumeroChave { get; set; }

        public string EstacionamentoSetor { get; set; }

        public string EstacionamentoNumeroVaga { get; set; }

        public string Divergencia1 { get; set; }

        public string Divergencia2 { get; set; }

        public string Divergencia3 { get; set; }

        public string Divergencia4 { get; set; }

        public string Divergencia5 { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string NumeroOficio { get; set; }

        public string MatriculaComandante { get; set; }

        public string TermoDetran { get; set; }

        public string VeiculoUF { get; set; }

        public DateTime DataHoraRemocao { get; set; }

        public DateTime DataHoraGuarda { get; set; }

        public DateTime? DataOficio { get; set; }

        public DateTime? DataTransbordo { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string LatitudeAcautelamento { get; set; }

        public string LongitudeAcautelamento { get; set; }

        public decimal? DistanciaAteAcautelamento { get; set; }

        public string FlagComboio { get; set; } = "N";

        public string FlagVeiculoNaoIdentificado { get; set; } = "N";

        public string FlagVeiculoSemRegistro { get; set; } = "N";

        public string FlagVeiculoRoubadoFurtado { get; set; } = "N";

        public string FlagChaveDeposito { get; set; } = "N";

        public string FlagEstadoLacre { get; set; } = "N";

        public string FlagVeiculoMesmasCondicoes { get; set; } = "N";

        /// <summary>
        /// Flag que identifica se o GGV já foi cadastrado
        /// </summary>
        public string FlagGgv { get; set; } = "N";

        public string FlagVistoria { get; set; } = "N";

        public string FlagVeiculoNaoOstentaPlaca { get; set; } = "N";

        public string FlagTransbordo { get; set; } = "N";

        public virtual FaturamentoProdutoModel FaturamentoProduto { get; set; }

        public virtual AutoridadeResponsavelModel AutoridadeResponsavel { get; set; }

        public virtual ClienteModel Cliente { get; set; }

        public virtual DepositoModel Deposito { get; set; }

        public virtual MotivoApreensaoModel MotivoApreensao { get; set; }

        public virtual ReboqueModel Reboque { get; set; }

        public virtual ReboquistaModel Reboquista { get; set; }

        public virtual StatusOperacaoModel StatusOperacao { get; set; }

        public virtual TipoVeiculoModel TipoVeiculo { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual UsuarioModel UsuarioAlteracao { get; set; }

        public virtual UsuarioModel UsuarioEdicao { get; set; }

        public virtual UsuarioModel UsuarioCadastroGgv { get; set; }

        public virtual AtendimentoModel Atendimento { get; set; }

        public virtual CondutorModel Condutor { get; set; }

        public virtual CorModel Cor { get; set; }

        public virtual MarcaModeloModel MarcaModelo { get; set; }

        public virtual ICollection<EnquadramentoInfracaoModel> EnquadramentosInfracoes { get; set; }

        public virtual ICollection<FaturamentoServicoGrvModel> FaturamentosServicosGrvs { get; set; }

        public virtual ICollection<LacreModel> Lacres { get; set; } = new List<LacreModel>();

        //public virtual Alterdatum Alterdatum { get; set; }

        //public virtual DetroGrv DetroGrv { get; set; }

        //public virtual GrvClientesCodigoIdentificacao GrvClientesCodigoIdentificacao { get; set; }

        //public virtual GrvDrfa GrvDrfa { get; set; }

        //public virtual Liberacao IdLiberacaoNavigation { get; set; }

        //public virtual LiberacaoEspecial LiberacaoEspecial { get; set; }

        //public virtual LiberacaoLeilao LiberacaoLeilao { get; set; }

        //public virtual SolicitacaoReboque SolicitacaoReboque { get; set; }

        //public virtual ICollection<AlterdataErro> AlterdataErros { get; set; } = new List<AlterdataErro>();

        //public virtual ICollection<CondutorEquipamentosOpcionai> CondutorEquipamentosOpcionais { get; set; } = new List<CondutorEquipamentosOpcionai>();

        //public virtual ICollection<DetranGrvStatusTransacao> DetranGrvStatusTransacaos { get; set; } = new List<DetranGrvStatusTransacao>();

        //public virtual ICollection<GrvBloqueio> GrvBloqueios { get; set; } = new List<GrvBloqueio>();

        //public virtual ICollection<GrvCobrancasLegai> GrvCobrancasLegais { get; set; } = new List<GrvCobrancasLegai>();

        //public virtual ICollection<GrvDocumento> GrvDocumentos { get; set; } = new List<GrvDocumento>();

        //public virtual ICollection<GrvFoto> GrvFotos { get; set; } = new List<GrvFoto>();

        //public virtual ICollection<GrvVistorium> GrvVistoria { get; set; } = new List<GrvVistorium>();

        //public virtual ICollection<GtvGrv> GtvGrvs { get; set; } = new List<GtvGrv>();

        //public virtual ICollection<NfeWsErro> NfeWsErros { get; set; } = new List<NfeWsErro>();

        //public virtual ICollection<Nfe> Nves { get; set; } = new List<Nfe>();
    }
}