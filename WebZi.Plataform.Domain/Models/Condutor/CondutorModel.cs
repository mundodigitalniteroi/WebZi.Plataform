using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Domain.Models.Condutor
{
    public class CondutorModel
    {
        public int CondutorId { get; set; }

        public int GrvId { get; set; }

        public long? PessoaId { get; set; }

        public decimal? EnquadramentoInfracaoId { get; set; }

        public string Documento { get; set; }

        public string Identidade { get; set; }

        public string OrgaoExpedidor { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string TelefoneDdd { get; set; }

        public string Email { get; set; }

        public string NumeroChaveVeiculo { get; set; }

        public string NumeroInfracao { get; set; }

        public string InformacoesAdicionais { get; set; }

        public string OutrosEquipamentos1 { get; set; }

        public string OutrosEquipamentos2 { get; set; }

        public string OutrosEquipamentos3 { get; set; }

        public string OutrosEquipamentos4 { get; set; }

        public string OutrosEquipamentos5 { get; set; }

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

        public string CelularDdd { get; set; }

        public virtual EnquadramentoInfracaoModel EnquadramentoInfracao { get; set; }

        public virtual GrvModel Grv { get; set; }
    }
}