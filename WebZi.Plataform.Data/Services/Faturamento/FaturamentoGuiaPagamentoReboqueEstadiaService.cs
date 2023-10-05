using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Bucket;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel.Faturamento;

namespace WebZi.Plataform.Data.Services.Faturamento
{
    public class FaturamentoGuiaPagamentoReboqueEstadiaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FaturamentoGuiaPagamentoReboqueEstadiaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GerarPagamentoReboqueEstadiaViewModel GetGuiaPagamentoReboqueEstadia(int FaturamentoId, int UsuarioId)
        {
            GerarPagamentoReboqueEstadiaViewModel ResultView = new();

            List<string> erros = new();

            if (FaturamentoId <= 0)
            {
                erros.Add("Identificador do Faturamento inválido");
            }

            if (UsuarioId <= 0)
            {
                erros.Add("Identificador do Usuário inválido");
            }

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

            FaturamentoModel Faturamento = _context.Faturamento
                .Include(i => i.TipoMeioCobranca)
                .Where(w => w.FaturamentoId == 909674)
                .AsNoTracking()
                .FirstOrDefault();

            if (Faturamento == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Faturamento não encontrado");

                return ResultView;
            }

            GrvModel Grv = _context.Grv
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
                .FirstOrDefault();

            if (!new GrvService(_context, _mapper).UserCanAccessGrv(Grv, UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized("Usuário sem permissão de acesso ao GRV ou GRV inexistente");

                return ResultView;
            }

            AtendimentoModel Atendimento = Grv.Atendimento;

            if (Faturamento.TipoMeioCobranca.DocumentoImpressao == null || !Faturamento.TipoMeioCobranca.DocumentoImpressao.Equals("GuiaPagamentoEstadiaReboque.rdlc"))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest($"Esse Faturamento está cadastrado em uma Forma de Pagamento que não está configurado para imprimir a Guia de Pagamento de Reboque e Estadia: {Faturamento.TipoMeioCobranca.Descricao}");

                return ResultView;
            }

            GerarPagamentoReboqueEstadiaViewModel GuiaPagamentoEstadiaReboque = new();

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

            GuiaPagamentoEstadiaReboque.DataHoraRemocao = Grv.DataHoraRemocao.ToString("dd/MM/yyyy HH:mm");

            GuiaPagamentoEstadiaReboque.DataHoraGuarda = Grv.DataHoraGuarda.ToString("dd/MM/yyyy HH:mm");

            GuiaPagamentoEstadiaReboque.DataGuarda = Grv.DataHoraGuarda.ToString("dd/MM/yyyy");

            GuiaPagamentoEstadiaReboque.HoraGuarda = Grv.DataHoraGuarda.ToString("HH:mm");

            GuiaPagamentoEstadiaReboque.EstacionamentoSetor = Grv.EstacionamentoSetor;

            GuiaPagamentoEstadiaReboque.EstacionamentoNumeroVaga = Grv.EstacionamentoNumeroVaga;

            GuiaPagamentoEstadiaReboque.NumeroChave = Grv.NumeroChave;

            GuiaPagamentoEstadiaReboque.ReboquistaNome = Grv.Reboquista.Nome;

            GuiaPagamentoEstadiaReboque.ReboquePlaca = Grv.Reboque.Placa;

            GuiaPagamentoEstadiaReboque.MarcaModelo = Grv.MarcaModelo.MarcaModelo;

            GuiaPagamentoEstadiaReboque.Cor = Grv.Cor.Cor;

            GuiaPagamentoEstadiaReboque.TipoVeiculo = Grv.TipoVeiculo.Descricao;

            // ATENDIMENTO
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

            // CLIENTE
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

            // DEPÓSITO
            GuiaPagamentoEstadiaReboque.DepositoNome = Grv.Deposito.Nome;

            GuiaPagamentoEstadiaReboque.DepositoEndereco = new EnderecoService(_context, _mapper)
                .FormatarEndereco(Grv.Deposito.Endereco, Grv.Deposito.NumeroEndereco, Grv.Deposito.ComplementoEndereco);

            GuiaPagamentoEstadiaReboque.DepositoMunicipio = StringHelper.TitleCase(Grv.Deposito.Endereco.Municipio);

            // DATA/HORA
            DateTime DataHoraAtual = new DepositoService(_context, _mapper)
                .GetDataHoraPorDeposito(Grv.Deposito.DepositoId);

            GuiaPagamentoEstadiaReboque.DataHoraAtual = DataHoraAtual.ToString("dd/MM/yyyy HH:mm");

            GuiaPagamentoEstadiaReboque.DataAtual = DataHoraAtual.ToString("dd/MM/yyyy");

            GuiaPagamentoEstadiaReboque.HoraAtual = DataHoraAtual.ToString("HH:mm");

            GuiaPagamentoEstadiaReboque.DataHoraAtualDateTime = DataHoraAtual;

            GuiaPagamentoEstadiaReboque.Logo = GetLogomarca(Grv.ClienteId);

            GuiaPagamentoEstadiaReboque.Mensagem = MensagemViewHelper.GetOk("Guia de Pagamento de Reboque e Estadia gerado com sucesso");

            return GuiaPagamentoEstadiaReboque;
        }

        public byte[] GetLogomarca(int ClienteId)
        {
            byte[] Logomarca = new BucketArquivoService(_context)
                .DownloadFile("CADLOGOCLIENTE", ClienteId);

            return Logomarca ?? null;
        }
    }
}