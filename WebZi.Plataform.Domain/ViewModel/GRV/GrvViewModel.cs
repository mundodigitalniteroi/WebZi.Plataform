namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class GrvViewModel
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

        public string FlagVeiculoNaoOstentaPlaca { get; set; }

        public string FlagTransbordo { get; set; }
    }
}