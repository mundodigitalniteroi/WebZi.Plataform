namespace WebZi.Plataform.Domain.DTO.GRV
{
    public class GrvDTO
    {
        public int IdentificadorProcesso { get; set; }

        public int IdentificadorCliente { get; set; }

        public int IdentificadorDeposito { get; set; }

        public byte? IdentificadorTipoVeiculo { get; set; }

        public int? IdentificadorReboquista { get; set; }

        public int? IdentificadorReboque { get; set; }

        public int? IdentificadorAutoridadeResponsavel { get; set; }

        public int? IdentificadorCor { get; set; }

        public int? IdentificadorMarcaModelo { get; set; }

        public byte? IdentificadorMotivoApreensao { get; set; }

        public string IdentificadorStatusOperacao { get; set; }

        public int? IdentificadorLiberacao { get; set; }

        public string NumeroFormularioProcesso { get; set; }

        public string CodigoProduto { get; set; }

        public string MatriculaAutoridadeResponsavel { get; set; }

        public string NomeAutoridadeResponsavel { get; set; }

        public string Placa { get; set; }

        public string PlacaOstentada { get; set; }

        public string Chassi { get; set; }

        public string Renavam { get; set; }

        public string Rfid { get; set; }

        public int? IdentificadorEnderecoLocalizacaoVeiculoCEP { get; set; }

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