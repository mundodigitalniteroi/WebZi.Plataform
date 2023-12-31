using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.ClienteDeposito;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.DTO.Faturamento;
using WebZi.Plataform.Domain.DTO.Faturamento.Servico;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Condutor;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel.Faturamento;
using WebZi.Plataform.Domain.Views.Faturamento;
using WebZi.Plataform.Domain.Views.Localizacao;
using Z.EntityFramework.Plus;

namespace WebZi.Plataform.Data.Services.Faturamento
{
    public class FaturamentoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public FaturamentoService(AppDbContext context)
        {
            _context = context;
        }

        public FaturamentoService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<SimulacaoDTO> SimularAsync(SimulacaoParameters model)
        {
            #region Validações dos parâmetros
            List<string> erros = new();

            if (model.IdentificadorProcesso <= 0 && model.Placa.IsNullOrWhiteSpace() && model.Chassi.IsNullOrWhiteSpace())
            {
                erros.Add("Informe o Identificador do Processo, Placa ou Chassi");
            }

            if (model.IdentificadorProcesso <= 0)
            {
                if (!model.Placa.IsNullOrWhiteSpace() && !model.Placa.IsPlaca())
                {
                    erros.Add("Placa inválida");
                }
                else if (model.Placa.IsNullOrWhiteSpace() && !model.Placa.IsChassi())
                {
                    erros.Add("Chassi inválido");
                }
            }

            if (model.IdentificadorCliente <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
            }

            if (model.IdentificadorDeposito <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorDepositoInvalido);
            }

            if (model?.DataHoraInicialParaCalculo > DateTime.Now)
            {
                erros.Add("A Data/Hora Inicial para o Cálculo não pode ser maior do que a Data/Hora atual");
            }

            if (model?.DataHoraFinalParaCalculo > DateTime.Now)
            {
                erros.Add("A Data/Hora Final para o Cálculo não pode ser maior do que a Data/Hora atual");
            }

            if (model?.DataHoraInicialParaCalculo > model?.DataHoraFinalParaCalculo)
            {
                erros.Add("A Data/Hora Inicial para o Cálculo não pode ser maior do que a Data/Hora Final para o Cálculo");
            }

            SimulacaoDTO ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);

                return ResultView;
            }
            #endregion Validações dos parâmetros

            #region Validações das Consultas
            GrvModel Grv = null;

            if (model.IdentificadorProcesso > 0)
            {
                Grv = await _context.Grv
                    .Include(x => x.TipoVeiculo)
                    .Include(x => x.FaturamentoProduto)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.GrvId == model.IdentificadorProcesso);

                if (Grv == null)
                {
                    ResultView.Mensagem = MensagemViewHelper.SetNewMessage(ResultView.Mensagem, MensagemPadraoEnum.NaoEncontradoGrv, MensagemTipoAvisoEnum.Impeditivo);

                    return ResultView;
                }
            }

            if (Grv == null)
            {
                if (!model.CodigoProduto.IsNullOrWhiteSpace())
                {
                    ResultView.Produto = await GetProdutoAsync(model.CodigoProduto.ToUpperTrim());

                    if (ResultView.Produto == null)
                    {
                        ResultView.Mensagem = MensagemViewHelper.SetNewMessage(ResultView.Mensagem, MensagemPadraoEnum.NaoEncontradoFaturamentoProduto, MensagemTipoAvisoEnum.Impeditivo);
                    }
                }
                else
                {
                    ResultView.Produto = await GetProdutoAsync("DEP");
                }
            }
            else
            {
                ResultView.Produto = _mapper.Map<FaturamentoProdutoDTO>(Grv.FaturamentoProduto);
            }

            ResultView.Mensagem = await new ClienteDepositoService(_context)
                .ValidateClienteDepositoAsync(model.IdentificadorCliente, model.IdentificadorDeposito);

            DetranRioService DetranRioService = new(_context, _mapper);

            if (Grv != null)
            {
                ResultView.Veiculo = Grv.Placa.IsPlaca() ? await DetranRioService.GetViewByPlacaAsync(Grv.Placa) : await DetranRioService.GetViewByChassiAsync(Grv.Chassi);
            }
            else
            {
                ResultView.Veiculo = model.Placa.IsPlaca() ? await DetranRioService.GetViewByPlacaAsync(model.Placa) : await DetranRioService.GetViewByChassiAsync(model.Chassi);
            }

            if (ResultView.Veiculo.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNewMessages(ResultView.Mensagem, ResultView.Veiculo.Mensagem);
            }
            else if (ResultView.Veiculo.TipoVeiculo == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNewMessage(ResultView.Mensagem, "Tipo do Veículo não retornado pelo Serviço do Departamento Estadual de Trânsito", MensagemTipoAvisoEnum.Impeditivo);
            }

            if (ResultView.Mensagem.AvisosImpeditivos.Count + ResultView.Mensagem.Erros.Count > 0)
            {
                ResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;

                return ResultView;
            }
            #endregion Validações das Consultas

            #region Aplicação das Configurações
            DateTime DataHoraPorDeposito = new DepositoService(_context)
                .GetDataHoraPorDeposito(model.IdentificadorDeposito);

            CalculoFaturamentoParametroModel ParametrosCalculoFaturamento = new()
            {
                DataHoraInicialParaCalculo = model.DataHoraInicialParaCalculo ?? DataHoraPorDeposito,

                DataHoraFinalParaCalculo = model.DataHoraFinalParaCalculo ?? DataHoraPorDeposito,

                DataHoraPorDeposito = DataHoraPorDeposito,

                FaturarSemGrv = model.IdentificadorProcesso <= 0,

                IsSimulacao = true,

                IsComboio = model.IsComboio,

                StatusOperacaoId = "V",

                FaturamentoProdutoId = ResultView.Produto.CodigoProduto,

                GrvId = Grv != null ? Grv.GrvId : 0,

                NumeroFormularioGrv = Grv != null ? Grv.NumeroFormularioGrv : string.Empty,

                TipoVeiculoId = ResultView.Veiculo.TipoVeiculo.IdentificadorTipoVeiculo,

                ClienteDeposito = await _context.ClienteDeposito
                    .Include(x => x.Cliente)
                    .Include(x => x.Deposito)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ClienteId == model.IdentificadorCliente && x.DepositoId == model.IdentificadorDeposito)
            };
            #endregion Aplicação das Configurações

            FaturamentoModel Faturamento = Faturar(ParametrosCalculoFaturamento);

            ResultView.Faturamento = _mapper.Map<FaturamentoDTO>(Faturamento);

            ResultView.Faturamento.ListagemFaturamentoComposicao= _mapper.Map<List<FaturamentoComposicaoDTO>>(Faturamento.ListagemFaturamentoComposicao);

            ResultView.Mensagem = MensagemViewHelper.SetOk();

            ResultView.DataHoraSimulacao = DateTime.Now;

            return ResultView;
        }

        public FaturamentoModel Faturar(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            if (ParametrosCalculoFaturamento.DataHoraFinalParaCalculo == DateTime.MinValue)
            {
                ParametrosCalculoFaturamento.DataHoraFinalParaCalculo = ParametrosCalculoFaturamento.DataHoraPorDeposito;
            }

            #region Selecionar o Atendimento
            AtendimentoModel Atendimento = new();

            if (!ParametrosCalculoFaturamento.FaturarSemGrv)
            {
                Atendimento = _context.Atendimento
                    .AsNoTracking()
                    .FirstOrDefault(x => x.GrvId == ParametrosCalculoFaturamento.GrvId);
            }

            #endregion Selecionar o Atendimento

            #region Selecionar os Serviços cadastrados no GRV
            List<ViewFaturamentoServicoGrvModel> FaturamentoServicosGrvs = new();

            if (!ParametrosCalculoFaturamento.FaturarSemGrv)
            {
                FaturamentoServicosGrvs = _context.ViewFaturamentoServicoGrv
                    .Where(x => x.GrvId == ParametrosCalculoFaturamento.GrvId &&
                                x.FaturamentoProdutoId == ParametrosCalculoFaturamento.FaturamentoProdutoId &&
                                x.FlagTributacao == "N")
                    .AsNoTracking()
                    .ToList();

                if (FaturamentoServicosGrvs?.Count == 0)
                {
                    throw new Exception("Não foi encontrado Serviço associado à este GRV");
                }
            }
            #endregion Selecionar os Serviços cadastrados no GRV

            #region Selecionar todos os Serviços associados ao CLIDEP, incluindo os com a Vigência finalizada
            List<ViewFaturamentoServicoAssociadoVeiculoModel> FaturamentoServicosAssociadosVeiculos = _context.ViewFaturamentoServicoAssociadoVeiculo
                .Where(x => x.ClienteId == ParametrosCalculoFaturamento.ClienteDeposito.ClienteId &&
                            x.DepositoId == ParametrosCalculoFaturamento.ClienteDeposito.DepositoId &&
                            x.TipoVeiculoId == ParametrosCalculoFaturamento.TipoVeiculoId &&
                            x.FaturamentoProdutoId == ParametrosCalculoFaturamento.FaturamentoProdutoId)
                .AsNoTracking()
                .ToList();

            if (FaturamentoServicosAssociadosVeiculos?.Count == 0)
            {
                throw new Exception("Não foi encontrado Serviço associado ao Cliente + Depósito + Tipo de Veículo + Produto");
            }

            if (ParametrosCalculoFaturamento.FaturarSemGrv)
            {
                FaturamentoServicosGrvs = _mapper
                    .Map<List<ViewFaturamentoServicoGrvModel>>(FaturamentoServicosAssociadosVeiculos
                    .Where(x => x.DataVigenciaFinal == null)
                    .ToList());
            }

            #endregion Selecionar todos os Serviços associados ao CLIDEP, incluindo os com a Vigência finalizada

            #region Verificação de Faturamentos anteriores
            // Faturamento.Status:
            // N = Novo Faturamento/Não Pago;
            // A = Faturamento Adicional e Não Pago (Pra quando a Fatura foi paga em atraso);
            // C = Cancelado, pra quando foi gerada uma Fatura para uma Fatura Vencida e que não foi paga;
            // P = Fatura Paga.

            // Se existir ao menos 1 Fatura paga, não deve dar Desconto
            if (!ParametrosCalculoFaturamento.FaturarSemGrv)
            {
                if (_context.Faturamento
                    .Where(x => x.AtendimentoId == Atendimento.AtendimentoId
                             && x.Status == "P")
                    .AsNoTracking()
                    .Any())
                {
                    // Faturamentos adicionais não podem receber descontos
                    ParametrosCalculoFaturamento.FaturamentoAdicional = true;
                }
            }

            // Consulta da última Fatura para cancelar
            FaturamentoModel UltimoFaturamento = null;

            if (!ParametrosCalculoFaturamento.FaturarSemGrv)
            {
                UltimoFaturamento = _context.Faturamento
                    .OrderByDescending(x => x.FaturamentoId)
                    .FirstOrDefault(x => x.AtendimentoId == Atendimento.AtendimentoId);

                if (UltimoFaturamento != null)
                {
                    #region Cancelar o Faturamento atual
                    UltimoFaturamento.UsuarioAlteracaoId = Atendimento.UsuarioCadastroId;

                    UltimoFaturamento.Status = "C";

                    UltimoFaturamento.DataAlteracao = ParametrosCalculoFaturamento.DataHoraPorDeposito;

                    if (!ParametrosCalculoFaturamento.IsSimulacao)
                    {
                        _context.Faturamento.Update(UltimoFaturamento);
                    }

                    if (ParametrosCalculoFaturamento.TipoMeioCobrancaId == 0)
                    {
                        ParametrosCalculoFaturamento.TipoMeioCobrancaId = UltimoFaturamento.TipoMeioCobrancaId;
                    }
                    #endregion Cancelar o Faturamento atual

                    // Se a Fatura for Nova, então está sendo cancelada e gerada uma nova, incluindo a aplicação dos Descontos caso houver.
                    if (UltimoFaturamento.Status == "A" || ParametrosCalculoFaturamento.FaturamentoAdicional)
                    {
                        ParametrosCalculoFaturamento.FlagFaturamentoCompleto = false;
                    }
                }
            }
            #endregion Verificação de Faturamentos anteriores

            #region Cálculo das Diárias
            CalculoDiariasModel CalculoDiarias =new CalculoDiariasService(_context)
                .Calcular(ParametrosCalculoFaturamento);

            ParametrosCalculoFaturamento.DataHoraInicialParaCalculo = CalculoDiarias.DataHoraInicialParaCalculo;
            #endregion Cálculo das Diárias

            #region Composição do Faturamento
            CalculoFaturamentoQuantidadeAlteradaModel FaturamentoQuantidadeAlterada = new();

            List<FaturamentoComposicaoModel> FaturamentoComposicoes = new();

            FaturamentoComposicaoModel FaturamentoComposicao = new();

            ViewFaturamentoServicoAssociadoVeiculoModel FaturamentoServicoAssociadoVeiculo = new();

            List<ViewFaturamentoServicoAssociadoVeiculoModel> FaturamentoServicosAssociadosVeiculosTodasVigenciasEncontradas = new();

            decimal ValorFaturado = 0;
            int DiariasCalculadas = 0;
            int Horas = 0;

            foreach (ViewFaturamentoServicoGrvModel FaturamentoServicoGrv in FaturamentoServicosGrvs)
            {
                if (!CheckServicoDeveSerCalculado(FaturamentoServicoGrv, UltimoFaturamento, ParametrosCalculoFaturamento))
                {
                    continue;
                }

                FaturamentoComposicao = new()
                {
                    FaturamentoServicoTipoVeiculoId = FaturamentoServicoGrv.FaturamentoServicoTipoVeiculoId,

                    TipoComposicao = FaturamentoServicoGrv.TipoCobranca,

                    ValorTipoComposicao = FaturamentoServicoGrv.PrecoPadrao,
                };

                // DIÁRIAS
                if (FaturamentoServicoGrv.TipoCobranca == TipoCobrancaFaturamentoEnum.Diárias)
                {
                    // Forma de Cobrança:
                    // AM: Ambos;
                    // VA: Vigência Atual (Valor Padrão);
                    // VI: Vigência Inicial.

                    // Quantidade Alterada só se aplica às Diárias
                    FaturamentoQuantidadeAlterada = null;

                    if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto && ParametrosCalculoFaturamento.FaturamentoQuantidadesAlteradas?.Count > 0)
                    {
                        FaturamentoQuantidadeAlterada = ParametrosCalculoFaturamento.FaturamentoQuantidadesAlteradas
                            .Find(w => w.FaturamentoServicoTipoVeiculoId == FaturamentoServicoGrv.FaturamentoServicoTipoVeiculoId);
                    }

                    if (new[] { "AM", "VI" }.Contains(FaturamentoServicoGrv.FormaCobranca))
                    {
                        DiariasCalculadas = CalculoDiarias.Diarias;

                        if (FaturamentoServicoGrv.FormaCobranca == "AM")
                        {
                            // Primeiro filtro, cobrar por todas as vigências encontradas
                            FaturamentoServicosAssociadosVeiculosTodasVigenciasEncontradas = FaturamentoServicosAssociadosVeiculos
                                .Where(x => (x.FaturamentoServicoTipoId == FaturamentoServicoGrv.FaturamentoServicoTipoId &&
                                           (ParametrosCalculoFaturamento.DataHoraInicialParaCalculo.Date >= x.DataVigenciaInicial && ParametrosCalculoFaturamento.DataHoraInicialParaCalculo.Date <= x.DataVigenciaFinal)) ||
                                            ParametrosCalculoFaturamento.DataHoraInicialParaCalculo <= x.DataVigenciaFinal || x.DataVigenciaFinal == null)
                                .ToList();

                            foreach (ViewFaturamentoServicoAssociadoVeiculoModel FaturamentoServicoAssociadoVeiculoAmbos in FaturamentoServicosAssociadosVeiculosTodasVigenciasEncontradas)
                            {
                                // Retorna a quantidade de Dias entre as datas
                                CalculoDiarias.Diarias = GetQuantidadeDiasServicoDiarias(FaturamentoServicoAssociadoVeiculoAmbos, ParametrosCalculoFaturamento.DataHoraInicialParaCalculo, ParametrosCalculoFaturamento.DataHoraPorDeposito);

                                if (CalculoDiarias.Diarias >= DiariasCalculadas)
                                {
                                    CalculoDiarias.Diarias = DiariasCalculadas;

                                    DiariasCalculadas = 0;
                                }
                                else
                                {
                                    DiariasCalculadas -= CalculoDiarias.Diarias;
                                }

                                FaturamentoComposicao.FaturamentoServicoTipoVeiculoId = FaturamentoServicoAssociadoVeiculoAmbos.FaturamentoServicoTipoVeiculoId;

                                FaturamentoComposicao.TipoComposicao = FaturamentoServicoAssociadoVeiculoAmbos.TipoCobranca;

                                FaturamentoComposicao.ValorTipoComposicao = FaturamentoServicoAssociadoVeiculoAmbos.PrecoPadrao;

                                // Aplicar Quantidade Alterada
                                if (FaturamentoQuantidadeAlterada != null)
                                {
                                    FaturamentoComposicao = AplicarQuantidadeAlterada(FaturamentoComposicao, FaturamentoQuantidadeAlterada);

                                    CalculoDiarias.Diarias += Convert.ToInt32(FaturamentoComposicao.QuantidadeAlterada);
                                }

                                FaturamentoComposicao.QuantidadeComposicao = CalculoDiarias.Diarias;

                                FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoAssociadoVeiculoAmbos.PrecoPadrao * CalculoDiarias.Diarias, 2);

                                FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;

                                // Aplicar os Descontos
                                if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto && ParametrosCalculoFaturamento.FaturamentoDescontos?.Count > 0)
                                {
                                    FaturamentoComposicao = AplicarDesconto(FaturamentoComposicao, ParametrosCalculoFaturamento.FaturamentoDescontos);
                                }

                                FaturamentoComposicao.TipoLancamento = TipoLancamentoFaturamentoEnum.Crédito;

                                ValorFaturado += FaturamentoComposicao.ValorFaturado;

                                FaturamentoComposicoes.Add(FaturamentoComposicao);

                                if (DiariasCalculadas == 0)
                                {
                                    break;
                                }
                            }

                            continue;
                        }
                        else if (FaturamentoServicoGrv.FormaCobranca == "VI")
                        {
                            // Segundo filtro, cobrar pela Vigência Inicial
                            FaturamentoServicoAssociadoVeiculo = FaturamentoServicosAssociadosVeiculos
                                .OrderBy(x => x.DataVigenciaInicial)
                                .FirstOrDefault(x => x.FaturamentoServicoTipoId == FaturamentoServicoGrv.FaturamentoServicoTipoId
                                    && (x.DataVigenciaFinal >= ParametrosCalculoFaturamento.DataHoraInicialParaCalculo.Date || x.DataVigenciaFinal == null));

                            // Aplicar Quantidade Alterada
                            if (FaturamentoQuantidadeAlterada != null)
                            {
                                FaturamentoComposicao = AplicarQuantidadeAlterada(FaturamentoComposicao, FaturamentoQuantidadeAlterada);

                                CalculoDiarias.Diarias += Convert.ToInt32(FaturamentoComposicao.QuantidadeAlterada);
                            }

                            FaturamentoComposicao.TipoComposicao = FaturamentoServicoAssociadoVeiculo.TipoCobranca;

                            FaturamentoComposicao.FaturamentoServicoTipoVeiculoId = FaturamentoServicoAssociadoVeiculo.FaturamentoServicoTipoVeiculoId;

                            FaturamentoComposicao.ValorTipoComposicao = FaturamentoServicoAssociadoVeiculo.PrecoPadrao;

                            FaturamentoComposicao.QuantidadeComposicao = CalculoDiarias.Diarias;

                            FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoComposicao.ValorTipoComposicao * CalculoDiarias.Diarias, 2);

                            FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                        }
                    }
                    else
                    {
                        // Aplicar Quantidade Alterada
                        if (FaturamentoQuantidadeAlterada != null)
                        {
                            FaturamentoComposicao = AplicarQuantidadeAlterada(FaturamentoComposicao, FaturamentoQuantidadeAlterada);

                            CalculoDiarias.Diarias += Convert.ToInt32(FaturamentoComposicao.QuantidadeAlterada);
                        }

                        FaturamentoComposicao.QuantidadeComposicao = CalculoDiarias.Diarias;

                        FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoGrv.PrecoPadrao * CalculoDiarias.Diarias, 2);

                        FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                    }
                }
                else if (FaturamentoServicoGrv.TipoCobranca == TipoCobrancaFaturamentoEnum.Horas)
                {
                    if (string.IsNullOrWhiteSpace(FaturamentoServicoGrv.TempoTrabalhado))
                    {
                        continue;
                    }

                    FaturamentoComposicao.QuantidadeComposicao = Math.Round(Convert.ToDecimal(TimeSpan.Parse(FaturamentoServicoGrv.TempoTrabalhado).TotalHours), 2);

                    FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoGrv.PrecoPadrao * FaturamentoComposicao.QuantidadeComposicao.Value, 2);

                    FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                }
                else if (FaturamentoServicoGrv.TipoCobranca == TipoCobrancaFaturamentoEnum.Quantidade)
                {
                    if (FaturamentoServicoGrv.FlagRebocada == "S")
                    {
                        FaturamentoComposicao.QuantidadeComposicao = 1;

                        if (FaturamentoServicoGrv.FormaCobranca == "VI") // VI: Vigência Inicial
                        {
                            FaturamentoServicoAssociadoVeiculo = FaturamentoServicosAssociadosVeiculos
                                .OrderBy(x => x.DataVigenciaInicial)
                                .FirstOrDefault(x => x.FaturamentoServicoTipoId == FaturamentoServicoGrv.FaturamentoServicoTipoId
                                                 && (x.DataVigenciaFinal >= ParametrosCalculoFaturamento.DataHoraInicialParaCalculo.Date || x.DataVigenciaFinal == null));

                            FaturamentoServicoGrv.PrecoPadrao = FaturamentoServicoAssociadoVeiculo.PrecoPadrao;

                            FaturamentoComposicao.FaturamentoServicoTipoVeiculoId = FaturamentoServicoAssociadoVeiculo.FaturamentoServicoTipoVeiculoId;

                            FaturamentoComposicao.ValorTipoComposicao = FaturamentoServicoAssociadoVeiculo.PrecoPadrao;
                        }
                    }
                    else
                    {
                        FaturamentoComposicao.QuantidadeComposicao = Math.Round(FaturamentoServicoGrv.Valor.Value, 2);
                    }

                    if (FaturamentoComposicao.QuantidadeComposicao == 0)
                    {
                        continue;
                    }

                    FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoGrv.PrecoPadrao * Math.Round(FaturamentoComposicao.QuantidadeComposicao.Value, 2), 2);

                    FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                }
                else if (FaturamentoServicoGrv.TipoCobranca == TipoCobrancaFaturamentoEnum.Valor)
                {
                    if (FaturamentoServicoGrv.FlagPermiteAlteracaoValor == "N" && (FaturamentoServicoGrv.PrecoPadrao > 0) && (FaturamentoServicoGrv.Valor == 0))
                    {
                        FaturamentoServicoGrv.Valor = 1;
                    }

                    if (FaturamentoServicoGrv.FlagRebocada == "S")
                    {
                        FaturamentoComposicao.QuantidadeComposicao = 1;

                        FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoGrv.Valor.Value, 2);

                        FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                    }
                    else
                    {
                        FaturamentoComposicao.QuantidadeComposicao = Math.Round(FaturamentoServicoGrv.Valor.Value, 2);

                        FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoGrv.PrecoPadrao * FaturamentoServicoGrv.Valor.Value, 2);

                        FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                    }
                }
                else if (FaturamentoServicoGrv.TipoCobranca == TipoCobrancaFaturamentoEnum.Tempo)
                {
                    if ((Horas = (int)(ParametrosCalculoFaturamento.DataHoraPorDeposito - ParametrosCalculoFaturamento.DataHoraInicialParaCalculo).TotalHours) == 0)
                    {
                        Horas++;
                    }

                    FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoGrv.PrecoPadrao * Horas, 2);

                    FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                }

                // Aplicar os Descontos
                if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto && ParametrosCalculoFaturamento.FaturamentoDescontos?.Count > 0)
                {
                    FaturamentoComposicao = AplicarDesconto(FaturamentoComposicao, ParametrosCalculoFaturamento.FaturamentoDescontos);
                }

                FaturamentoComposicao.TipoLancamento = TipoLancamentoFaturamentoEnum.Crédito;

                ValorFaturado += FaturamentoComposicao.ValorFaturado;

                FaturamentoComposicoes.Add(FaturamentoComposicao);
            }
            #endregion Composição do Faturamento

            #region Tributação
            if (!ParametrosCalculoFaturamento.FaturarSemGrv && ValorFaturado > 0)
            {
                List<CalculoTributacaoModel> Tributacoes = CalcularTributacao(_context,
                    ParametrosCalculoFaturamento,
                    ValorFaturado,
                    Atendimento.NotaFiscalDocumento,
                    Atendimento.NotaFiscalMunicipio,
                    Atendimento.NotaFiscalUF);

                if (Tributacoes != null)
                {
                    foreach (CalculoTributacaoModel Tributacao in Tributacoes)
                    {
                        FaturamentoComposicoes.Add(Tributacao);

                        ValorFaturado += Tributacao.ValorFaturado;
                    }
                }
            }
            #endregion Tributação

            #region Cadastro do Faturamento
            FaturamentoModel Faturamento = new();

            if (!ParametrosCalculoFaturamento.FaturarSemGrv)
            {
                Faturamento = new()
                {
                    AtendimentoId = Atendimento.AtendimentoId,

                    UsuarioCadastroId = ParametrosCalculoFaturamento.UsuarioCadastroId,

                    TipoMeioCobrancaId = ParametrosCalculoFaturamento.TipoMeioCobrancaId,

                    HoraDiaria = CalculoDiarias.HoraDiaria,

                    MaximoDiariasParaCobranca = CalculoDiarias.MaximoDiariasParaCobranca,

                    MaximoDiasVencimento = CalculoDiarias.MaximoDiasVencimento,

                    FlagUsarHoraDiaria = CalculoDiarias.FlagUsarHoraDiaria ? "S" : "N",

                    FlagClienteRealizaFaturamentoArrecadacao = CalculoDiarias.FlagClienteRealizaFaturamentoArrecadacao ? "S" : "N",

                    FlagCobrarDiariasDiasCorridos = CalculoDiarias.FlagCobrarDiariasDiasCorridos ? "S" : "N",

                    DataCalculo = CalculoDiarias.DataHoraInicialParaCalculo,

                    DataVencimento = CalcularDataVencimento(ParametrosCalculoFaturamento, CalculoDiarias, ParametrosCalculoFaturamento.FlagPermissaoDataRetroativaFaturamento ? ParametrosCalculoFaturamento.DataHoraPorDeposito : ParametrosCalculoFaturamento.DataHoraFinalParaCalculo),

                    DataCadastro = ParametrosCalculoFaturamento.DataHoraPorDeposito,

                    ValorFaturado = ValorFaturado,

                    ListagemFaturamentoComposicao = FaturamentoComposicoes
                };
            }
            else
            {
                Faturamento = new()
                {
                    HoraDiaria = CalculoDiarias.HoraDiaria,

                    MaximoDiariasParaCobranca = CalculoDiarias.MaximoDiariasParaCobranca,

                    MaximoDiasVencimento = CalculoDiarias.MaximoDiasVencimento,

                    FlagUsarHoraDiaria = CalculoDiarias.FlagUsarHoraDiaria ? "S" : "N",

                    FlagClienteRealizaFaturamentoArrecadacao = CalculoDiarias.FlagClienteRealizaFaturamentoArrecadacao ? "S" : "N",

                    FlagCobrarDiariasDiasCorridos = CalculoDiarias.FlagCobrarDiariasDiasCorridos ? "S" : "N",

                    DataCalculo = CalculoDiarias.DataHoraInicialParaCalculo,

                    DataVencimento = CalcularDataVencimento(ParametrosCalculoFaturamento, CalculoDiarias, ParametrosCalculoFaturamento.FlagPermissaoDataRetroativaFaturamento ? ParametrosCalculoFaturamento.DataHoraPorDeposito : ParametrosCalculoFaturamento.DataHoraFinalParaCalculo),

                    DataCadastro = ParametrosCalculoFaturamento.DataHoraPorDeposito,

                    ValorFaturado = ValorFaturado,

                    ListagemFaturamentoComposicao = FaturamentoComposicoes
                };
            }

            if (ParametrosCalculoFaturamento.FlagPermissaoDataRetroativaFaturamento)
            {
                Faturamento.DataRetroativa = ParametrosCalculoFaturamento.DataHoraFinalParaCalculo.Date;

                Faturamento.FlagPermissaoDataRetroativaFaturamento = "S";
            }

            if (UltimoFaturamento != null)
            {
                Faturamento.Sequencia += UltimoFaturamento.Sequencia;
            }

            if (!ParametrosCalculoFaturamento.FaturarSemGrv && !ParametrosCalculoFaturamento.IsSimulacao)
            {
                Faturamento.NumeroIdentificacao = CreateNumeroIdentificacao(ParametrosCalculoFaturamento, Faturamento.Sequencia);

                // _context.SetUserContextInfo(Faturamento.UsuarioCadastroId);

                _context.Faturamento.Add(Faturamento);
            }

            return Faturamento;
            #endregion Cadastro do Faturamento
        }

        private static bool CheckServicoDeveSerCalculado(ViewFaturamentoServicoGrvModel FaturamentoServicoGrv, FaturamentoModel UltimoFaturamento, CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            if (ParametrosCalculoFaturamento.FaturamentoProdutoId != "DEP" &&
                ParametrosCalculoFaturamento.FaturamentoProdutoId != "DRF" &&
                FaturamentoServicoGrv.FaturamentoServicoGrvId == 0)
            {
                return false;
            }
            else if (FaturamentoServicoGrv.FlagPermiteAlteracaoValor == "S" &&
                     FaturamentoServicoGrv.Valor <= 0)
            {
                return false;
            }
            else if (FaturamentoServicoGrv.FlagRealizarCobranca == "N")
            {
                // Se o Usuário escolheu por não cobrar o Serviço
                return false;
            }
            else if (!ParametrosCalculoFaturamento.FlagFaturamentoCompleto &&
                     FaturamentoServicoGrv.FlagCobrarSomentePrimeiraFatura == "S")
            {
                // Se não for o primeiro Faturamento e se o Serviço for para ser cobrado apenas no primeiro Faturamento
                return false;
            }
            else if (FaturamentoServicoGrv.FlagCobrarSomentePrimeiraFatura == "S" &&
                     UltimoFaturamento != null &&
                     UltimoFaturamento.Status == "P")
            {
                // Se a última Fatura for paga e o Serviço só pode ser cobrado na primeira Fatura
                return false;
            }
            else if (ParametrosCalculoFaturamento.IsComboio &&
                     FaturamentoServicoGrv.FlagRebocada == "S")
            {
                // Não cobrar rebocada caso o veículo entrou no Depósito por Comboio
                return false;
            }
            else if (FaturamentoServicoGrv.FaturamentoRegraTipoCodigo != null &&
                     FaturamentoServicoGrv.FaturamentoRegraTipoCodigo == FaturamentoRegraTipoEnum.CobrarTarifaBancaria &&
                     !ParametrosCalculoFaturamento.TipoMeioCobrancaId.Equals(1))
            {
                // Se o serviço tiver a regra "Cobrança de Tarifa Bancária" e se o Tipo do Meio de Cobrança for Boleto
                return false;
            }

            return true;
        }

        private static int GetQuantidadeDiasServicoDiarias(ViewFaturamentoServicoAssociadoVeiculoModel FaturamentoServicoAssociadoVeiculo, DateTime DataHoraGuarda, DateTime DataHoraAtualPorDeposito)
        {
            DateTime DataInicial = DataHoraGuarda;

            DateTime DataFinal = DataHoraAtualPorDeposito;

            if (FaturamentoServicoAssociadoVeiculo.DataVigenciaInicial > DataHoraGuarda)
            {
                DataInicial = FaturamentoServicoAssociadoVeiculo.DataVigenciaInicial;
            }

            // Se data final da vigência for menor que a data atual
            if ((FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal > DateTime.MinValue) &&
                (FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value.Date < DataHoraAtualPorDeposito.Date))
            {
                DataFinal = new DateTime(FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value.Year, FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value.Month, FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value.Day, 23, 59, 59);
            }

            return 1 + DateTimeHelper.GetDaysBetweenTwoDates(DataInicial, DataFinal);
        }

        private static FaturamentoComposicaoModel AplicarQuantidadeAlterada(FaturamentoComposicaoModel FaturamentoComposicao, CalculoFaturamentoQuantidadeAlteradaModel FaturamentoQuantidadeAlterada)
        {
            FaturamentoComposicao.UsuarioAlteracaoQuantidadeId = FaturamentoQuantidadeAlterada.UsuarioAlteracaoQuantidadeId;

            FaturamentoComposicao.QuantidadeAlterada = FaturamentoQuantidadeAlterada.QuantidadeAlterada;

            FaturamentoComposicao.ObservacaoQuantidadeAlterada = FaturamentoQuantidadeAlterada.ObservacaoQuantidadeAlterada;

            return FaturamentoComposicao;
        }

        private static FaturamentoComposicaoModel AplicarDesconto(FaturamentoComposicaoModel FaturamentoComposicao, List<CalculoFaturamentoDescontoModel> ListFaturamentoDesconto)
        {
            if (ListFaturamentoDesconto != null)
            {
                CalculoFaturamentoDescontoModel FaturamentoDesconto = ListFaturamentoDesconto
                    .Find(w => w.FaturamentoServicoTipoVeiculoId == FaturamentoComposicao.FaturamentoServicoTipoVeiculoId);

                if (FaturamentoDesconto != null)
                {
                    FaturamentoComposicao.UsuarioDescontoId = FaturamentoDesconto.UsuarioDescontoId;

                    FaturamentoComposicao.TipoDesconto = FaturamentoDesconto.TipoDesconto;

                    FaturamentoComposicao.ValorDesconto = FaturamentoDesconto.ValorDesconto;

                    FaturamentoComposicao.QuantidadeDesconto = FaturamentoDesconto.QuantidadeDesconto;

                    FaturamentoComposicao.ObservacaoDesconto = FaturamentoDesconto.ObservacaoDesconto;

                    if (FaturamentoComposicao.TipoDesconto == TipoDescontoFaturamentoEnum.Valor)
                    {
                        FaturamentoComposicao.ValorFaturado = Math.Round(FaturamentoComposicao.ValorComposicao - FaturamentoDesconto.ValorDesconto, 2);
                    }
                    else
                    {
                        FaturamentoComposicao.ValorFaturado = Math.Round(NumberHelper.GetPercentage(FaturamentoComposicao.ValorComposicao, FaturamentoDesconto.QuantidadeDesconto), 2);
                    }
                }
            }

            return FaturamentoComposicao;
        }

        private static List<CalculoTributacaoModel> CalcularTributacao(AppDbContext _context, CalculoFaturamentoParametroModel ParametrosCalculoFaturamento, decimal valorCalculado, string notaFiscalCnpj, string notaFiscalMunicipio, string notaFiscalUF)
        {
            if (valorCalculado <= 0 && string.IsNullOrWhiteSpace(notaFiscalCnpj) || string.IsNullOrWhiteSpace(notaFiscalMunicipio) || string.IsNullOrWhiteSpace(notaFiscalUF))
            {
                return null;
            }

            List<ViewFaturamentoServicoAssociadoVeiculoModel> ServicosTributados = _context.ViewFaturamentoServicoAssociadoVeiculo
                .Where(x => x.ClienteId == ParametrosCalculoFaturamento.ClienteDeposito.ClienteId
                    && x.DepositoId == ParametrosCalculoFaturamento.ClienteDeposito.DepositoId
                    && x.TipoVeiculoId == ParametrosCalculoFaturamento.TipoVeiculoId
                    && x.FaturamentoProdutoId == ParametrosCalculoFaturamento.FaturamentoProdutoId
                    && x.FlagTributacao == "S"
                    && x.DataVigenciaFinal == null)
                .AsNoTracking()
                .ToList();

            if (ServicosTributados == null)
            {
                return null;
            }

            ViewEnderecoCompletoModel Endereco = _context.EnderecoCompleto
                .AsNoTracking()
                .FirstOrDefault(x => x.CEPId == ParametrosCalculoFaturamento.ClienteDeposito.Deposito.CEPId);

            if (Endereco == null)
            {
                return null;
            }

            if (StringHelper.Normalize(notaFiscalMunicipio) != StringHelper.Normalize(Endereco.Municipio) || notaFiscalUF != Endereco.UF)
            {
                return null;
            }

            #region Selecionar Regras do Faturamento
            FaturamentoRegraModel FaturamentoRegra = _context.FaturamentoRegra
                .Include(x => x.FaturamentoRegraTipo)
                .Where(x => x.ClienteId == ParametrosCalculoFaturamento.ClienteDeposito.ClienteId
                         && x.DepositoId == ParametrosCalculoFaturamento.ClienteDeposito.DepositoId
                         && x.FaturamentoRegraTipo.Codigo == FaturamentoRegraTipoEnum.DescontoISS)
                .AsNoTracking()
                .FirstOrDefault();

            if (FaturamentoRegra != null && Convert.ToDecimal(FaturamentoRegra.Valor) > valorCalculado)
            {
                return null;
            }
            #endregion Selecionar Regras do Faturamento

            List<CalculoTributacaoModel> Tributacoes = new();

            CalculoTributacaoModel Tributacao;

            foreach (ViewFaturamentoServicoAssociadoVeiculoModel item in ServicosTributados)
            {
                if (item.FaturamentoRegraTipoCodigo == FaturamentoRegraTipoEnum.DescontoISS)
                {
                    continue;
                }

                Tributacao = new CalculoTributacaoModel
                {
                    FaturamentoServicoTipoVeiculoId = item.FaturamentoServicoTipoVeiculoId,

                    FaturamentoServicoAssociadoId = item.FaturamentoServicoAssociadoId,

                    TipoComposicao = item.TipoCobranca,

                    TipoLancamento = TipoLancamentoFaturamentoEnum.Débito,

                    QuantidadeComposicao = 1,

                    ValorTipoComposicao = item.PrecoPadrao
                };

                if (Tributacao.TipoComposicao == "P")
                {
                    Tributacao.ValorComposicao = valorCalculado * (item.PrecoPadrao / 100);

                    Tributacao.ValorFaturado = Tributacao.ValorComposicao;
                }
                else if (Tributacao.TipoComposicao == "V")
                {
                    Tributacao.ValorComposicao = item.PrecoPadrao;

                    Tributacao.ValorFaturado = Tributacao.ValorComposicao;
                }

                Tributacao.ValorComposicao *= -1;

                Tributacao.ValorFaturado *= -1;

                Tributacoes.Add(Tributacao);
            }

            return Tributacoes;
        }

        private static string CreateNumeroIdentificacao(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento, int Sequencia)
        {
            return StringHelper.AddCharToLeft(ParametrosCalculoFaturamento.NumeroFormularioGrv, '0', 9) +
                   StringHelper.AddCharToLeft(ParametrosCalculoFaturamento.ClienteDeposito.DepositoId.ToString(), '0', 4) +
                   StringHelper.AddCharToLeft(Sequencia.ToString(), '0', 3);
        }

        private static DateTime CalcularDataVencimento(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento, CalculoDiariasModel CalculoDiarias, DateTime? dataFinal = null)
        {
            if (!dataFinal.HasValue)
            {
                dataFinal = ParametrosCalculoFaturamento.DataHoraPorDeposito;
            }

            DateTime dataVencimento = dataFinal.Value;

            dataVencimento = dataVencimento.SetTime(23, 59, 59);

            // Se for pra cobrar por dias corridos, não é preciso verificar dias não úteis
            if (CalculoDiarias.FlagCobrarDiariasDiasCorridos)
            {
                return dataVencimento;
            }

            // Se for identificado que o dia é Sábado, Domingo ou Feriado, irá somar dias até que o dia seja um dia útil
            while (true)
            {
                if (dataVencimento.DayOfWeek == DayOfWeek.Saturday) //1. Se for Sábado
                {
                    dataVencimento = dataVencimento.AddDays(2);
                }
                else if (dataVencimento.DayOfWeek == DayOfWeek.Sunday) //2. Se for Domingo
                {
                    dataVencimento = dataVencimento.AddDays(1);
                }
                else if (CalculoDiarias.Feriados != null) //3. Se for Feriado
                {
                    if (CalculoDiarias.Feriados.Any(x => x.Date == dataVencimento.Date))
                    {
                        // É feriado
                        dataVencimento = dataVencimento.AddDays(1);
                    }
                    else
                    {
                        break; // Não é feriado
                    }
                }
            }

            return dataVencimento;
        }

        public MensagemDTO UpdateFormaPagamento(int FaturamentoId, byte TipoMeioCobrancaId, int UsuarioId)
        {
            MensagemDTO ResultView = new();

            List<string> erros = new();

            if (FaturamentoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorFaturamentoInvalido);
            }

            if (TipoMeioCobrancaId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorFormaPagamentoInvalido);
            }

            if (UsuarioId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorUsuarioInvalido);
            }

            if (erros.Count > 0)
            {
                return MensagemViewHelper.SetBadRequest(erros);
            }

            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                return MensagemViewHelper.SetUnauthorized();
            }

            FaturamentoModel Faturamento = _context.Faturamento
                .Include(x => x.TipoMeioCobranca)
                .Include(x => x.Atendimento)
                .ThenInclude(x => x.Grv)
                .ThenInclude(x => x.Cliente)
                .AsNoTracking()
                .FirstOrDefault(x => x.FaturamentoId == FaturamentoId);

            if (Faturamento == null)
            {
                return MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoFaturamento);
            }
            else if (Faturamento.Status == "C")
            {
                return MensagemViewHelper.SetBadRequest("Esse Faturamento foi cancelado");
            }
            else if (Faturamento.Status == "P")
            {
                return MensagemViewHelper.SetBadRequest("Esse Faturamento já foi pago");
            }
            else if (Faturamento.TipoMeioCobrancaId == TipoMeioCobrancaId)
            {
                return MensagemViewHelper.SetBadRequest("Forma de Pagamento já selecionado");
            }

            TipoMeioCobrancaModel TipoMeioCobranca = _context.TipoMeioCobranca
                .AsNoTracking()
                .FirstOrDefault(x => x.TipoMeioCobrancaId == TipoMeioCobrancaId);

            if (TipoMeioCobranca == null)
            {
                return MensagemViewHelper.SetBadRequest($"Forma de Pagamento inexistente: {TipoMeioCobrancaId}");
            }
            else if (TipoMeioCobranca.Alias == TipoMeioCobrancaAliasEnum.PixEstatico &&
                     Faturamento.Atendimento.Grv.Cliente.FlagPossuiPixEstatico == "N")
            {
                return MensagemViewHelper.SetBadRequest("Este Cliente não está configurado para emitir a Forma de Pagamento PIX Estático");
            }
            else if (TipoMeioCobranca.Alias == TipoMeioCobrancaAliasEnum.PixDinamico &&
                     Faturamento.Atendimento.Grv.Cliente.FlagPossuiPixDinamico == "N")
            {
                return MensagemViewHelper.SetBadRequest("Este Cliente não está configurado para emitir a Forma de Pagamento PIX Dinâmico");
            }

            FaturamentoModel FaturamentoUpdate = _context.Faturamento
                .FirstOrDefault(x => x.FaturamentoId == FaturamentoId);

            FaturamentoUpdate.TipoMeioCobrancaId = TipoMeioCobrancaId;

            using IDbContextTransaction transaction = _context.Database.BeginTransaction();

            try
            {
                DeleteTipoMeioCobrancaAtual(FaturamentoId, Faturamento.TipoMeioCobranca);

                _context.Faturamento.Update(FaturamentoUpdate);

                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return MensagemViewHelper.SetInternalServerError("Ocorreu um erro ao alterar a Forma de Pagamento", ex);
            }

            return MensagemViewHelper.SetOk("Forma de Pagamento alterado com sucesso");
        }

        private void DeleteTipoMeioCobrancaAtual(int FaturamentoId, TipoMeioCobrancaModel TipoMeioCobrancaAtual)
        {
            if (TipoMeioCobrancaAtual.Alias == TipoMeioCobrancaAliasEnum.Boleto ||
                TipoMeioCobrancaAtual.Alias == TipoMeioCobrancaAliasEnum.BoletoEspecial)
            {
                new BoletoService(_context, _httpClientFactory)
                    .Cancel(FaturamentoId);
            }
            else if (TipoMeioCobrancaAtual.Alias == TipoMeioCobrancaAliasEnum.PixEstatico)
            {
                _context.PixEstatico
                    .Where(x => x.FaturamentoId == FaturamentoId)
                    .Delete();
            }
            else if (TipoMeioCobrancaAtual.Alias == TipoMeioCobrancaAliasEnum.PixDinamico)
            {
                _context.PixDinamico
                    .Where(x => x.FaturamentoId == FaturamentoId)
                    .Delete();
            }
        }

        public async Task<FaturamentoProdutoDTO> GetProdutoAsync(string FaturamentoProdutoId)
        {
            FaturamentoProdutoModel result = await _context.FaturamentoProduto
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FaturamentoProdutoId == FaturamentoProdutoId);

            if (result != null)
            {
                return _mapper
                    .Map<FaturamentoProdutoDTO>(result);
            }
            else
            {
                return null;
            }
        }

        public async Task<FaturamentoProdutoListDTO> ListProdutosAsync()
        {
            FaturamentoProdutoListDTO ResultView = new();

            List<FaturamentoProdutoModel> result = await _context.FaturamentoProduto
                .AsNoTracking()
                .ToListAsync();

            ResultView.Listagem = _mapper
                .Map<List<FaturamentoProdutoDTO>>(result
                .OrderBy(x => x.Descricao)
                .ToList());

            ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);

            return ResultView;
        }

        public async Task<ServicoAssociadoTipoVeiculoListDTO> ListServicoAssociadoTipoVeiculoAsync(int GrvId, int UsuarioId)
        {
            ServicoAssociadoTipoVeiculoListDTO ResultView = new();

            MensagemDTO Mensagem = new GrvService(_context)
                .ValidateInputGrv(GrvId, UsuarioId);

            if (Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                ResultView.Mensagem = Mensagem;

                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvId);

            List<ViewFaturamentoServicoAssociadoVeiculoModel> result = _context
                .ViewFaturamentoServicoAssociadoVeiculo
                .Where(x => x.ClienteId == Grv.ClienteId
                         && x.DepositoId == Grv.DepositoId
                         && x.TipoVeiculoId == Grv.TipoVeiculoId
                         && x.FaturamentoProdutoId == Grv.FaturamentoProdutoId
                         && (new[] { "DEP", "DRF" }.Contains(Grv.FaturamentoProdutoId) ? x.FlagCobrarGGV == "S" : true)
                         && x.DataVigenciaFinal == null)
                .AsNoTracking()
                .ToList();

            if (result?.Count > 0)
            {
                foreach (ViewFaturamentoServicoAssociadoVeiculoModel item in result)
                {
                    if (item.FlagNaoCobrarSeNaoUsouReboque == "N" && Grv.FlagComboio == "S")
                    {
                        continue;
                    }
                    else if (item.FlagServicoObrigatorio == "S" || item.FlagServicoObrigatorioGlobal == "S")
                    {
                        continue;
                    }

                    ResultView.Listagem.Add(new()
                    {
                        IdentificadorServicoAssociadoTipoVeiculo = item.FaturamentoServicoTipoVeiculoId,

                        DescricaoServico = item.ServicoDescricao,

                        TipoCobranca = item.TipoCobranca,

                        DescricaoTipoCobranca = item.TipoCobrancaDescricao,

                        FlagPermiteAlteracaoValor = item.FlagPermiteAlteracaoValor,

                        PrecoPadrao = item.PrecoPadrao,

                        PrecoMinimoObrigatorio = item.PrecoValorMinimo,

                        DataVigenciaInicial = item.DataVigenciaInicial.Date
                    });
                }

                if (ResultView.Listagem.Count > 0)
                {
                    ResultView.Mensagem = MensagemViewHelper
                        .SetFound(ResultView.Listagem.Count);
                }
                else
                {
                    ResultView.Mensagem = MensagemViewHelper.SetNotFound();
                }
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ServicoAssociadoGrvListDTO> ListServicoAssociadoGrvAsync(int GrvId, int UsuarioId)
        {
            ServicoAssociadoGrvListDTO ResultView = new();

            MensagemDTO Mensagem = new GrvService(_context)
                .ValidateInputGrv(GrvId, UsuarioId);

            if (Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                ResultView.Mensagem = Mensagem;

                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvId);

            List<ViewFaturamentoServicoGrvModel> result = _context.ViewFaturamentoServicoGrv
                .Where(x => x.GrvId == GrvId)
                .AsNoTracking()
                .ToList();

            if (result?.Count > 0)
            {
                foreach (ViewFaturamentoServicoGrvModel item in result)
                {
                    if (item.FlagNaoCobrarSeNaoUsouReboque == "N" && Grv.FlagComboio == "S")
                    {
                        continue;
                    }
                    else if (item.FlagServicoObrigatorio == "S" || item.FlagServicoObrigatorioGlobal == "S")
                    {
                        continue;
                    }

                    //ResultView.Listagem.Add(new()
                    //{
                    //    ServicoAssociadoTipoVeiculoId = item.FaturamentoServicoTipoVeiculoId,

                    //    ServicoDescricao = item.ServicoDescricao,

                    //    TipoCobranca = item.TipoCobranca,

                    //    FlagPermiteAlteracaoValor = item.FlagPermiteAlteracaoValor,

                    //    PrecoPadrao = item.PrecoPadrao,

                    //    PrecoValorMinimo = item.PrecoValorMinimo,

                    //    DataVigenciaInicial = item.DataVigenciaInicial
                    //});
                }

                if (ResultView.Listagem.Count > 0)
                {
                    ResultView.Mensagem = MensagemViewHelper
                        .SetFound(ResultView.Listagem.Count);
                }
                else
                {
                    ResultView.Mensagem = MensagemViewHelper.SetNotFound();
                }
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<int> GetUltimoFaturamentoIdAsync(int GrvId)
        {
            GrvModel Grv = await _context.Grv
                .Include(x => x.Atendimento)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvId);

            if (Grv == null)
            {
                return 0;
            }

            int? FaturamentoId = await _context.Faturamento
                .Where(x => x.AtendimentoId == Grv.Atendimento.AtendimentoId && x.Status != "C")
                .AsNoTracking()
                .OrderByDescending(x => x.FaturamentoId)
                .Select(x => x.FaturamentoId)
                .FirstOrDefaultAsync();

            if (FaturamentoId == null)
            {
                return 0;
            }

            return FaturamentoId.Value;
        }
    }
}