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

        public string EnderecoLocalizacaoVeiculoCEP { get; set; }

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

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string VeiculoUF { get; set; }

        public DateTime DataHoraRemocao { get; set; }

        public string LatitudeAcautelamento { get; set; }

        public string LongitudeAcautelamento { get; set; }

        public string CodigoIdentificacaoCliente { get; set; }

        public string FlagVeiculoNaoUsouReboque { get; set; }

        public string FlagVeiculoNaoIdentificado { get; set; }

        public string FlagVeiculoSemRegistro { get; set; }

        public string FlagVeiculoRoubadoFurtado { get; set; }

        public string FlagEstadoLacre { get; set; }

        public string FlagVeiculoNaoOstentaPlaca { get; set; }

        public CondutorCadastroViewModel Condutor { get; set; }

        public List<string> Lacres { get; set;}

        public List<EnquadramentoInfracaoGrvCadastroViewModel> EnquadramentosInfracoes { get; set; }

        public List<byte[]> Fotos { get; set; }

        // Isso é cadastrado no GGV
        // public ICollection<CondutorEquipamentoOpcionalCadastroViewModel> CondutorEquipamentosOpcionais { get; set; }
    }
}