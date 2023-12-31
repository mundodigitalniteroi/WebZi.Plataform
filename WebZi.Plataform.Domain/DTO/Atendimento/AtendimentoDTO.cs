using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.Atendimento
{
    public class AtendimentoDTO
    {
        public MensagemDTO Mensagem { get; set; } = new();

        public int IdentificadorAtendimento { get; set; }

        public int IdentificadorProcesso { get; set; }

        public byte IdentificadorQualificacaoResponsavel { get; set; }

        public string ResponsavelNome { get; set; }

        public string ResponsavelDocumento { get; set; }

        public string ResponsavelCNH { get; set; }

        public string ResponsavelEndereco { get; set; }

        public string ResponsavelNumero { get; set; }

        public string ResponsavelComplemento { get; set; }

        public string ResponsavelBairro { get; set; }

        public string ResponsavelMunicipio { get; set; }

        public string ResponsavelUF { get; set; }

        public string ResponsavelCEP { get; set; }

        public string ResponsavelDDD { get; set; }

        public string ResponsavelTelefone { get; set; }

        public string ProprietarioNome { get; set; }

        public byte? IdentificadorProprietarioTipoDocumento { get; set; }

        public string ProprietarioDocumento { get; set; }

        public string FormaLiberacao { get; set; }

        public string FormaLiberacaoNome { get; set; }

        public string FormaLiberacaoCNH { get; set; }

        public string FormaLiberacaoCPF { get; set; }

        public string FormaLiberacaoPlaca { get; set; }

        public string ProprietarioEndereco { get; set; }

        public string ProprietarioNumero { get; set; }

        public string ProprietarioComplemento { get; set; }

        public string ProprietarioBairro { get; set; }

        public string ProprietarioMunicipio { get; set; }

        public string ProprietarioUF { get; set; }

        public string ProprietarioCEP { get; set; }

        public string ProprietarioDDD { get; set; }

        public string ProprietarioTelefone { get; set; }

        public string NotaFiscalNome { get; set; }

        public string NotaFiscalDocumento { get; set; }

        public string NotaFiscalEndereco { get; set; }

        public string NotaFiscalNumero { get; set; }

        public string NotaFiscalComplemento { get; set; }

        public string NotaFiscalBairro { get; set; }

        public string NotaFiscalMunicipio { get; set; }

        public string NotaFiscalUF { get; set; }

        public string NotaFiscalCEP { get; set; }

        public string NotaFiscalDDD { get; set; }

        public string NotaFiscalTelefone { get; set; }

        public string NotaFiscalEmail { get; set; }

        public string NotaFiscalInscricaoMunicipal { get; set; }

        public int TotalImpressoes { get; set; } = 1;

        public string StatusCadastroERP { get; set; }

        public string StatusCadastroOrdensVendaERP { get; set; }

        public string FlagPagamentoFinanciado { get; set; }

        public string FlagAtendimentoWS { get; set; }

        public DateTime? DataHoraInicioAtendimento { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }
    }
}