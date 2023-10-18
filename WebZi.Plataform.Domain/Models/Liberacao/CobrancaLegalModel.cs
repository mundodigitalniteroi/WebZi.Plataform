using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.Liberacao
{
    public class CobrancaLegalModel
    {
        public int CobrancaLegalId { get; set; }

        public int GrvId { get; set; }

        public byte TipoCobrancaLegalId { get; set; }

        public int? MunicipioId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public string NumeroAutoInfracao { get; set; }

        public decimal? Exercicio { get; set; }

        public decimal Valor { get; set; }

        public DateTime? DataVencimento { get; set; }

        public DateTime DataCadastro { get; set; }

        public virtual GrvModel Grv { get; set; }

        public virtual TipoCobrancaLegalModel TipoCobrancaLegal { get; set; }

        public virtual UsuarioModel UsuarioCadastro { get; set; }
    }
}