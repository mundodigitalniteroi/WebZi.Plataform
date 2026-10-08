using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.GRV
{
    public class InfracaoVlockDTO
    {
        public string AutoNumero { get; set; } = string.Empty;

        public string Codigo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;
    }

    public class Get2ViaVlockDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public int? IdentificadorProcesso { get; set; }

        public string NumeroProcesso { get; set; } = string.Empty;

        public DateTime? DataHora { get; set; }

        public string DeviceId { get; set; } = string.Empty;

        public string Placa { get; set; } = string.Empty;

        public string TipoVeiculo { get; set; } = string.Empty;

        public string MarcaModelo { get; set; } = string.Empty;

        public string Cor { get; set; } = string.Empty;

        public string Chassi { get; set; } = string.Empty;

        public string Renavam { get; set; } = string.Empty;

        public string Cliente { get; set; } = string.Empty;

        public string Deposito { get; set; } = string.Empty;

        public string Autoridade { get; set; } = string.Empty;

        public string Agente { get; set; } = string.Empty;

        public string MatriculaAgente { get; set; } = string.Empty;

        public string OrgaoEmissor { get; set; } = string.Empty;

        public string MotivoApreensao { get; set; } = string.Empty;

        public string Endereco { get; set; } = string.Empty;

        public string EnderecoNumero { get; set; } = string.Empty;

        public string EnderecoBairro { get; set; } = string.Empty;

        public string EnderecoMunicipio { get; set; } = string.Empty;

        public string EnderecoUF { get; set; } = string.Empty;

        public string EnderecoCompleto { get; set; } = string.Empty;

        public string Latitude { get; set; } = string.Empty;

        public string Longitude { get; set; } = string.Empty;

        public string EnderecoAcautelamento { get; set; } = string.Empty;

        public string LatitudeAcautelamento { get; set; } = string.Empty;

        public string LongitudeAcautelamento { get; set; } = string.Empty;

        public string NomeCondutor { get; set; } = string.Empty;

        public string TelefoneCondutor { get; set; } = string.Empty;

        public string TelefoneDddCondutor { get; set; } = string.Empty;

        public string Observacoes { get; set; } = string.Empty;

        public string Lacre { get; set; } = string.Empty;

        public List<InfracaoVlockDTO> Infracoes { get; set; } = new();
    }
}