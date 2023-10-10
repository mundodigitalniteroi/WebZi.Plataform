namespace WebZi.Plataform.Domain.ViewModel.Banco.PIX
{
    public class PixDinamicoGeradoViewModel
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public int PixDinamicoId { get; set; }

        public int FaturamentoId { get; set; }

        public int PixDinamicoTipoStatusGeracaoId { get; set; }

        public string TxId { get; set; }

        public int Revisao { get; set; }

        public DateTime PixHorario { get; set; }

        public string PagadorNome { get; set; }

        public string PagadorCpf { get; set; }

        public string PagadorCnpj { get; set; }

        public string EndToEndId { get; set; }

        public string QrString { get; set; }

        public string QrCode { get; set; }

        public DateTime CalendarioCriacao { get; set; }

        public int CalendarioExpiracao { get; set; }

        public string Devedor { get; set; }

        public string Location { get; set; }

        public int LocationId { get; set; }

        public string TipoCobranca { get; set; }

        public string Chave { get; set; }

        public string SolicitacaoPagador { get; set; }

        public string InfoAdicionais { get; set; }

        public decimal ValorOriginal { get; set; }

        public string Json { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }
    }
}