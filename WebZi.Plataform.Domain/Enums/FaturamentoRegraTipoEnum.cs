using System.Security.Cryptography;

namespace WebZi.Plataform.Domain.Enums
{
    public static class FaturamentoRegraTipoEnum
    {
        public static readonly string CalculoDiasNaoCobradas = "CALCDIASNAOCOBRADAS";
        public static readonly string CargaTributaria = "CARGATRIBUTARIA";
        public static readonly string CobrarTarifaBancaria = "COBRATARIFABANCARIA";
        public static readonly string DescontoISS = "DESCONTOISS";
        public static readonly string HorasPatio = "HORASPATIO";
        public static readonly string HoraLiberacaoPrimeiroDiaUtil = "HRLIBDIAUTIL";
        public static readonly string MaximoDiariasCobrancaFaturaAdicional = "MAXDIASFATADICIONAL";
        public static readonly string MaximoDiasPrazoLiberacaoVeiculo = "MAXDIASLIBERACAO";
        public static readonly string HoraLimiteLiberacaoVeiculo = "MAXHORALIBERACAO";
        public static readonly string NaoCobrarDiariaDiaAtualQuandoQuantidadeDiariasMaiorQueUm = "NAOCOBRARDIARIAATUAL";
        public static readonly string NaoCobrarPrimeiraDiaria = "NAOCOBRPRIMEIRDIARIA";
        public static readonly string ObrigatorioInformarInscricaoMunicipal = "ATENDINSCRICMUNIC";
        public static readonly string EmitirNFE = "NFE";
        public static readonly string TamanhoMaximoArquivoFTP = "VALMAXFTPDOCTOS";
        public static readonly string QuantidadeDiasSomarDataVencimentoBoleto = "VENCIMENTOBOLETOD+";
    }
}