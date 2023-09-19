using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.Faturamento.View;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Localizacao;
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

        public async Task<FaturamentoModel> Calcular(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            // TODO: Aplicar desconto
            //if (ParametrosCalculoFaturamento.StatusOperacaoId == "L")
            //{

            //}

            // TODO: Verificar necessidade de Alteração da Quantidade dos Serviços selecionados
            // ParametrosCalculoFaturamentoModel.FaturamentoQuantidadeAlteradaList = RetornarAlteracaoQuantidadeServico();

            CalculoFaturamentoModel CalculoFaturamento = new()
            {
                AtendimentoModel = ParametrosCalculoFaturamento.Atendimento
            };

            if (ParametrosCalculoFaturamento.DataLiberacao == DateTime.MinValue)
            {
                ParametrosCalculoFaturamento.DataLiberacao = ParametrosCalculoFaturamento.DataHoraAtualPorDeposito;
            }

            // TODO: Implementar Tributação

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
                throw new Exception("Não foi encontrado Serviço associado ao Cliente + Depósito associado ao Tipo de Veículo");
            }
            #endregion Selecionar todos os Serviços associados ao CLIDEP, incluindo os com a Vigência finalizada


            #region Verificação de Faturamentos anteriores
            // Faturamento.Status:
            // N = Novo Faturamento/Não Pago;
            // A = Faturamento Adicional e Não Pago (Pra quando a Fatura foi paga em atraso);
            // C = Cancelado, pra quando foi gerada uma Fatura para uma Fatura Vencida e que não foi paga;
            // P = Fatura Paga.

            FaturamentoModel Faturamento = new();

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
                Faturamento.Sequencia += UltimoFaturamento.Sequencia;

                #region Cancelar o Faturamento atual
                UltimoFaturamento.UsuarioAlteracaoId = ParametrosCalculoFaturamento.Atendimento.UsuarioCadastroId;

                UltimoFaturamento.Status = "C";

                UltimoFaturamento.DataAlteracao = ParametrosCalculoFaturamento.DataHoraAtualPorDeposito;

                if (ParametrosCalculoFaturamento.FlagCadastrarFaturamento)
                {
                    _context.Faturamentos.Update(UltimoFaturamento);
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
            int diarias = CalculoDiarias.Diarias;

            CalculoFaturamentoQuantidadeAlteradaModel FaturamentoQuantidadeAlterada = new();

            List<FaturamentoComposicaoModel> FaturamentoComposicoes = new();

            FaturamentoComposicaoModel FaturamentoComposicao = new();

            ViewFaturamentoServicoAssociadoVeiculoModel FaturamentoServicoAssociadoVeiculo = new();

            List<ViewFaturamentoServicoAssociadoVeiculoModel> FaturamentoServicosAssociadosVeiculosAmbos = new();

            int dias = 0;
            int diariasRestantes = 0;
            int horas = 0;

            try
            {
                foreach (ViewFaturamentoServicoGrvModel FaturamentoServicoGrv in FaturamentoServicosGrvs)
                {
                    FaturamentoQuantidadeAlterada = new();

                    if (!VerificarServicoDeveSerCalculado(FaturamentoServicoGrv, UltimoFaturamento, ParametrosCalculoFaturamento))
                    {
                        continue;
                    }

                    FaturamentoComposicao = new()
                    {
                        ServicoDescricao = FaturamentoServicoGrv.ServicoDescricao,

                        FaturamentoServicoTipoVeiculoId = FaturamentoServicoGrv.FaturamentoServicoTipoVeiculoId,

                        TipoComposicao = FaturamentoServicoGrv.TipoCobranca,

                        ValorTipoComposicao = FaturamentoServicoGrv.PrecoPadrao,

                        DataVigenciaInicial = FaturamentoServicoGrv.DataVigenciaInicial,

                        DataVigenciaFinal = FaturamentoServicoGrv.DataVigenciaFinal.Value
                    };

                    // DIÁRIAS
                    if (FaturamentoServicoGrv.TipoCobranca == "D" && (new[] { "AM", "VI" }.Contains(FaturamentoServicoGrv.FormaCobranca)))
                    {
                        // Forma de Cobrança:
                        // AM: Ambos;
                        // VA: Vigência Atual(Valor Padrão);
                        // VI: Vigência Inicial.

                        diariasRestantes = CalculoDiarias.Diarias;

                        if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto && ParametrosCalculoFaturamento.FaturamentoQuantidadesAlteradas != null)
                        {
                            FaturamentoQuantidadeAlterada = ParametrosCalculoFaturamento.FaturamentoQuantidadesAlteradas
                                .Find(w => w.FaturamentoServicoTipoVeiculoId == FaturamentoServicoGrv.FaturamentoServicoTipoVeiculoId);
                        }

                        if (FaturamentoServicoGrv.FormaCobranca == "AM")
                        {
                            // Primeiro filtro, cobrar por todas as vigências encontradas
                            FaturamentoServicosAssociadosVeiculosAmbos = FaturamentoServicosAssociadosVeiculos
                                .Where(w => w.FaturamentoServicoTipoId == FaturamentoServicoGrv.FaturamentoServicoTipoId)
                                .Where(w => (ParametrosCalculoFaturamento.Grv.DataHoraGuarda.Date >= w.DataVigenciaInicial &&
                                             ParametrosCalculoFaturamento.Grv.DataHoraGuarda.Date <= w.DataVigenciaFinal) || ParametrosCalculoFaturamento.Grv.DataHoraGuarda <= w.DataVigenciaFinal || (w.DataVigenciaFinal == null || w.DataVigenciaFinal == DateTime.MinValue))
                                .ToList();

                            foreach (ViewFaturamentoServicoAssociadoVeiculoModel FaturamentoServicoAssociadoVeiculoAmbos in FaturamentoServicosAssociadosVeiculosAmbos)
                            {
                                FaturamentoComposicao = new FaturamentoComposicaoModel
                                {
                                    ServicoDescricao = FaturamentoServicoAssociadoVeiculoAmbos.ServicoDescricao,

                                    DataVigenciaInicial = FaturamentoServicoAssociadoVeiculoAmbos.DataVigenciaInicial,

                                    DataVigenciaFinal = FaturamentoServicoAssociadoVeiculoAmbos.DataVigenciaFinal.Value
                                };

                                // Retorna a quantidade de Dias entre as datas
                                dias = RetornarQuantidadeDiasServicoDiarias(FaturamentoServicoAssociadoVeiculoAmbos, ParametrosCalculoFaturamento.Grv.DataHoraGuarda, ParametrosCalculoFaturamento.DataHoraAtualPorDeposito);

                                if (dias >= diariasRestantes)
                                {
                                    dias = diariasRestantes;

                                    diariasRestantes = 0;
                                }
                                else
                                {
                                    diariasRestantes -= dias;
                                }

                                FaturamentoComposicao.FaturamentoServicoTipoVeiculoId = FaturamentoServicoAssociadoVeiculoAmbos.FaturamentoServicoTipoVeiculoId;

                                FaturamentoComposicao.TipoComposicao = FaturamentoServicoAssociadoVeiculoAmbos.TipoCobranca;

                                FaturamentoComposicao.ValorTipoComposicao = FaturamentoServicoAssociadoVeiculoAmbos.PrecoPadrao;

                                if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto && (FaturamentoQuantidadeAlterada != null))
                                {
                                    FaturamentoComposicao = AplicarQuantidadeAlterada(FaturamentoComposicao, ParametrosCalculoFaturamento.FaturamentoQuantidadesAlteradas);

                                    dias += Convert.ToInt32(FaturamentoComposicao.QuantidadeAlterada);
                                }

                                FaturamentoComposicao.QuantidadeComposicao = dias;

                                FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoAssociadoVeiculoAmbos.PrecoPadrao * dias, 2);

                                FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;

                                // APLICAR OS DESCONTOS
                                if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto && ParametrosCalculoFaturamento.FaturamentoDescontos != null)
                                {
                                    FaturamentoComposicao = AplicarDesconto(FaturamentoComposicao, ParametrosCalculoFaturamento.FaturamentoDescontos);
                                }

                                FaturamentoComposicao.TipoLancamento = "C"; // Crédito

                                Faturamento.ValorFaturado += FaturamentoComposicao.ValorFaturado;

                                FaturamentoComposicoes.Add(FaturamentoComposicao);

                                if (diariasRestantes == 0)
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
                                .Where(w => w.DataVigenciaFinal >= ParametrosCalculoFaturamento.Grv.DataHoraGuarda.Date || (w.DataVigenciaFinal == null || w.DataVigenciaFinal == DateTime.MinValue))
                                .OrderBy(o => o.DataVigenciaInicial)
                                .FirstOrDefault();

                            if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto && ParametrosCalculoFaturamento.FaturamentoQuantidadesAlteradas != null)
                            {
                                FaturamentoQuantidadeAlterada = ParametrosCalculoFaturamento.FaturamentoQuantidadesAlteradas
                                    .Find(w => w.FaturamentoServicoTipoVeiculoId == FaturamentoServicoGrv.FaturamentoServicoTipoVeiculoId);
                            }

                            dias = CalculoDiarias.Diarias;

                            if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto && ParametrosCalculoFaturamento.FaturamentoQuantidadesAlteradas != null)
                            {
                                FaturamentoComposicao = AplicarQuantidadeAlterada(FaturamentoComposicao, ParametrosCalculoFaturamento.FaturamentoQuantidadesAlteradas);

                                dias += Convert.ToInt32(FaturamentoComposicao.QuantidadeAlterada);
                            }

                            FaturamentoComposicao.TipoComposicao = FaturamentoServicoAssociadoVeiculo.TipoCobranca;

                            FaturamentoComposicao.ServicoDescricao = FaturamentoServicoAssociadoVeiculo.ServicoDescricao;

                            FaturamentoComposicao.DataVigenciaInicial = FaturamentoServicoAssociadoVeiculo.DataVigenciaInicial;

                            FaturamentoComposicao.DataVigenciaFinal = FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value;

                            FaturamentoComposicao.FaturamentoServicoTipoVeiculoId = FaturamentoServicoAssociadoVeiculo.FaturamentoServicoTipoVeiculoId;

                            FaturamentoComposicao.ValorTipoComposicao = FaturamentoServicoAssociadoVeiculo.PrecoPadrao;

                            FaturamentoComposicao.QuantidadeComposicao = dias;

                            FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoComposicao.ValorTipoComposicao * dias, 2);

                            FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                        }
                    }
                    else if (FaturamentoServicoGrv.TipoCobranca == "D")
                    {
                        if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto && ParametrosCalculoFaturamento.FaturamentoQuantidadesAlteradas != null)
                        {
                            FaturamentoQuantidadeAlterada = ParametrosCalculoFaturamento.FaturamentoQuantidadesAlteradas
                                .Find(w => w.FaturamentoServicoTipoVeiculoId == FaturamentoServicoGrv.FaturamentoServicoTipoVeiculoId);
                        }

                        dias = CalculoDiarias.Diarias;

                        if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto && (FaturamentoQuantidadeAlterada != null))
                        {
                            FaturamentoComposicao = AplicarQuantidadeAlterada(FaturamentoComposicao, ParametrosCalculoFaturamento.FaturamentoQuantidadesAlteradas);

                            dias += Convert.ToInt32(FaturamentoComposicao.QuantidadeAlterada);
                        }

                        FaturamentoComposicao.QuantidadeComposicao = dias;

                        FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoGrv.PrecoPadrao * dias, 2);

                        FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
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

                            if (FaturamentoServicoGrv.FormaCobranca == "VI")
                            {
                                FaturamentoServicoAssociadoVeiculo = FaturamentoServicosAssociadosVeiculos
                                    .Where(w => w.FaturamentoServicoTipoId == FaturamentoServicoGrv.FaturamentoServicoTipoId)
                                    .Where(w => w.DataVigenciaFinal >= ParametrosCalculoFaturamento.Grv.DataHoraGuarda.Date || (w.DataVigenciaFinal == null || w.DataVigenciaFinal == DateTime.MinValue))
                                    .OrderBy(o => o.DataVigenciaInicial)
                                    .First();

                                FaturamentoServicoGrv.PrecoPadrao = FaturamentoServicoAssociadoVeiculo.PrecoPadrao;

                                FaturamentoComposicao.ServicoDescricao = FaturamentoServicoAssociadoVeiculo.ServicoDescricao;

                                FaturamentoComposicao.DataVigenciaInicial = FaturamentoServicoAssociadoVeiculo.DataVigenciaInicial;

                                FaturamentoComposicao.DataVigenciaFinal = FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value;

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
                        if ((horas = (int)(ParametrosCalculoFaturamento.DataHoraAtualPorDeposito - ParametrosCalculoFaturamento.Grv.DataHoraGuarda).TotalHours) == 0)
                        {
                            horas++;
                        }

                        FaturamentoComposicao.ValorComposicao = Math.Round(FaturamentoServicoGrv.PrecoPadrao * horas, 2);

                        FaturamentoComposicao.ValorFaturado = FaturamentoComposicao.ValorComposicao;
                    }

                    // APLICAR OS DESCONTOS
                    if (ParametrosCalculoFaturamento.FlagFaturamentoCompleto && ParametrosCalculoFaturamento.FaturamentoDescontos != null)
                    {
                        FaturamentoComposicao = AplicarDesconto(FaturamentoComposicao, ParametrosCalculoFaturamento.FaturamentoDescontos);
                    }

                    FaturamentoComposicao.TipoLancamento = "C"; // Crédito

                    Faturamento.ValorFaturado += FaturamentoComposicao.ValorFaturado;

                    FaturamentoComposicoes.Add(FaturamentoComposicao);
                }
            }
            catch (Exception)
            {
                throw;
            }
            #endregion Composição do Faturamento

            if (Faturamento.ValorFaturado > 0)
            {
                List<CalculoTributacaoModel> Tributacoes = await CalcularTributacao(_context,
                    ParametrosCalculoFaturamento,
                    Faturamento.ValorFaturado,
                    ParametrosCalculoFaturamento.Grv.Atendimento.NotaFiscalDocumento,
                    ParametrosCalculoFaturamento.Grv.Atendimento.NotaFiscalMunicipio,
                    ParametrosCalculoFaturamento.Grv.Atendimento.NotaFiscalUf);

                foreach (CalculoTributacaoModel Tributacao in Tributacoes)
                {
                    FaturamentoComposicoes.Add(Tributacao);

                    CalculoFaturamento.Tributacoes.Add(Tributacao);

                    Faturamento.ValorFaturado += Tributacao.ValorFaturado;
                }
            }

            #region CADASTRO DO FATURAMENTO
            Faturamento.AtendimentoId = ParametrosCalculoFaturamento.Atendimento.AtendimentoId;

            Faturamento.UsuarioCadastroId = ParametrosCalculoFaturamento.UsuarioCadastroId;

            Faturamento.TipoMeioCobrancaId = ParametrosCalculoFaturamento.TipoMeioCobrancaId;

            // Faturamento.numero_identificacao = GerarNumeroIdentificacao(Grv.numero_formulario_grv, Grv.id_deposito, Faturamento.sequencia);

            Faturamento.HoraDiaria = CalculoDiarias.HoraDiaria;

            Faturamento.MaximoDiariasParaCobranca = CalculoDiarias.MaximoDiariasParaCobranca;

            Faturamento.MaximoDiasVencimento = CalculoDiarias.MaximoDiasVencimento;

            Faturamento.FlagUsarHoraDiaria = CalculoDiarias.FlagUsarHoraDiaria;

            Faturamento.FlagLimitacaoJudicial = "N";

            Faturamento.FlagClienteRealizaFaturamentoArrecadacao = CalculoDiarias.FlagClienteRealizaFaturamentoArrecadacao;

            Faturamento.FlagCobrarDiariasDiasCorridos = CalculoDiarias.FlagCobrarDiariasDiasCorridos;

            if (ParametrosCalculoFaturamento.FlagPermissaoDataRetroativaFaturamento)
            {
                Faturamento.DataRetroativa = ParametrosCalculoFaturamento.DataLiberacao.Date;

                Faturamento.FlagPermissaoDataRetroativaFaturamento = "S";
            }
            else
            {
                Faturamento.FlagPermissaoDataRetroativaFaturamento = "N";
            }

            Faturamento.DataCalculo = CalculoDiarias.DataHoraInicialParaCalculo;

            Faturamento.DataVencimento = CalcularDataVencimento(ParametrosCalculoFaturamento, CalculoDiarias, ParametrosCalculoFaturamento.FlagPermissaoDataRetroativaFaturamento ? ParametrosCalculoFaturamento.DataHoraAtualPorDeposito : ParametrosCalculoFaturamento.DataLiberacao);

            Faturamento.DataCadastro = ParametrosCalculoFaturamento.DataHoraAtualPorDeposito;

            try
            {
                if (ParametrosCalculoFaturamento.FlagCadastrarFaturamento)
                {
                    await _context.Faturamentos.AddAsync(Faturamento);
                }
            }
            catch (Exception ex)
            {
                if (true)
                {

                }
            }

            CalculoFaturamento.FaturamentoModel = Faturamento;

            #endregion CADASTRO DO FATURAMENTO

            return Faturamento;
        }

        private static bool VerificarServicoDeveSerCalculado(ViewFaturamentoServicoGrvModel FaturamentoServicoGrv, FaturamentoModel UltimoFaturamento, CalculoFaturamentoParametroModel ParametrosCalculoFaturamento)
        {
            if (ParametrosCalculoFaturamento.Grv.FaturamentoProdutoId != "DEP" &&
                ParametrosCalculoFaturamento.Grv.FaturamentoProdutoId != "DRF" &&
                FaturamentoServicoGrv.FaturamentoServicoGrvId == 0)
            {
                return false;
            }
            else if (FaturamentoServicoGrv.FlagPermiteAlteracaoValor.Equals("S") && (FaturamentoServicoGrv.Valor <= 0))
            {
                return false;
            }
            else if (FaturamentoServicoGrv.FlagRealizarCobranca == "N")
            {
                // Se o Usuário escolheu por não cobrar o Serviço
                return false;
            }
            else if (!ParametrosCalculoFaturamento.FlagFaturamentoCompleto && FaturamentoServicoGrv.FlagCobrarSomentePrimeiraFatura == "S")
            {
                // Se não for o primeiro Faturamento e se o Serviço for para ser cobrado apenas no primeiro Faturamento
                return false;
            }
            else if (FaturamentoServicoGrv.FlagCobrarSomentePrimeiraFatura == "S" && UltimoFaturamento != null && UltimoFaturamento.Status == "P")
            {
                // Se a última Fatura for paga e o Serviço só pode ser cobrado na primeira Fatura
                return false;
            }
            else if (ParametrosCalculoFaturamento.Grv.FlagComboio == "S" && FaturamentoServicoGrv.FlagRebocada == "S")
            {
                // Não cobrar rebocada caso o veículo entrou no Depósito por Comboio
                return false;
            }
            else if (FaturamentoServicoGrv.FaturamentoRegraTipoCodigo.Equals("COBRATARIFABANCARIA") && !ParametrosCalculoFaturamento.TipoMeioCobrancaId.Equals(1))
            {
                // Se o serviço tiver a regra "Cobrança de Tarifa Bancária" e se o Tipo do Meio de Cobrança for Boleto
                return false;
            }

            return true;
        }

        private static int RetornarQuantidadeDiasServicoDiarias(ViewFaturamentoServicoAssociadoVeiculoModel FaturamentoServicoAssociadoVeiculo, DateTime DataHoraGuarda, DateTime DataHoraAtualPorDeposito)
        {
            DateTime data_ini = DataHoraGuarda;

            // 2018-08-09 - Cristiney solicitou para que seja utilizada a hora atual
            // var data_fin = new DateTime(data_atual.Year, data_atual.Month, data_atual.Day, 23, 59, 59);
            DateTime data_fin = new DateTime(DataHoraAtualPorDeposito.Year, DataHoraAtualPorDeposito.Month, DataHoraAtualPorDeposito.Day, DataHoraAtualPorDeposito.Hour, DataHoraAtualPorDeposito.Minute, DataHoraAtualPorDeposito.Second);

            if (FaturamentoServicoAssociadoVeiculo.DataVigenciaInicial > DataHoraGuarda)
            {
                data_ini = FaturamentoServicoAssociadoVeiculo.DataVigenciaInicial;
            }

            // Se data final da vigência for menor que a data atual
            if ((FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal > DateTime.MinValue) &&
                (FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value.Date < DataHoraAtualPorDeposito.Date))
            {
                data_fin = new DateTime(FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value.Year, FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value.Month, FaturamentoServicoAssociadoVeiculo.DataVigenciaFinal.Value.Day, 23, 59, 59);
            }

            return 1 + DateTimeHelper.GetDaysBetweenTwoDates(data_ini, data_fin);
        }

        private static FaturamentoComposicaoModel AplicarQuantidadeAlterada(FaturamentoComposicaoModel FaturamentoComposicao, List<CalculoFaturamentoQuantidadeAlteradaModel> CalculoFaturamentoQuantidadesAlteradas)
        {
            if (CalculoFaturamentoQuantidadesAlteradas != null)
            {
                CalculoFaturamentoQuantidadeAlteradaModel FaturamentoQuantidadeAlteradaModelModel = CalculoFaturamentoQuantidadesAlteradas
                    .Find(w => w.FaturamentoServicoTipoVeiculoId == FaturamentoComposicao.FaturamentoServicoTipoVeiculoId);

                if (FaturamentoQuantidadeAlteradaModelModel != null)
                {
                    FaturamentoComposicao.UsuarioAlteracaoQuantidadeId = FaturamentoQuantidadeAlteradaModelModel.UsuarioAlteracaoQuantidadeId;

                    FaturamentoComposicao.QuantidadeAlterada = FaturamentoQuantidadeAlteradaModelModel.QuantidadeAlterada;

                    FaturamentoComposicao.ObservacaoQuantidadeAlterada = FaturamentoQuantidadeAlteradaModelModel.ObservacaoQuantidadeAlterada;
                }
            }

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
            if (valorCalculado <= 0 && string.IsNullOrWhiteSpace(notaFiscalCnpj) || (notaFiscalCnpj.Trim().Length != 14) || string.IsNullOrWhiteSpace(notaFiscalMunicipio) || string.IsNullOrWhiteSpace(notaFiscalUf))
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

                    ValorTipoComposicao = item.PrecoPadrao,

                    ServicoDescricao = "TRIBUTAÇÃO"
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

        private static DateTime CalcularDataVencimento(CalculoFaturamentoParametroModel ParametrosCalculoFaturamento, CalculoDiariasModel CalculoDiarias, DateTime? dataFinal = null)
        {
            if (!dataFinal.HasValue)
            {
                dataFinal = ParametrosCalculoFaturamento.DataHoraAtualPorDeposito;
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