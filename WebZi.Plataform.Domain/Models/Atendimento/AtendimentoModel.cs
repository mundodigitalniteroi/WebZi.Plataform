using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.Atendimento
{
    public class AtendimentoModel
    {
        public int AtendimentoId { get; set; }

        public int GrvId { get; set; }

        public byte QualificacaoResponsavelId { get; set; }

        public long? PessoaFaturamentoId { get; set; }

        public int? EmpresaFaturamentoId { get; set; }

        public string DocumentoSapId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

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

        public string ProprietarioNome { get; set; }

        public byte? ProprietarioTipoDocumentoId { get; set; }

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

        public byte? NotaFiscalIdTipoLogradouro { get; set; }

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

        public int TotalImpressoes { get; set; } = 1;

        public string StatusCadastroSap { get; set; } = "N";

        public string StatusCadastroOrdensVendaSap { get; set; } = "N";

        public DateTime? DataHoraInicioAtendimento { get; set; }

        public DateTime? DataImpressao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string FlagPagamentoFinanciado { get; set; } = "N";

        public string FlagAtendimentoWs { get; set; } = "N";

        public string NotaFiscalInscricaoMunicipal { get; set; }

        public virtual GrvModel Grv { get; set; }

        public virtual QualificacaoResponsavelModel QualificacaoResponsavel { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }

        public virtual UsuarioModel UsuarioAlteracao { get; set; }

        //public virtual ICollection<AtendimentoFotosResponsavei> AtendimentoFotosResponsaveis { get; set; }

        //public virtual ICollection<AtendimentoSaidaReparo> AtendimentoSaidaReparos { get; set; }

        public virtual ICollection<FaturamentoModel> Faturamentos { get; set; }
    }
}