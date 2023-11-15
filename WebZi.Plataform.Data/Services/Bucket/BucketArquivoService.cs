using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Bucket.Work;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.ViewModel.Generic;

namespace WebZi.Plataform.Data.Services.Bucket
{
    public class BucketArquivoService
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public BucketArquivoService(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
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

            HttpClientFactoryService Service = new(_httpClientFactory);

            List<BucketArquivoRetornoModel> BucketArquivosRetorno = Service.PostBasicAuth<List<BucketArquivoRetornoModel>>
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

        public void SendFiles(string CodigoTabelaOrigem, int UsuarioCadastroId, List<BucketListaCadastroModel> Files, string TipoCadastro = "")
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

            HttpClientFactoryService Service = new(_httpClientFactory);

            foreach (BucketListaCadastroModel File in Files)
            {
                NomeArquivo = Guid.NewGuid().ToString() + ".jpg";

                ArquivosEnvio.Add(new()
                {
                    NomeBucket = Configuracao.RepositorioArquivoNomeBucket,

                    NomeArquivo = NomeArquivo,

                    ArquivoBase64 = Convert.ToBase64String(File.File),

                    NomePasta = BucketNomeTabelaOrigem.DiretorioRemoto,

                    NomeArquivoCompleto = NomeArquivo,

                    NomeArquivoOriginal = NomeArquivo,
                });

                List<BucketArquivoRetornoModel> BucketArquivosRetorno = Service.PostBasicAuth<List<BucketArquivoRetornoModel>>
                (
                    url: Configuracao.RepositorioArquivoUrl,
                    username: Configuracao.RepositorioArquivoUsername,
                    password: Configuracao.RepositorioArquivoPassword,
                    obj: ArquivosEnvio
                ).ToList();

                BucketArquivoModel BucketArquivo = new()
                {
                    NomeTabelaOrigemId = BucketNomeTabelaOrigem.NomeTabelaOrigemId,

                    TabelaOrigemId = File.Id,

                    UsuarioCadastroId = UsuarioCadastroId,

                    NomeArquivo = BucketArquivosRetorno[0].NomeArquivo,

                    TamanhoBytes = BucketArquivosRetorno[0].TamanhoBytes,

                    Url = BucketArquivosRetorno[0].Url,

                    PermissaoAcesso = BucketArquivosRetorno[0].PermissaoAcesso,

                    TipoCadastro = TipoCadastro
                };

                _context.BucketArquivo.Add(BucketArquivo);

                _context.SaveChanges();
            }
        }

        public void SendFiles(string CodigoTabelaOrigem, int TabelaOrigemId, int UsuarioCadastroId, List<BucketFileModel> Files)
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

            foreach (var File in Files)
            {
                NomeArquivo = Guid.NewGuid().ToString() + ".jpg";

                ArquivosEnvio.Add(new()
                {
                    NomeBucket = Configuracao.RepositorioArquivoNomeBucket,

                    NomeArquivo = NomeArquivo,

                    ArquivoBase64 = Convert.ToBase64String(File.File),

                    NomePasta = BucketNomeTabelaOrigem.DiretorioRemoto,

                    NomeArquivoCompleto = NomeArquivo,

                    NomeArquivoOriginal = NomeArquivo,

                    TipoArquivo = File.TipoCadastro
                });
            }

            HttpClientFactoryService Service = new(_httpClientFactory);

            List<BucketArquivoRetornoModel> BucketArquivosRetorno = Service.PostBasicAuth<List<BucketArquivoRetornoModel>>
            (
                url: Configuracao.RepositorioArquivoUrl,
                username: Configuracao.RepositorioArquivoUsername,
                password: Configuracao.RepositorioArquivoPassword,
                obj: ArquivosEnvio
            ).ToList();

            string TipoCadastro = string.Empty;

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

                    PermissaoAcesso = BucketArquivoRetorno.PermissaoAcesso
                };

                TipoCadastro = ArquivosEnvio
                    .Where(x => x.NomeArquivo == BucketArquivoRetorno.NomeArquivo)
                    .Select(x => x.TipoArquivo)
                    .FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(TipoCadastro))
                {
                    BucketArquivo.TipoCadastro = TipoCadastro;
                }

                _context.BucketArquivo.Add(BucketArquivo);

                _context.SaveChanges();
            }
        }

        public async Task<ImageViewModelList> DownloadFileAsync(string CodigoTabelaOrigem, int TabelaOrigemId)
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
                result = result
                    .OrderBy(x => x.RepositorioArquivoId)
                    .ToList();

                ResultView.Listagem = new();

                HttpClientFactoryService Service = new(_httpClientFactory);

                foreach (BucketArquivoModel BucketArquivo in result)
                {
                    ResultView.Listagem.Add(new()
                    {
                        Identificador = BucketArquivo.RepositorioArquivoId,

                        Imagem = await Service.DownloadFileAsync(BucketArquivo.Url)
                    }); ;
                }

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<ImageViewModelList> DownloadFilesAsync(string CodigoTabelaOrigem, List<int> ListagemTabelaOrigemId)
        {
            List<string> erros = new();

            if (string.IsNullOrWhiteSpace(CodigoTabelaOrigem))
            {
                erros.Add("Primeiro é necessário informar o Código Tabela de Origem");
            }

            if (ListagemTabelaOrigemId?.Count == 0)
            {
                erros.Add("Primeiro é necessário informar os Identificadorer da Tabela de Origem");
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
                         && ListagemTabelaOrigemId.Contains(x.TabelaOrigemId))
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                result = result
                    .OrderBy(x => x.RepositorioArquivoId)
                    .ToList();

                HttpClientFactoryService Service = new(_httpClientFactory);

                foreach (BucketArquivoModel BucketArquivo in result)
                {
                    ResultView.Listagem.Add(new()
                    {
                        Identificador = BucketArquivo.RepositorioArquivoId,

                        Imagem = await Service.DownloadFileAsync(BucketArquivo.Url)
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
                .Include(x => x.BucketNomeTabelaOrigem)
                .Where(x => x.BucketNomeTabelaOrigem.Codigo == CodigoTabelaOrigem
                         && ListagemTabelaOrigemId.Contains(x.TabelaOrigemId))
                .ToList();

            if (BucketArquivos.Count > 0)
            {
                foreach (BucketArquivoModel BucketArquivo in BucketArquivos)
                {
                    _context.BucketArquivo.Remove(BucketArquivo);

                    _context.SaveChanges();
                }

                HttpClientFactoryService Service = new(_httpClientFactory);

                foreach (BucketArquivoModel BucketArquivo in BucketArquivos)
                {
                    DeletarArquivoEnvioModel DeletarArquivoEnvio = new()
                    {
                        NomeBucket = Configuracao.RepositorioArquivoNomeBucket,

                        NomeArquivo = BucketArquivo.NomeArquivo,

                        NomePasta = BucketArquivo.BucketNomeTabelaOrigem.DiretorioRemoto
                    };

                    Service.DeleteBasicAuth<BucketMensagemRetornoModel>
                    (
                        url: Configuracao.RepositorioArquivoUrl,
                        username: Configuracao.RepositorioArquivoUsername,
                        password: Configuracao.RepositorioArquivoPassword,
                        obj: DeletarArquivoEnvio
                    );
                }
            }
        }

        public void DeleteFiles(string CodigoTabelaOrigem, int TabelaOrigemId)
        {
            DeleteFiles(CodigoTabelaOrigem, (List<int>)new() { TabelaOrigemId });
        }

        public void DeleteFile(int RepositorioArquivoId)
        {
            BucketArquivoModel BucketArquivo = _context.BucketArquivo
                .Where(w => w.RepositorioArquivoId == RepositorioArquivoId)
                .FirstOrDefault();

            if (BucketArquivo != null)
            {
                ConfiguracaoModel Configuracao = _context.Configuracao
                    .AsNoTracking()
                    .FirstOrDefault();

                _context.BucketArquivo.Remove(BucketArquivo);

                _context.SaveChanges();

                HttpClientFactoryService Service = new(_httpClientFactory);

                DeletarArquivoEnvioModel DeletarArquivoEnvio = new()
                {
                    NomeBucket = Configuracao.RepositorioArquivoNomeBucket,

                    NomeArquivo = BucketArquivo.NomeArquivo,

                    NomePasta = BucketArquivo.BucketNomeTabelaOrigem.DiretorioRemoto
                };

                Service.DeleteBasicAuth<BucketMensagemRetornoModel>
                (
                    url: Configuracao.RepositorioArquivoUrl,
                    username: Configuracao.RepositorioArquivoUsername,
                    password: Configuracao.RepositorioArquivoPassword,
                    obj: DeletarArquivoEnvio
                );
            }
        }
    }
}