using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Report
{
    public class GuiaDeclaracaoRetiradaLeilaoDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public int IdentificadorProcesso { get; set; }

        public string NumeroProcesso { get; set; }

        public string Titulo { get; set; }

        public string ClienteNome { get; set; }

        public string ClienteEndereco { get; set; }

        public string DataEmissao { get; set; }

        public string HoraEmissao { get; set; }

        public string DataHoraEmissao { get; set; }

        public string TextoDeclaracaoRetirada1 { get; set; }

        public string TextoDeclaracaoRetirada2 { get; set; }

        public string TextoDeclaracaoRetirada3 { get; set; }

        public string NumeroLote { get; set; }

        public string VeiculoMarcaModelo { get; set; }

        public string VeiculoPlaca { get; set; }

        public string VeiculoRenavam { get; set; }

        public string VeiculoChassi { get; set; }

        public string VeiculoCor { get; set; }

        public string VeiculoAno { get; set; }

        public string VeiculoAnoFabricacao { get; set; }

        public string VeiculoAnoModelo { get; set; }

        public string GrvEstacionamentoSetor { get; set; }

        public string GrvEstacionamentoNumeroVaga { get; set; }

        public string GrvNumeroChave { get; set; }

        public string ValorArrematacao { get; set; }

        public string CidadeData { get; set; }

        public string ProprietarioProcurador { get; set; }

        public string ProprietarioCpf { get; set; }

        public string UsuarioNome { get; set; }

        public string UsuarioMatricula { get; set; }

        public string UsuarioCpf { get; set; }
    }
}