namespace WebZi.Plataform.Domain.Models.GRV
{
    public class EnquadramentoInfracaoGrvModel
    {
        public int GrvEnquadramentoInfracaoId { get; set; }

        public int GrvId { get; set; }

        public decimal EnquadramentoInfracaoId { get; set; }

        public string NumeroInfracao { get; set; }

        public virtual EnquadramentoInfracaoModel EnquadramentoInfracao { get; set; }
    }
}