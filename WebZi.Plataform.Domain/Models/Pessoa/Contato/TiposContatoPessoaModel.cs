using WebZi.Plataform.Domain.Models.Documento;

namespace WebZi.Plataform.Domain.Models.Pessoa.Contato
{
    public class TiposContatoPessoaModel
    {
        public long PessoaTipoContatoId { get; set; }
        public long PessoaId { get; set; }
        public int TipoContatoId { get; set; }
        public string Descricao { get; set; }
        public char FlagContatoPrincipal { get; set; }

        public PessoaModel Pessoa { get; set; }
        public TiposContatosModel TiposContatos { get; set; }
    }
}
