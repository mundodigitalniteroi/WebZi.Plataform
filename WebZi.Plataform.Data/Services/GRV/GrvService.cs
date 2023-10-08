using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Numerics;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;
using WebZi.Plataform.Domain.Views.Usuario;

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
                erros.Add("Identificador do GrvId inválido");
            }

            if (UsuarioId <= 0)
            {
                erros.Add("Identificador do Usuário inválido");
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
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("GRV não encontrado");

                return ResultView;
            }
            else if (!new GrvService(_context, _mapper).UserCanAccessGrv(Grv, UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized("Usuário sem permissão de acesso ao GRV");

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
                erros.Add("Informe o Número do Processo");
            }
            else if (!NumberHelper.IsNumber(NumeroFormularioGrv) || Convert.ToInt64(NumeroFormularioGrv) <= 0)
            {
                erros.Add("Número do Processo inválido");
            }

            if (ClienteId <= 0)
            {
                erros.Add("Identificador do Cliente inválido");
            }

            if (DepositoId <= 0)
            {
                erros.Add("Identificador do Depósito inválido ");
            }

            if (UsuarioId <= 0)
            {
                erros.Add("Identificador do Usuário inválido");
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
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Cliente inexistente");

                return ResultView;
            }

            DepositoModel Deposito = await _context.Deposito
                .Where(w => w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Deposito == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Depósito inexistente");

                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Where(w => w.NumeroFormularioGrv.Equals(NumeroFormularioGrv) && w.ClienteId.Equals(ClienteId) && w.DepositoId.Equals(DepositoId))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("GRV não encontrado");

                return ResultView;
            }
            else if (!new GrvService(_context, _mapper).UserCanAccessGrv(Grv, UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized("Usuário sem permissão de acesso ao GRV");

                return ResultView;
            }

            ResultView.Grvs = _mapper.Map<List<GrvViewModel>>(Grv);

            ResultView.Mensagem = MensagemViewHelper.GetOkFound();

            return ResultView;
        }

        public async Task<GrvPesquisaResultViewModelList> Pesquisar(GrvPesquisaInputViewModel GrvPesquisa)
        {
            List<string> erros = new();

            if (GrvPesquisa.ListaCodigoProduto.Count == 0)
            {
                erros.Add("Primeiro é necessário informar ao menos um Código de Produto");
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
                        erros.Add($"Código de Produto inexistente: {Codigo}");
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
                        erros.Add($"Status Operação inexistente: {StatusOperacao}");
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(GrvPesquisa.NumeroProcesso) &&
                (!NumberHelper.IsNumber(GrvPesquisa.NumeroProcesso) || Convert.ToInt64(GrvPesquisa.NumeroProcesso) <= 0))
            {
                erros.Add("Número do Processo inválido");
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
                erros.Add("Flag do Veículo não identificado precisa ser \"S\" ou \"N\" (sem aspas)");
            }

            if (GrvPesquisa.ClienteId < 0)
            {
                erros.Add("Identificador do Cliente inválido");
            }

            if (GrvPesquisa.DepositoId < 0)
            {
                erros.Add("Identificador do Depósito inválido");
            }

            if (GrvPesquisa.UsuarioId <= 0)
            {
                erros.Add("Identificador do Usuário inválido");
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
            else if (GrvPesquisa.DataFinalRemocao.Value.Date > DateTime.Now.Date)
            {
                erros.Add("A Data Final não pode ser maior do que a Data Atual");
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
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("A pesquisa não retornou registros");

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
            ViewUsuarioClienteDepositoModel Usuario = _context.ViewUsuarioClienteDeposito
                .Where(w => w.UsuarioId == UsuarioId && w.ClienteId == ClienteId && w.DepositoId == DepositoId)
                .FirstOrDefault();

            return Usuario != null;
        }

        public bool UserCanAccessGrv(GrvModel Grv, int UsuarioId)
        {
            if (Grv == null)
            {
                return false;
            }

            return _context.ViewUsuarioClienteDeposito
                .Where(w => w.UsuarioId == UsuarioId && w.ClienteId == Grv.ClienteId && w.DepositoId == Grv.DepositoId)
                .FirstOrDefault() != null;
        }
    }
}