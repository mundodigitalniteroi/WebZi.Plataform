using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebZi.Plataform.CrossCutting.Contacts;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Localizacao;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Bucket;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.ClienteDeposito;
using WebZi.Plataform.Domain.Models.Condutor;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Documento;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Faturamento;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;
using WebZi.Plataform.Domain.ViewModel.Localizacao;
using WebZi.Plataform.Domain.Views.Usuario;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebZi.Plataform.Domain.Services.GRV
{
    public class GrvService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _provider;

        public GrvService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GrvService(AppDbContext context)
        {
            _context = context;
        }

        public GrvService(AppDbContext context, IMapper mapper, IServiceProvider provider)
        {
            _context = context;
            _mapper = mapper;
            _provider = provider;
        }

        public GrvCadastradoViewModel Create(GrvCadastroViewModel GrvCadastro)
        {
            GrvCadastradoViewModel ResultView = new();

            GrvModel Grv = new()
            {
                ClienteId = GrvCadastro.IdentificadorCliente,

                DepositoId = GrvCadastro.IdentificadorDeposito,

                TipoVeiculoId = GrvCadastro.IdentificadorTipoVeiculo,

                ReboquistaId = GrvCadastro.IdentificadorReboquista,

                ReboqueId = GrvCadastro.IdentificadorReboque,

                AutoridadeResponsavelId = GrvCadastro.IdentificadorAutoridadeResponsavel,

                CorId = GrvCadastro.IdentificadorCor,

                MarcaModeloId = GrvCadastro.IdentificadorMarcaModelo,

                MotivoApreensaoId = GrvCadastro.IdentificadorMotivoApreensao,

                UsuarioCadastroId = GrvCadastro.IdentificadorUsuario,

                NumeroFormularioGrv = GrvCadastro.NumeroProcesso.Trim(),

                FaturamentoProdutoId = GrvCadastro.CodigoProduto,

                MatriculaAutoridadeResponsavel = GrvCadastro.MatriculaAutoridadeResponsavel.ToUpper().Trim(),

                NomeAutoridadeResponsavel = GrvCadastro.NomeAutoridadeResponsavel.ToUpper().Trim(),

                Placa = GrvCadastro.Placa.ToUpper(),

                PlacaOstentada = GrvCadastro.PlacaOstentada.ToUpper(),

                Chassi = GrvCadastro.Chassi.ToUpper().Trim(),

                Renavam = GrvCadastro.Renavam.ToUpper().Trim(),

                Rfid = GrvCadastro.Rfid.ToUpper().Trim(),

                EnderecoLocalizacaoVeiculoLogradouro = GrvCadastro.EnderecoLocalizacaoVeiculoLogradouro.ToUpper().Trim(),

                EnderecoLocalizacaoVeiculoNumero = GrvCadastro.EnderecoLocalizacaoVeiculoNumero.ToUpper().Trim(),

                EnderecoLocalizacaoVeiculoComplemento = GrvCadastro.EnderecoLocalizacaoVeiculoComplemento.ToUpper().Trim(),

                EnderecoLocalizacaoVeiculoBairro = GrvCadastro.EnderecoLocalizacaoVeiculoBairro.ToUpper().Trim(),

                EnderecoLocalizacaoVeiculoMunicipio = GrvCadastro.EnderecoLocalizacaoVeiculoMunicipio.ToUpper().Trim(),

                EnderecoLocalizacaoVeiculoUF = GrvCadastro.EnderecoLocalizacaoVeiculoUF.ToUpper().Trim(),

                EnderecoLocalizacaoVeiculoReferencia = GrvCadastro.EnderecoLocalizacaoVeiculoReferencia.ToUpper().Trim(),

                EnderecoLocalizacaoVeiculoPontoReferencia = GrvCadastro.EnderecoLocalizacaoVeiculoPontoReferencia.ToUpper().Trim(),

                NumeroChave = GrvCadastro.NumeroChave.ToUpper().Trim(),

                EstacionamentoSetor = GrvCadastro.EstacionamentoSetor.ToUpper().Trim(),

                EstacionamentoNumeroVaga = GrvCadastro.EstacionamentoNumeroVaga.ToUpper().Trim(),

                Latitude = GrvCadastro.Latitude.ToUpper().Trim(),

                Longitude = GrvCadastro.Longitude.ToUpper().Trim(),

                VeiculoUF = GrvCadastro.VeiculoUF.ToUpper().Trim(),

                DataHoraRemocao = GrvCadastro.DataHoraRemocao,

                LatitudeAcautelamento = GrvCadastro.LatitudeAcautelamento.ToUpper().Trim(),

                LongitudeAcautelamento = GrvCadastro.LongitudeAcautelamento.ToUpper().Trim(),

                FlagComboio = GrvCadastro.FlagVeiculoNaoUsouReboque,

                FlagVeiculoNaoIdentificado = GrvCadastro.FlagVeiculoNaoIdentificado,

                FlagVeiculoSemRegistro = GrvCadastro.FlagVeiculoSemRegistro,

                FlagVeiculoRoubadoFurtado = GrvCadastro.FlagVeiculoRoubadoFurtado,

                FlagEstadoLacre = GrvCadastro.FlagEstadoLacre,

                FlagVeiculoNaoOstentaPlaca = GrvCadastro.FlagVeiculoNaoOstentaPlaca,

                Condutor = _mapper.Map<CondutorModel>(GrvCadastro.Condutor),

                ListagemEnquadramentoInfracao = _mapper.Map<List<EnquadramentoInfracaoGrvModel>>(GrvCadastro.EnquadramentosInfracoes)
            };

            if (!string.IsNullOrWhiteSpace(GrvCadastro.EnderecoLocalizacaoVeiculoCEP))
            {
                EnderecoViewModel Endereco = new EnderecoService(_context, _mapper).GetByCEP(GrvCadastro.EnderecoLocalizacaoVeiculoCEP.Replace("-", ""));

                if (Endereco != null)
                {
                    Grv.EnderecoLocalizacaoVeiculoCEPId = Endereco.IdentificadorCEP;

                    Grv.EnderecoLocalizacaoVeiculoLogradouro = Endereco.Logradouro;

                    Grv.EnderecoLocalizacaoVeiculoBairro = Endereco.Bairro;

                    Grv.EnderecoLocalizacaoVeiculoMunicipio = Endereco.MunicipioPtbr;

                    Grv.EnderecoLocalizacaoVeiculoUF = Endereco.UF;
                }
            }

            if (GrvCadastro.EnquadramentosInfracoes?.Count > 0)
            {
                GrvCadastro.EnquadramentosInfracoes = GrvCadastro.EnquadramentosInfracoes
                    .OrderBy(x => x.NumeroInfracao)
                    .ToList();

                GrvCadastro.EnquadramentosInfracoes.ForEach(x => x.NumeroInfracao = x.NumeroInfracao.ToUpper().Trim());
            }

            if (GrvCadastro.Lacres?.Count > 0)
            {
                GrvCadastro.Lacres = GrvCadastro.Lacres
                    .ConvertAll(x => x.ToUpper().Trim())
                    .OrderBy(x => x)
                    .ToList();

                foreach (string item in GrvCadastro.Lacres)
                {
                    Grv.ListagemLacre.Add(new LacreModel { UsuarioCadastroId = GrvCadastro.IdentificadorUsuario, Lacre = item });
                }
            }

            ClienteDepositoModel ClienteDeposito = _context.ClienteDeposito
                .Where(x => x.ClienteId == GrvCadastro.IdentificadorCliente
                        && x.DepositoId == GrvCadastro.IdentificadorDeposito)
                .AsNoTracking()
                .FirstOrDefault();

            if (ClienteDeposito != null && ClienteDeposito.FlagCadastrarGrvBloqueado == "S")
            {
                Grv.StatusOperacaoId = "B";
            }

            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Grv.Add(Grv);

                    _context.SaveChanges();

                    transaction.Commit();

                    if (ClienteDeposito.Cliente != null && ClienteDeposito.Cliente.FlagClientePossuiCodigoIdentificacao == "S")
                    {
                        ClienteCodigoIdentificacaoModel ClienteCodigoIdentificacao = new()
                        {
                            GrvId = Grv.GrvId,

                            UsuarioCadastroId = GrvCadastro.IdentificadorUsuario,

                            CodigoIdentificacao = GrvCadastro.CodigoIdentificacaoCliente
                        };

                        _context.ClienteCodigoIdentificacao.Add(ClienteCodigoIdentificacao);

                        _context.SaveChanges();
                    }

                    ResultView.IdentificadorGrv = Grv.GrvId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                    return ResultView;
                }
            }

            if (GrvCadastro.Fotos?.Count > 0)
            {
                SendFiles(new()
                {
                    IdentificadorGrv = Grv.GrvId,

                    IdentificadorUsuario = Grv.UsuarioCadastroId,

                    Fotos = GrvCadastro.Fotos
                });
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkCreate();

            return ResultView;
        }

        public async Task<GrvViewModelList> GetById(int GrvId, int UsuarioId)
        {
            GrvViewModelList ResultView = new()
            {
                Mensagem = ValidarInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Where(x => x.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);

                return ResultView;
            }

            ResultView.Mensagem = new GrvService(_context).ValidarInputGrv(Grv, UsuarioId);

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            ResultView.Listagem.Add(_mapper.Map<GrvViewModel>(Grv));

            ResultView.Mensagem = MensagemViewHelper.GetOkFound();

            return ResultView;
        }

        public async Task<GrvViewModelList> GetByNumeroFormularioGrv(string NumeroFormularioGrv, string CodigoProduto, int ClienteId, int DepositoId, int UsuarioId)
        {
            GrvViewModelList ResultView = new()
            {
                Mensagem = ValidarInputGrv(NumeroFormularioGrv, CodigoProduto, ClienteId, DepositoId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Where(x => x.NumeroFormularioGrv == NumeroFormularioGrv
                         && x.ClienteId == ClienteId
                         && x.DepositoId == DepositoId
                         && x.FaturamentoProdutoId == CodigoProduto)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);

                return ResultView;
            }

            ResultView.Mensagem = new GrvService(_context).ValidarInputGrv(Grv, UsuarioId);

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            ResultView.Listagem.Add(_mapper.Map<GrvViewModel>(Grv));

            ResultView.Mensagem = MensagemViewHelper.GetOkFound();

            return ResultView;
        }

        public async Task<GrvPesquisaResultViewModelList> Search(GrvPesquisaInputViewModel GrvPesquisa)
        {
            List<string> erros = new();

            if (GrvPesquisa.ListagemCodigoProduto.Count == 0)
            {
                erros.Add("Informe ao menos um Código de Produto");
            }
            else
            {
                List<string> Produtos = await _context.FaturamentoProduto
                    .Select(x => x.FaturamentoProdutoId)
                    .AsNoTracking()
                    .ToListAsync();

                foreach (string Codigo in GrvPesquisa.ListagemCodigoProduto)
                {
                    if (Produtos.FirstOrDefault(f => f == Codigo.ToUpper().Trim()) == null)
                    {
                        erros.Add($"{MensagemPadraoEnum.NaoEncontradoFaturamentoProduto}: {Codigo}");
                    }
                }
            }

            if (GrvPesquisa.ListagemStatusOperacao.Count > 0)
            {
                List<string> StatusOperacoes = await _context.StatusOperacao
                    .Select(x => x.StatusOperacaoId)
                    .AsNoTracking()
                    .ToListAsync();

                foreach (string StatusOperacao in GrvPesquisa.ListagemStatusOperacao)
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
                erros.Add(MensagemPadraoEnum.NumeroProcessoInvalido);
            }

            if (!string.IsNullOrWhiteSpace(GrvPesquisa.FlagVeiculoNaoIdentificado) && GrvPesquisa.FlagVeiculoNaoIdentificado != "S" && GrvPesquisa.FlagVeiculoNaoIdentificado != "N")
            {
                erros.Add("Flag do Veículo não identificado inválido, informe \"S\" ou \"N\" (sem aspas)");
            }

            if (!string.IsNullOrWhiteSpace(GrvPesquisa.FlagVeiculoNaoIdentificado))
            {
                if (!string.IsNullOrWhiteSpace(GrvPesquisa.PlacaVeiculo))
                {
                    erros.Add("Ao informar que o Veículo não possui identificação, não informe a Placa");
                }

                if (!string.IsNullOrWhiteSpace(GrvPesquisa.Chassi))
                {
                    erros.Add("Ao informar que o Veículo não possui identificação, não informe o Chassi");
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(GrvPesquisa.PlacaVeiculo) && !VeiculoHelper.IsPlaca(GrvPesquisa.PlacaVeiculo))
                {
                    erros.Add("Placa inválida");
                }

                if (!string.IsNullOrWhiteSpace(GrvPesquisa.Chassi) && !VeiculoHelper.IsChassi(GrvPesquisa.Chassi))
                {
                    erros.Add("Chassi inválido");
                }
            }

            if (GrvPesquisa.IdentificadorUsuario <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            if (!GrvPesquisa.DataInicialRemocao.HasValue)
            {
                GrvPesquisa.DataInicialRemocao = DateTime.Now.AddDays(-180);
            }

            if (!GrvPesquisa.DataFinalRemocao.HasValue)
            {
                GrvPesquisa.DataFinalRemocao = DateTime.Now;
            }

            if (GrvPesquisa.DataInicialRemocao.Value.Date > GrvPesquisa.DataFinalRemocao.Value.Date)
            {
                erros.Add("A Data Inicial não pode ser maior do que a Data Final");
            }
            else if ((GrvPesquisa.DataFinalRemocao.Value.Date - GrvPesquisa.DataInicialRemocao.Value.Date).Days > 180)
            {
                erros.Add("O período de pesquisa não pode superar 180 dias");
            }

            GrvPesquisaResultViewModelList ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            List<GrvModel> result = await _context.Grv
                .Include(x => x.Cliente)
                .Include(x => x.Deposito)
                .Include(x => x.StatusOperacao)
                .Include(x => x.UsuarioClienteDepositoGrv)
                .Where(x => x.UsuarioClienteDepositoGrv.UsuarioId == GrvPesquisa.IdentificadorUsuario &&
                             GrvPesquisa.ListagemCodigoProduto.Contains(x.FaturamentoProdutoId) &&
                             x.UsuarioClienteDepositoGrv.FaturamentoProdutoCodigo == x.FaturamentoProdutoId &&
                            (x.DataHoraRemocao.Date >= GrvPesquisa.DataInicialRemocao.Value.Date && x.DataHoraRemocao.Date <= GrvPesquisa.DataFinalRemocao.Value.Date) &&
                            (GrvPesquisa.ListagemCodigoProduto.Count > 0 ? GrvPesquisa.ListagemCodigoProduto.Contains(x.FaturamentoProdutoId) : true) &&
                            (GrvPesquisa.ListagemStatusOperacao.Count > 0 ? GrvPesquisa.ListagemStatusOperacao.Contains(x.StatusOperacaoId) : true) &&
                            (!string.IsNullOrWhiteSpace(GrvPesquisa.NumeroProcesso) ? x.NumeroFormularioGrv == GrvPesquisa.NumeroProcesso : true) &&
                            (!string.IsNullOrWhiteSpace(GrvPesquisa.PlacaVeiculo) ? x.Placa == GrvPesquisa.PlacaVeiculo : true) &&
                            (!string.IsNullOrWhiteSpace(GrvPesquisa.Chassi) ? x.Chassi == GrvPesquisa.Chassi : true) &&
                            (!string.IsNullOrWhiteSpace(GrvPesquisa.FlagVeiculoNaoIdentificado) ? x.FlagVeiculoNaoIdentificado == GrvPesquisa.FlagVeiculoNaoIdentificado : true) &&
                            (GrvPesquisa.IdentificadorCliente > 0 ? x.ClienteId == GrvPesquisa.IdentificadorCliente : true) &&
                            (GrvPesquisa.IdentificadorDeposito > 0 ? x.DepositoId == GrvPesquisa.IdentificadorDeposito : true))
                .OrderBy(o => Convert.ToInt64(o.NumeroFormularioGrv))
                .Take(100)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("A pesquisa não retornou registro");

                return ResultView;
            }

            foreach (GrvModel Grv in result)
            {
                ResultView.Listagem.Add(new()
                {
                    IdentificadorGrv = Grv.GrvId,

                    StatusOperacaoId = Grv.StatusOperacaoId,

                    NumeroProcesso = Grv.NumeroFormularioGrv,

                    Placa = Grv.Placa,

                    Chassi = Grv.Chassi,

                    StatusOperacao = Grv.StatusOperacao.Descricao,

                    DataHoraRemocao = Grv.DataHoraRemocao,

                    DataHoraGuarda = Grv.DataHoraGuarda.Value,

                    Cliente = Grv.Cliente.Nome,

                    Deposito = Grv.Deposito.Nome
                });
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        public async Task<GrvPesquisaDadosMestresViewModel> ListarItensPesquisa(int UsuarioId)
        {
            return new()
            {
                ListagemProduto = await ListarProdutos(),

                ListagemCliente = await _provider
                    .GetService<ClienteService>()
                    .ListagemSimplificada(UsuarioId),

                ListagemDeposito = await _provider
                    .GetService<DepositoService>()
                    .ListagemSimplificada(UsuarioId),

                ListagemStatusOperacao = await _provider
                    .GetService<GrvService>()
                    .ListarStatusOperacao(),

                //ListagemReboque = await _provider
                //    .GetService<UsuarioService>()
                //    .ListarUsuarioClienteDepositoReboqueSimplificada(UsuarioId),

                //ListagemReboquista = await _provider
                //    .GetService<UsuarioService>()
                //    .ListarUsuarioClienteDepositoReboquistaSimplificada(UsuarioId)
            };
        }

        public async Task<MotivoApreensaoViewModelList> ListarMotivoApreensao()
        {
            MotivoApreensaoViewModelList ResultView = new();

            List<MotivoApreensaoModel> result = await _context.MotivoApreensao
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<MotivoApreensaoViewModel>>(result
                    .OrderBy(o => o.Descricao)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<FaturamentoProdutoViewModelList> ListarProdutos()
        {
            FaturamentoProdutoViewModelList ResultView = new();

            List<FaturamentoProdutoModel> result = await _context.FaturamentoProduto
                .AsNoTracking()
                .ToListAsync();

            ResultView.Listagem = _mapper.Map<List<FaturamentoProdutoViewModel>>(result.OrderBy(x => x.Descricao).ToList());

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        public MensagemViewModel ValidarInputGrv(int GrvId, int UsuarioId)
        {
            return ValidarInputGrv(GrvId, "", "", 0, 0, UsuarioId);
        }

        public MensagemViewModel ValidarInputGrv(GrvModel Grv, int UsuarioId)
        {
            return ValidarInputGrv(Grv.GrvId, Grv.NumeroFormularioGrv, Grv.FaturamentoProdutoId, Grv.ClienteId, Grv.DepositoId, UsuarioId);
        }

        public MensagemViewModel ValidarInputGrv(string NumeroFormularioGrv, string CodigoProduto, int ClienteId, int DepositoId, int UsuarioId)
        {
            return ValidarInputGrv(0, NumeroFormularioGrv, CodigoProduto, ClienteId, DepositoId, UsuarioId);
        }

        private MensagemViewModel ValidarInputGrv(int GrvId, string NumeroFormularioGrv, string CodigoProduto, int ClienteId, int DepositoId, int UsuarioId)
        {
            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                return MensagemViewHelper.GetUnauthorized();
            }

            List<string> erros = new();

            if (GrvId <= 0 && string.IsNullOrWhiteSpace(NumeroFormularioGrv))
            {
                erros.Add("Informe o Identificador do GRV ou o Número do Processo");
            }
            else if (GrvId <= 0)
            {
                if (!NumberHelper.IsNumber(NumeroFormularioGrv) || NumeroFormularioGrv.Length > 14 || Convert.ToInt64(NumeroFormularioGrv) <= 0)
                {
                    erros.Add(MensagemPadraoEnum.NumeroProcessoInvalido);
                }

                if (string.IsNullOrWhiteSpace(CodigoProduto))
                {
                    erros.Add(MensagemPadraoEnum.InformeCodigoProduto);
                }

                if (ClienteId <= 0)
                {
                    erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
                }

                if (DepositoId <= 0)
                {
                    erros.Add(MensagemPadraoEnum.IdentificadorDepositoInvalido);
                }
            }

            if (UsuarioId <= 0)
            {
                erros.Add("Primeiro é necessário informar o Identificador do Usuário que está realizando o cadastro");
            }

            if (erros.Count > 0)
            {
                return MensagemViewHelper.GetBadRequest(erros);
            }

            ViewUsuarioClienteDepositoGrvModel Grv = new();

            if (GrvId > 0)
            {
                Grv = _context.ViewUsuarioClienteDepositoGrv
                    .Where(x => x.GrvId == GrvId)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (Grv == null)
                {
                    return MensagemViewHelper.GetUnauthorized("Usuário não possui acesso ao GRV ou o GRV não existe");
                }
            }
            else
            {
                //FaturamentoProdutoModel FaturamentoProduto = await _context.FaturamentoProduto
                //    .Where(x => x.FaturamentoProdutoId == CodigoProduto)
                //    .AsNoTracking()
                //    .FirstOrDefaultAsync();

                //if (FaturamentoProduto == null)
                //{
                //    return MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoFaturamentoProduto);
                //}

                //ClienteModel Cliente = await _context.Cliente
                //    .Where(x => x.ClienteId == ClienteId)
                //    .AsNoTracking()
                //    .FirstOrDefaultAsync();

                //if (Cliente == null)
                //{
                //    return MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoCliente);
                //}

                //DepositoModel Deposito = await _context.Deposito
                //    .Where(x => x.DepositoId == DepositoId)
                //    .AsNoTracking()
                //    .FirstOrDefaultAsync();

                //if (Deposito == null)
                //{
                //    return MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoDeposito);
                //}

                //ClienteDepositoModel ClienteDeposito = await _context.ClienteDeposito
                //    .Where(x => x.ClienteId == ClienteId
                //        && x.DepositoId == DepositoId)
                //    .AsNoTracking()
                //    .FirstOrDefaultAsync();

                //if (ClienteDeposito == null)
                //{
                //    return MensagemViewHelper.GetNotFound("Este Cliente e Depósito não são associados");
                //}

                Grv = _context.ViewUsuarioClienteDepositoGrv
                    .Where(x => x.FaturamentoProdutoCodigo == CodigoProduto
                             && x.ClienteId == Grv.ClienteId
                             && x.DepositoId == Grv.DepositoId
                             && x.NumeroFormularioGrv == NumeroFormularioGrv)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (Grv == null)
                {
                    return MensagemViewHelper.GetUnauthorized("Usuário não possui acesso ao GRV ou o GRV não existe");
                }
            }

            return MensagemViewHelper.GetOk();
        }

        public MensagemViewModel SendFiles(GrvFotoViewModel Fotos)
        {
            MensagemViewModel ResultView = new();

            if (Fotos.Fotos.Count == 0)
            {
                return MensagemViewHelper.GetBadRequest("Nenhuma imagem enviada para a API");
            }

            GrvModel Grv = _context.Grv
                .Where(w => w.GrvId == Fotos.IdentificadorGrv)
                .AsNoTracking()
                .FirstOrDefault();

            if (Grv == null)
            {
                return MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);
            }

            ResultView = new GrvService(_context).ValidarInputGrv(Grv, Fotos.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            new BucketArquivoService(_context)
                .SendFiles("GRVFOTOSVEICCAD", Fotos.IdentificadorGrv, Fotos.IdentificadorUsuario, Fotos.Fotos);

            return MensagemViewHelper.GetOkCreate(Fotos.Fotos.Count);
        }

        public async Task<MensagemViewModel> ValidarInformacoesParaCadastro(GrvCadastroViewModel GrvCadastro)
        {
            #region Validações de IDs
            List<string> erros = new();

            if (GrvCadastro.IdentificadorCliente <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
            }

            if (GrvCadastro.IdentificadorDeposito <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorDepositoInvalido);
            }

            if (GrvCadastro.IdentificadorTipoVeiculo <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorTipoVeiculoInvalido);
            }

            if (GrvCadastro.FlagVeiculoNaoUsouReboque != "S"
                && GrvCadastro.FlagVeiculoNaoUsouReboque != "N")
            {
                erros.Add("Flag do Veículo não usou Reboque inválido, informe \"S\" ou \"N\" (sem aspas)");
            }
            else if (GrvCadastro.FlagVeiculoNaoUsouReboque == "N")
            {
                if (GrvCadastro.IdentificadorReboquista <= 0)
                {
                    erros.Add(MensagemPadraoEnum.IdentificadorReboquistaInvalido);
                }

                if (GrvCadastro.IdentificadorReboque <= 0)
                {
                    erros.Add(MensagemPadraoEnum.IdentificadorReboqueInvalido);
                }
            }
            else
            {
                if (GrvCadastro.IdentificadorReboquista > 0)
                {
                    erros.Add("Ao informar que o Veículo não usou Reboque, não informe o Identificador do Reboquista");
                }

                if (GrvCadastro.IdentificadorReboque > 0)
                {
                    erros.Add("Ao informar que o Veículo não usou Reboque, não informe o Identificador do Reboque");
                }
            }

            if (GrvCadastro.FlagVeiculoNaoOstentaPlaca != "S"
                && GrvCadastro.FlagVeiculoNaoOstentaPlaca != "N")
            {
                erros.Add("Flag do Veículo não ostenta Placa inválido, informe \"S\" ou \"N\" (sem aspas)");
            }

            if (GrvCadastro.IdentificadorAutoridadeResponsavel <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorAutoridadeResponsavelInvalido);
            }

            if (string.IsNullOrWhiteSpace(GrvCadastro.MatriculaAutoridadeResponsavel))
            {
                erros.Add("Informe a Matrícula da Autoridade Responsável.");
            }

            if (GrvCadastro.IdentificadorCor <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorCorInvalido);
            }

            if (GrvCadastro.IdentificadorMarcaModelo <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorMarcaModeloInvalido);
            }

            if (GrvCadastro.IdentificadorMotivoApreensao <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorMotivoApreensaoInvalido);
            }

            if (GrvCadastro.IdentificadorUsuario <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            if (string.IsNullOrWhiteSpace(GrvCadastro.CodigoProduto))
            {
                erros.Add("Informe o Código de Produto");
            }

            if (string.IsNullOrWhiteSpace(GrvCadastro.NumeroProcesso))
            {
                erros.Add(MensagemPadraoEnum.InformeNumeroProcesso);
            }
            else if (!NumberHelper.IsNumber(GrvCadastro.NumeroProcesso) || Convert.ToInt64(GrvCadastro.NumeroProcesso) <= 0)
            {
                erros.Add(MensagemPadraoEnum.NumeroProcessoInvalido);
            }

            if (GrvCadastro.FlagVeiculoNaoIdentificado != "S"
                && GrvCadastro.FlagVeiculoNaoIdentificado != "N")
            {
                erros.Add("Flag do Veículo não identificado inválido, informe \"S\" ou \"N\" (sem aspas)");
            }
            else if (GrvCadastro.FlagVeiculoNaoIdentificado == "S")
            {
                if (!string.IsNullOrWhiteSpace(GrvCadastro.Placa) || !string.IsNullOrWhiteSpace(GrvCadastro.Chassi))
                {
                    erros.Add("Ao informar que o Veículo não foi identificado, não se deve informar a Placa nem o Chassi");
                }
            }
            else if (GrvCadastro.FlagVeiculoSemRegistro == "S")
            {
                if (!string.IsNullOrWhiteSpace(GrvCadastro.Placa))
                {
                    erros.Add("Ao informar que o Veículo não possui registro, não se deve informar a Placa");
                }
                else if (string.IsNullOrWhiteSpace(GrvCadastro.Chassi))
                {
                    erros.Add("Informe o Chassi");
                }
                else if (!VeiculoHelper.IsChassi(GrvCadastro.Chassi))
                {
                    erros.Add("Chassi inválido");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(GrvCadastro.Placa))
                {
                    erros.Add("Informe a Placa");
                }
                else if (!VeiculoHelper.IsPlaca(GrvCadastro.Placa))
                {
                    erros.Add("Placa inválida");
                }

                if (string.IsNullOrWhiteSpace(GrvCadastro.Chassi))
                {
                    erros.Add("Informe o Chassi");
                }
                else if (!VeiculoHelper.IsChassi(GrvCadastro.Chassi))
                {
                    erros.Add("Chassi inválido");
                }
            }

            if (GrvCadastro.FlagVeiculoSemRegistro != "S"
                && GrvCadastro.FlagVeiculoSemRegistro != "N")
            {
                erros.Add("Flag do Veículo sem registro inválido, informe \"S\" ou \"N\" (sem aspas)");
            }
            else if (GrvCadastro.FlagVeiculoSemRegistro == "S")
            {
                if (string.IsNullOrWhiteSpace(GrvCadastro.Chassi))
                {
                    erros.Add("Informe o Chassi");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(GrvCadastro.Chassi) && !VeiculoHelper.IsChassi(GrvCadastro.Chassi))
                    {
                        erros.Add("Chassi inválido");
                    }
                }
            }

            if (GrvCadastro.FlagVeiculoRoubadoFurtado != "S"
                && GrvCadastro.FlagVeiculoRoubadoFurtado != "N")
            {
                erros.Add("Flag do Veículo Roubado/Furtado inválido, informe \"S\" ou \"N\" (sem aspas)");
            }

            if (!string.IsNullOrWhiteSpace(GrvCadastro.EnderecoLocalizacaoVeiculoCEP))
            {
                if (!LocalizacaoHelper.IsCEP(GrvCadastro.EnderecoLocalizacaoVeiculoCEP))
                {
                    erros.Add("CEP inválido");
                }
            }

            if (GrvCadastro.Condutor == null)
            {
                erros.Add("Informe os dados do Condutor");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(GrvCadastro.Condutor.Email)
                    && !EmailHelper.IsEmail(GrvCadastro.Condutor.Email))
                {
                    erros.Add($"E-mail do Condutor é inválido: {GrvCadastro.Condutor.Email}");
                }

                if (!string.IsNullOrWhiteSpace(GrvCadastro.Condutor.Documento))
                {
                    if (!DocumentHelper.IsCPF(GrvCadastro.Condutor.Documento))
                    {
                        erros.Add("CPF do Condutor inválido");
                    }
                }

                if (string.IsNullOrWhiteSpace(GrvCadastro.Condutor.StatusAssinaturaCondutor))
                {
                    erros.Add("Informe o Status da Assinatura do Condutor");
                }
                else
                {
                    List<TabelaGenericaModel> TabelaGenerica = await new SistemaService(_context)
                        .ListarTabelaGenerica("STATUS_ASSINATURA_CONDUTOR");

                    if (!TabelaGenerica.Select(x => x.Sigla).ToList().Contains(GrvCadastro.Condutor.StatusAssinaturaCondutor))
                    {
                        erros.Add("Status da Assinatura do Condutor inválido");
                    }
                }

                if (!string.IsNullOrWhiteSpace(GrvCadastro.Condutor.TelefoneDDD)
                    && !ContactHelper.IsDDD(GrvCadastro.Condutor.TelefoneDDD))
                {
                    erros.Add("DDD do Telefone do Condutor inválido");
                }

                if (!string.IsNullOrWhiteSpace(GrvCadastro.Condutor.Telefone)
                    && !ContactHelper.IsTelephoneOrCellphone(GrvCadastro.Condutor.Telefone))
                {
                    erros.Add("Telefone do Condutor inválido");
                }

                if (!string.IsNullOrWhiteSpace(GrvCadastro.Condutor.TelefoneDDD)
                    && string.IsNullOrWhiteSpace(GrvCadastro.Condutor.Telefone))
                {
                    erros.Add("Ao informar o DDD do Telefone do Condutor é necessário informar o Telefone do Condutor");
                }
                else if (string.IsNullOrWhiteSpace(GrvCadastro.Condutor.TelefoneDDD)
                    && !string.IsNullOrWhiteSpace(GrvCadastro.Condutor.Telefone))
                {
                    erros.Add("Ao informar o Telefone do Condutor é necessário informar o DDD do Telefone do Condutor");
                }

                if (GrvCadastro.Condutor.FlagDocumentacaoVeiculo != "S"
                    && GrvCadastro.Condutor.FlagDocumentacaoVeiculo != "N")
                {
                    erros.Add("Flag da Documentação foi deixada no Veículo inválida, informe \"S\" ou \"N\" (sem aspas)");
                }

                if (GrvCadastro.Condutor.FlagChaveVeiculo != "S"
                    && GrvCadastro.Condutor.FlagChaveVeiculo != "N")
                {
                    erros.Add("Flag da Chave ficou no Veículo inválida, informe \"S\" ou \"N\" (sem aspas)");
                }
                else if (GrvCadastro.Condutor.FlagChaveVeiculo == "S"
                    && string.IsNullOrWhiteSpace(GrvCadastro.Condutor.NumeroChaveVeiculo))
                {
                    erros.Add("Ao informar que a Chave ficou no Veículo, é necessário informar o Número/Código da Chave");
                }
            }

            if (GrvCadastro.FlagEstadoLacre != "S"
                    && GrvCadastro.FlagEstadoLacre != "N")
            {
                erros.Add("Flag do Status dos Lacres inválido, informe \"S\" ou \"N\" (sem aspas)");
            }

            if (GrvCadastro?.Lacres.Count == 0)
            {
                erros.Add("Informe os Lacres");
            }
            else
            {
                List<IGrouping<string, string>> Lacres = GrvCadastro.Lacres
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .GroupBy(x => x)
                    .Where(x => x.Count() > 1)
                    .ToList();

                if (Lacres.Count >= 1)
                {
                    erros.Add("Existem Lacres duplicados");
                }

                Lacres = GrvCadastro.Lacres
                    .Where(x => string.IsNullOrWhiteSpace(x))
                    .GroupBy(x => x)
                    .ToList();

                if (Lacres.Count >= 1)
                {
                    erros.Add("Existem Lacres não informados");
                }
            }

            MensagemViewModel ResultView = new();

            if (erros.Count > 0)
            {
                ResultView = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }
            #endregion Validações de IDs

            #region Validações do Usuário
            if (!new UsuarioService(_context).IsUserActive(GrvCadastro.IdentificadorUsuario))
            {
                return MensagemViewHelper.GetUnauthorized();
            }
            #endregion Validações do Usuário

            #region Consultas
            ClienteModel Cliente = await _context.Cliente
                .Include(x => x.Endereco)
                .Include(x => x.ClientesDepositos)
                .Where(w => w.ClienteId == GrvCadastro.IdentificadorCliente)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Cliente == null)
            {
                ResultView.AvisosImpeditivos.Add(MensagemPadraoEnum.NaoEncontradoCliente);
            }
            else if (Cliente.FlagClientePossuiCodigoIdentificacao == "S"
                     && string.IsNullOrWhiteSpace(GrvCadastro.CodigoIdentificacaoCliente))
            {
                ResultView.AvisosImpeditivos.Add($"Informe o {Cliente.LabelClienteCodigoIdentificacao}");
            }
            else if (Cliente.ClientesDepositos != null && Cliente.ClientesDepositos.Count > 0)
            {
                if (Cliente.ClientesDepositos.FirstOrDefault().FlagCadastrarGrvBloqueado == "S")
                {
                    StatusOperacaoModel StatusOperacao = await _context.StatusOperacao
                        .Where(w => w.StatusOperacaoId == "B")
                        .AsNoTracking()
                        .FirstOrDefaultAsync();

                    ResultView.AvisosInformativos.Add($"Esse GRV receberá o Status de Operação {StatusOperacao.Descricao} devido à configuração do Cliente");
                }
            }

            DepositoModel Deposito = await _context.Deposito
                .Where(w => w.DepositoId == GrvCadastro.IdentificadorDeposito)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Deposito == null)
            {
                ResultView.AvisosImpeditivos.Add(MensagemPadraoEnum.NaoEncontradoDeposito);
            }

            DateTime DataHoraPorDeposito = new DepositoService(_context, _mapper)
                .GetDataHoraPorDeposito(GrvCadastro.IdentificadorDeposito);

            if (GrvCadastro.DataHoraRemocao.Date > DataHoraPorDeposito.Date)
            {
                ResultView.AvisosImpeditivos.Add("A Data da Remoção não pode ser maior do que a Data atual");
            }
            else if (GrvCadastro.DataHoraRemocao.Hour == 0 && GrvCadastro.DataHoraRemocao.Minute == 0)
            {
                ResultView.AvisosImpeditivos.Add("A Hora da Remoção não pode ser igual a 00:00.");
            }

            TipoVeiculoModel TipoVeiculo = await _context.TipoVeiculo
                .Where(w => w.TipoVeiculoId == GrvCadastro.IdentificadorTipoVeiculo)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (TipoVeiculo == null)
            {
                ResultView.AvisosImpeditivos.Add("Tipo do Veículo inexistente");
            }

            ReboquistaModel Reboquista = await _context.Reboquista
                .Where(w => w.ReboquistaId == GrvCadastro.IdentificadorReboquista)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Reboquista == null)
            {
                ResultView.AvisosImpeditivos.Add("Reboquista inexistente");
            }

            ReboqueModel Reboque = await _context.Reboque
                .Where(w => w.ReboqueId == GrvCadastro.IdentificadorReboque)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Reboque == null)
            {
                ResultView.AvisosImpeditivos.Add("Reboque inexistente");
            }

            AutoridadeResponsavelModel AutoridadeResponsavel = await _context.AutoridadeResponsavel
                .Include(x => x.OrgaoEmissor)
                .Where(w => w.AutoridadeResponsavelId == GrvCadastro.IdentificadorAutoridadeResponsavel)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (AutoridadeResponsavel == null)
            {
                ResultView.AvisosImpeditivos.Add("Autoridade Responsável não encontrada");
            }
            else if (AutoridadeResponsavel.OrgaoEmissor.UF != Cliente.Endereco.UF)
            {
                ResultView.AvisosImpeditivos.Add($"A Autoridade Responsável ({AutoridadeResponsavel.OrgaoEmissor.UF}) informada não pertence a mesma Unidade Federativa do cadastro do Cliente {Cliente.Nome} ({Cliente.Endereco.UF})");
            }

            CorModel Cor = await _context.Cor
                .Where(w => w.CorId == GrvCadastro.IdentificadorCor)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Cor == null)
            {
                ResultView.AvisosImpeditivos.Add("Cor não encontrada");
            }

            MarcaModeloModel MarcaModelo = await _context.MarcaModelo
                .Where(w => w.MarcaModeloId == GrvCadastro.IdentificadorMarcaModelo)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (MarcaModelo == null)
            {
                ResultView.AvisosImpeditivos.Add("Marca/Modelo inexistente");
            }

            MotivoApreensaoModel MotivoApreensao = await _context.MotivoApreensao
                .Where(w => w.MotivoApreensaoId == GrvCadastro.IdentificadorMotivoApreensao)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (MotivoApreensao == null)
            {
                ResultView.AvisosImpeditivos.Add("Motivo de Apreensão inexistente");
            }
            else if (MotivoApreensao.FlagDefault == "S")
            {
                if (GrvCadastro?.EnquadramentosInfracoes.Count == 0)
                {
                    ResultView.AvisosImpeditivos.Add("Informe o Enquadramento da Infração");
                }
                else
                {
                    List<IGrouping<decimal, decimal>> EnquadramentosInfracoes = GrvCadastro.EnquadramentosInfracoes
                        .Where(x => x.IdentificadorEnquadramentoInfracao <= 0)
                        .Select(x => x.IdentificadorEnquadramentoInfracao)
                        .GroupBy(x => x)
                        .ToList();

                    if (EnquadramentosInfracoes.Count >= 1)
                    {
                        ResultView.AvisosImpeditivos.Add("Existem Enquadramento da Infração com Identificador inválido");
                    }

                    EnquadramentosInfracoes = GrvCadastro.EnquadramentosInfracoes
                        .Where(x => x.IdentificadorEnquadramentoInfracao > 0)
                        .Select(x => x.IdentificadorEnquadramentoInfracao)
                        .GroupBy(x => x)
                        .Where(x => x.Count() > 1)
                        .ToList();

                    if (EnquadramentosInfracoes.Count >= 1)
                    {
                        ResultView.AvisosImpeditivos.Add("Existem Enquadramento da Infração duplicados");
                    }

                    List<IGrouping<string, string>> NumeroInfracao = GrvCadastro.EnquadramentosInfracoes
                        .Where(x => string.IsNullOrWhiteSpace(x.NumeroInfracao.Trim()))
                        .Select(x => x.NumeroInfracao.Trim())
                        .GroupBy(x => x)
                        .ToList();

                    if (NumeroInfracao.Count >= 1)
                    {
                        ResultView.AvisosImpeditivos.Add("Existem Número da Infração não informados");
                    }

                    NumeroInfracao = GrvCadastro.EnquadramentosInfracoes
                        .Where(x => !string.IsNullOrWhiteSpace(x.NumeroInfracao.Trim()))
                        .Select(x => x.NumeroInfracao.Trim())
                        .GroupBy(x => x)
                        .Where(x => x.Count() > 1)
                        .ToList();

                    if (NumeroInfracao.Count >= 1)
                    {
                        ResultView.AvisosImpeditivos.Add("Existem Número da Infração duplicados");
                    }
                }
            }

            FaturamentoProdutoModel Produtos = await _context.FaturamentoProduto
                .Where(w => w.FaturamentoProdutoId == GrvCadastro.CodigoProduto)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (MotivoApreensao == null)
            {
                ResultView.AvisosImpeditivos.Add("Código do Produto inexistente");
            }

            if (!string.IsNullOrWhiteSpace(GrvCadastro.EnderecoLocalizacaoVeiculoCEP))
            {
                if (await _context.CEP
                    .Where(x => x.CEP == GrvCadastro.EnderecoLocalizacaoVeiculoCEP.Replace("-", ""))
                    .FirstOrDefaultAsync() == null)
                {
                    ResultView.AvisosImpeditivos.Add("CEP inexistente");
                }
            }

            GrvModel Grv = await _context.Grv
                .Where(w => w.NumeroFormularioGrv == GrvCadastro.NumeroProcesso && w.ClienteId == GrvCadastro.IdentificadorCliente && w.DepositoId == GrvCadastro.IdentificadorDeposito)
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

        public async Task<StatusAssinaturaCondutorViewModelList> ListarStatusAssinaturaCondutor()
        {
            StatusAssinaturaCondutorViewModelList ResultView = new();

            List<TabelaGenericaModel> result = await new SistemaService(_context)
                .ListarTabelaGenerica("STATUS_ASSINATURA_CONDUTOR");

            if (result?.Count == 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();

                return ResultView;
            }

            foreach (var item in result)
            {
                ResultView.Listagem.Add(new()
                {
                    IdentificadorStatusAssinaturaCondutor = item.Sigla,
                    Descricao = item.Valor1
                });
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        public async Task<MensagemViewModel> VerificarAlteracaoStatusGRV(int GrvId, string StatusOperacaoId, int UsuarioId)
        {
            MensagemViewModel ResultView = new GrvService(_context).ValidarInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Include(x => x.StatusOperacao)
                .Where(x => x.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Grv == null)
            {
                return MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);
            }

            StatusOperacaoModel StatusOperacao = await _context.StatusOperacao
                .Where(x => x.StatusOperacaoId == StatusOperacaoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (StatusOperacao == null)
            {
                return MensagemViewHelper.GetNotFound(MensagemPadraoEnum.NaoEncontradoStatusOperacao);
            }
            else if (Grv.StatusOperacao.StatusOperacaoId != StatusOperacaoId)
            {
                return MensagemViewHelper.GetBadRequest($"O Status da Operação foi alterado de \"{Grv.StatusOperacao.Descricao.ToUpper()}\" para \"{StatusOperacao.Descricao.ToUpper()}\"");
            }

            return MensagemViewHelper.GetOk("O Status da Operação não foi alterado");
        }

        public async Task<AutoridadeResponsavelViewModelList> ListAutoridadeResponsavel(string UF)
        {
            AutoridadeResponsavelViewModelList ResultView = new();

            if (string.IsNullOrWhiteSpace(UF))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Informe a Unidade Federativa");

                return ResultView;
            }
            else if (!LocalizacaoHelper.IsUF(UF))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Unidade Federativa inválida");

                return ResultView;
            }

            OrgaoEmissorModel result = await _context.OrgaoEmissor
                .Include(i => i.AutoridadesResponsaveis)
                .Where(w => w.UF == UF.ToUpper().Trim())
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Unidade Federativa sem Órgão Emissor cadastrado");

                return ResultView;
            }

            if (result.AutoridadesResponsaveis?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<AutoridadeResponsavelViewModel>>(result.AutoridadesResponsaveis
                    .OrderBy(o => o.Divisao)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.AutoridadesResponsaveis.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<LacreViewModelList> ListarLacre(int GrvId, int UsuarioId)
        {
            LacreViewModelList ResultView = new()
            {
                Mensagem = new GrvService(_context).ValidarInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            List<LacreModel> result = await _context.Lacre
                .Where(x => x.GrvId == GrvId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<LacreViewModel>>(result
                    .OrderBy(x => x.Lacre)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<StatusOperacaoViewModelList> GetStatusOperacaoById(string StatusOperacaoId)
        {
            StatusOperacaoViewModelList ResultView = new();

            if (string.IsNullOrWhiteSpace(StatusOperacaoId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Identificador do Status da Operação inválido");

                return ResultView;
            }

            StatusOperacaoModel result = await _context.StatusOperacao
                .Where(w => w.StatusOperacaoId == StatusOperacaoId.ToUpper().Trim())
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView.Listagem.Add(result);

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<StatusOperacaoViewModelList> ListarStatusOperacao()
        {
            StatusOperacaoViewModelList ResultView = new();

            List<StatusOperacaoModel> result = await _context.StatusOperacao
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                result = result
                    .OrderBy(o => o.Descricao)
                    .ToList();

                ResultView.Listagem = result;

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<ImageViewModelList> ListarFotos(int GrvId, int UsuarioId)
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

            ImageViewModelList ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(erros);

                return ResultView;
            }

            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized();

                return ResultView;
            }

            return await new BucketArquivoService(_context)
                .DownloadFiles("GRVFOTOSVEICCAD", GrvId);
        }

        public MensagemViewModel ExcluirFotos(int GrvId, int UsuarioId, List<int> ListagemTabelaOrigemId)
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

            List<BucketArquivoModel> BucketArquivos = _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .Where(x => x.TabelaOrigemId != GrvId
                            && ListagemTabelaOrigemId.Contains(x.RepositorioArquivoId)
                            && x.BucketNomeTabelaOrigem.Codigo == "GRVFOTOSVEICCAD")
                .AsNoTracking()
                .ToList();

            if (BucketArquivos != null)
            {
                List<string> erros = new()
                {
                    $"A(s) seguinte(s) Fotos não pertencem ao GRV {GrvId}:"
                };

                foreach (BucketArquivoModel BucketArquivo in BucketArquivos)
                {
                    erros.Add(BucketArquivo.RepositorioArquivoId.ToString());
                }
            }

            new BucketArquivoService(_context)
                .DeleteFiles("GRVFOTOSVEICCAD", ListagemTabelaOrigemId);

            return MensagemViewHelper.GetOk("Foto(s) excluída(s) com sucesso");
        }
    }
}