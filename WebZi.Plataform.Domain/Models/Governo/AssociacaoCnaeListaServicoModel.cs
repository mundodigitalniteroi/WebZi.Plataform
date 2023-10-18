using WebZi.Plataform.Domain.Models.Empresa;

namespace WebZi.Plataform.Domain.Models.Governo
{
    public class AssociacaoCnaeListaServicoModel
    {
        public int CnaeListaServicoId { get; set; }

        public int CnaeId { get; set; }

        public int ListaServicoId { get; set; }

        public DateTime DataCadastro { get; set; }

        public virtual CnaeModel Cnae { get; set; }

        public virtual ListaServicoModel ListaServico { get; set; }

        public virtual ICollection<EmpresaModel> Empresas { get; set; }
    }
}