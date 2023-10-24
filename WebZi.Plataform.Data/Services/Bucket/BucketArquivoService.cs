using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Bucket.Work;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.ViewModel.Generic;
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
                .Where(x => x.Codigo == CodigoTabelaOrigem)
                .AsNoTracking()
                .FirstOrDefault();

            string NomeArquivo = string.Empty;

            List<BucketArquivoEnvioModel> ArquivosEnvio = new();

            foreach (byte[] File in Files)
            {
                NomeArquivo = Guid.NewGuid().ToString() + ".jpg";

                ArquivosEnvio.Add(new()
                {
                    NomeBucket = Configuracao.RepositorioArquivoNomeBucket,

                    NomeArquivo = NomeArquivo,

                    ArquivoBase64 = Convert.ToBase64String(File),

                    NomePasta = BucketNomeTabelaOrigem.DiretorioRemoto,

                    NomeArquivoCompleto = NomeArquivo,

                    NomeArquivoOriginal = NomeArquivo,
                });
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

        public async Task<ImageViewModelList> DownloadFiles(string CodigoTabelaOrigem, int TabelaOrigemId)
        {
            List<string> erros = new();

            if (string.IsNullOrWhiteSpace(CodigoTabelaOrigem))
            {
                erros.Add("Primeiro é necessário informar o Código Tabela de Origem");
            }

            if (TabelaOrigemId <= 0)
            {
                erros.Add("Primeiro é necessário informar o Identificador da Tabela de Origem");
            }

            ImageViewModelList ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            List<BucketArquivoModel> result = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .Where(x => x.BucketNomeTabelaOrigem.Codigo == CodigoTabelaOrigem
                         && x.TabelaOrigemId == TabelaOrigemId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                result = result.OrderBy(x => x.RepositorioArquivoId).ToList();

                foreach (BucketArquivoModel BucketArquivo in result)
                {
                    ResultView.Listagem.Add(new()
                    {
                        Identificador = BucketArquivo.RepositorioArquivoId,

                        Imagem = HttpClientHelper.DownloadFile(BucketArquivo.Url)
                    });
                }

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public void DeleteFiles(string CodigoTabelaOrigem, List<int> ListagemTabelaOrigemId)
        {
            ConfiguracaoModel Configuracao = _context.Configuracao
                .AsNoTracking()
                .FirstOrDefault();

            List<BucketArquivoModel> BucketArquivos = _context.BucketArquivo
                .Include(i => i.BucketNomeTabelaOrigem)
                .Where(w => w.BucketNomeTabelaOrigem.Codigo == CodigoTabelaOrigem
                         && ListagemTabelaOrigemId.Contains(w.TabelaOrigemId))
                .ToList();

            if (BucketArquivos.Count > 0)
            {
                foreach (BucketArquivoModel BucketArquivo in BucketArquivos)
                {
                    _context.BucketArquivo.Remove(BucketArquivo);

                    DeletarArquivoEnvioModel DeletarArquivoEnvio = new()
                    {
                        NomeBucket = Configuracao.RepositorioArquivoNomeBucket,

                        NomeArquivo = BucketArquivo.NomeArquivo,

                        NomePasta = BucketArquivo.BucketNomeTabelaOrigem.DiretorioRemoto
                    };

                    HttpClientHelper.DeleteBasicAuth<BucketMensagemRetornoModel>
                    (
                        url: Configuracao.RepositorioArquivoUrl,
                        username: Configuracao.RepositorioArquivoUsername,
                        password: Configuracao.RepositorioArquivoPassword,
                        obj: DeletarArquivoEnvio
                    );
                }
            }
        }

        public void DeleteFile(string CodigoTabelaOrigem, int TabelaOrigemId)
        {
            DeleteFiles(CodigoTabelaOrigem, new() { TabelaOrigemId });
        }
    }
}