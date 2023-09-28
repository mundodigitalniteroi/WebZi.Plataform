using Microsoft.EntityFrameworkCore;
using System.Data;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Faturamento.View;
using WebZi.Plataform.Domain.Models.Localizacao.View;

namespace WebZi.Plataform.Data.Services.Faturamento
{
    public class FaturamentoService
    {
        private readonly AppDbContext _context;

        public FaturamentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FaturamentoModel> Faturar(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            if (ParametrosCalculoFaturamento.DataLiberacao == DateTime.MinValue)
            {
                ParametrosCalculoFaturamento.DataLiberacao = ParametrosCalculoFaturamento.DataHoraPorDeposito;
            }

            #region Selecionar os Serviços cadastrados no GRV
            List<ViewFaturamentoServicoGrvModel> FaturamentoServicosGrvs = await _context.ViewFaturamentoServicosGrvs
                .Where(w => w.GrvId == ParametrosCalculoFaturamento.Grv.GrvId &&
                            w.FaturamentoProdutoId == ParametrosCalculoFaturamento.Grv.FaturamentoProdutoId &&
                            w.FlagTributacao == "N")
                .AsNoTracking()
                .ToListAsync();

            if (FaturamentoServicosGrvs == null)
            {
                throw new Exception("Não foi encontrado Serviço cadastrado para este GRV");
            }
            #endregion Selecionar os Serviços cadastrados no GRV

            #region Selecionar todos os Serviços associados ao CLIDEP, incluindo os com a Vigência finalizada
            List<ViewFaturamentoServicoAssociadoVeiculoModel> FaturamentoServicosAssociadosVeiculos = await _context.ViewFaturamentoServicosAssociadosVeiculos
                .Where(w => w.ClienteId == ParametrosCalculoFaturamento.Grv.ClienteId &&
                            w.DepositoId == ParametrosCalculoFaturamento.Grv.DepositoId &&
                            w.TipoVeiculoId == ParametrosCalculoFaturamento.Grv.TipoVeiculoId &&
                            w.FaturamentoProdutoId == ParametrosCalculoFaturamento.Grv.FaturamentoProdutoId
                )
                .AsNoTracking()
                .ToListAsync();

            if (FaturamentoServicosAssociadosVeiculos == null)
            {
                throw new Exception("Não foi encontrado Serviço associado ao Cliente + Depósito + Tipo de Veículo");
            }
            #endregion Selecionar todos os Serviços associados ao CLIDEP, incluindo os com a Vigência finalizada

            #region Verificação de Faturamentos anteriores
            // Faturamento.Status:
            // N = Novo Faturamento/Não Pago;
            // A = Faturamento Adicional e Não Pago (Pra quando a Fatura foi paga em atraso);
            // C = Cancelado, pra quando foi gerada uma Fatura para uma Fatura Vencida e que não foi paga;
            // P = Fatura Paga.

            // Se existir ao menos 1 Fatura paga, não deve dar Desconto
            if (await _context.Faturamentos
                .Where(w => w.AtendimentoId == ParametrosCalculoFaturamento.Atendimento.AtendimentoId && w.Status == "P")
                .AsNoTracking()
                .AnyAsync())
            {
                // Faturamentos adicionais não podem receber descontos
                ParametrosCalculoFaturamento.FaturamentoAdicional = true;
            }

            // Consulta da última Fatura para cancelar
            FaturamentoModel UltimoFaturamento = await _context.Faturamentos
                .Where(w => w.AtendimentoId == ParametrosCalculoFaturamento.Atendimento.AtendimentoId)
                .OrderByDescending(o => o.FaturamentoId)
                .FirstOrDefaultAsync();

            if (UltimoFaturamento != null)
            {
                #region Cancelar o Faturamento atual
                UltimoFaturamento.UsuarioAlteracaoId = ParametrosCalculoFaturamento.Atendimento.UsuarioCadastroId;

                UltimoFaturamento.Status = "C";

                UltimoFaturamento.DataAlteracao = ParametrosCalculoFaturamento.DataHoraPorDeposito;

                if (ParametrosCalculoFaturamento.FlagCadastrarFaturamento)
                {
                    _context.Faturamentos.Update(UltimoFaturamento);
                }

                if (ParametrosCalculoFaturamento.TipoMeioCobranca.TipoMeioCobrancaId == 0)
                {
                    ParametrosCalculoFaturamento.TipoMeioCobranca.TipoMeioCobrancaId = UltimoFaturamento.TipoMeioCobrancaId;
                }
                #endregion Cancelar o Faturamento atual

                // Se a Fatura for Nova, então está sendo cancelada e gerada uma nova, incluindo a aplicação dos Descontos caso houver.
                if (UltimoFaturamento.Status == "A" || ParametrosCalculoFaturamento.FaturamentoAdicional)
                {
                    ParametrosCalculoFaturamento.FlagFaturamentoCompleto = false;
                }
            }
            #endregion Verificação de Faturamentos anteriores

            #region Cálculo das Diárias
            CalculoDiariasModel CalculoDiarias = new();

            if (ParametrosCalculoFaturamento.Diarias == 0)
            {
                CalculoDiarias = await new CalculoDiariasService(_context)
                    .Calcular(ParametrosCalculoFaturamento);
            }
            else
            {
                CalculoDiarias.Diarias = ParametrosCalculoFaturamento.Diarias;
            }

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
                if (!VerificarServicoDeveSerCalculado(FaturamentoServicoGrv, UltimoFaturamento, ParametrosCalculoFaturamento))
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
                if (FaturamentoServicoGrv.TipoCobranca == "D")
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
                                .Where(w => w.FaturamentoServicoTipoId == FaturamentoServicoGrv.FaturamentoServicoTipoId &&
                                           (ParametrosCalculoFaturamento.Grv.DataHoraGuarda.Date >= w.DataVigenciaInicial && ParametrosCalculoFaturamento.Grv.DataHoraGuarda.Date <= w.DataVigenciaFinal) ||
                                            ParametrosCalculoFaturamento.Grv.DataHoraGuarda <= w.DataVigenciaFinal || w.DataVigenciaFinal == null)
                                .ToList();

                            foreach (ViewFaturamentoServicoAssociadoVeiculoModel FaturamentoServicoAssociadoVeiculoAmbos in FaturamentoServicosAssociadosVeiculosTodasVigenciasEncontradas)
                            {
                                // Retorna a quantidade de Dias entre as datas
                                CalculoDiarias.Diarias = RetornarQuantidadeDiasServicoDiarias(FaturamentoServicoAssociadoVeiculoAmbos, ParametrosCalculoFaturamento.Grv.DataHoraGuarda, ParametrosCalculoFaturamento.DataHoraPorDeposito);

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

                                FaturamentoComposicao.TipoLancamento = "C"; // Crédito

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
                                .Where(w => w.FaturamentoServicoTipoId == FaturamentoServicoGrv.FaturamentoServicoTipoId)
                                .Where(w => w.DataVigenciaFinal >= ParametrosCalculoFaturamento.Grv.DataHoraGuarda.Date || w.DataVigenciaFinal == null)
                                .OrderBy(o => o.DataVigenciaInicial)
                                .FirstOrDefault();

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
                else if (FaturamentoServicoGrv.TipoCobranca == "H")
                {
                    if (string.IsNullOrWhiteSpace(FaturamentoServicoGrv.TempoTrabalhado))
                    {
                        continue;
                    }

                    FaturamentoComposicao.QuantidadeComposicao = Math.Round(Convert.ToDecimal(TimeSpan.Parse(FaturamentoServicoGrv.TempoTrabalhado).TotalHours), 2);

                    FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoGrv.PrecoPadrao * FaturamentoComposicao.QuantidadeComposicao.Value, 2);

                    FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                }
                else if (FaturamentoServicoGrv.TipoCobranca == "Q") // Quantidade
                {
                    if (FaturamentoServicoGrv.FlagRebocada == "S")
                    {
                        FaturamentoComposicao.QuantidadeComposicao = 1;

                        if (FaturamentoServicoGrv.FormaCobranca == "VI") // VI: Vigência Inicial
                        {
                            FaturamentoServicoAssociadoVeiculo = FaturamentoServicosAssociadosVeiculos
                                .Where(w => w.FaturamentoServicoTipoId == FaturamentoServicoGrv.FaturamentoServicoTipoId)
                                .Where(w => w.DataVigenciaFinal >= ParametrosCalculoFaturamento.Grv.DataHoraGuarda.Date || w.DataVigenciaFinal == null)
                                .OrderBy(o => o.DataVigenciaInicial)
                                .First();

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
                else if (FaturamentoServicoGrv.TipoCobranca == "V")
                {
                    if (FaturamentoServicoGrv.FlagPermiteAlteracaoValor.Equals("N") && (FaturamentoServicoGrv.PrecoPadrao > 0) && (FaturamentoServicoGrv.Valor == 0))
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
                else if (FaturamentoServicoGrv.TipoCobranca == "T") // Tempo
                {
                    if ((Horas = (int)(ParametrosCalculoFaturamento.DataHoraPorDeposito - ParametrosCalculoFaturamento.Grv.DataHoraGuarda).TotalHours) == 0)
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

                FaturamentoComposicao.TipoLancamento = "C"; // Crédito

                ValorFaturado += FaturamentoComposicao.ValorFaturado;

                FaturamentoComposicoes.Add(FaturamentoComposicao);
            }
            #endregion Composição do Faturamento

            #region Tributação
            if (ValorFaturado > 0)
            {
                List<CalculoTributacaoModel> Tributacoes = await CalcularTributacao(_context,
                    ParametrosCalculoFaturamento,
                    ValorFaturado,
                    ParametrosCalculoFaturamento.Atendimento.NotaFiscalDocumento,
                    ParametrosCalculoFaturamento.Atendimento.NotaFiscalMunicipio,
                    ParametrosCalculoFaturamento.Atendimento.NotaFiscalUf);

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
            FaturamentoModel Faturamento = new()
            {
                AtendimentoId = ParametrosCalculoFaturamento.Atendimento.AtendimentoId,

                UsuarioCadastroId = ParametrosCalculoFaturamento.UsuarioCadastroId,

                TipoMeioCobrancaId = ParametrosCalculoFaturamento.TipoMeioCobranca.TipoMeioCobrancaId,

                HoraDiaria = CalculoDiarias.HoraDiaria,

                MaximoDiariasParaCobranca = CalculoDiarias.MaximoDiariasParaCobranca,

                MaximoDiasVencimento = CalculoDiarias.MaximoDiasVencimento,

                FlagUsarHoraDiaria = CalculoDiarias.FlagUsarHoraDiaria,

                FlagClienteRealizaFaturamentoArrecadacao = CalculoDiarias.FlagClienteRealizaFaturamentoArrecadacao,

                FlagCobrarDiariasDiasCorridos = CalculoDiarias.FlagCobrarDiariasDiasCorridos,

                DataCalculo = CalculoDiarias.DataHoraInicialParaCalculo,

                DataVencimento = CalcularDataVencimento(ParametrosCalculoFaturamento, CalculoDiarias, ParametrosCalculoFaturamento.FlagPermissaoDataRetroativaFaturamento ? ParametrosCalculoFaturamento.DataHoraPorDeposito : ParametrosCalculoFaturamento.DataLiberacao),

                DataCadastro = ParametrosCalculoFaturamento.DataHoraPorDeposito,

                ValorFaturado = ValorFaturado,

                FaturamentoComposicoes = FaturamentoComposicoes
            };

            if (ParametrosCalculoFaturamento.FlagPermissaoDataRetroativaFaturamento)
            {
                Faturamento.DataRetroativa = ParametrosCalculoFaturamento.DataLiberacao.Date;

                Faturamento.FlagPermissaoDataRetroativaFaturamento = "S";
            }

            if (UltimoFaturamento != null)
            {
                Faturamento.Sequencia += UltimoFaturamento.Sequencia;
            }

            Faturamento.NumeroIdentificacao = GerarNumeroIdentificacao(ParametrosCalculoFaturamento, Faturamento.Sequencia);

            if (ParametrosCalculoFaturamento.FlagCadastrarFaturamento)
            {
                // _context.SetUserContextInfo(Faturamento.UsuarioCadastroId);

                await _context.Faturamentos.AddAsync(Faturamento);
            }

            return Faturamento;
            #endregion Cadastro do Faturamento
        }

        private static bool VerificarServicoDeveSerCalculado(ViewFaturamentoServicoGrvModel FaturamentoServicoGrv, FaturamentoModel UltimoFaturamento, CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            if (ParametrosCalculoFaturamento.Grv.FaturamentoProdutoId != "DEP" &&
                ParametrosCalculoFaturamento.Grv.FaturamentoProdutoId != "DRF" &&
                FaturamentoServicoGrv.FaturamentoServicoGrvId == 0)
            {
                return false;
            }
            else if (FaturamentoServicoGrv.FlagPermiteAlteracaoValor.Equals("S") &&
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
            else if (ParametrosCalculoFaturamento.Grv.FlagComboio == "S" &&
                     FaturamentoServicoGrv.FlagRebocada == "S")
            {
                // Não cobrar rebocada caso o veículo entrou no Depósito por Comboio
                return false;
            }
            else if (FaturamentoServicoGrv.FaturamentoRegraTipoCodigo != null &&
                     FaturamentoServicoGrv.FaturamentoRegraTipoCodigo.Equals("COBRATARIFABANCARIA") &&
                     !ParametrosCalculoFaturamento.TipoMeioCobranca.TipoMeioCobrancaId.Equals(1))
            {
                // Se o serviço tiver a regra "Cobrança de Tarifa Bancária" e se o Tipo do Meio de Cobrança for Boleto
                return false;
            }

            return true;
        }

        private static int RetornarQuantidadeDiasServicoDiarias(ViewFaturamentoServicoAssociadoVeiculoModel FaturamentoServicoAssociadoVeiculo, DateTime DataHoraGuarda, DateTime DataHoraAtualPorDeposito)
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

        private static FaturamentoComposicaoModel AplicarDesconto(FaturamentoComposicaoModel FaturamentoComposicao, List<CalculoFaturamentoDescontoModel> FaturamentoDescontos)
        {
            if (FaturamentoDescontos != null)
            {
                CalculoFaturamentoDescontoModel FaturamentoDesconto = FaturamentoDescontos
                    .Find(w => w.FaturamentoServicoTipoVeiculoId == FaturamentoComposicao.FaturamentoServicoTipoVeiculoId);

                if (FaturamentoDesconto != null)
                {
                    FaturamentoComposicao.UsuarioDescontoId = FaturamentoDesconto.UsuarioDescontoId;

                    FaturamentoComposicao.TipoDesconto = FaturamentoDesconto.TipoDesconto;

                    FaturamentoComposicao.ValorDesconto = FaturamentoDesconto.ValorDesconto;

                    FaturamentoComposicao.QuantidadeDesconto = FaturamentoDesconto.QuantidadeDesconto;

                    FaturamentoComposicao.ObservacaoDesconto = FaturamentoDesconto.ObservacaoDesconto;

                    if (FaturamentoComposicao.TipoDesconto == "V") // Se o Tipo do Desconto for VALOR FIXO
                    {
                        FaturamentoComposicao.ValorFaturado = Math.Round(FaturamentoComposicao.ValorComposicao - FaturamentoDesconto.ValorDesconto, 2); /*Cleiton Silva - 23/11/18*/
                    }
                    else
                    {
                        FaturamentoComposicao.ValorFaturado = Math.Round(NumberHelper.GetPercentage(FaturamentoComposicao.ValorComposicao, FaturamentoDesconto.QuantidadeDesconto), 2); /*Cleiton Silva - 23/11/18*/
                    }
                }
            }

            return FaturamentoComposicao;
        }

        private static async Task<List<CalculoTributacaoModel>> CalcularTributacao(AppDbContext _context, CalculoFaturamentoParametroModel ParametrosCalculoFaturamento, decimal valorCalculado, string notaFiscalCnpj, string notaFiscalMunicipio, string notaFiscalUf)
        {
            if (valorCalculado <= 0 && string.IsNullOrWhiteSpace(notaFiscalCnpj) || string.IsNullOrWhiteSpace(notaFiscalMunicipio) || string.IsNullOrWhiteSpace(notaFiscalUf))
            {
                return null;
            }

            List<ViewFaturamentoServicoAssociadoVeiculoModel> ServicosTributados = await _context.ViewFaturamentoServicosAssociadosVeiculos
                .Where(w => w.ClienteId == ParametrosCalculoFaturamento.Grv.ClienteId &&
                            w.DepositoId == ParametrosCalculoFaturamento.Grv.DepositoId &&
                            w.TipoVeiculoId == ParametrosCalculoFaturamento.Grv.TipoVeiculoId &&
                            w.FaturamentoProdutoId == ParametrosCalculoFaturamento.Grv.FaturamentoProdutoId &&
                            w.FlagTributacao == "S" &&
                            w.DataVigenciaFinal == null
                )
                .AsNoTracking()
                .ToListAsync();

            if (ServicosTributados == null)
            {
                return null;
            }

            ViewEnderecoCompletoModel Endereco = await _context.ViewEnderecosCompletos
                .Where(w => w.CepId == ParametrosCalculoFaturamento.Grv.Deposito.CepId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Endereco == null)
            {
                return null;
            }

            if (StringHelper.Normalize(notaFiscalMunicipio) != StringHelper.Normalize(Endereco.Municipio) || notaFiscalUf != Endereco.UF)
            {
                return null;
            }

            #region Selecionar Regras do Faturamento
            FaturamentoRegraModel FaturamentoRegra = await _context.FaturamentoRegras
                .Include(i => i.FaturamentoRegraTipo)
                .Where(w => w.ClienteId == ParametrosCalculoFaturamento.Grv.ClienteId &&
                            w.DepositoId == ParametrosCalculoFaturamento.Grv.DepositoId &&
                            w.FaturamentoRegraTipo.Codigo == "DESCONTOISS")
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (FaturamentoRegra != null && Convert.ToDecimal(FaturamentoRegra.Valor) > valorCalculado)
            {
                return null;
            }
            #endregion Selecionar Regras do Faturamento

            List<CalculoTributacaoModel> Tributacoes = new();

            CalculoTributacaoModel Tributacao;

            foreach (ViewFaturamentoServicoAssociadoVeiculoModel item in ServicosTributados)
            {
                if (item.FaturamentoRegraTipoCodigo.Equals("DESCONTOISS"))
                {
                    continue;
                }

                Tributacao = new CalculoTributacaoModel
                {
                    FaturamentoServicoTipoVeiculoId = item.FaturamentoServicoTipoVeiculoId,

                    FaturamentoServicoAssociadoId = item.FaturamentoServicoAssociadoId,

                    TipoComposicao = item.TipoCobranca,

                    TipoLancamento = "D", // Débito

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

        private static string GerarNumeroIdentificacao(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento, int Sequencia)
        {
            return StringHelper.AddStringLeft(ParametrosCalculoFaturamento.Grv.NumeroFormularioGrv, '0', 9) +
                   StringHelper.AddStringLeft(ParametrosCalculoFaturamento.Grv.DepositoId.ToString(), '0', 4) +
                   StringHelper.AddStringLeft(Sequencia.ToString(), '0', 3);
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
            if (CalculoDiarias.FlagCobrarDiariasDiasCorridos == "S")
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
    }
}