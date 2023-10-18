using WebZi.Plataform.Domain.Models.Empresa;

namespace WebZi.Plataform.Domain.Models.Governo
{
    public class CnaeModel
    {
        public int CnaeId { get; set; }

        public string Codigo { get; set; }

        public string CodigoFormatado { get; set; }

        public string Descricao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string FlagPrincipal { get; set; } = "N";

        public virtual ICollection<EmpresaModel> Empresas { get; set; }

        public virtual ICollection<AssociacaoCnaeListaServicoModel> CnaeListaServicos { get; set; }
    }
}