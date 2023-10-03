namespace WebZi.Plataform.Domain.Models.GRV
{
    public class LacreModel
    {
        public int LacreId { get; set; }

        public int GrvId { get; set; }

        public byte? LacreMotivoDesassociacaoId { get; set; }

        public int? UsuarioCadastroId { get; set; }

        public int? UsuarioAlteracaoId { get; set; }

        public string Lacre { get; set; }

        public string LacreAnterior { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public virtual GrvModel Grv { get; set; }

        //public virtual GrvLacresMotivosDesassociacao LacreMotivoDesassociacao { get; set; }

        //public virtual Usuario UsuarioAlteracaoNavigation { get; set; }

        //public virtual Usuario UsuarioCadastroNavigation { get; set; }
    }
}