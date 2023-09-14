using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Deposito;
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

        public int? DetranMarcaModeloId { get; set; }

        public int? CepId { get; set; }

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

        public string FaturamentoProdutoCodigo { get; set; }

        public string MatriculaAutoridadeResponsavel { get; set; }

        public string NomeAutoridadeResponsavel { get; set; }

        public string Placa { get; set; }

        public string PlacaOstentada { get; set; }

        public string Chassi { get; set; }

        public string Renavam { get; set; }

        public string Rfid { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Municipio { get; set; }

        public string Uf { get; set; }

        public string Referencia { get; set; }

        public string PontoReferencia { get; set; }

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

        public DateTime? DataOficio { get; set; }

        public DateTime? DataHoraRemocao { get; set; }

        public DateTime? DataHoraGuarda { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string FlagComboio { get; set; }

        public string FlagVeiculoNaoIdentificado { get; set; }

        public string FlagVeiculoSemRegistro { get; set; }

        public string FlagVeiculoRoubadoFurtado { get; set; }

        public string FlagChaveDeposito { get; set; }

        public string FlagEstadoLacre { get; set; }

        public string FlagVeiculoMesmasCondicoes { get; set; }

        /// <summary>
        /// Flag que identifica se o GGV já foi cadastrado
        /// </summary>
        public string FlagGgv { get; set; }

        public string FlagVistoria { get; set; }

        public string TermoDetran { get; set; }

        public string FlagVeiculoNaoOstentaPlaca { get; set; }

        public string FlagTransbordo { get; set; }

        public DateTime? DataTransbordo { get; set; }

        public int? AgenteId { get; set; }

        public string LongitudeAcautelamento { get; set; }

        public string LatitudeAcautelamento { get; set; }

        public decimal? DistanciaAteAcautelamento { get; set; }

        public string VeiculoUf { get; set; }

        //public virtual TbDepFaturamentoProduto FaturamentoProdutoCodigoNavigation { get; set; }

        //public virtual TbDepAutoridadesResponsavei IdAutoridadeResponsavelNavigation { get; set; }

        public virtual ClienteModel Cliente { get; set; }

        public virtual DepositoModel Deposito { get; set; }

        //public virtual TbDepLiberacao IdLiberacaoNavigation { get; set; }

        //public virtual TbDepGrvMotivoApreensao IdMotivoApreensaoNavigation { get; set; }

        //public virtual TbDepReboque IdReboqueNavigation { get; set; }

        //public virtual TbDepReboquista IdReboquistaNavigation { get; set; }

        public virtual StatusOperacaoModel StatusOperacao { get; set; }

        public virtual TipoVeiculoModel TipoVeiculo { get; set; }

        public virtual Usuario.UsuarioModel UsuarioCadastro { get; set; }

        public virtual Usuario.UsuarioModel UsuarioAlteracao { get; set; }

        public virtual Usuario.UsuarioModel UsuarioEdicao { get; set; }

        public virtual Usuario.UsuarioModel UsuarioCadastroGgv { get; set; }

        //public virtual ICollection<TbDepAlterdataErro> TbDepAlterdataErros { get; set; } = new List<TbDepAlterdataErro>();

        //public virtual TbDepAlterdatum TbDepAlterdatum { get; set; }

        public virtual Atendimento.AtendimentoModel Atendimento { get; set; }

        //public virtual TbDepCondutor TbDepCondutor { get; set; }

        //public virtual ICollection<TbDepCondutorEquipamentosOpcionai> TbDepCondutorEquipamentosOpcionais { get; set; } = new List<TbDepCondutorEquipamentosOpcionai>();

        //public virtual ICollection<TbDepDetranGrvStatusTransacao> TbDepDetranGrvStatusTransacaos { get; set; } = new List<TbDepDetranGrvStatusTransacao>();

        //public virtual TbDepDetroGrv TbDepDetroGrv { get; set; }

        //public virtual ICollection<TbDepFaturamentoServicosGrv> TbDepFaturamentoServicosGrvs { get; set; } = new List<TbDepFaturamentoServicosGrv>();

        //public virtual ICollection<TbDepGrvBloqueio> TbDepGrvBloqueios { get; set; } = new List<TbDepGrvBloqueio>();

        //public virtual TbDepGrvClientesCodigoIdentificacao TbDepGrvClientesCodigoIdentificacao { get; set; }

        //public virtual ICollection<TbDepGrvCobrancasLegai> TbDepGrvCobrancasLegais { get; set; } = new List<TbDepGrvCobrancasLegai>();

        //public virtual ICollection<TbDepGrvDocumento> TbDepGrvDocumentos { get; set; } = new List<TbDepGrvDocumento>();

        //public virtual TbDepGrvDrfa TbDepGrvDrfa { get; set; }

        //public virtual ICollection<TbDepGrvEnquadramentoInfraco> TbDepGrvEnquadramentoInfracos { get; set; } = new List<TbDepGrvEnquadramentoInfraco>();

        //public virtual ICollection<TbDepGrvFoto> TbDepGrvFotos { get; set; } = new List<TbDepGrvFoto>();

        //public virtual ICollection<TbDepGrvLacre> TbDepGrvLacres { get; set; } = new List<TbDepGrvLacre>();

        //public virtual ICollection<TbDepGrvVistorium> TbDepGrvVistoria { get; set; } = new List<TbDepGrvVistorium>();

        //public virtual ICollection<TbDepGtvGrv> TbDepGtvGrvs { get; set; } = new List<TbDepGtvGrv>();

        //public virtual TbDepLiberacaoEspecial TbDepLiberacaoEspecial { get; set; }

        //public virtual TbDepLiberacaoLeilao TbDepLiberacaoLeilao { get; set; }

        //public virtual ICollection<TbDepNfeWsErro> TbDepNfeWsErros { get; set; } = new List<TbDepNfeWsErro>();

        //public virtual ICollection<TbDepNfe> TbDepNves { get; set; } = new List<TbDepNfe>();

        //public virtual TbDepSolicitacaoReboque TbDepSolicitacaoReboque { get; set; }
    }
}