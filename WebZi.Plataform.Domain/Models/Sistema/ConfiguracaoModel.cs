namespace WebZi.Plataform.Domain.Models.Sistema
{
    public class ConfiguracaoModel
    {
        public byte ConfiguracaoId { get; set; }

        public string VersaoSistema { get; set; }

        public string CheckUrl { get; set; }

        public DateTime? DataNovaVersao { get; set; }

        public string FtpGrvDocumentosIp { get; set; }

        public string FtpGrvDocumentosUser { get; set; }

        public string FtpGrvDocumentosPass { get; set; }

        public string FlagDetranOnline { get; set; }

        public string FlagDetranProducaoOnline { get; set; }

        public string FlagDetranDesenvolvimentoOnline { get; set; }

        public string FlagAtualizacaoObrigatoria { get; set; }

        public string FlagServicosWindowsAtivos { get; set; }

        public int? TamanhoMaximoArquivoEnvioServidor { get; set; }

        public string HorarioVerao { get; set; }

        public string SystemUpdateInstallDirectory { get; set; }

        public string SystemUpdateFtpHost { get; set; }

        public string SystemUpdateFtpPort { get; set; }

        public string SystemUpdateFtpUserName { get; set; }

        public string SystemUpdateFtpPassword { get; set; }

        public string RepositorioArquivoUsername { get; set; }

        public string RepositorioArquivoPassword { get; set; }

        public string RepositorioArquivoUrl { get; set; }

        public string RepositorioArquivoNomeBucket { get; set; }

        public string PixUsername { get; set; }

        public string PixPassword { get; set; }

        public string PixUrl { get; set; }
    }
}