using System.ComponentModel.DataAnnotations;
using WebZi.Plataform.Domain.ViewModel.GGV;

namespace WebZi.Plataform.Domain.ViewModel.GRV.Cadastro
{
    public class GrvParameters
    {
        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorCliente { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorDeposito { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public byte IdentificadorTipoVeiculo { get; set; }

        public int IdentificadorReboquista { get; set; }

        public int IdentificadorReboque { get; set; }

        public int IdentificadorAutoridadeResponsavel { get; set; }

        public int IdentificadorCor { get; set; }

        public int IdentificadorMarcaModelo { get; set; }

        public byte IdentificadorMotivoApreensao { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public int IdentificadorUsuario { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [StringLength(14, MinimumLength = 1)]
        public string NumeroProcesso { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [StringLength(3, MinimumLength = 3)]
        public string CodigoProduto { get; set; }

        [MaxLength(30)]
        public string MatriculaAutoridadeResponsavel { get; set; }

        [MaxLength(150)]
        public string NomeAutoridadeResponsavel { get; set; }

        [MaxLength(7)]
        public string Placa { get; set; }

        [MaxLength(24)]
        public string Chassi { get; set; }

        [MaxLength(15)]
        public string Renavam { get; set; }

        [MaxLength(100)]
        public string Rfid { get; set; }

        [StringLength(7, MinimumLength = 7)]
        public string EnderecoLocalizacaoVeiculoCEP { get; set; }

        [MaxLength(150)]
        public string EnderecoLocalizacaoVeiculoLogradouro { get; set; }

        [MaxLength(15)]
        public string EnderecoLocalizacaoVeiculoNumero { get; set; }

        [MaxLength(50)]
        public string EnderecoLocalizacaoVeiculoComplemento { get; set; }

        [MaxLength(150)]
        public string EnderecoLocalizacaoVeiculoBairro { get; set; }

        [MaxLength(75)]
        public string EnderecoLocalizacaoVeiculoMunicipio { get; set; }

        [MaxLength(2)]
        public string EnderecoLocalizacaoVeiculoUF { get; set; }

        [MaxLength(30)]
        public string EnderecoLocalizacaoVeiculoReferencia { get; set; }

        [MaxLength(30)]
        public string EnderecoLocalizacaoVeiculoPontoReferencia { get; set; }

        [MaxLength(10)]
        public string NumeroChave { get; set; }

        [MaxLength(35)]
        public string EstacionamentoSetor { get; set; }

        [MaxLength(10)]
        public string EstacionamentoNumeroVaga { get; set; }

        [MaxLength(15)]
        public string Latitude { get; set; }

        [MaxLength(15)]
        public string Longitude { get; set; }

        [MaxLength(2)]
        public string VeiculoUF { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        public DateTime DataHoraRemocao { get; set; }

        [MaxLength(15)]
        public string LatitudeAcautelamento { get; set; }

        [MaxLength(15)]
        public string LongitudeAcautelamento { get; set; }

        public decimal? DistanciaAteAcautelamento { get; set; }

        [MaxLength(20)]
        public string CodigoIdentificacaoCliente { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagVeiculoNaoUsouReboque { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagVeiculoNaoIdentificado { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagVeiculoSemRegistro { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagVeiculoRoubadoFurtado { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagEstadoLacre { get; set; }

        [Required(ErrorMessage = "Propriedade obrigatória")]
        [RegularExpression("S|N", ErrorMessage = "Valor da Flag inválido, informe S ou N")]
        public string FlagVeiculoNaoOstentaPlaca { get; set; }

        public CondutorParameters Condutor { get; set; }

        public List<CondutorDocumentoParameters> ListagemDocumentoCondutor { get; set; }

        public List<EnquadramentoInfracaoParameters> ListagemEnquadramentoInfracao { get; set; }

        public List<EquipamentoOpcionalParameters> ListagemEquipamentoOpcional { get; set; }

        public List<byte[]> ListagemFoto { get; set; }

        public List<string> ListagemLacre { get; set; }

        public byte[] ImagemAssinaturaAgente { get; set; }

        public byte[] ImagemAssinaturaCondutor { get; set; }
    }
}