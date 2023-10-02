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

        public int? DetranMarcaModeloId { get; set; }

        public int? CepId { get; set; }

        public byte? MotivoApreensaoId { get; set; }

        public string StatusOperacaoId { get; set; }

        public int? LiberacaoId { get; set; }

        public string NumeroFormularioGrv { get; set; }

        public string FaturamentoProdutoId { get; set; }

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

        public DateTime DataHoraRemocao { get; set; }

        public DateTime DataHoraGuarda { get; set; }

        public string FlagComboio { get; set; }

        public string FlagVeiculoNaoIdentificado { get; set; }

        public string FlagVeiculoSemRegistro { get; set; }

        public string FlagVeiculoRoubadoFurtado { get; set; }

        public string FlagChaveDeposito { get; set; }

        public string FlagEstadoLacre { get; set; }

        public string FlagVeiculoMesmasCondicoes { get; set; }

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
    }
}