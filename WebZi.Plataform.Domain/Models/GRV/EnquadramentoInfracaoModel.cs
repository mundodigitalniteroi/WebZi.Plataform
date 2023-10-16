using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Domain.Models.GRV
{
    public class EnquadramentoInfracaoModel
    {
        public decimal EnquadramentoInfracaoId { get; set; }

        public int UsuarioId { get; set; }

        public string CodigoInfracao { get; set; }

        public short? Artigo { get; set; }

        public string Inciso { get; set; }

        public string Descricao { get; set; }

        public DateTime DataCadastro { get; set; }

        public string Status { get; set; } = "S";

        public virtual UsuarioModel Usuario { get; set; }
    }
}