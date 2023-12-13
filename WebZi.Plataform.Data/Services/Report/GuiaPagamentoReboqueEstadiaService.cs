﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Number;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.Report;

namespace WebZi.Plataform.Data.Services.Report
{
    public class GuiaPagamentoReboqueEstadiaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public GuiaPagamentoReboqueEstadiaService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        private GuiaPagamentoReboqueEstadiaViewModel FillAtendimento(GuiaPagamentoReboqueEstadiaViewModel GuiaPagamentoEstadiaReboque, AtendimentoModel Atendimento)
        {
            GuiaPagamentoEstadiaReboque.QualificacaoResponsavel = Atendimento.QualificacaoResponsavel.Descricao;

            GuiaPagamentoEstadiaReboque.AtendimentoResponsavelNome = Atendimento.ResponsavelNome;

            GuiaPagamentoEstadiaReboque.AtendimentoResponsavelDocumento = DocumentHelper.FormatCPF(Atendimento.ResponsavelDocumento);

            GuiaPagamentoEstadiaReboque.AtendimentoFormaLiberacao = Atendimento.FormaLiberacao;

            GuiaPagamentoEstadiaReboque.AtendimentoFormaLiberacaoNome = Atendimento.FormaLiberacaoNome;

            GuiaPagamentoEstadiaReboque.AtendimentoFormaLiberacaoCNH = Atendimento.FormaLiberacaoCNH;

            GuiaPagamentoEstadiaReboque.AtendimentoFormaLiberacaoCPF = Atendimento.FormaLiberacaoCPF;

            GuiaPagamentoEstadiaReboque.AtendimentoFormaLiberacaoPlaca = Atendimento.FormaLiberacaoPlaca;

            if (Atendimento.ResponsavelDocumento.Length == 11)
            {
                GuiaPagamentoEstadiaReboque.Identificador = "Identificador (CPF): " + DocumentHelper.FormatCPF(Atendimento.ResponsavelDocumento);
            }
            else
            {
                GuiaPagamentoEstadiaReboque.Identificador = "Identificador (CNPJ): " + DocumentHelper.FormatCNPJ(Atendimento.ResponsavelDocumento);
            }

            return GuiaPagamentoEstadiaReboque;
        }

        private GuiaPagamentoReboqueEstadiaViewModel FillCliente(GuiaPagamentoReboqueEstadiaViewModel GuiaPagamentoEstadiaReboque, GrvModel Grv)
        {
            GuiaPagamentoEstadiaReboque.ClienteNome = Grv.Cliente.Nome;

            GuiaPagamentoEstadiaReboque.ClienteCNPJ = DocumentHelper.FormatCNPJ(Grv.Cliente.CNPJ);

            GuiaPagamentoEstadiaReboque.ClienteEndereco = new EnderecoService(_context, _mapper)
                .FormatarEndereco(Grv.Cliente.Endereco, Grv.Cliente.NumeroEndereco, Grv.Cliente.ComplementoEndereco);

            GuiaPagamentoEstadiaReboque.ClienteDadosBancarios = "Banco: " + Grv.Cliente.AgenciaBancaria.Banco.Nome + ". Ag: " + Grv.Cliente.AgenciaBancaria.CodigoAgencia + ". CC: " + Grv.Cliente.AgenciaBancaria.ContaCorrente + "-" + Grv.Cliente.AgenciaBancaria.DigitoVerificador;

            GuiaPagamentoEstadiaReboque.ClienteDadosBancarios = GuiaPagamentoEstadiaReboque.ClienteDadosBancarios.Replace("..", ".");

            if (Grv.Cliente.FlagClienteRealizaFaturamentoArrecadacao == "S")
            {
                GuiaPagamentoEstadiaReboque.CreditoDe = "Crédito de: " + Grv.Cliente.Nome + " (CNPJ: " + DocumentHelper.FormatCNPJ(Grv.Cliente.CNPJ) + ")";
            }
            else
            {
                GuiaPagamentoEstadiaReboque.CreditoDe = "Crédito de: " + Grv.Cliente.Empresa.Nome + " (CNPJ: " + DocumentHelper.FormatCNPJ(Grv.Cliente.Empresa.CNPJ) + ")";
            }

            return GuiaPagamentoEstadiaReboque;
        }

        private GuiaPagamentoReboqueEstadiaViewModel FillComposicaoFaturamento(GuiaPagamentoReboqueEstadiaViewModel GuiaPagamentoEstadiaReboque, FaturamentoModel Faturamento)
        {
            decimal ValorDemaisServicos = 0;

            foreach (FaturamentoComposicaoModel Composicao in Faturamento.FaturamentoComposicoes)
            {
                if (Composicao.FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.FaturamentoServicoTipo.OrdemImpressao == 1)
                {
                    GuiaPagamentoEstadiaReboque.QuantidadeEstadias = (int)Composicao.QuantidadeComposicao;

                    GuiaPagamentoEstadiaReboque.PrecoEstadias = NumberHelper.FormatMoney(Composicao.ValorTipoComposicao);

                    GuiaPagamentoEstadiaReboque.ValorFaturadoEstadias = NumberHelper.FormatMoney(Composicao.ValorFaturado);
                }
                else if (Composicao.FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.FaturamentoServicoTipo.OrdemImpressao == 2)
                {
                    GuiaPagamentoEstadiaReboque.PrecoReboque = NumberHelper.FormatMoney(Composicao.ValorTipoComposicao);

                    GuiaPagamentoEstadiaReboque.ValorFaturadoReboque = NumberHelper.FormatMoney(Composicao.ValorFaturado);
                }
                else if (Composicao.FaturamentoServicoTipoVeiculo.FaturamentoServicoAssociado.FaturamentoServicoTipo.OrdemImpressao == 3)
                {
                    GuiaPagamentoEstadiaReboque.QuantidadeQuilometragem = ((int)Composicao.QuantidadeComposicao).ToString();

                    GuiaPagamentoEstadiaReboque.PrecoQuilometragem = NumberHelper.FormatMoney(Composicao.ValorTipoComposicao);

                    GuiaPagamentoEstadiaReboque.ValorFaturadoQuilometragem = NumberHelper.FormatMoney(Composicao.ValorFaturado);
                }
                else
                {
                    ValorDemaisServicos += Composicao.ValorFaturado;
                }

                if (ValorDemaisServicos > 0)
                {
                    GuiaPagamentoEstadiaReboque.ValorDemaisServicos += NumberHelper.FormatMoney(ValorDemaisServicos);
                }
            }

            return GuiaPagamentoEstadiaReboque;
        }

        private GuiaPagamentoReboqueEstadiaViewModel FillDataHoraAtual(GuiaPagamentoReboqueEstadiaViewModel GuiaPagamentoEstadiaReboque, int DepositoId)
        {
            DateTime DataHoraAtual = new DepositoService(_context, _mapper)
                .GetDataHoraPorDeposito(DepositoId);

            GuiaPagamentoEstadiaReboque.DataHoraAtual = DataHoraAtual.ToString("dd/MM/yyyy HH:mm");

            GuiaPagamentoEstadiaReboque.DataAtual = DataHoraAtual.ToString("dd/MM/yyyy");

            GuiaPagamentoEstadiaReboque.HoraAtual = DataHoraAtual.ToString("HH:mm");

            GuiaPagamentoEstadiaReboque.DataHoraAtualDateTime = DataHoraAtual;

            return GuiaPagamentoEstadiaReboque;
        }

        private GuiaPagamentoReboqueEstadiaViewModel FillDeposito(GuiaPagamentoReboqueEstadiaViewModel GuiaPagamentoEstadiaReboque, GrvModel Grv)
        {
            GuiaPagamentoEstadiaReboque.DepositoNome = Grv.Deposito.Nome;

            GuiaPagamentoEstadiaReboque.DepositoEndereco = new EnderecoService(_context, _mapper)
                .FormatarEndereco(Grv.Deposito.Endereco, Grv.Deposito.NumeroEndereco, Grv.Deposito.ComplementoEndereco);

            GuiaPagamentoEstadiaReboque.DepositoMunicipio = Grv.Deposito.Endereco.Municipio.ToTitleCase();

            return GuiaPagamentoEstadiaReboque;
        }

        private GuiaPagamentoReboqueEstadiaViewModel FillFaturamento(GuiaPagamentoReboqueEstadiaViewModel GuiaPagamentoEstadiaReboque, FaturamentoModel Faturamento)
        {
            GuiaPagamentoEstadiaReboque.FaturamentoNumeroIdentificacao = Faturamento.NumeroIdentificacao;

            GuiaPagamentoEstadiaReboque.FaturamentoValorFaturado = NumberHelper.FormatMoney(Faturamento.ValorFaturado);

            GuiaPagamentoEstadiaReboque.FaturamentoDataVencimento = Faturamento.DataVencimento.ToString("dd/MM/yyyy");

            GuiaPagamentoEstadiaReboque.PrazoRetiradaVeiculo = Faturamento.DataPrazoRetiradaVeiculo.Value.ToString("dd/MM/yyyy HH:mm") + "hrs";

            GuiaPagamentoEstadiaReboque.FaturamentoValorPagar = "Realizar depósito identificado “na boca do caixa” no valor de " + NumberHelper.FormatMoney(Faturamento.ValorFaturado);

            if (Faturamento.FlagPermissaoDataRetroativaFaturamento == "S" && Faturamento.DataRetroativa.HasValue)
            {
                GuiaPagamentoEstadiaReboque.DataHoraAtual = Faturamento.DataRetroativa.Value.ToString("dd/MM/yyyy") + " " + GuiaPagamentoEstadiaReboque.HoraAtual;

                GuiaPagamentoEstadiaReboque.DataAtual = Faturamento.DataRetroativa.Value.ToString("dd/MM/yyyy");
            }

            return GuiaPagamentoEstadiaReboque;
        }

        private GuiaPagamentoReboqueEstadiaViewModel FillGrv(GuiaPagamentoReboqueEstadiaViewModel GuiaPagamentoEstadiaReboque, GrvModel Grv)
        {
            if (!string.IsNullOrWhiteSpace(Grv.Placa))
            {
                GuiaPagamentoEstadiaReboque.PlacaChassi = Grv.Placa;
            }
            else
            {
                GuiaPagamentoEstadiaReboque.PlacaChassi = Grv.Chassi;
            }

            GuiaPagamentoEstadiaReboque.Placa = Grv.Placa;

            GuiaPagamentoEstadiaReboque.Chassi = Grv.Chassi;

            GuiaPagamentoEstadiaReboque.Renavam = Grv.Renavam;

            GuiaPagamentoEstadiaReboque.NumeroFormularioGrv = Grv.NumeroFormularioGrv;

            GuiaPagamentoEstadiaReboque.DataHoraRemocao = Grv.DataHoraGuarda.Value.ToString("dd/MM/yyyy HH:mm");

            GuiaPagamentoEstadiaReboque.DataHoraGuarda = Grv.DataHoraGuarda.Value.ToString("dd/MM/yyyy HH:mm");

            GuiaPagamentoEstadiaReboque.DataGuarda = Grv.DataHoraGuarda.Value.ToString("dd/MM/yyyy");

            GuiaPagamentoEstadiaReboque.HoraGuarda = Grv.DataHoraGuarda.Value.ToString("HH:mm");

            GuiaPagamentoEstadiaReboque.EstacionamentoSetor = Grv.EstacionamentoSetor;

            GuiaPagamentoEstadiaReboque.EstacionamentoNumeroVaga = Grv.EstacionamentoNumeroVaga;

            GuiaPagamentoEstadiaReboque.NumeroChave = Grv.NumeroChave;

            GuiaPagamentoEstadiaReboque.ReboquistaNome = Grv.Reboquista.Nome;

            GuiaPagamentoEstadiaReboque.ReboquePlaca = Grv.Reboque.Placa;

            GuiaPagamentoEstadiaReboque.MarcaModelo = Grv.MarcaModelo.MarcaModelo;

            GuiaPagamentoEstadiaReboque.Cor = Grv.Cor.Cor;

            GuiaPagamentoEstadiaReboque.TipoVeiculo = Grv.TipoVeiculo.Descricao;

            return GuiaPagamentoEstadiaReboque;
        }

        private GuiaPagamentoReboqueEstadiaViewModel FillRodape(GuiaPagamentoReboqueEstadiaViewModel GuiaPagamentoEstadiaReboque, int UsuarioId)
        {
            UsuarioModel Usuario = _context.Usuario
                .Include(i => i.Pessoa)
                .Where(w => w.UsuarioId == UsuarioId)
                .AsNoTracking()
                .FirstOrDefault();

            StringBuilder NomeCompleto = new();

            NomeCompleto.Append(Usuario.Pessoa.Nome);

            if (!string.IsNullOrWhiteSpace(Usuario.Pessoa.NomeMeio))
            {
                NomeCompleto.Append(" " + Usuario.Pessoa.NomeMeio);
            }

            NomeCompleto.Append(" " + Usuario.Pessoa.Sobrenome);

            GuiaPagamentoEstadiaReboque.Rodape = "Impressão realizada em " +
                GuiaPagamentoEstadiaReboque.DataHoraAtualDateTime.ToString("dd/MM/yyyy") +
                " às " +
                GuiaPagamentoEstadiaReboque.DataHoraAtualDateTime.ToShortTimeString() +
                ". " +
                "USUÁRIO: " + NomeCompleto;

            return GuiaPagamentoEstadiaReboque;
        }

        public async Task<GuiaPagamentoReboqueEstadiaViewModel> GetGuiaPagamentoReboqueEstadiaAsync(int FaturamentoId, int UsuarioId, bool SelecionarFaturamentoPago = false)
        {
            GuiaPagamentoReboqueEstadiaViewModel ResultView = new();

            if (FaturamentoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorFaturamentoInvalido);

                return ResultView;
            }

            FaturamentoModel Faturamento = await _context.Faturamento
                .Include(i => i.TipoMeioCobranca)
                .Include(i => i.FaturamentoComposicoes)
                .ThenInclude(t => t.FaturamentoServicoTipoVeiculo)
                .ThenInclude(t => t.FaturamentoServicoAssociado)
                .ThenInclude(t => t.FaturamentoServicoTipo)
                .Where(w => w.FaturamentoId == FaturamentoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

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

            GrvModel Grv = await _context.Grv
                .Include(i => i.Cliente)
                .ThenInclude(t => t.Endereco)
                .Include(i => i.Cliente)
                .ThenInclude(t => t.AgenciaBancaria)
                .Include(i => i.Cliente)
                .ThenInclude(t => t.AgenciaBancaria.Banco)
                .Include(i => i.Cliente)
                .ThenInclude(t => t.Empresa)
                .Include(i => i.Deposito)
                .ThenInclude(t => t.Endereco)
                .Include(i => i.Reboque)
                .Include(i => i.Reboquista)
                .Include(i => i.MarcaModelo)
                .Include(i => i.Cor)
                .Include(i => i.TipoVeiculo)
                .Include(i => i.Atendimento)
                .ThenInclude(t => t.QualificacaoResponsavel)
                .Where(w => w.Atendimento.AtendimentoId == Faturamento.AtendimentoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

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

            GuiaPagamentoReboqueEstadiaViewModel GuiaPagamentoEstadiaReboque = new();

            // GRV
            GuiaPagamentoEstadiaReboque = FillGrv(GuiaPagamentoEstadiaReboque, Grv);

            // ATENDIMENTO
            GuiaPagamentoEstadiaReboque = FillAtendimento(GuiaPagamentoEstadiaReboque, Grv.Atendimento);

            // CLIENTE
            GuiaPagamentoEstadiaReboque = FillCliente(GuiaPagamentoEstadiaReboque, Grv);

            // DEPÓSITO
            GuiaPagamentoEstadiaReboque = FillDeposito(GuiaPagamentoEstadiaReboque, Grv);

            // DATA/HORA
            GuiaPagamentoEstadiaReboque = FillDataHoraAtual(GuiaPagamentoEstadiaReboque, Grv.Deposito.DepositoId);

            // FATURAMENTO
            GuiaPagamentoEstadiaReboque = FillFaturamento(GuiaPagamentoEstadiaReboque, Faturamento);

            // COMPOSIÇÃO DO FATURAMENTO
            GuiaPagamentoEstadiaReboque = FillComposicaoFaturamento(GuiaPagamentoEstadiaReboque, Faturamento);

            // RODAPÉ
            GuiaPagamentoEstadiaReboque = FillRodape(GuiaPagamentoEstadiaReboque, UsuarioId);

            // LOGOMARCA
            ImageViewModelList Listagem = await new ClienteService(_context, _mapper, _httpClientFactory)
                .GetLogomarcaAsync(Grv.ClienteId);

            GuiaPagamentoEstadiaReboque.Logo = Listagem.Listagem.FirstOrDefault().Imagem;

            GuiaPagamentoEstadiaReboque.Mensagem = MensagemViewHelper.SetOk("Guia de Pagamento de Reboque e Estadia gerado com sucesso");

            return GuiaPagamentoEstadiaReboque;
        }
    }
}