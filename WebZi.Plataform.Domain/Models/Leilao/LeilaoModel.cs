namespace WebZi.Plataform.Domain.Models.Leilao
{
    public class LeilaoModel
    {
        public int LeilaoId { get; set; }

        public int LeiloeiroId { get; set; }

        public int UsuarioCadastroId { get; set; }

        public int? ComitenteId { get; set; }

        public int ExpositorId { get; set; }

        public int LeilaoStatusId { get; set; }

        public int? EmpresaId { get; set; }

        public int RegraPrestacaoContaId { get; set; }

        public string Descricao { get; set; }

        public string NomeLocal { get; set; }

        public string NumeroDiarioOficial { get; set; }

        public string OrdemInternaMatriz { get; set; }

        public string OrdemInternaLeilao { get; set; }

        public string DataLeilao { get; set; }

        public string HoraLeilao { get; set; }

        public DateTime? DataHoraCadastro { get; set; }

        public string DataAgendamento { get; set; }

        public string DataInicioRetirada { get; set; }

        public string DataFimRetirada { get; set; }

        public string DataHoraPublicacaoDiarioOficial { get; set; }

        public string Cep { get; set; }

        public string Endereco { get; set; }

        public string NumeroEndereco { get; set; }

        public string ComplementoEndereco { get; set; }

        public string Bairro { get; set; }

        public string Municipio { get; set; }

        public string Uf { get; set; }

        public string IdentificacaoLeilaoOrgao { get; set; }

        public DateTime? DataNotificacao { get; set; }

        public DateTime? DataEncerramento { get; set; }

        public DateTime? DataEditalLiberacao { get; set; }

        public string EmailNotificacao { get; set; }

        public string LeilaoDsin { get; set; } = "N";

        //public virtual TbComitente IdComitenteNavigation { get; set; }

        //public virtual TbLeiloeiro IdLeiloeiroNavigation { get; set; }

        //public virtual TbLeilaoRegrasPrestacaoConta IdRegraPrestacaoContasNavigation { get; set; }

        public virtual LeilaoStatusModel LeilaoStatus { get; set; }

        //public virtual ICollection<TbArrematantesFatura> TbArrematantesFaturas { get; set; }

        //public virtual ICollection<TbLeilaoEditai> TbLeilaoEditais { get; set; }

        public virtual ICollection<LeilaoLoteModel> Lotes { get; set; }
    }
}