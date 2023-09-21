using WebZi.Plataform.Domain.Models.Cliente;

namespace WebZi.Plataform.Domain.Models.Servico
{
    public class ReboqueModel
    {
        public int ReboqueId { get; set; }

        public int ClienteId { get; set; }

        public int DepositoId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string Codigo { get; set; }

        public string Placa { get; set; }

        public string Chassi { get; set; }

        public string Renavam { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public decimal? Ano { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string FlagAtivo { get; set; } = "S";

        public virtual ClienteModel Cliente { get; set; }

        //public virtual ICollection<ReboquesTerceirizado> ReboquesTerceirizados { get; set; } = new List<ReboquesTerceirizado>();

        //public virtual ICollection<SolicitacaoReboque> SolicitacaoReboques { get; set; } = new List<SolicitacaoReboque>();
    }
}