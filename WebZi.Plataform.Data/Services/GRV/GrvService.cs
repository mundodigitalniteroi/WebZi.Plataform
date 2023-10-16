using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.ServiceModel.Channels;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Bucket;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Condutor;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;

namespace WebZi.Plataform.Domain.Services.GRV
{
    public class GrvService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GrvService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GrvViewModelList> GetById(int GrvId, int UsuarioId)
        {
            List<string> erros = new();

            if (GrvId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorGrvInvalido);
            }

            if (UsuarioId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            GrvViewModelList ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Where(w => w.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.GrvNaoEncontrado);

                return ResultView;
            }
            else if (!new GrvService(_context, _mapper).UserCanAccessGrv(Grv, UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized(MensagemPadraoEnum.UsuarioSemPermissaoAcessoGrv);

                return ResultView;
            }

            ResultView.Grvs = _mapper.Map<List<GrvViewModel>>(Grv);

            ResultView.Mensagem = MensagemViewHelper.GetOkFound();

            return ResultView;
        }

        public async Task<GrvViewModelList> GetByNumeroFormularioGrv(string NumeroFormularioGrv, int ClienteId, int DepositoId, int UsuarioId)
        {
            List<string> erros = new();

            if (string.IsNullOrWhiteSpace(NumeroFormularioGrv))
            {
                erros.Add(MensagemPadraoEnum.InformeNumeroProcesso);
            }
            else if (!NumberHelper.IsNumber(NumeroFormularioGrv) || Convert.ToInt64(NumeroFormularioGrv) <= 0)
            {
                erros.Add(MensagemPadraoEnum.NumeroProcessoInvalido);
            }

            if (ClienteId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
            }

            if (DepositoId <= 0)
            {
                erros.Add("Identificador do Depósito inválido ");
            }

            if (UsuarioId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            GrvViewModelList ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            ClienteModel Cliente = await _context.Cliente
                .Where(w => w.ClienteId == ClienteId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Cliente == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.ClienteNaoEncontrado);

                return ResultView;
            }

            DepositoModel Deposito = await _context.Deposito
                .Where(w => w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Deposito == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.DepositoNaoEncontrado);

                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Where(w => w.NumeroFormularioGrv.Equals(NumeroFormularioGrv) && w.ClienteId.Equals(ClienteId) && w.DepositoId.Equals(DepositoId))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.GrvNaoEncontrado);

                return ResultView;
            }
            else if (!new GrvService(_context, _mapper).UserCanAccessGrv(Grv, UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized(MensagemPadraoEnum.UsuarioSemPermissaoAcessoGrv);

                return ResultView;
            }

            ResultView.Grvs = _mapper.Map<List<GrvViewModel>>(Grv);

            ResultView.Mensagem = MensagemViewHelper.GetOkFound();

            return ResultView;
        }

        public async Task<GrvPesquisaResultViewModelList> Search(GrvPesquisaInputViewModel GrvPesquisa)
        {
            List<string> erros = new();

            if (GrvPesquisa.ListaCodigoProduto.Count == 0)
            {
                erros.Add("Informe ao menos um Código de Produto");
            }
            else
            {
                List<string> Produtos = await _context.FaturamentoProduto.Select(s => s.FaturamentoProdutoId)
                    .AsNoTracking()
                    .ToListAsync();

                foreach (string Codigo in GrvPesquisa.ListaCodigoProduto)
                {
                    if (Produtos.FirstOrDefault(f => f == Codigo.ToUpper().Trim()) == null)
                    {
                        erros.Add($"Código de Produto não encontrado: {Codigo}");
                    }
                }
            }

            if (GrvPesquisa.ListaStatusOperacao.Count > 0)
            {
                List<string> StatusOperacoes = await _context.StatusOperacao.Select(s => s.StatusOperacaoId)
                    .AsNoTracking()
                    .ToListAsync();

                foreach (string StatusOperacao in GrvPesquisa.ListaStatusOperacao)
                {
                    if (StatusOperacoes.FirstOrDefault(f => f == StatusOperacao.ToUpper().Trim()) == null)
                    {
                        erros.Add($"Status Operação não encontrado: {StatusOperacao}");
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(GrvPesquisa.NumeroProcesso) &&
                (!NumberHelper.IsNumber(GrvPesquisa.NumeroProcesso) || Convert.ToInt64(GrvPesquisa.NumeroProcesso) <= 0))
            {
                erros.Add(MensagemPadraoEnum.NumeroProcessoInvalido);
            }

            if (!string.IsNullOrWhiteSpace(GrvPesquisa.Placa) && !VeiculoHelper.IsPlaca(GrvPesquisa.Placa))
            {
                erros.Add("Placa inválida");
            }

            if (!string.IsNullOrWhiteSpace(GrvPesquisa.Chassi) && !VeiculoHelper.IsChassi(GrvPesquisa.Chassi))
            {
                erros.Add("Chassi inválido");
            }

            if (!string.IsNullOrWhiteSpace(GrvPesquisa.FlagVeiculoNaoIdentificado) && GrvPesquisa.FlagVeiculoNaoIdentificado != "S" && GrvPesquisa.FlagVeiculoNaoIdentificado != "N")
            {
                erros.Add("Flag do Veículo não identificado inválido, informe \"S\" ou \"N\" (sem aspas)");
            }

            if (GrvPesquisa.ClienteId < 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
            }

            if (GrvPesquisa.DepositoId < 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorDepositoInvalido);
            }

            if (GrvPesquisa.UsuarioId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            if (!GrvPesquisa.DataInicialRemocao.HasValue)
            {
                GrvPesquisa.DataInicialRemocao = DateTime.Now.AddDays(-30);
            }

            if (!GrvPesquisa.DataFinalRemocao.HasValue)
            {
                GrvPesquisa.DataFinalRemocao = DateTime.Now;
            }

            if (GrvPesquisa.DataInicialRemocao.Value.Date > GrvPesquisa.DataFinalRemocao.Value.Date)
            {
                erros.Add("A Data Inicial não pode ser maior do que a Data Final");
            }
            else if ((GrvPesquisa.DataFinalRemocao.Value.Date - GrvPesquisa.DataInicialRemocao.Value.Date).Days > 30)
            {
                erros.Add("O período de pesquisa não pode superar 30 dias");
            }

            GrvPesquisaResultViewModelList ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            List<GrvModel> Grvs = await _context.Grv
                .Include(i => i.Cliente)
                .Include(i => i.Deposito)
                .Include(i => i.StatusOperacao)
                .Include(i => i.UsuarioClienteDepositoGrvModel)
                .Where(w => w.UsuarioClienteDepositoGrvModel.UsuarioId == GrvPesquisa.UsuarioId && w.UsuarioClienteDepositoGrvModel.FaturamentoProdutoId == w.FaturamentoProdutoId &&
                            (w.DataHoraRemocao.Date >= GrvPesquisa.DataInicialRemocao.Value.Date && w.DataHoraRemocao.Date <= GrvPesquisa.DataFinalRemocao.Value.Date) &&
                            (GrvPesquisa.ListaCodigoProduto.Count > 0 ? GrvPesquisa.ListaCodigoProduto.Contains(w.FaturamentoProdutoId) : true) &&
                            (GrvPesquisa.ListaStatusOperacao.Count > 0 ? GrvPesquisa.ListaStatusOperacao.Contains(w.StatusOperacaoId) : true) &&
                            (!string.IsNullOrWhiteSpace(GrvPesquisa.NumeroProcesso) ? w.NumeroFormularioGrv == GrvPesquisa.NumeroProcesso : true) &&
                            (!string.IsNullOrWhiteSpace(GrvPesquisa.Placa) ? w.Placa == GrvPesquisa.Placa : true) &&
                            (!string.IsNullOrWhiteSpace(GrvPesquisa.Chassi) ? w.Chassi == GrvPesquisa.Chassi : true) &&
                            (!string.IsNullOrWhiteSpace(GrvPesquisa.FlagVeiculoNaoIdentificado) ? w.FlagVeiculoNaoIdentificado == GrvPesquisa.FlagVeiculoNaoIdentificado : true) &&
                            (GrvPesquisa.ClienteId > 0 ? w.ClienteId == GrvPesquisa.ClienteId : true) &&
                            (GrvPesquisa.DepositoId > 0 ? w.DepositoId == GrvPesquisa.DepositoId : true))
                .OrderBy(o => Convert.ToInt64(o.NumeroFormularioGrv))
                .Take(100)
                .AsNoTracking()
                .ToListAsync();

            if (Grvs?.Count == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("A pesquisa não retornou registro");

                return ResultView;
            }

            foreach (GrvModel Grv in Grvs)
            {
                ResultView.Grvs.Add(new()
                {
                    GrvId = Grv.GrvId,

                    StatusOperacaoId = Grv.StatusOperacaoId,

                    NumeroProcesso = Grv.NumeroFormularioGrv,

                    Placa = Grv.Placa,

                    Chassi = Grv.Chassi,

                    StatusOperacao = Grv.StatusOperacao.Descricao,

                    DataHoraRemocao = Grv.DataHoraRemocao,

                    DataHoraGuarda = Grv.DataHoraGuarda,

                    Cliente = Grv.Cliente.Nome,

                    Deposito = Grv.Deposito.Nome
                });
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(Grvs.Count);

            return ResultView;
        }

        public bool GrvExists(int GrvId)
        {
            return _context.Grv
                .Where(w => w.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefault() != null;
        }

        public bool UserCanAccessGrv(int ClienteId, int DepositoId, int UsuarioId)
        {
            return _context.ViewUsuarioClienteDeposito
                .Where(w => w.ClienteId == ClienteId && w.DepositoId == DepositoId && w.UsuarioId == UsuarioId)
                .AsNoTracking()
                .FirstOrDefault() != null;
        }

        public bool UserCanAccessGrv(GrvModel Grv, int UsuarioId)
        {
            return Grv != null && _context.ViewUsuarioClienteDeposito
                .Where(w => w.ClienteId == Grv.ClienteId && w.DepositoId == Grv.DepositoId && w.UsuarioId == UsuarioId)
                .AsNoTracking()
                .FirstOrDefault() != null;
        }

        public async Task<MensagemViewModel> SendFiles(GrvFotoViewModel Fotos)
        {
            List<string> erros = new();

            if (Fotos.GrvId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorGrvInvalido);
            }

            if (Fotos.UsuarioId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            if (Fotos.Fotos.Count == 0)
            {
                erros.Add("");
            }

            MensagemViewModel ResultView = new();

            if (erros.Count > 0)
            {
                ResultView = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Where(w => w.GrvId == Fotos.GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                ResultView = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.GrvNaoEncontrado);

                return ResultView;
            }
            else if (!new GrvService(_context, _mapper).UserCanAccessGrv(Grv, Fotos.UsuarioId))
            {
                ResultView = MensagemViewHelper.GetUnauthorized(MensagemPadraoEnum.UsuarioSemPermissaoAcessoGrv);

                return ResultView;
            }

            new BucketArquivoService(_context)
                .SendFiles("GRVFOTOSVEICCAD", Fotos.GrvId, Fotos.UsuarioId, Fotos.Fotos);

            return MensagemViewHelper.GetOkCreate(Fotos.Fotos.Count);
        }

        public async Task<MensagemViewModel> ValidarInformacoesParaCadastro(GrvCadastroViewModel GrvCadastro)
        {
            #region Validações de IDs
            List<string> erros = new();

            if (GrvCadastro.ClienteId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
            }

            if (GrvCadastro.DepositoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorDepositoInvalido);
            }

            if (GrvCadastro.TipoVeiculoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorTipoVeiculoInvalido);
            }

            if (GrvCadastro.ReboquistaId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorReboquistaInvalido);
            }

            if (GrvCadastro.ReboqueId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorReboqueInvalido);
            }

            if (GrvCadastro.AutoridadeResponsavelId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorAutoridadeResponsavelInvalido);
            }

            if (GrvCadastro.CorId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorCorInvalido);
            }

            if (GrvCadastro.MarcaModeloId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorMarcaModeloInvalido);
            }

            if (GrvCadastro.MotivoApreensaoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorMotivoApreensaoInvalido);
            }

            if (GrvCadastro.UsuarioId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            if (string.IsNullOrWhiteSpace(GrvCadastro.CodigoProduto))
            {
                erros.Add("Informe o Código de Produto");
            }

            MensagemViewModel ResultView = new();

            if (erros.Count > 0)
            {
                ResultView = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }
            #endregion Validações de IDs

            #region Validações do Usuário
            if (!new UsuarioService(_context).IsUserActive(GrvCadastro.UsuarioId))
            {
                return MensagemViewHelper.GetUnauthorized();
            }
            #endregion Validações do Usuário

            #region Consultas
            ClienteModel Cliente = await _context.Cliente
                .Where(w => w.ClienteId == GrvCadastro.ClienteId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Cliente == null)
            {
                ResultView.AvisosImpeditivos.Add(MensagemPadraoEnum.ClienteNaoEncontrado);
            }

            DepositoModel Deposito = await _context.Deposito
                .Where(w => w.DepositoId == GrvCadastro.DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Deposito == null)
            {
                ResultView.AvisosImpeditivos.Add(MensagemPadraoEnum.DepositoNaoEncontrado);
            }

            TipoVeiculoModel TipoVeiculo = await _context.TipoVeiculo
                .Where(w => w.TipoVeiculoId == GrvCadastro.TipoVeiculoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (TipoVeiculo == null)
            {
                ResultView.AvisosImpeditivos.Add("Tipo do Veículo não encontrado");
            }

            ReboquistaModel Reboquista = await _context.Reboquista
                .Where(w => w.ReboquistaId == GrvCadastro.ReboquistaId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Reboquista == null)
            {
                ResultView.AvisosImpeditivos.Add("Reboquista não encontrado");
            }

            ReboqueModel Reboque = await _context.Reboque
                .Where(w => w.ReboqueId == GrvCadastro.ReboqueId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Reboque == null)
            {
                ResultView.AvisosImpeditivos.Add("Reboque não encontrado");
            }

            AutoridadeResponsavelModel AutoridadeResponsavel = await _context.AutoridadeResponsavel
                .Where(w => w.AutoridadeResponsavelId == GrvCadastro.AutoridadeResponsavelId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (AutoridadeResponsavel == null)
            {
                ResultView.AvisosImpeditivos.Add("Autoridade Responsável não encontrada");
            }

            CorModel Cor = await _context.Cor
                .Where(w => w.CorId == GrvCadastro.CorId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Cor == null)
            {
                ResultView.AvisosImpeditivos.Add("Cor não encontrada");
            }

            MarcaModeloModel MarcaModelo = await _context.MarcaModelo
                .Where(w => w.DetranMarcaModeloId == GrvCadastro.MarcaModeloId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (MarcaModelo == null)
            {
                ResultView.AvisosImpeditivos.Add("Marca/Modelo não encontrado");
            }

            MotivoApreensaoModel MotivoApreensao = await _context.MotivoApreensao
                .Where(w => w.MotivoApreensaoId == GrvCadastro.MotivoApreensaoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (MotivoApreensao == null)
            {
                ResultView.AvisosImpeditivos.Add("Motivo de Apreensão não encontrado");
            }

            var Produtos = await _context.FaturamentoProduto
                .Where(w => w.FaturamentoProdutoId == GrvCadastro.CodigoProduto)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (MotivoApreensao == null)
            {
                ResultView.AvisosImpeditivos.Add("Código do Produto não encontrado");
            }

            GrvModel Grv = await _context.Grv
                .Where(w => w.NumeroFormularioGrv == GrvCadastro.NumeroProcesso && w.ClienteId == GrvCadastro.ClienteId && w.DepositoId == GrvCadastro.DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv != null)
            {
                ResultView.AvisosImpeditivos.Add("GRV já cadastrado");
            }
            #endregion Consultas

            if (ResultView.AvisosImpeditivos.Count > 0)
            {
                ResultView.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;
            }
            else
            {
                ResultView.HtmlStatusCode = HtmlStatusCodeEnum.Ok;
            }

            return ResultView;
        }

        public GrvCadastradoViewModel Create(GrvCadastroViewModel GrvCadastro)
        {
            GrvCadastradoViewModel ResultView = new();

            GrvModel Grv = _mapper.Map<GrvModel>(GrvCadastro);

            Grv.Condutor = _mapper.Map<CondutorModel>(GrvCadastro.Condutor);

            Grv.EnquadramentosInfracoes = _mapper.Map<List<EnquadramentoInfracaoGrvModel>>(GrvCadastro.EnquadramentosInfracoes);

            if (GrvCadastro.EnquadramentosInfracoes.Count > 0)
            {
                GrvCadastro.EnquadramentosInfracoes = GrvCadastro.EnquadramentosInfracoes
                    .OrderBy(x => x)
                    .ToList();
            }

            if (GrvCadastro.Lacres.Count > 0)
            {
                GrvCadastro.Lacres = GrvCadastro.Lacres
                    .ConvertAll(d => d.ToUpper())
                    .ConvertAll(d => d.Trim())
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();

                foreach (string item in GrvCadastro.Lacres)
                {
                    Grv.Lacres.Add(new LacreModel { UsuarioCadastroId = GrvCadastro.UsuarioId, Lacre = item });
                }
            }

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Grv.Add(Grv);

                    _context.SaveChanges();

                    transaction.Commit();

                    ResultView.GrvId = Grv.GrvId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                    return ResultView;
                }
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkCreate();

            return ResultView;
        }
    }
}