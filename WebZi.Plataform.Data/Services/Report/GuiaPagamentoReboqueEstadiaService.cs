using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Text;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Secutity;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Banco.PIX;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.DetranHub;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.DTO.Banco.PIX;
using WebZi.Plataform.Domain.DTO.Faturamento.Simulacao;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.Report;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Banco.PIX.Dinamico.Persistencia;
using WebZi.Plataform.Domain.Models.Banco.PIX.Estatico;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Options;
using WebZi.Plataform.Domain.Services.GRV;

namespace WebZi.Plataform.Data.Services.Report
{
    public class GuiaPagamentoReboqueEstadiaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IServiceProvider _provider;
        private readonly IOptions<DetranHubOptions> _detranHubOptions;

        public GuiaPagamentoReboqueEstadiaService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        public GuiaPagamentoReboqueEstadiaService(
            AppDbContext context,
            IMapper mapper,
            IHttpClientFactory httpClientFactory,
            IServiceProvider provider,
            IOptions<DetranHubOptions> detranHubOptions) : this(context, mapper, httpClientFactory)
        {
            _provider = provider;
            _detranHubOptions = detranHubOptions;
        }

        private GuiaPagamentoReboqueEstadiaDTO FillAtendimento(GuiaPagamentoReboqueEstadiaDTO GuiaPagamentoEstadiaReboque, AtendimentoModel Atendimento)
        {
            if (Atendimento == null)
            {
                return GuiaPagamentoEstadiaReboque;
            }

            GuiaPagamentoEstadiaReboque.QualificacaoResponsavel = Atendimento.QualificacaoResponsavel?.Descricao;

            GuiaPagamentoEstadiaReboque.AtendimentoResponsavelNome = Atendimento.ResponsavelNome;

            GuiaPagamentoEstadiaReboque.AtendimentoResponsavelDocumento = !string.IsNullOrWhiteSpace(Atendimento.ResponsavelDocumento)
                ? DocumentHelper.FormatCPF(Atendimento.ResponsavelDocumento)
                : null;

            GuiaPagamentoEstadiaReboque.AtendimentoResponsavelEndereco = Atendimento.ResponsavelEndereco;

            GuiaPagamentoEstadiaReboque.AtendimentoResponsavelNumero = Atendimento.ResponsavelNumero;

            GuiaPagamentoEstadiaReboque.AtendimentoResponsavelComplemento = Atendimento.ResponsavelComplemento;

            GuiaPagamentoEstadiaReboque.AtendimentoResponsavelBairro = Atendimento.ResponsavelBairro;

            GuiaPagamentoEstadiaReboque.AtendimentoResponsavelMunicipio = Atendimento.ResponsavelMunicipio;

            GuiaPagamentoEstadiaReboque.AtendimentoResponsavelUF = Atendimento.ResponsavelUF;

            GuiaPagamentoEstadiaReboque.AtendimentoFormaLiberacao = Atendimento.FormaLiberacao;

            GuiaPagamentoEstadiaReboque.AtendimentoFormaLiberacaoNome = Atendimento.FormaLiberacaoNome;

            GuiaPagamentoEstadiaReboque.AtendimentoFormaLiberacaoCNH = Atendimento.FormaLiberacaoCNH;

            GuiaPagamentoEstadiaReboque.AtendimentoFormaLiberacaoCPF = Atendimento.FormaLiberacaoCPF;

            GuiaPagamentoEstadiaReboque.AtendimentoFormaLiberacaoPlaca = Atendimento.FormaLiberacaoPlaca;

            if (!string.IsNullOrWhiteSpace(Atendimento.ResponsavelDocumento))
            {
                if (Atendimento.ResponsavelDocumento.Length == 11)
                {
                    GuiaPagamentoEstadiaReboque.Identificador = "Identificador (CPF): " + DocumentHelper.FormatCPF(Atendimento.ResponsavelDocumento);
                }
                else
                {
                    GuiaPagamentoEstadiaReboque.Identificador = "Identificador (CNPJ): " + DocumentHelper.FormatCNPJ(Atendimento.ResponsavelDocumento);
                }
            }

            return GuiaPagamentoEstadiaReboque;
        }

        private GuiaPagamentoReboqueEstadiaDTO FillCliente(GuiaPagamentoReboqueEstadiaDTO GuiaPagamentoEstadiaReboque, GrvModel Grv)
        {
            if (Grv?.Cliente == null)
            {
                return GuiaPagamentoEstadiaReboque;
            }

            GuiaPagamentoEstadiaReboque.ClienteNome = Grv.Cliente.Nome;

            GuiaPagamentoEstadiaReboque.ClienteCNPJ = !string.IsNullOrWhiteSpace(Grv.Cliente.CNPJ)
                ? DocumentHelper.FormatCNPJ(Grv.Cliente.CNPJ)
                : null;

            if (Grv.Cliente.Endereco != null)
            {
                GuiaPagamentoEstadiaReboque.ClienteEndereco = new EnderecoService(_context, _mapper)
                    .FormatarEndereco(Grv.Cliente.Endereco, Grv.Cliente.NumeroEndereco, Grv.Cliente.ComplementoEndereco);
            }

            if (Grv.Cliente.AgenciaBancaria != null)
            {
                string bancoNome = Grv.Cliente.AgenciaBancaria.Banco?.Nome ?? string.Empty;
                GuiaPagamentoEstadiaReboque.ClienteDadosBancarios = "Banco: " + bancoNome + ". Ag: " + Grv.Cliente.AgenciaBancaria.CodigoAgencia + ". CC: " + Grv.Cliente.AgenciaBancaria.ContaCorrente + "-" + Grv.Cliente.AgenciaBancaria.DigitoVerificador;
                GuiaPagamentoEstadiaReboque.ClienteDadosBancarios = GuiaPagamentoEstadiaReboque.ClienteDadosBancarios.Replace("..", ".");
            }

            if (Grv.Cliente.FlagClienteRealizaFaturamentoArrecadacao == "S")
            {
                string clienteCnpj = !string.IsNullOrWhiteSpace(Grv.Cliente.CNPJ) ? DocumentHelper.FormatCNPJ(Grv.Cliente.CNPJ) : string.Empty;
                GuiaPagamentoEstadiaReboque.CreditoDe = "Crédito de: " + Grv.Cliente.Nome + " (CNPJ: " + clienteCnpj + ")";
            }
            else if (Grv.Cliente.Empresa != null)
            {
                string empresaCnpj = !string.IsNullOrWhiteSpace(Grv.Cliente.Empresa.CNPJ) ? DocumentHelper.FormatCNPJ(Grv.Cliente.Empresa.CNPJ) : string.Empty;
                GuiaPagamentoEstadiaReboque.CreditoDe = "Crédito de: " + Grv.Cliente.Empresa.Nome + " (CNPJ: " + empresaCnpj + ")";
            }

            return GuiaPagamentoEstadiaReboque;
        }

        private async Task<GuiaPagamentoReboqueEstadiaDTO> FillComposicaoFaturamentoAsync(
            GuiaPagamentoReboqueEstadiaDTO GuiaPagamentoEstadiaReboque,
            FaturamentoModel Faturamento,
            int GrvId)
        {
            decimal ValorDemaisServicos = 0;
            decimal ValorTotalDesconto = 0;

            if (Faturamento?.ListagemFaturamentoComposicao != null)
            {
                foreach (FaturamentoComposicaoModel Composicao in Faturamento.ListagemFaturamentoComposicao)
                {
                    if (Composicao.ValorDesconto.HasValue && Composicao.ValorDesconto.Value > 0)
                    {
                        ValorTotalDesconto += Composicao.ValorDesconto.Value;
                    }

                    int? ordemImpressao = Composicao.FaturamentoServicoTipoVeiculo?.FaturamentoServicoAssociado?.FaturamentoServicoTipo?.OrdemImpressao;

                    if (ordemImpressao == 1)
                    {
                        GuiaPagamentoEstadiaReboque.QuantidadeEstadias = (int)Composicao.QuantidadeComposicao;

                        GuiaPagamentoEstadiaReboque.PrecoEstadias = NumberHelper.FormatMoney(Composicao.ValorTipoComposicao);

                        GuiaPagamentoEstadiaReboque.ValorFaturadoEstadias = NumberHelper.FormatMoney(Composicao.ValorFaturado);

                        if (Composicao.ValorDesconto.HasValue && Composicao.ValorDesconto.Value > 0)
                        {
                            GuiaPagamentoEstadiaReboque.TipoDescontoEstadias = Composicao.TipoDesconto ?? string.Empty;
                            GuiaPagamentoEstadiaReboque.ValorDescontoEstadias = NumberHelper.FormatMoney(Composicao.ValorDesconto.Value);
                        }
                    }
                    else if (ordemImpressao == 2)
                    {
                        GuiaPagamentoEstadiaReboque.PrecoReboque = NumberHelper.FormatMoney(Composicao.ValorTipoComposicao);

                        GuiaPagamentoEstadiaReboque.ValorFaturadoReboque = NumberHelper.FormatMoney(Composicao.ValorFaturado);

                        if (Composicao.ValorDesconto.HasValue && Composicao.ValorDesconto.Value > 0)
                        {
                            GuiaPagamentoEstadiaReboque.TipoDescontoReboque = Composicao.TipoDesconto ?? string.Empty;
                            GuiaPagamentoEstadiaReboque.ValorDescontoReboque = NumberHelper.FormatMoney(Composicao.ValorDesconto.Value);
                        }
                    }
                    else if (ordemImpressao == 3)
                    {
                        GuiaPagamentoEstadiaReboque.QuantidadeQuilometragem = ((int)Composicao.QuantidadeComposicao).ToString();

                        GuiaPagamentoEstadiaReboque.PrecoQuilometragem = NumberHelper.FormatMoney(Composicao.ValorTipoComposicao);

                        GuiaPagamentoEstadiaReboque.ValorFaturadoQuilometragem = NumberHelper.FormatMoney(Composicao.ValorFaturado);

                        if (Composicao.ValorDesconto.HasValue && Composicao.ValorDesconto.Value > 0)
                        {
                            GuiaPagamentoEstadiaReboque.TipoDescontoQuilometragem = Composicao.TipoDesconto ?? string.Empty;
                            GuiaPagamentoEstadiaReboque.ValorDescontoQuilometragem = NumberHelper.FormatMoney(Composicao.ValorDesconto.Value);
                        }
                    }
                    else
                    {
                        ValorDemaisServicos += Composicao.ValorFaturado;
                    }

                    if (ValorDemaisServicos > 0)
                    {
                        GuiaPagamentoEstadiaReboque.ValorDemaisServicos = NumberHelper.FormatMoney(ValorDemaisServicos);
                    }
                }
            }

            if (ValorTotalDesconto > 0)
            {
                GuiaPagamentoEstadiaReboque.ValorTotalDesconto = NumberHelper.FormatMoney(ValorTotalDesconto);
            }

            if (Faturamento?.ListagemFaturamentoComposicao != null)
            {
                GuiaPagamentoEstadiaReboque.ListagemServico =
                    _mapper.Map<List<SimulacaoFaturamentoComposicaoDTO>>(Faturamento.ListagemFaturamentoComposicao);
            }

            List<TabelaGenericaModel> ListagemTipoCobranca = await new TabelaGenericaService(_context)
                .ListAsync("FAT_TIPO_COBRANCA");

            if (GuiaPagamentoEstadiaReboque.ListagemServico != null)
            {
                foreach (var Servico in GuiaPagamentoEstadiaReboque.ListagemServico)
                {
                    var faturamentoServicoTipoVeiculo = await _context.FaturamentoServicoTipoVeiculo
                        .Include(x => x.FaturamentoServicoAssociado)
                            .ThenInclude(faturamentoServicoAssociadoModel => faturamentoServicoAssociadoModel.FaturamentoServicoTipo)
                        .Include(x => x.FaturamentoServicosGrvs)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.FaturamentoServicoTipoVeiculoId == Servico.IdentificadorFaturamentoServicoTipoVeiculo);

                    var servicoGrv = faturamentoServicoTipoVeiculo?.FaturamentoServicosGrvs
                        ?.FirstOrDefault(x => x.GrvId == GrvId);

                    Servico.IdentificadorServicoGrv = servicoGrv?.FaturamentoServicoGrvId;
                    Servico.TempoTrabalhado = servicoGrv?.TempoTrabalhado;

                    if (Servico.TipoServico == TipoCobrancaFaturamentoEnum.Horas || Servico.TipoServico == "H")
                    {
                        Servico.QuantidadeServico = null;
                    }

                    Servico.IdentificadorFaturamentoServicoAssociado =
                        faturamentoServicoTipoVeiculo?.FaturamentoServicoAssociadoId;

                    Servico.DescricaoTipoServico = ListagemTipoCobranca
                        ?.FirstOrDefault(x => x.ValorCadastro == Servico.TipoServico)?.Descricao;

                    Servico.NomeServico = faturamentoServicoTipoVeiculo?.FaturamentoServicoAssociado?.Descricao;

                    if (faturamentoServicoTipoVeiculo?.FaturamentoServicoAssociado != null)
                    {
                        Servico.DataVigenciaInicial = faturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.DataVigenciaInicial;
                        Servico.DataVigenciaFinal = faturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.DataVigenciaFinal;
                    }

                    Servico.FlagServicoObrigatorio =
                        faturamentoServicoTipoVeiculo?.FaturamentoServicoAssociado?.FlagServicoObrigatorio == "S" ||
                        faturamentoServicoTipoVeiculo?.FaturamentoServicoAssociado?.FaturamentoServicoTipo
                            ?.FlagServicoObrigatorio == "S"
                            ? "S"
                            : "N";
                }
            }

            return GuiaPagamentoEstadiaReboque;
        }

        private GuiaPagamentoReboqueEstadiaDTO FillDataHoraAtual(GuiaPagamentoReboqueEstadiaDTO GuiaPagamentoEstadiaReboque, int DepositoId)
        {
            DateTime DataHoraAtual = DateTime.Now;

            try
            {
                if (DepositoId > 0)
                {
                    DataHoraAtual = new DepositoService(_context, _mapper)
                        .GetDataHoraPorDeposito(DepositoId);
                }
            }
            catch
            {
                DataHoraAtual = DateTime.Now;
            }

            GuiaPagamentoEstadiaReboque.DataHoraAtual = DataHoraAtual.ToString("dd/MM/yyyy HH:mm");

            GuiaPagamentoEstadiaReboque.DataAtual = DataHoraAtual.ToString("dd/MM/yyyy");

            GuiaPagamentoEstadiaReboque.HoraAtual = DataHoraAtual.ToString("HH:mm");

            GuiaPagamentoEstadiaReboque.DataHoraAtualDateTime = DataHoraAtual;

            return GuiaPagamentoEstadiaReboque;
        }


        private GuiaPagamentoReboqueEstadiaDTO FillDataHoraPersistido(GuiaPagamentoReboqueEstadiaDTO guiaPagamentoEstadiaReboque, AtendimentoModel atendimento, FaturamentoModel faturamento)
        {
            DateTime DataHoraPersistida;

            if (faturamento?.DataEmissaoDocumento.HasValue == true)
            {
                DataHoraPersistida = faturamento.DataEmissaoDocumento.Value;
            }
            else if (atendimento?.DataImpressao.HasValue == true)
            {
                DataHoraPersistida = atendimento.DataImpressao.Value;
            }
            else if (faturamento != null)
            {
                DataHoraPersistida = faturamento.DataCadastro;
            }
            else
            {
                DataHoraPersistida = DateTime.Now;
            }

            guiaPagamentoEstadiaReboque.DataHoraAtual = DataHoraPersistida.ToString("dd/MM/yyyy HH:mm");

            guiaPagamentoEstadiaReboque.DataAtual = DataHoraPersistida.ToString("dd/MM/yyyy");

            guiaPagamentoEstadiaReboque.HoraAtual = DataHoraPersistida.ToString("HH:mm");

            guiaPagamentoEstadiaReboque.DataHoraAtualDateTime = DataHoraPersistida;

            return guiaPagamentoEstadiaReboque;
        }
        private GuiaPagamentoReboqueEstadiaDTO FillDeposito(GuiaPagamentoReboqueEstadiaDTO GuiaPagamentoEstadiaReboque, GrvModel Grv)
        {
            if (Grv?.Deposito == null)
            {
                return GuiaPagamentoEstadiaReboque;
            }

            GuiaPagamentoEstadiaReboque.DepositoNome = Grv.Deposito.Nome;

            if (Grv.Deposito.Endereco != null)
            {
                GuiaPagamentoEstadiaReboque.DepositoEndereco = new EnderecoService(_context, _mapper)
                    .FormatarEndereco(Grv.Deposito.Endereco, Grv.Deposito.NumeroEndereco, Grv.Deposito.ComplementoEndereco);

                GuiaPagamentoEstadiaReboque.DepositoMunicipio = Grv.Deposito.Endereco.Municipio?.ToTitleCase();
            }

            return GuiaPagamentoEstadiaReboque;
        }

        private GuiaPagamentoReboqueEstadiaDTO FillFaturamento(GuiaPagamentoReboqueEstadiaDTO GuiaPagamentoEstadiaReboque, FaturamentoModel Faturamento)
        {
            if (Faturamento == null)
            {
                return GuiaPagamentoEstadiaReboque;
            }

            GuiaPagamentoEstadiaReboque.FaturamentoNumeroIdentificacao = Faturamento.NumeroIdentificacao;

            GuiaPagamentoEstadiaReboque.StatusPagamento = Faturamento.Status;

            GuiaPagamentoEstadiaReboque.FaturamentoValorFaturado = Faturamento.ValorFaturado.FormatMoney();

            GuiaPagamentoEstadiaReboque.FaturamentoDataVencimento = Faturamento.DataVencimento.ToString("dd/MM/yyyy");

            if (Faturamento.DataPrazoRetiradaVeiculo.HasValue)
            {
                GuiaPagamentoEstadiaReboque.PrazoRetiradaVeiculo = Faturamento.DataPrazoRetiradaVeiculo.Value.ToString("dd/MM/yyyy HH:mm") + "hrs";
            }
            else
            {
                GuiaPagamentoEstadiaReboque.PrazoRetiradaVeiculo = string.Empty;
            }

            var valorPagamento = Faturamento.ValorPagamento ?? 0;
            GuiaPagamentoEstadiaReboque.FaturamentoValorPagar = "Realizar depósito identificado “na boca do caixa” no valor de " + (Faturamento.TipoMeioCobrancaId == 12 ? valorPagamento : Faturamento.ValorFaturado).FormatMoney();

            if (Faturamento.FlagPermissaoDataRetroativaFaturamento == "S" && Faturamento.DataRetroativa.HasValue)
            {
                GuiaPagamentoEstadiaReboque.DataHoraAtual = Faturamento.DataRetroativa.Value.ToString("dd/MM/yyyy") + " " + GuiaPagamentoEstadiaReboque.HoraAtual;

                GuiaPagamentoEstadiaReboque.DataAtual = Faturamento.DataRetroativa.Value.ToString("dd/MM/yyyy");
            }

            return GuiaPagamentoEstadiaReboque;
        }

        private async Task ObterDadosVeiculoDetranHubAsync(GuiaPagamentoReboqueEstadiaDTO GuiaPagamentoEstadiaReboque, string placaInput, string chassiInput)
        {
            if (string.IsNullOrWhiteSpace(placaInput) && string.IsNullOrWhiteSpace(chassiInput))
            {
                return;
            }

            bool temPlacaValida = !string.IsNullOrWhiteSpace(placaInput) && placaInput.IsPlaca();
            bool temChassiValido = !string.IsNullOrWhiteSpace(chassiInput) && chassiInput.IsChassi();

            if (!temPlacaValida && !temChassiValido)
            {
                return;
            }

            try
            {
                var detranHubOptions = _detranHubOptions ?? _provider?.GetService<IOptions<DetranHubOptions>>();
                var detranHubService = _provider?.GetService<DetranHubService>()
                    ?? (detranHubOptions != null && _httpClientFactory != null && _mapper != null
                        ? new DetranHubService(_httpClientFactory, _mapper, detranHubOptions)
                        : (_httpClientFactory != null && _mapper != null
                            ? new DetranHubService(_httpClientFactory, _mapper)
                            : null));

                if (detranHubService != null)
                {
                    string placa = temPlacaValida ? placaInput : null;
                    string chassi = placa == null && temChassiValido ? chassiInput : null;

                    var detranHubResult = await detranHubService.SearchToPlateOrChassi(placa, chassi);

                    if (detranHubResult?.Veiculo != null)
                    {
                        var veiculoHub = detranHubResult.Veiculo;

                        if (string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.Placa) && !string.IsNullOrWhiteSpace(veiculoHub.Placa))
                        {
                            GuiaPagamentoEstadiaReboque.Placa = veiculoHub.Placa;
                        }

                        if (string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.Chassi) && !string.IsNullOrWhiteSpace(veiculoHub.Chassi))
                        {
                            GuiaPagamentoEstadiaReboque.Chassi = veiculoHub.Chassi;
                        }

                        if (string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.Renavam) && !string.IsNullOrWhiteSpace(veiculoHub.Renavam))
                        {
                            GuiaPagamentoEstadiaReboque.Renavam = veiculoHub.Renavam;
                        }

                        if (string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.Cor) && !string.IsNullOrWhiteSpace(veiculoHub.CorPrimaria))
                        {
                            GuiaPagamentoEstadiaReboque.Cor = veiculoHub.CorPrimaria;
                        }

                        if (string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.MarcaModelo) && !string.IsNullOrWhiteSpace(veiculoHub.MarcaModelo))
                        {
                            GuiaPagamentoEstadiaReboque.MarcaModelo = veiculoHub.MarcaModelo;
                        }

                        if (string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.TipoVeiculo) && !string.IsNullOrWhiteSpace(veiculoHub.TipoVeiculo))
                        {
                            GuiaPagamentoEstadiaReboque.TipoVeiculo = veiculoHub.TipoVeiculo;
                        }
                    }
                }
            }
            catch
            {
                // Falha no DetranHub não impede continuidade
            }
        }

        private async Task<GuiaPagamentoReboqueEstadiaDTO> FillGrvAsync(GuiaPagamentoReboqueEstadiaDTO GuiaPagamentoEstadiaReboque, GrvModel Grv)
        {
            if (Grv == null)
            {
                return GuiaPagamentoEstadiaReboque;
            }

            GuiaPagamentoEstadiaReboque.Placa = Grv.Placa;

            GuiaPagamentoEstadiaReboque.Chassi = Grv.Chassi;

            GuiaPagamentoEstadiaReboque.Renavam = Grv.Renavam;

            GuiaPagamentoEstadiaReboque.NumeroFormularioGrv = Grv.NumeroFormularioGrv;
            GuiaPagamentoEstadiaReboque.CodigoIdentificacaoCliente = Grv.ClienteCodigoIdentificacao?.CodigoIdentificacao;

            if (Grv.DataHoraGuarda.HasValue)
            {
                GuiaPagamentoEstadiaReboque.DataHoraRemocao = Grv.DataHoraGuarda.Value.ToString("dd/MM/yyyy HH:mm");

                GuiaPagamentoEstadiaReboque.DataHoraGuarda = Grv.DataHoraGuarda.Value.ToString("dd/MM/yyyy HH:mm");

                GuiaPagamentoEstadiaReboque.DataGuarda = Grv.DataHoraGuarda.Value.ToString("dd/MM/yyyy");

                GuiaPagamentoEstadiaReboque.HoraGuarda = Grv.DataHoraGuarda.Value.ToString("HH:mm");
            }

            GuiaPagamentoEstadiaReboque.EstacionamentoSetor = Grv.EstacionamentoSetor;

            GuiaPagamentoEstadiaReboque.EstacionamentoNumeroVaga = Grv.EstacionamentoNumeroVaga;

            GuiaPagamentoEstadiaReboque.NumeroChave = Grv.NumeroChave;

            if (Grv.FlagComboio == "N")
            {
                GuiaPagamentoEstadiaReboque.ReboquistaNome = Grv.Reboquista?.Nome;

                GuiaPagamentoEstadiaReboque.ReboquePlaca = Grv.Reboque?.Placa;
            }

            GuiaPagamentoEstadiaReboque.MarcaModelo = Grv.MarcaModelo?.MarcaModelo;

            GuiaPagamentoEstadiaReboque.Cor = Grv.Cor?.Cor;

            GuiaPagamentoEstadiaReboque.TipoVeiculo = Grv.TipoVeiculo?.Descricao;

            // Consultar no DetranHub pela Placa ou Chassi para complementar informações faltantes
            await ObterDadosVeiculoDetranHubAsync(GuiaPagamentoEstadiaReboque, Grv.Placa, Grv.Chassi);

            // Fallback para DetranRioVeiculo local se ainda faltarem informações
            if (string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.Cor) ||
                string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.MarcaModelo) ||
                string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.TipoVeiculo) ||
                string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.Chassi) ||
                string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.Renavam))
            {
                try
                {
                    string placaLimpa = (GuiaPagamentoEstadiaReboque.Placa ?? Grv.Placa)?.NormalizePlaca();
                    string chassiLimpo = (GuiaPagamentoEstadiaReboque.Chassi ?? Grv.Chassi)?.NormalizeChassi();

                    var veiculoBd = await _context.DetranRioVeiculo
                        .Include(x => x.Cor)
                        .Include(x => x.MarcaModelo)
                        .AsNoTracking()
                        .OrderByDescending(x => x.DetranVeiculoId)
                        .FirstOrDefaultAsync(x =>
                            (!string.IsNullOrWhiteSpace(placaLimpa) && x.Placa == placaLimpa) ||
                            (string.IsNullOrWhiteSpace(placaLimpa) && !string.IsNullOrWhiteSpace(chassiLimpo) && x.Chassi == chassiLimpo));

                    if (veiculoBd != null)
                    {
                        if (string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.Cor) && veiculoBd.Cor != null)
                        {
                            GuiaPagamentoEstadiaReboque.Cor = veiculoBd.Cor.Cor;
                        }

                        if (string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.MarcaModelo) && veiculoBd.MarcaModelo != null)
                        {
                            GuiaPagamentoEstadiaReboque.MarcaModelo = veiculoBd.MarcaModelo.MarcaModelo;
                        }

                        if (string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.TipoVeiculo) && !string.IsNullOrWhiteSpace(veiculoBd.DescricaoTipo))
                        {
                            GuiaPagamentoEstadiaReboque.TipoVeiculo = veiculoBd.DescricaoTipo;
                        }

                        if (string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.Renavam) && !string.IsNullOrWhiteSpace(veiculoBd.Renavam))
                        {
                            GuiaPagamentoEstadiaReboque.Renavam = veiculoBd.Renavam;
                        }

                        if (string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.Chassi) && !string.IsNullOrWhiteSpace(veiculoBd.Chassi))
                        {
                            GuiaPagamentoEstadiaReboque.Chassi = veiculoBd.Chassi;
                        }
                    }
                }
                catch
                {
                    // Falha no complemento local não impede continuidade
                }
            }

            if (!string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.Placa) && !string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.Chassi))
            {
                GuiaPagamentoEstadiaReboque.PlacaChassi = $"{GuiaPagamentoEstadiaReboque.Placa} / {GuiaPagamentoEstadiaReboque.Chassi}";
            }
            else if (!string.IsNullOrWhiteSpace(GuiaPagamentoEstadiaReboque.Placa))
            {
                GuiaPagamentoEstadiaReboque.PlacaChassi = GuiaPagamentoEstadiaReboque.Placa;
            }
            else
            {
                GuiaPagamentoEstadiaReboque.PlacaChassi = GuiaPagamentoEstadiaReboque.Chassi;
            }

            return GuiaPagamentoEstadiaReboque;
        }

        private GuiaPagamentoReboqueEstadiaDTO FillRodape(GuiaPagamentoReboqueEstadiaDTO GuiaPagamentoEstadiaReboque, int UsuarioId)
        {
            UsuarioModel Usuario = _context.Usuario
                .Include(x => x.Pessoa)
                .AsNoTracking()
                .FirstOrDefault(x => x.UsuarioId == UsuarioId);

            string nomeCompleto = string.Empty;

            if (Usuario?.Pessoa != null)
            {
                StringBuilder sbNome = new();

                sbNome.Append(Usuario.Pessoa.Nome);

                if (!string.IsNullOrWhiteSpace(Usuario.Pessoa.NomeMeio))
                {
                    sbNome.Append(" " + Usuario.Pessoa.NomeMeio);
                }

                if (!string.IsNullOrWhiteSpace(Usuario.Pessoa.Sobrenome))
                {
                    sbNome.Append(" " + Usuario.Pessoa.Sobrenome);
                }

                nomeCompleto = sbNome.ToString();
            }

            DateTime dataHora = GuiaPagamentoEstadiaReboque.DataHoraAtualDateTime != default
                ? GuiaPagamentoEstadiaReboque.DataHoraAtualDateTime
                : DateTime.Now;

            GuiaPagamentoEstadiaReboque.Rodape = "Impressão realizada em " +
                dataHora.ToString("dd/MM/yyyy") +
                " às " +
                dataHora.ToShortTimeString() +
                ". " +
                "USUÁRIO: " + nomeCompleto;

            return GuiaPagamentoEstadiaReboque;
        }

        public async Task<GuiaPagamentoReboqueEstadiaDTO> GetGuiaPagamentoReboqueEstadiaAsync(int FaturamentoId, int UsuarioId, bool SelecionarFaturamentoPago = false)
        {
            GuiaPagamentoReboqueEstadiaDTO ResultView = new();

            if (FaturamentoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorFaturamentoInvalido);

                return ResultView;
            }

            #region Consulta
            FaturamentoModel Faturamento = await _context.Faturamento
                .Include(x => x.TipoMeioCobranca)
                .Include(x => x.ListagemFaturamentoComposicao)
                    .ThenInclude(x => x.FaturamentoServicoTipoVeiculo)
                    .ThenInclude(x => x.FaturamentoServicoAssociado)
                    .ThenInclude(x => x.FaturamentoServicoTipo)
                .Include(x => x.ListagemPixDinamico)
                .Include(x => x.ListagemPixEstatico)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FaturamentoId == FaturamentoId);

            GrvModel Grv = await _context.Grv
                .Include(x => x.Cliente)
                .ThenInclude(x => x.Endereco)
                .Include(x => x.Cliente)
                .ThenInclude(x => x.AgenciaBancaria)
                .Include(x => x.Cliente)
                .ThenInclude(x => x.AgenciaBancaria.Banco)
                .Include(x => x.Cliente)
                .ThenInclude(x => x.Empresa)
                .Include(x => x.Deposito)
                .ThenInclude(x => x.Endereco)
                .Include(x => x.Reboque)
                .Include(x => x.Reboquista)
                .Include(x => x.MarcaModelo)
                .Include(x => x.Cor)
                .Include(x => x.TipoVeiculo)
                .Include(x => x.Atendimento)
                .ThenInclude(x => x.QualificacaoResponsavel)
                .Include(x => x.ClienteCodigoIdentificacao)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Atendimento.AtendimentoId == Faturamento.AtendimentoId);

            PixDinamicoModel pixDinamico = Faturamento?.ListagemPixDinamico
                ?.OrderByDescending(x => x.DataCadastro)
                .FirstOrDefault();
            PixEstaticoModel pixEstatico = Faturamento?.ListagemPixEstatico
                ?.OrderByDescending(x => x.DataCadastro)
                .FirstOrDefault();
            #endregion


            if (Faturamento == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoFaturamento);

                return ResultView;
            }
            else if (Faturamento.Status == "C")
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Esse Faturamento foi cancelado");

                return ResultView;
            }
            else if (Faturamento.Status == "P" && !SelecionarFaturamentoPago)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Esse Faturamento já foi pago");

                return ResultView;
            }
            //else if (Faturamento.TipoMeioCobranca.DocumentoImpressao == null || Faturamento.TipoMeioCobranca.DocumentoImpressao != "GuiaPagamentoEstadiaReboque.rdlc")
            //{
            //    ResultView.Mensagem = MensagemViewHelper
            //        .GetBadRequest($"Esse Faturamento está cadastrado em uma Forma de Pagamento que não está configurada para imprimir a " +
            //        $"Guia de Pagamento de Reboque e Estadia. Forma de Pagamento atual: {Faturamento.TipoMeioCobranca.Descricao}");

            //    return ResultView;
            //}



            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);

                return ResultView;
            }

            ResultView.Mensagem = new GrvService(_context).ValidateInputGrv(Grv, UsuarioId);

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }



            GuiaPagamentoReboqueEstadiaDTO GuiaPagamentoEstadiaReboque = new();


            // GRV
            GuiaPagamentoEstadiaReboque = await FillGrvAsync(GuiaPagamentoEstadiaReboque, Grv);

            // ATENDIMENTO
            GuiaPagamentoEstadiaReboque = FillAtendimento(GuiaPagamentoEstadiaReboque, Grv.Atendimento);

            // CLIENTE
            GuiaPagamentoEstadiaReboque = FillCliente(GuiaPagamentoEstadiaReboque, Grv);

            // DEPÓSITO
            GuiaPagamentoEstadiaReboque = FillDeposito(GuiaPagamentoEstadiaReboque, Grv);

            // DATA/HORA
            GuiaPagamentoEstadiaReboque = FillDataHoraAtual(GuiaPagamentoEstadiaReboque, Grv.Deposito?.DepositoId ?? 0);

            // FATURAMENTO
            GuiaPagamentoEstadiaReboque = FillFaturamento(GuiaPagamentoEstadiaReboque, Faturamento);

            // COMPOSIÇÃO DO FATURAMENTO
            GuiaPagamentoEstadiaReboque = await FillComposicaoFaturamentoAsync(GuiaPagamentoEstadiaReboque, Faturamento, Grv.GrvId);

            // RODAPÉ
            GuiaPagamentoEstadiaReboque = FillRodape(GuiaPagamentoEstadiaReboque, UsuarioId);

            // LOGOMARCA
            if (Grv.ClienteId > 0)
            {
                ImageListDTO Listagem = await new ClienteService(_context, _mapper, _httpClientFactory)
                    .GetLogomarcaAsync(Grv.ClienteId);

                GuiaPagamentoEstadiaReboque.Logo = Listagem?.Listagem
                    ?.FirstOrDefault()
                    ?.Imagem;
            }

            if (pixDinamico != null)
            {
                GuiaPagamentoEstadiaReboque.PixChave = pixDinamico.Chave;

                if (!string.IsNullOrWhiteSpace(pixDinamico.QrCode))
                {
                    try
                    {
                        GuiaPagamentoEstadiaReboque.QrCode = Convert.FromBase64String(pixDinamico.QrCode);
                    }
                    catch
                    {
                        if (!string.IsNullOrWhiteSpace(pixDinamico.QrString))
                        {
                            GuiaPagamentoEstadiaReboque.QrCode = QRCodeHelper.CreateImageAsByteArray(pixDinamico.QrString);
                        }
                        else if (!string.IsNullOrWhiteSpace(pixDinamico.Pix))
                        {
                            GuiaPagamentoEstadiaReboque.QrCode = QRCodeHelper.CreateImageAsByteArray(pixDinamico.Pix);
                        }
                    }
                }
                else if (!string.IsNullOrWhiteSpace(pixDinamico.QrString))
                {
                    GuiaPagamentoEstadiaReboque.QrCode = QRCodeHelper.CreateImageAsByteArray(pixDinamico.QrString);
                }
                else if (!string.IsNullOrWhiteSpace(pixDinamico.Pix))
                {
                    GuiaPagamentoEstadiaReboque.QrCode = QRCodeHelper.CreateImageAsByteArray(pixDinamico.Pix);
                }
            }

            if (pixEstatico != null)
            {
                GuiaPagamentoEstadiaReboque.PixChave = pixEstatico.Chave;

                if (!string.IsNullOrWhiteSpace(pixEstatico.QRCode))
                {
                    try
                    {
                        GuiaPagamentoEstadiaReboque.QrCode = Convert.FromBase64String(pixEstatico.QRCode);
                    }
                    catch
                    {
                        if (!string.IsNullOrWhiteSpace(pixEstatico.QRString))
                        {
                            GuiaPagamentoEstadiaReboque.QrCode = QRCodeHelper.CreateImageAsByteArray(pixEstatico.QRString);
                        }
                    }
                }
                else if (!string.IsNullOrWhiteSpace(pixEstatico.QRString))
                {
                    GuiaPagamentoEstadiaReboque.QrCode = QRCodeHelper.CreateImageAsByteArray(pixEstatico.QRString);
                }
            }



            GuiaPagamentoEstadiaReboque.Mensagem = MensagemViewHelper.SetOk("Guia de Pagamento de Reboque e Estadia gerado com sucesso");

            return GuiaPagamentoEstadiaReboque;
        }
        public async Task<GuiaPagamentoReboqueEstadiaDTO> ConsultarGuiaPagamentoReboqueEstadiaAsync(int FaturamentoId, int UsuarioId)
        {
            GuiaPagamentoReboqueEstadiaDTO ResultView = new();

            if (FaturamentoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorFaturamentoInvalido);

                return ResultView;
            }

            #region Consulta
            FaturamentoModel Faturamento = await _context.Faturamento
                .Include(x => x.TipoMeioCobranca)
                .Include(x => x.ListagemFaturamentoComposicao)
                    .ThenInclude(x => x.FaturamentoServicoTipoVeiculo)
                    .ThenInclude(x => x.FaturamentoServicoAssociado)
                    .ThenInclude(x => x.FaturamentoServicoTipo)
                .Include(x => x.ListagemPixDinamico)
                .Include(x => x.ListagemPixEstatico)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FaturamentoId == FaturamentoId);

            GrvModel Grv = await _context.Grv
                .Include(x => x.Cliente)
                .ThenInclude(x => x.Endereco)
                .Include(x => x.Cliente)
                .ThenInclude(x => x.AgenciaBancaria)
                .Include(x => x.Cliente)
                .ThenInclude(x => x.AgenciaBancaria.Banco)
                .Include(x => x.Cliente)
                .ThenInclude(x => x.Empresa)
                .Include(x => x.Deposito)
                .ThenInclude(x => x.Endereco)
                .Include(x => x.Reboque)
                .Include(x => x.Reboquista)
                .Include(x => x.MarcaModelo)
                .Include(x => x.Cor)
                .Include(x => x.TipoVeiculo)
                .Include(x => x.Atendimento)
                .ThenInclude(x => x.QualificacaoResponsavel)
                .Include(x => x.ClienteCodigoIdentificacao)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Atendimento.AtendimentoId == Faturamento.AtendimentoId);

            PixDinamicoModel pixDinamico = Faturamento?.ListagemPixDinamico
                ?.OrderByDescending(x => x.DataCadastro)
                .FirstOrDefault();
            PixEstaticoModel pixEstatico = Faturamento?.ListagemPixEstatico
                ?.OrderByDescending(x => x.DataCadastro)
                .FirstOrDefault();
            #endregion


            if (Faturamento == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoFaturamento);

                return ResultView;
            }
            else if (Faturamento.Status == "C")
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Esse Faturamento foi cancelado");

                return ResultView;
            }
            //else if (Faturamento.TipoMeioCobranca.DocumentoImpressao == null || Faturamento.TipoMeioCobranca.DocumentoImpressao != "GuiaPagamentoEstadiaReboque.rdlc")
            //{
            //    ResultView.Mensagem = MensagemViewHelper
            //        .GetBadRequest($"Esse Faturamento está cadastrado em uma Forma de Pagamento que não está configurada para imprimir a " +
            //        $"Guia de Pagamento de Reboque e Estadia. Forma de Pagamento atual: {Faturamento.TipoMeioCobranca.Descricao}");

            //    return ResultView;
            //}



            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoGrv);

                return ResultView;
            }

            ResultView.Mensagem = new GrvService(_context).ValidateInputGrv(Grv, UsuarioId);

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }



            GuiaPagamentoReboqueEstadiaDTO GuiaPagamentoEstadiaReboque = new();


            // GRV
            GuiaPagamentoEstadiaReboque = await FillGrvAsync(GuiaPagamentoEstadiaReboque, Grv);

            // ATENDIMENTO
            GuiaPagamentoEstadiaReboque = FillAtendimento(GuiaPagamentoEstadiaReboque, Grv.Atendimento);

            // CLIENTE
            GuiaPagamentoEstadiaReboque = FillCliente(GuiaPagamentoEstadiaReboque, Grv);

            // DEPÓSITO
            GuiaPagamentoEstadiaReboque = FillDeposito(GuiaPagamentoEstadiaReboque, Grv);

            // DATA/HORA
            GuiaPagamentoEstadiaReboque = FillDataHoraPersistido(GuiaPagamentoEstadiaReboque, Grv.Atendimento, Faturamento);

            // FATURAMENTO
            GuiaPagamentoEstadiaReboque = FillFaturamento(GuiaPagamentoEstadiaReboque, Faturamento);

            // COMPOSIÇÃO DO FATURAMENTO
            GuiaPagamentoEstadiaReboque = await FillComposicaoFaturamentoAsync(GuiaPagamentoEstadiaReboque, Faturamento, Grv.GrvId);

            // RODAPÉ
            GuiaPagamentoEstadiaReboque = FillRodape(GuiaPagamentoEstadiaReboque, UsuarioId);

            // LOGOMARCA
            if (Grv.ClienteId > 0)
            {
                ImageListDTO Listagem = await new ClienteService(_context, _mapper, _httpClientFactory)
                    .GetLogomarcaAsync(Grv.ClienteId);

                GuiaPagamentoEstadiaReboque.Logo = Listagem?.Listagem
                    ?.FirstOrDefault()
                    ?.Imagem;
            }

            if (pixDinamico != null)
            {
                GuiaPagamentoEstadiaReboque.PixChave = pixDinamico.Chave;

                if (!string.IsNullOrWhiteSpace(pixDinamico.QrCode))
                {
                    try
                    {
                        GuiaPagamentoEstadiaReboque.QrCode = Convert.FromBase64String(pixDinamico.QrCode);
                    }
                    catch
                    {
                        if (!string.IsNullOrWhiteSpace(pixDinamico.QrString))
                        {
                            GuiaPagamentoEstadiaReboque.QrCode = QRCodeHelper.CreateImageAsByteArray(pixDinamico.QrString);
                        }
                        else if (!string.IsNullOrWhiteSpace(pixDinamico.Pix))
                        {
                            GuiaPagamentoEstadiaReboque.QrCode = QRCodeHelper.CreateImageAsByteArray(pixDinamico.Pix);
                        }
                    }
                }
                else if (!string.IsNullOrWhiteSpace(pixDinamico.QrString))
                {
                    GuiaPagamentoEstadiaReboque.QrCode = QRCodeHelper.CreateImageAsByteArray(pixDinamico.QrString);
                }
                else if (!string.IsNullOrWhiteSpace(pixDinamico.Pix))
                {
                    GuiaPagamentoEstadiaReboque.QrCode = QRCodeHelper.CreateImageAsByteArray(pixDinamico.Pix);
                }
            }

            if (pixEstatico != null)
            {
                GuiaPagamentoEstadiaReboque.PixChave = pixEstatico.Chave;

                if (!string.IsNullOrWhiteSpace(pixEstatico.QRCode))
                {
                    try
                    {
                        GuiaPagamentoEstadiaReboque.QrCode = Convert.FromBase64String(pixEstatico.QRCode);
                    }
                    catch
                    {
                        if (!string.IsNullOrWhiteSpace(pixEstatico.QRString))
                        {
                            GuiaPagamentoEstadiaReboque.QrCode = QRCodeHelper.CreateImageAsByteArray(pixEstatico.QRString);
                        }
                    }
                }
                else if (!string.IsNullOrWhiteSpace(pixEstatico.QRString))
                {
                    GuiaPagamentoEstadiaReboque.QrCode = QRCodeHelper.CreateImageAsByteArray(pixEstatico.QRString);
                }
            }



            GuiaPagamentoEstadiaReboque.Mensagem = MensagemViewHelper.SetOk("Guia de Pagamento de Reboque e Estadia gerado com sucesso");

            return GuiaPagamentoEstadiaReboque;
        }
    }
}