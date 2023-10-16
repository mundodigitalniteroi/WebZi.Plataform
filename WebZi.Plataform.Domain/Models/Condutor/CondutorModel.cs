using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Models.Condutor
{
    public class CondutorModel
    {
        public int CondutorId { get; set; }

        public int GrvId { get; set; }

        public long? PessoaId { get; set; }

        public string Documento { get; set; }

        public string Identidade { get; set; }

        public string OrgaoExpedidor { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string TelefoneDDD { get; set; }

        public string Email { get; set; }

        public string NumeroChaveVeiculo { get; set; }

        public string NumeroInfracao { get; set; }

        public string InformacoesAdicionais { get; set; }

        /// <summary>
        /// 1 = ASSINOU;
        /// 2 = AUSENTE;
        /// 3 = EVADIU-SE;
        /// 4 = RECUSOU-SE.
        /// </summary>
        public string StatusAssinaturaCondutor { get; set; } = "2";

        public string FlagChaveVeiculo { get; set; } = "N";

        public string FlagDocumentacaoVeiculo { get; set; } = "N";

        public string Celular { get; set; }

        public string CelularDDD { get; set; }

        public virtual GrvModel Grv { get; set; }
    }
}