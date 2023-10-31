using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Bucket;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.Veiculo;
using WebZi.Plataform.Data.Services.Vistoria;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.ClienteDeposito;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.GGV;
using WebZi.Plataform.Domain.ViewModel.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;

namespace WebZi.Plataform.Data.Services.GGV
{
    public class GgvService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public GgvService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        public MensagemViewModel CadastrarFotos(CadastroFotoVeiculoViewModel Fotos)
        {
            MensagemViewModel ResultView = new GrvService(_context)
                .ValidarInputGrv(Fotos.IdentificadorGrv, Fotos.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (Fotos.Fotos.Count == 0)
            {
                return MensagemViewHelper.GetBadRequest("Nenhuma imagem enviada para a API");
            }

            GrvModel Grv = new GrvService(_context).GetGrv(Fotos.IdentificadorGrv);
            
            if (!new[] { "V", "L", "U", "T", "R", "E", "B", "D", "1", "2", "3", "4" }.Contains(Grv.StatusOperacao.StatusOperacaoId))
            {
                return MensagemViewHelper.GetBadRequest($"O Status da Operação deste GRV não permite o envio de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            new BucketArquivoService(_context, _httpClientFactory)
                .SendFiles("GGVFOTOSVEICCAD", Fotos.IdentificadorGrv, Fotos.IdentificadorUsuario, Fotos.Fotos);

            return MensagemViewHelper.GetOkCreate(Fotos.Fotos.Count);
        }

        public async Task<MensagemViewModel> ExcluirFotosAsync(int GrvId, int UsuarioId, List<int> ListagemTabelaOrigemId)
        {
            if (ListagemTabelaOrigemId.Count == 0)
            {
                return MensagemViewHelper.GetBadRequest("Informe os Identificadores das Fotos");
            }

            MensagemViewModel ResultView = new GrvService(_context).ValidarInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await new GrvService(_context).GetGrvAsync(GrvId);

            if (!new[] { "E", "B", "D", "G", "L", "R", "T", "U", "V" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.GetBadRequest($"O GRV está em um Status de Operação que impede a exclusão de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            List<BucketArquivoModel> BucketArquivos = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .Where(x => x.TabelaOrigemId != GrvId
                         && ListagemTabelaOrigemId.Contains(x.RepositorioArquivoId)
                         && x.BucketNomeTabelaOrigem.Codigo == "GGVFOTOSVEICCAD")
                .AsNoTracking()
                .ToListAsync();

            if (BucketArquivos?.Count > 0)
            {
                List<string> erros = new()
                {
                    $"A(s) seguinte(s) Fotos não pertencem ao GRV {GrvId}:"
                };

                foreach (BucketArquivoModel BucketArquivo in BucketArquivos)
                {
                    erros.Add(BucketArquivo.RepositorioArquivoId.ToString());
                }

                return MensagemViewHelper.GetBadRequest(erros);
            }

            new BucketArquivoService(_context, _httpClientFactory)
                .DeleteFiles("GGVFOTOSVEICCAD", ListagemTabelaOrigemId);

            return MensagemViewHelper.GetOkDelete(BucketArquivos.Count, "Foto(s) excluída(s) com sucesso");
        }

        public async Task<DadosMestresViewModel> ListarDadosMestresAsync(byte TipoVeiculoId)
        {
            VistoriaService VistoriaService = new(_context);

            DadosMestresViewModel DadosMestres = new()
            {
                ListagemCorOstentada = await new SistemaService(_context, _mapper)
                    .ListarCorAsync(),

                ListagemEquipamento = await new VeiculoService(_context, _mapper)
                    .ListarEquipamentoOpcionalAsync(TipoVeiculoId),

                ListagemEstadoGeralVeiculo = await VistoriaService
                    .ListarEstadoGeralVeiculoAsync(),

                ListagemSituacaoChassi = await VistoriaService
                    .ListarSituacaoChassiAsync(),

                ListagemStatusVistoria = await VistoriaService
                    .ListarStatusVistoriaAsync(),

                ListagemTipoAvaria = await new TipoAvariaService(_context, _mapper)
                    .ListarTipoAvariaAsync(),

                ListagemTipoDirecao = await VistoriaService
                    .ListarTipoDirecaoAsync()
            };

            return DadosMestres;
        }

        public async Task<ImageViewModelList> ListarFotosAsync(int GrvId, int UsuarioId)
        {
            ImageViewModelList ResultView = new()
            {
                Mensagem = new GrvService(_context).ValidarInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            return await new BucketArquivoService(_context, _httpClientFactory)
                .DownloadFiles("GGVFOTOSVEICCAD", GrvId);
        }

        public async Task<MensagemViewModel> ValidarInformacoesPersistenciaAsync(GgvPersistenciaViewModel GrvPersistencia)
        {
            if (GrvPersistencia == null)
            {
                return MensagemViewHelper.GetBadRequest("O Modelo está nulo");
            }

            #region Validações de IDs
            List<string> erros = new();

            if (GrvPersistencia.IdentificadorGrv <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorGrvInvalido);
            }

            if (GrvPersistencia.IdentificadorUsuario <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }
            #endregion Validações de IDs

            if (erros.Count > 0)
            {
                return MensagemViewHelper.GetBadRequest(erros);
            }

            MensagemViewModel ResultView = new GrvService(_context)
                .ValidarInputGrv(GrvPersistencia.IdentificadorGrv, GrvPersistencia.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Include(x => x.Deposito)
                .Where(x => x.GrvId == GrvPersistencia.IdentificadorGrv)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            DateTime DataHoraPorDeposito = new DepositoService(_context)
                .GetDataHoraPorDeposito(Grv.DepositoId);

            if (GrvPersistencia.DataHoraGuarda.Date > DataHoraPorDeposito.Date)
            {
                ResultView.AvisosImpeditivos.Add("A Data da Guarda não pode ser maior do que a Data atual");
            }
            
            if (GrvPersistencia.DataHoraGuarda.Hour == 0 && GrvPersistencia.DataHoraGuarda.Minute == 0)
            {
                ResultView.AvisosImpeditivos.Add("A Hora da Guarda não pode ser igual a 00:00");
            }
            
            if (Grv.DataHoraRemocao > GrvPersistencia.DataHoraGuarda)
            {
                ResultView.AvisosImpeditivos.Add("• A Data/Hora da Guarda não pode ser maior à Data/Hora da Remoção");
            }

            if (Grv.DataHoraRemocao == GrvPersistencia.DataHoraGuarda)
            {
                ResultView.AvisosImpeditivos.Add("• A Data/Hora da Guarda não pode ser igual à Data/Hora da Remoção");
            }

            if (Grv.Deposito.GrvLimiteMinimoDatahoraGuarda == 0)
            {
                Grv.Deposito.GrvLimiteMinimoDatahoraGuarda = 30;
            }

            if (((DataHoraPorDeposito.Date - GrvPersistencia.DataHoraGuarda.Date).TotalDays) > Grv.Deposito.GrvLimiteMinimoDatahoraGuarda)
            {
                if (Grv.Deposito.GrvLimiteMinimoDatahoraGuarda >= 365)
                {
                    Grv.Deposito.GrvLimiteMinimoDatahoraGuarda /= 365;

                    if (Grv.Deposito.GrvLimiteMinimoDatahoraGuarda >= 1)
                    {
                        ResultView.AvisosImpeditivos.Add("A Data da Guarda não pode ser inferior a 1 ano.");
                    }
                    else
                    {
                        ResultView.AvisosImpeditivos.Add("A Data da Guarda não pode ser inferior a " + Grv.Deposito.GrvLimiteMinimoDatahoraGuarda + " anos.");
                    }
                }
            }

            //ClienteDepositoModel ClienteDeposito = await _context.ClienteDeposito
            //    .Where(x => x.ClienteId == Grv.ClienteId
            //             && x.DepositoId == Grv.DepositoId)
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync();

            return ResultView;
        }
    }
}