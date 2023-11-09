using WebZi.Plataform.Domain.ViewModel.GGV.Cadastro;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;

namespace WebZi.Plataform.Domain.ViewModel.GRV
{
    public class GrvPersistenciaViewModel
    {
        public int IdentificadorCliente { get; set; }

        public int IdentificadorDeposito { get; set; }

        public byte IdentificadorTipoVeiculo { get; set; }

        public int IdentificadorReboquista { get; set; }

        public int IdentificadorReboque { get; set; }

        public int IdentificadorAutoridadeResponsavel { get; set; }

        public int IdentificadorCor { get; set; }

        public int IdentificadorMarcaModelo { get; set; }

        public byte IdentificadorMotivoApreensao { get; set; }

        public int IdentificadorUsuario { get; set; }

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

        public CadastroCondutorViewModel Condutor { get; set; }

        public List<CadastroCondutorDocumentoViewModel> ListagemDocumentoCondutor { get; set; }

        public List<CadastroEnquadramentoInfracaoGrvViewModel> ListagemEnquadramentoInfracao { get; set; }

        public List<CadastroEquipamentoOpcionalViewModel> ListagemEquipamentoOpcional { get; set; }

        public List<byte[]> ListagemFoto { get; set; }

        public List<string> ListagemLacre { get; set; }
    }
}