using Microsoft.EntityFrameworkCore;
using System.Net;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Bucket.Work;
using WebZi.Plataform.Domain.Models.Sistema;
using Z.EntityFramework.Plus;

namespace WebZi.Plataform.Data.Services.Bucket
{
    public class BucketArquivoService
    {
        private readonly AppDbContext _context;

        public BucketArquivoService(AppDbContext context)
        {
            _context = context;
        }

        public void SendFile(string CodigoTabelaOrigem, int TabelaOrigemId, int UsuarioCadastroId, byte[] File, string TipoCadastro = "")
        {
            SendFiles(CodigoTabelaOrigem, TabelaOrigemId, UsuarioCadastroId, new() { File }, TipoCadastro);
        }

        public void SendFiles(string CodigoTabelaOrigem, int TabelaOrigemId, int UsuarioCadastroId, List<byte[]> Files, string TipoCadastro = "")
        {
            ConfiguracaoModel Configuracao = _context.Configuracao
                .AsNoTracking()
                .FirstOrDefault();

            BucketNomeTabelaOrigemModel BucketNomeTabelaOrigem = _context.BucketNomeTabelaOrigem
                .Where(w => w.Codigo == CodigoTabelaOrigem)
                .AsNoTracking()
                .FirstOrDefault();

            string NomeArquivo = string.Empty;

            List<BucketArquivoEnvioModel> ArquivosEnvio = new();

            foreach (byte[] File in Files)
            {
                NomeArquivo = Guid.NewGuid().ToString() + ".jpg";

                ArquivosEnvio = new()
                {
                    new()
                    {
                        NomeBucket = Configuracao.RepositorioArquivoNomeBucket,

                        NomeArquivo = NomeArquivo,

                        ArquivoBase64 = Convert.ToBase64String(File),

                        NomePasta = BucketNomeTabelaOrigem.DiretorioRemoto,

                        NomeArquivoCompleto = NomeArquivo,

                        NomeArquivoOriginal = NomeArquivo,
                    }
                };
            }

            List<BucketArquivoRetornoModel> BucketArquivosRetorno = HttpClientHelper.PostBasicAuth<List<BucketArquivoRetornoModel>>
            (
                url: Configuracao.RepositorioArquivoUrl,
                username: Configuracao.RepositorioArquivoUsername,
                password: Configuracao.RepositorioArquivoPassword,
                obj: ArquivosEnvio
            ).ToList();

            foreach (BucketArquivoRetornoModel BucketArquivoRetorno in BucketArquivosRetorno)
            {
                BucketArquivoModel BucketArquivo = new()
                {
                    NomeTabelaOrigemId = BucketNomeTabelaOrigem.NomeTabelaOrigemId,

                    TabelaOrigemId = TabelaOrigemId,

                    UsuarioCadastroId = UsuarioCadastroId,

                    NomeArquivo = BucketArquivoRetorno.NomeArquivo,

                    TamanhoBytes = BucketArquivoRetorno.TamanhoBytes,

                    Url = BucketArquivoRetorno.Url,

                    PermissaoAcesso = BucketArquivoRetorno.PermissaoAcesso,

                    TipoCadastro = TipoCadastro
                };

                _context.BucketArquivo.Add(BucketArquivo);

                _context.SaveChanges();
            }
        }

        public byte[] DownloadFile(string CodigoTabelaOrigem, int TabelaOrigemId)
        {
            BucketArquivoModel BucketArquivo = _context.BucketArquivo
                .Include(i => i.BucketNomeTabelaOrigem)
                .Where(w => w.TabelaOrigemId == TabelaOrigemId && w.BucketNomeTabelaOrigem.Codigo == CodigoTabelaOrigem)
                .AsNoTracking()
                .FirstOrDefault();

            return BucketArquivo != null ? HttpClientHelper.DownloadFile(BucketArquivo.Url) : null;
        }

        public BucketMensagemRetornoModel DeleteFile(string CodigoTabelaOrigem, int TabelaOrigemId)
        {
            ConfiguracaoModel Configuracao = _context.Configuracao
                .AsNoTracking()
                .FirstOrDefault();

            BucketArquivoModel BucketArquivo = _context.BucketArquivo
                .Include(i => i.BucketNomeTabelaOrigem)
                .Where(w => w.TabelaOrigemId == TabelaOrigemId && w.BucketNomeTabelaOrigem.Codigo == CodigoTabelaOrigem)
                .FirstOrDefault();

            if (BucketArquivo == null)
            {
                return null;
            }

            _context.BucketArquivo.Remove(BucketArquivo);

            DeletarArquivoEnvioModel DeletarArquivoEnvio = new()
            {
                NomeBucket = Configuracao.RepositorioArquivoNomeBucket,

                NomeArquivo = BucketArquivo.NomeArquivo,

                NomePasta = BucketArquivo.BucketNomeTabelaOrigem.DiretorioRemoto
            };

            return HttpClientHelper.DeleteBasicAuth<BucketMensagemRetornoModel>
            (
                url: Configuracao.RepositorioArquivoUrl,
                username: Configuracao.RepositorioArquivoUsername,
                password: Configuracao.RepositorioArquivoPassword,
                obj: DeletarArquivoEnvio
            );
        }
    }
}