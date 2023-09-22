namespace WebZi.Plataform.Domain.Models.GRV
{
    public class LacreModel
    {
        public int LacreId { get; set; }

        public int GrvId { get; set; }

        public byte? LacreMotivoDesassociacaoId { get; set; }

        public int? UsuarioCadastroId { get; set; }

        public int? UsuarioAtualizacaoId { get; set; }

        public string Lacre { get; set; }

        public string LacreAnterior { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        //public virtual GrvLacresMotivosDesassociacao LacreMotivoDesassociacao { get; set; }

        //public virtual Usuario IdUsuarioAtualizacaoNavigation { get; set; }

        //public virtual Usuario IdUsuarioCadastroNavigation { get; set; }
    }
}