namespace WebZi.Plataform.Domain.ViewModel.Atendimento
{
    public class AtendimentoCadastroViewModel
    {
        public int GrvId { get; set; }

        public byte TipoMeioCobrancaId { get; set; }

        public byte QualificacaoResponsavelId { get; set; }

        public int UsuarioId { get; set; }

        public string ResponsavelNome { get; set; }

        public string ResponsavelDocumento { get; set; }

        public string ResponsavelCnh { get; set; }

        public string ResponsavelEndereco { get; set; }

        public string ResponsavelNumero { get; set; }

        public string ResponsavelComplemento { get; set; }

        public string ResponsavelBairro { get; set; }

        public string ResponsavelMunicipio { get; set; }

        public string ResponsavelUf { get; set; }

        public string ResponsavelCep { get; set; }

        public string ResponsavelDdd { get; set; }

        public string ResponsavelTelefone { get; set; }

        public byte[] ResponsavelFoto { get; set; }

        public string ProprietarioNome { get; set; }

        public byte ProprietarioTipoDocumentoId { get; set; }

        public string ProprietarioDocumento { get; set; }

        public string FormaLiberacao { get; set; }

        public string FormaLiberacaoNome { get; set; }

        public string FormaLiberacaoCnh { get; set; }

        public string FormaLiberacaoCpf { get; set; }

        public string FormaLiberacaoPlaca { get; set; }

        public string ProprietarioEndereco { get; set; }

        public string ProprietarioNumero { get; set; }

        public string ProprietarioComplemento { get; set; }

        public string ProprietarioBairro { get; set; }

        public string ProprietarioMunicipio { get; set; }

        public string ProprietarioUf { get; set; }

        public string ProprietarioCep { get; set; }

        public string ProprietarioDdd { get; set; }

        public string ProprietarioTelefone { get; set; }

        public string NotaFiscalNome { get; set; }

        public string NotaFiscalDocumento { get; set; }

        public string NotaFiscalEndereco { get; set; }

        public string NotaFiscalNumero { get; set; }

        public string NotaFiscalComplemento { get; set; }

        public string NotaFiscalBairro { get; set; }

        public string NotaFiscalMunicipio { get; set; }

        public string NotaFiscalUf { get; set; }

        public string NotaFiscalCep { get; set; }

        public string NotaFiscalDdd { get; set; }

        public string NotaFiscalTelefone { get; set; }

        public string NotaFiscalEmail { get; set; }

        public string NotaFiscalInscricaoMunicipal { get; set; }

        public DateTime? DataHoraInicioAtendimento { get; set; }
    }
}