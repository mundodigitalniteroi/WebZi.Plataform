namespace WebZi.Plataform.Domain.ViewModel.Report
{
    public class ValidacaoGuiaAutorizacaoRetiradaVeiculoViewModel
    {
        public MensagemViewModel Mensagem { get; set; } = new();

        public string Cliente { get; set; }

        public string Deposito { get; set; }

        public string EnderecoDeposito { get; set; }

        public string NumeroProcesso { get; set; }

        public DateTime DataHoraGuarda { get; set; }

        public string Placa { get; set; }

        public string Chassi { get; set; }

        public string Renavam { get; set; }

        public string MarcaModelo { get; set; }

        public string Cor { get; set; }

        public string Setor { get; set; }

        public string Vaga { get; set; }

        public string LocalizacaoChaveClaviculario { get; set; }

        public DateTime DataAutorizacaoRetirada { get; set; }

        public string UsuarioResponsavelAtendimento { get; set; }

        public string PessoaResponsavelLiberacao { get; set; }

        public string DocumentoPessoaResponsavelLiberacao { get; set; }

        public List<string> ListagemLacre { get; set; } = new();


        public string ResponsavelNome { get; set; }

        public string ResponsavelCPF { get; set; }

        public string ResponsavelCNH { get; set; }

        public byte[] FotoResponsavel { get; set; }


        public string FormaLiberacao { get; set; }

        public string FormaLiberacaoCNH { get; set; }

        public string FormaLiberacaoCPF { get; set; }

        public string FormaLiberacaoPlaca { get; set; }
    }
}