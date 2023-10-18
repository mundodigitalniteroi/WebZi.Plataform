namespace WebZi.Plataform.Domain.Models.Empresa
{
    public class EmpresaClassificacaoModel
    {
        public byte IdEmpresaClassificacao { get; set; }

        public string Descricao { get; set; }

        public string FlagMatriz { get; set; } = "N";

        // public virtual ICollection<TbGloEmpEmpresa> TbGloEmpEmpresas { get; set; }
    }
}