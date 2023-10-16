using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;

namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class GrvCadastroViewModel
    {
        public int ClienteId { get; set; }

        public int DepositoId { get; set; }

        public byte TipoVeiculoId { get; set; }

        public int ReboquistaId { get; set; }

        public int ReboqueId { get; set; }

        public int AutoridadeResponsavelId { get; set; }

        public int CorId { get; set; }

        public int? CorOstentadaId { get; set; }

        public int MarcaModeloId { get; set; }

        public byte MotivoApreensaoId { get; set; }

        public int UsuarioId { get; set; }

        public string NumeroProcesso { get; set; }

        public string CodigoProduto { get; set; }

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

        public DateTime? DataOficio { get; set; }

        public DateTime? DataTransbordo { get; set; }

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

        public string FlagVistoria { get; set; } = "N";

        public string FlagVeiculoNaoOstentaPlaca { get; set; } = "N";

        public string FlagTransbordo { get; set; } = "N";

        public CondutorCadastroViewModel Condutor { get; set; }

        public List<string> Lacres { get; set;}

        public List<EnquadramentoInfracaoGrvCadastroViewModel> EnquadramentosInfracoes { get; set; }

        // Isso é cadastrado no GGV
        // public ICollection<CondutorEquipamentoOpcionalCadastroViewModel> CondutorEquipamentosOpcionais { get; set; }
    }
}