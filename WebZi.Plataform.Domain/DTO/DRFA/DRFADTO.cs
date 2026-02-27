using WebZi.Plataform.Data.Services.DRFA;
using WebZi.Plataform.Domain.DTO.Sistema;

namespace WebZi.Plataform.Domain.DTO.DRFA
{
    public class DRFADTO
    {

        public MensagemDTO Mensagem { get; set; } = new ();
        public int IdentificadorDRFA { get; set; }
        public int IdentificadorProcesso { get; set; }
        public byte IndentificadorTipoRegistro { get; set; }
        public int IdentificadorOrgaoEmissor { get; set; }
        public byte IdentificadorAutoridadeDivisao { get; set; }
        public int IdentificadorUsuarioCadastrado { get; set; }
        public int IdentificadorUsuarioAlteracao { get; set; }
        public string AutoridadeDivisaoComplemento { get; set; }
        public string NumeroRegistroRouboFurto { get; set; }
        public string MatriculaAgente { get; set; }
        public string NomeAgente { get; set; }
        public string LocalRemocaoEnderecoCompleto { get; set; }
        public string LocalRemocaoReferencia { get; set; }
        public string LocalRemocaoLatitude { get; set; }
        public string LocalRemocaoLongitude { get; set; }
        public string DataCadastro { get; set; }
        public string DataAlteracao { get; set; }
        public char FlagRegistroRecuperacao { get; set; }
        public char FlagRegistroAgendamento { get; set; }

        public RegistroRecuperacaoDTO RegistroRecuperacao { get; set; }
        public AgendamentoRetiradaDTO AgendamentoRetirada { get; set; }
    }
}
