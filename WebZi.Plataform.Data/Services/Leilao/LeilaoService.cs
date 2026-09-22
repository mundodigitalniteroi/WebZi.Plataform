using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Globalization;
using WebZi.Plataform.CrossCutting.Configuration;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.CrossCutting.Documents;
using WebZi.Plataform.CrossCutting.Secutity;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Atendimento;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.DetranHub;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Data.Services.Report;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.Leilao;
using WebZi.Plataform.Domain.DTO.Report;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.Models.Arrematantes;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Leilao;
using WebZi.Plataform.Domain.Options;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel.Leilao;
using WebZi.Plataform.Domain.ViewModel.Liberacao;
using WebZi.Plataform.Domain.Views.Usuario;
using WebZi.Plataform.Domain.Views.Veiculos;
using Z.EntityFramework.Plus;

namespace WebZi.Plataform.Data.Services.Leilao
{
    public class LeilaoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<DetranHubOptions> _detranHubOptions;
        private readonly IServiceProvider _provider;
        public LeilaoService(AppDbContext context)
        {
            _context = context;
        }

        public LeilaoService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory,
            IOptions<DetranHubOptions> detranHubOptions)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _detranHubOptions = detranHubOptions;
        }

        public LeilaoService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory,
            IOptions<DetranHubOptions> detranHubOptions, IServiceProvider provider)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _detranHubOptions = detranHubOptions;
            _provider = provider;
        }


        public async Task<MensagemDTO> CadastrarArrematanteAsync(CadastrarArrematanteParameters parameters, CancellationToken ct)
        {
            MensagemDTO ResultView = new();

            GrvModel Grv = await _context.Grv
                .Include(x => x.StatusOperacao)
                .AsTracking()
                .FirstOrDefaultAsync(x => x.GrvId == parameters.IdentificadorProcesso, ct);

            if (parameters.IdentificadorProcesso >= 0 && Grv is null)
            {
                ResultView = MensagemViewHelper.SetBadRequest("Processo não encontrado.");
                return ResultView;
            }

            if (Grv.StatusOperacaoId is not "1" and not "3")
            {
                ResultView = MensagemViewHelper.SetBadRequest($"Grv não esta no status correto para cadastro de arrematante. Status Atual: {Grv.StatusOperacao.Descricao}, Status Necessário: Pré-Leilão");
                return ResultView;
            }
            if (parameters.DataLeilao < Grv.DataHoraGuarda)
            {
                ResultView = MensagemViewHelper.SetBadRequest("A data do leilão não pode ser anterior à data de guarda do veículo.");
                return ResultView;
            }
            ArrematantesModel arrematante = new()
            {
                GrvId = parameters.IdentificadorProcesso,
                NumeroProcesso = Grv.NumeroFormularioGrv,
                Nome = parameters.Nome,
                CpfCnpj = parameters.CpfCnpj,
                TelefoneCelular = parameters.TelefoneCelular,
                Email = parameters.Email,
                Logradouro = parameters.Logradouro,
                Numero = parameters.Numero,
                Complemento = parameters.Complemento,
                Bairro = parameters.Bairro,
                Cidade = parameters.Cidade,
                Estado = parameters.Estado,
                Cep = parameters.Cep,
                NomeLeilao = parameters.NomeLeilao,
                NumeroLote = parameters.NumeroLote,
                ValorArrematacao = parameters.ValorArrematacao,
                ValorTaxaAdministrativa = parameters.ValorTaxaAdministrativa,
                ValorOutrasTaxas = parameters.ValorOutrasTaxas,
                ValorComissao = parameters.ValorComissao,
                ValorTotal = parameters.ValorTotal,
                DataLeilao = parameters.DataLeilao,
                DataCadastro = DateTime.Now
            };
            try
            {
                await _context.Arrematantes.AddAsync(arrematante, ct);

                Grv.StatusOperacaoId = "3";
                Grv.DataAlteracao = DateTime.Now;

                await _context.SaveChangesAsync(ct);

                return MensagemViewHelper.SetCreateSuccess("Arrematante cadastrado com sucesso.");
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetBadRequest(ex.Message);
                return ResultView;
            }
        }

        public async Task<MensagemDTO> AtualizarArrematanteAsync(AtualizarArrematanteParameters parameters, int? usuarioId, CancellationToken ct)
        {
            var arrematante = await _context.Arrematantes
                .Include(x => x.Grv)
                .AsTracking()
                .FirstOrDefaultAsync(x =>
                    (parameters.IdentificadorArrematante > 0 && x.ArrematanteId == parameters.IdentificadorArrematante) ||
                    (parameters.IdentificadorProcesso.HasValue && parameters.IdentificadorProcesso.Value > 0 && x.GrvId == parameters.IdentificadorProcesso.Value), ct);

            if (arrematante == null)
            {
                return MensagemViewHelper.SetNotFound("Arrematante não encontrado.");
            }

            arrematante.Nome = parameters.Nome;
            arrematante.CpfCnpj = parameters.CpfCnpj;
            arrematante.TelefoneCelular = parameters.TelefoneCelular;
            arrematante.Email = parameters.Email;
            arrematante.Logradouro = parameters.Logradouro;
            arrematante.Numero = parameters.Numero;
            arrematante.Complemento = parameters.Complemento;
            arrematante.Bairro = parameters.Bairro;
            arrematante.Cidade = parameters.Cidade;
            arrematante.Estado = parameters.Estado;
            arrematante.Cep = parameters.Cep;
            arrematante.NomeLeilao = parameters.NomeLeilao;
            arrematante.NumeroLote = parameters.NumeroLote;
            arrematante.ValorArrematacao = parameters.ValorArrematacao;
            arrematante.ValorTaxaAdministrativa = parameters.ValorTaxaAdministrativa;
            arrematante.ValorOutrasTaxas = parameters.ValorOutrasTaxas;
            arrematante.ValorComissao = parameters.ValorComissao;
            arrematante.ValorTotal = parameters.ValorTotal;
            arrematante.DataLeilao = parameters.DataLeilao;

            try
            {
                await _context.SaveChangesAsync(ct);
                return MensagemViewHelper.SetUpdateSuccess("Arrematante atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return MensagemViewHelper.SetBadRequest(ex.Message);
            }
        }

        public async Task<MensagemDTO> DesvincularArrematanteAsync(int identificadorArrematante, int? usuarioId, CancellationToken ct)
        {
            var arrematante = await _context.Arrematantes
                .Include(x => x.Grv)
                .AsTracking()
                .FirstOrDefaultAsync(x => x.ArrematanteId == identificadorArrematante, ct);

            if (arrematante == null)
            {
                return MensagemViewHelper.SetNotFound("Arrematante não encontrado.");
            }

            if (arrematante.Grv != null)
            {
                if (arrematante.Grv.StatusOperacaoId == "3" && arrematante.Grv.StatusOperacaoId == "6")
                {
                    arrematante.Grv.StatusOperacaoId = "1";
                }
                arrematante.Grv.UsuarioAlteracaoId = usuarioId;
                arrematante.Grv.DataAlteracao = DateTime.Now;
            }

            _context.Arrematantes.Remove(arrematante);

            try
            {

                if (arrematante != null && arrematante.Grv.StatusOperacaoId == "3" && arrematante.Grv.StatusOperacaoId == "6" && arrematante.Grv.StatusOperacaoId == "7")
                    await _provider.GetService<AtendimentoService>().DeleteAtendimentoAsync(arrematante.Grv.NumeroFormularioGrv, usuarioId.Value, arrematante.Grv.ClienteId);

                await _context.SaveChangesAsync(ct);
                return MensagemViewHelper.SetDeleteSuccess("Arrematante desvinculado e excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return MensagemViewHelper.SetBadRequest(ex.Message);
            }
        }

        public async Task<MensagemDTO> DesvincularLeilaoAsync(int identificadorProcesso, int? usuarioId, CancellationToken ct)
        {
            var grv = await _context.Grv
                .Include(x => x.StatusOperacao)
                .AsTracking()
                .FirstOrDefaultAsync(x => x.GrvId == identificadorProcesso, ct);

            if (grv == null)
            {
                return MensagemViewHelper.SetNotFound("Processo não encontrado.");
            }

            var arrematante = await _context.Arrematantes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == identificadorProcesso, ct);

            if (arrematante != null)
            {
                _context.Arrematantes.Remove(arrematante);
            }


            grv.StatusOperacaoId = "V";
            grv.UsuarioAlteracaoId = usuarioId;
            grv.DataAlteracao = DateTime.Now;

            try
            {
                if (arrematante != null && arrematante.Grv.StatusOperacaoId == "3" && arrematante.Grv.StatusOperacaoId == "6" && arrematante.Grv.StatusOperacaoId == "7")
                    await _provider.GetService<AtendimentoService>().DeleteAtendimentoAsync(grv.NumeroFormularioGrv, usuarioId.Value, grv.ClienteId);

                await _context.SaveChangesAsync(ct);
                return MensagemViewHelper.SetUpdateSuccess("Processo desvinculado do leilão com sucesso.");
            }
            catch (Exception ex)
            {
                return MensagemViewHelper.SetBadRequest(ex.Message);
            }
        }

        public async Task<SelecionarArrematanteDTO> GetArrematantePorProcessoAsync(int identificadorProcesso, CancellationToken ct)
        {
            if (identificadorProcesso <= 0)
            {
                return new SelecionarArrematanteDTO
                {
                    Mensagem = MensagemViewHelper.SetBadRequest("Identificador do processo inválido.")
                };
            }

            var grv = await _context.Grv
                .Include(x => x.StatusOperacao)
                .Include(x => x.Cor)
                .Include(x => x.MarcaModelo)
                .Include(x => x.TipoVeiculo)
                .Include(x => x.Cliente)
                .Include(x => x.Deposito)
                .Include(x => x.Arrematante)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == identificadorProcesso, ct);

            if (grv == null)
            {
                return new SelecionarArrematanteDTO
                {
                    Mensagem = MensagemViewHelper.SetNotFound("Processo não encontrado.")
                };
            }

            var statusPermitidos = new[] { "1", "3", "7", "6" };
            if (string.IsNullOrEmpty(grv.StatusOperacaoId) || !statusPermitidos.Contains(grv.StatusOperacaoId))
            {
                return new SelecionarArrematanteDTO
                {
                    Mensagem = MensagemViewHelper.SetBadRequest($"O processo não está em status válido para consulta de arrematante (Status permitidos: 1, 3, 6 ou 7). Status atual: {grv.StatusOperacao?.Descricao}")
                };
            }

            var resultView = new SelecionarArrematanteDTO
            {
                Processo = MapProcesso(grv),
                Veiculo = MapVeiculo(grv),
                Mensagem = MensagemViewHelper.SetFound()
            };

            if (grv.Arrematante != null)
            {
                resultView.Arrematante = MapArrematante(grv.Arrematante);
            }
            else
            {
                var leilaoArrematante = await _context.LeilaoArrematante
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.IdGrv == identificadorProcesso, ct);

                if (leilaoArrematante != null)
                {
                    resultView.Arrematante = MapArrematante(leilaoArrematante);
                }
            }

            await ConsultaVeiculoDetranHubAsync(resultView.Veiculo);

            return resultView;
        }

        private static ArrematanteProcessoDTO MapProcesso(GrvModel grv) => new()
        {
            IdentificadorProcesso = grv.GrvId,
            NumeroProcesso = grv.NumeroFormularioGrv,
            StatusOperacaoId = grv.StatusOperacaoId,
            StatusOperacaoDescricao = grv.StatusOperacao?.Descricao
        };

        private static ArrematanteVeiculoDTO MapVeiculo(GrvModel grv) => new()
        {
            Placa = grv.Placa,
            PlacaOstentada = grv.PlacaOstentada,
            Chassi = grv.Chassi,
            Renavam = grv.Renavam,
            MarcaModelo = grv.MarcaModelo?.MarcaModelo,
            Cor = grv.Cor?.Cor,
            TipoVeiculo = grv.TipoVeiculo?.Descricao,
            VeiculoUF = grv.VeiculoUF,
            DataHoraGuarda = grv.DataHoraGuarda,
            ClienteNome = grv.Cliente?.Nome,
            DepositoNome = grv.Deposito?.Nome,
            DepositoEndereco = !string.IsNullOrEmpty(grv.Deposito?.EnderecoMob)
                ? grv.Deposito.EnderecoMob
                : (grv.Deposito != null ? $"{grv.Deposito.Logradouro}{(string.IsNullOrEmpty(grv.Deposito.NumeroEndereco) ? "" : $", {grv.Deposito.NumeroEndereco}")}" : null),
            DepositoTelefone = grv.Deposito?.TelefoneMob
        };

        private static ArrematanteDadosDTO MapArrematante(ArrematantesModel a) => new()
        {
            IdentificadorArrematante = a.ArrematanteId,
            Nome = a.Nome,
            CpfCnpj = a.CpfCnpj,
            TelefoneFixo = null,
            TelefoneCelular = a.TelefoneCelular,
            Email = a.Email,
            Logradouro = a.Logradouro,
            Numero = a.Numero,
            Complemento = a.Complemento,
            Bairro = a.Bairro,
            Cidade = a.Cidade,
            Estado = a.Estado,
            Cep = a.Cep,
            DataCadastro = a.DataCadastro,
            Leilao = new ArrematanteLeilaoDTO
            {
                NomeLeilao = a.NomeLeilao,
                NumeroLote = a.NumeroLote,
                ValorArrematacao = a.ValorArrematacao,
                ValorTaxaAdministrativa = a.ValorTaxaAdministrativa,
                ValorOutrasTaxas = a.ValorOutrasTaxas,
                ValorComissao = a.ValorComissao,
                ValorTotal = a.ValorTotal,
                DataLeilao = a.DataLeilao
            }
        };

        private static ArrematanteDadosDTO MapArrematante(ViewLeilaoArremanteteModel a) => new()
        {
            Nome = a.ArrematanteNomeArrematante,
            CpfCnpj = a.ArrematanteCpfCnpj,
            TelefoneFixo = a.ArrematanteTelefoneFixo,
            TelefoneCelular = !string.IsNullOrWhiteSpace(a.ArrematanteTelefoneCelular)
                ? a.ArrematanteTelefoneCelular
                : a.ArrematanteTelefoneFixo,
            Email = a.ArrematanteEmail,
            Logradouro = a.ArrematanteLogradouro,
            Numero = a.ArrematanteNumero,
            Complemento = a.ArrematanteComplemento,
            Bairro = a.ArrematanteBairro,
            Cidade = a.ArrematanteCidade,
            Estado = a.ArrematanteEstado,
            Cep = a.ArrematanteCep,
            Leilao = new ArrematanteLeilaoDTO()
        };

        private async Task ConsultaVeiculoDetranHubAsync(ArrematanteVeiculoDTO veiculo)
        {
            if (veiculo == null || (veiculo.Placa.IsNullOrWhiteSpace() && veiculo.Chassi.IsNullOrWhiteSpace()))
                return;

            var detranHubService = _detranHubOptions != null
                ? new DetranHubService(_httpClientFactory, _mapper, _detranHubOptions)
                : (_httpClientFactory != null && _mapper != null ? new DetranHubService(_httpClientFactory, _mapper) : null);

            if (detranHubService == null)
                return;

            string placa = veiculo.Placa.IsPlaca() ? veiculo.Placa : null;
            string chassi = placa == null ? veiculo.Chassi : null;

            var detranHubResult = await detranHubService.SearchToPlateOrChassi(placa, chassi);
            var veiculoHub = detranHubResult?.Veiculo;
            if (veiculoHub == null)
                return;

            veiculo.AnoFabricacao ??= veiculoHub.AnoFabricacao?.ToString();
            veiculo.AnoModelo ??= veiculoHub.AnoModelo?.ToString();

            if (!string.IsNullOrWhiteSpace(veiculoHub.MarcaModelo))
                veiculo.MarcaModelo = veiculoHub.MarcaModelo;

            if (!string.IsNullOrWhiteSpace(veiculoHub.CorPrimaria))
                veiculo.Cor = veiculoHub.CorPrimaria;

            if (!string.IsNullOrWhiteSpace(veiculoHub.Renavam) && string.IsNullOrWhiteSpace(veiculo.Renavam))
                veiculo.Renavam = veiculoHub.Renavam;

            if (!string.IsNullOrWhiteSpace(veiculoHub.Chassi) && string.IsNullOrWhiteSpace(veiculo.Chassi))
                veiculo.Chassi = veiculoHub.Chassi;

            if (!string.IsNullOrWhiteSpace(veiculoHub.TipoVeiculo) && string.IsNullOrWhiteSpace(veiculo.TipoVeiculo))
                veiculo.TipoVeiculo = veiculoHub.TipoVeiculo;

            if (!string.IsNullOrWhiteSpace(veiculoHub.Uf) && string.IsNullOrWhiteSpace(veiculo.VeiculoUF))
                veiculo.VeiculoUF = veiculoHub.Uf;
        }


        public async Task<GuiaDeclaracaoRetiradaLeilaoDTO> CreateDeclaracaoRetiradaAsync(int GrvId, int UsuarioId, CancellationToken ct)
        {
            GuiaDeclaracaoRetiradaLeilaoDTO ResultView = new()
            {
                Mensagem = new GrvService(_context)
                    .ValidateInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await _context.Grv
                .Include(x => x.TipoVeiculo)
                .Include(x => x.StatusOperacao)
                .Include(x => x.Cliente)
                    .ThenInclude(x => x.Endereco)
                .Include(x => x.Deposito)
                    .ThenInclude(x => x.Endereco)
                .Include(x => x.Cor)
                .Include(x => x.MarcaModelo)
                .Include(x => x.Atendimento)
                .Include(x => x.Liberacao)
                .Include(x => x.Arrematante)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvId, cancellationToken: ct);

            if (Grv == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Processo não encontrado.");
                return ResultView;
            }

            if (Grv.StatusOperacaoId is not "6" and not "7")
            {
                ResultView.Mensagem = MensagemViewHelper
                    .SetBadRequest(
                        $"O Status atual deste Processo não permite a geração do Documento. Status atual: {Grv.StatusOperacao?.Descricao}");

                return ResultView;
            }

            var culturaPtBr = CultureInfo.GetCultureInfo("pt-BR");
            DateTime dataAtual = DateTime.Now;

            ResultView.IdentificadorProcesso = Grv.GrvId;
            ResultView.NumeroProcesso = Grv.NumeroFormularioGrv ?? string.Empty;
            ResultView.Titulo = "DECLARAÇÃO DE RETIRADA DE VEÍCULO ARREMATADO";

            ResultView.ClienteNome = Grv.Cliente?.Nome ?? string.Empty;
            if (Grv.Cliente?.Endereco != null)
            {
                ResultView.ClienteEndereco = new EnderecoService()
                    .FormatarEndereco(Grv.Cliente.Endereco, Grv.Cliente.NumeroEndereco, Grv.Cliente.ComplementoEndereco);

                if (!string.IsNullOrWhiteSpace(Grv.Cliente.Endereco.CEP))
                {
                    ResultView.ClienteEndereco += $". CEP {Grv.Cliente.Endereco.CEP}";
                }
            }
            else
            {
                ResultView.ClienteEndereco = string.Empty;
            }

            ResultView.DataEmissao = dataAtual.ToString("dd/MM/yyyy");
            ResultView.HoraEmissao = dataAtual.ToString("HH:mm:ss");
            ResultView.DataHoraEmissao = dataAtual.ToString("dd/MM/yyyy HH:mm:ss");

            if (Grv.Arrematante != null)
            {
                string respNome = Grv.Arrematante.Nome ?? string.Empty;
                string respDoc = Grv.Arrematante.CpfCnpj ?? string.Empty;
                string depositoNome = Grv.Deposito?.Nome?.ToUpper() ?? string.Empty;

                string numeroLote = Grv.Arrematante.NumeroLote ?? string.Empty;

                DateTime dataRetirada = Grv.Liberacao?.DataCadastro ?? dataAtual;
                string horaRetiradaStr = dataRetirada.ToString("HH\\hmm");
                string dataRetiradaExtenso = dataRetirada.ToString("dd 'DE' MMMM 'DE' yyyy", culturaPtBr).ToUpper();

                string dataLeilaoStr = string.Empty;
                if (Grv.Arrematante.DataLeilao != null)
                {
                    dataLeilaoStr = Grv.Arrematante.DataLeilao.Value.ToString("dd/MM/yyyy");
                }

                ResultView.TextoDeclaracaoRetirada1 =
                    $"Eu {respNome}, portador(a) do CPF {respDoc}, declaro que às {horaRetiradaStr} do dia " +
                    $"{dataRetiradaExtenso} retirei do Depósito {depositoNome} o veículo, conforme descrito abaixo, referente  " +
                    $"ao Lote nº {numeroLote}, do Leilão realizado no dia {dataLeilaoStr}.";

                ResultView.NumeroLote = numeroLote;

                var detranHubService = _detranHubOptions != null
                    ? new DetranHubService(_httpClientFactory, _mapper, _detranHubOptions)
                    : (_httpClientFactory != null && _mapper != null ? new DetranHubService(_httpClientFactory, _mapper) : null);

                if (detranHubService != null)
                {
                    string placa = Grv.Placa.IsPlaca() ? Grv.Placa : null;
                    string chassi = placa == null ? Grv.Chassi : null;

                    var detranHubResult = await detranHubService.SearchToPlateOrChassi(placa, chassi);

                    if (detranHubResult?.Veiculo != null)
                    {
                        var veiculoHub = detranHubResult.Veiculo;

                        if (!string.IsNullOrWhiteSpace(veiculoHub.MarcaModelo))
                            ResultView.VeiculoMarcaModelo = veiculoHub.MarcaModelo;

                        if (!string.IsNullOrWhiteSpace(veiculoHub.Placa))
                            ResultView.VeiculoPlaca = VeiculoHelper.FormatPlaca(veiculoHub.Placa);

                        if (!string.IsNullOrWhiteSpace(veiculoHub.Renavam))
                            ResultView.VeiculoRenavam = veiculoHub.Renavam;

                        if (!string.IsNullOrWhiteSpace(veiculoHub.Chassi))
                            ResultView.VeiculoChassi = veiculoHub.Chassi;

                        if (!string.IsNullOrWhiteSpace(veiculoHub.CorPrimaria))
                            ResultView.VeiculoCor = veiculoHub.CorPrimaria;

                        if (veiculoHub.AnoFabricacao.HasValue)
                            ResultView.VeiculoAnoFabricacao = veiculoHub.AnoFabricacao.Value.ToString();

                        if (veiculoHub.AnoModelo.HasValue)
                            ResultView.VeiculoAnoModelo = veiculoHub.AnoModelo.Value.ToString();
                    }
                }

                ResultView.VeiculoAno = !string.IsNullOrWhiteSpace(ResultView.VeiculoAnoFabricacao) && !string.IsNullOrWhiteSpace(ResultView.VeiculoAnoModelo)
                    ? (ResultView.VeiculoAnoFabricacao == ResultView.VeiculoAnoModelo ? ResultView.VeiculoAnoFabricacao : $"{ResultView.VeiculoAnoFabricacao}/{ResultView.VeiculoAnoModelo}")
                    : (!string.IsNullOrWhiteSpace(ResultView.VeiculoAnoFabricacao) ? ResultView.VeiculoAnoFabricacao : ResultView.VeiculoAnoModelo);

                ResultView.VeiculoMarcaModelo ??= Grv.MarcaModelo?.MarcaModelo;
                ResultView.VeiculoPlaca ??= Grv.Placa;
                ResultView.VeiculoRenavam ??= Grv.Renavam;
                ResultView.VeiculoChassi ??= Grv.Chassi;
                ResultView.VeiculoCor ??= Grv.Cor?.Cor;

                ResultView.GrvEstacionamentoSetor = Grv.EstacionamentoSetor ?? string.Empty;
                ResultView.GrvEstacionamentoNumeroVaga = Grv.EstacionamentoNumeroVaga ?? string.Empty;
                ResultView.GrvNumeroChave = Grv.NumeroChave ?? string.Empty;

                string valorArrematacaoStr = Grv.Arrematante.ValorTotal ?? string.Empty;
                ResultView.ValorArrematacao = valorArrematacaoStr;

                if (!string.IsNullOrWhiteSpace(valorArrematacaoStr))
                {
                    ResultView.TextoDeclaracaoRetirada2 = $"Declaro ter arrematado o Lote pelo valor de R$ {valorArrematacaoStr}.";
                }
                else
                {
                    ResultView.TextoDeclaracaoRetirada2 = "Declaro ter arrematado o Lote.";
                }

                ResultView.TextoDeclaracaoRetirada3 =
                    "Declaro também que, no ato da retirada do veículo no Depósito, recebi os seguintes documentos:\n\n" +
                    "- Nota Fiscal,\n\n" +
                    "- Auto de Leilão";

                string cidade = Grv.Deposito?.Endereco?.Municipio ?? "";
                ResultView.CidadeData = $"{cidade.ToUpper()}, {dataAtual.ToString("dd 'DE' MMMM 'DE' yyyy", culturaPtBr).ToUpper()}";

                ResultView.ProprietarioProcurador = respNome;
                ResultView.ProprietarioCpf = respDoc;
            }
            else
            {
                var liberacaoLeilao = await _context.ViewLiberacaoLeilao
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.IdGrv == GrvId, cancellationToken: ct);

                if (liberacaoLeilao != null)
                {
                    if (!string.IsNullOrWhiteSpace(liberacaoLeilao.Nome))
                        ResultView.ClienteNome = liberacaoLeilao.Nome;

                    if (!string.IsNullOrWhiteSpace(liberacaoLeilao.EnderecoCompleto))
                        ResultView.ClienteEndereco = liberacaoLeilao.EnderecoCompleto;

                    ResultView.NumeroProcesso = !string.IsNullOrWhiteSpace(liberacaoLeilao.Processo) ? liberacaoLeilao.Processo : ResultView.NumeroProcesso;
                    ResultView.TextoDeclaracaoRetirada1 = liberacaoLeilao.Mensagem1;
                    ResultView.NumeroLote = liberacaoLeilao.CodigoLote;
                    ResultView.VeiculoMarcaModelo = liberacaoLeilao.MarcaModelo ?? Grv.MarcaModelo?.MarcaModelo;
                    ResultView.VeiculoPlaca = liberacaoLeilao.Placa ?? Grv.Placa;
                    ResultView.VeiculoRenavam = liberacaoLeilao.Renavam ?? Grv.Renavam;
                    ResultView.VeiculoChassi = liberacaoLeilao.Chassi ?? Grv.Chassi;
                    ResultView.VeiculoCor = liberacaoLeilao.Cor ?? Grv.Cor?.Cor;
                    ResultView.VeiculoAno = liberacaoLeilao.Ano;
                    ResultView.GrvEstacionamentoSetor = liberacaoLeilao.GrvEstacionamentoSetor ?? Grv.EstacionamentoSetor ?? string.Empty;
                    ResultView.GrvEstacionamentoNumeroVaga = liberacaoLeilao.GrvEstacionamentoNumeroVaga ?? Grv.EstacionamentoNumeroVaga ?? string.Empty;
                    ResultView.GrvNumeroChave = liberacaoLeilao.GrvNumeroChave ?? Grv.NumeroChave ?? string.Empty;
                    ResultView.TextoDeclaracaoRetirada2 = liberacaoLeilao.Mensagem2;

                    var docs = new List<string>();
                    if (!string.IsNullOrWhiteSpace(liberacaoLeilao.Mensagem3)) docs.Add(liberacaoLeilao.Mensagem3);
                    if (!string.IsNullOrWhiteSpace(liberacaoLeilao.Mensagem4)) docs.Add(liberacaoLeilao.Mensagem4);
                    if (!string.IsNullOrWhiteSpace(liberacaoLeilao.Mensagem5)) docs.Add(liberacaoLeilao.Mensagem5);

                    ResultView.TextoDeclaracaoRetirada3 = docs.Count > 0
                        ? string.Join("\n\n", docs)
                        : "Declaro também que, no ato da retirada do veículo no Depósito, recebi os seguintes documentos:\n\n- Nota Fiscal,\n\n- Auto de Leilão";

                    ResultView.CidadeData = !string.IsNullOrWhiteSpace(liberacaoLeilao.Mensagem6)
                        ? liberacaoLeilao.Mensagem6
                        : $"{Grv.Deposito?.Endereco?.Municipio?.ToUpper() ?? "RIO DE JANEIRO"}, {dataAtual.ToString("dd 'DE' MMMM 'DE' yyyy", culturaPtBr).ToUpper()}";

                    ResultView.ProprietarioProcurador = liberacaoLeilao.ArrematanteNomeArrematante?.Replace("Proprietário/Procurador: ", "").Trim() ?? (Grv.Atendimento?.ResponsavelNome ?? "");
                    ResultView.ProprietarioCpf = liberacaoLeilao.ArrematanteCpfCnpj?.Replace("CPF: ", "").Trim() ?? (Grv.Atendimento?.ResponsavelDocumento ?? "");
                }
            }

            ViewUsuarioModel Usuario = await _context.ViewUsuario
                .FirstOrDefaultAsync(x => x.UsuarioId == UsuarioId, cancellationToken: ct);

            if (Usuario != null)
            {
                ResultView.UsuarioNome = Usuario.NomeCompleto;
                ResultView.UsuarioMatricula = Usuario.Matricula;
                ResultView.UsuarioCpf = !string.IsNullOrWhiteSpace(Usuario.CpfFormatado)
                    ? Usuario.CpfFormatado
                    : (!string.IsNullOrWhiteSpace(Usuario.Cpf) && Usuario.Cpf.Length == 11 ? DocumentHelper.FormatCPF(Usuario.Cpf) : Usuario.Matricula);
            }

            ResultView.Mensagem = MensagemViewHelper.SetOk(ResultView.Mensagem, "Documento gerado com sucesso");

            return ResultView;
        }


        public async Task<MensagemDTO> ChangeStatusPreLeilaoAsync(IngressarLoteParameters parameters, int IdentificadorUsuario, CancellationToken ct)
        {
            var temIds = parameters.IdentificadoresProcesso != null && parameters.IdentificadoresProcesso.Count > 0;
            var temNumeros = parameters.NumerosDeProcesso != null && parameters.NumerosDeProcesso.Count > 0;

            if (!temIds && !temNumeros)
            {
                return MensagemViewHelper.SetBadRequest("Nenhum processo informado. Forneça IdentificadoresProcesso ou NumerosDeProcesso.");
            }

            var targetGrvIds = new HashSet<int>();

            if (temIds)
            {
                var resultPorId = await _context.Grv
                    .Select(x => new
                    {
                        x.GrvId,
                        x.NumeroFormularioGrv,
                        x.Placa,
                        x.UsuarioAlteracaoId,
                        x.DataAlteracao,
                        x.StatusOperacaoId
                    })
                    .Where(x => parameters.IdentificadoresProcesso.Contains(x.GrvId))
                    .ToListAsync(ct);

                var idsNaoEncontrados = parameters.IdentificadoresProcesso
                    .Where(id => !resultPorId.Select(x => x.GrvId).Contains(id))
                    .ToList();

                if (idsNaoEncontrados.Count > 0)
                {
                    return MensagemViewHelper.SetBadRequest(
                        $"Os seguintes IDs de processo não foram encontrados: {string.Join(", ", idsNaoEncontrados)}"
                    );
                }

                var idsJaEmPreLeilao = resultPorId
                    .Where(x => x.StatusOperacaoId == "1")
                    .Select(x => x.GrvId)
                    .ToList();

                if (idsJaEmPreLeilao.Count > 0)
                {
                    return MensagemViewHelper.SetBadRequest(
                        $"Os seguintes IDs de processo já estão em pré-leilão: {string.Join(", ", idsJaEmPreLeilao)}"
                    );
                }

                var idsStatusIncorretos = resultPorId
                    .Where(x => x.StatusOperacaoId != "V")
                    .Select(x => x.GrvId)
                    .ToList();

                if (idsStatusIncorretos.Count > 0)
                {
                    return MensagemViewHelper.SetBadRequest(
                        $"Os seguintes IDs de processo não estão no status necessário (V) para inserção ao pré leilão: {string.Join(", ", idsStatusIncorretos)}"
                    );
                }

                foreach (var item in resultPorId)
                {
                    targetGrvIds.Add(item.GrvId);
                }
            }

            if (temNumeros)
            {
                var resultPorNumero = await _context.Grv
                    .Select(x => new
                    {
                        x.GrvId,
                        x.NumeroFormularioGrv,
                        x.Placa,
                        x.UsuarioAlteracaoId,
                        x.DataAlteracao,
                        x.StatusOperacaoId
                    })
                    .Where(x => parameters.NumerosDeProcesso.Contains(x.NumeroFormularioGrv))
                    .ToListAsync(ct);

                var gruposDuplicados = resultPorNumero
                    .GroupBy(x => x.NumeroFormularioGrv)
                    .Where(g => g.Count() > 1)
                    .ToList();

                if (gruposDuplicados.Count > 0)
                {
                    var listaDuplicados = gruposDuplicados.Select(g =>
                    {
                        var detalhes = string.Join(", ", g.Select(x => $"[ID: {x.GrvId}, Placa: {(string.IsNullOrWhiteSpace(x.Placa) ? "Sem Placa" : x.Placa)}]"));
                        return $"Processo nº {g.Key}: {detalhes}";
                    });

                    return MensagemViewHelper.SetBadRequest(
                        $"Foram encontrados múltiplos GRVs com o mesmo número de processo. Para prosseguir, informe os Identificadores de Processo (IDs). Detalhes: {string.Join(" | ", listaDuplicados)}"
                    );
                }

                var processosNaoEncontrados = parameters.NumerosDeProcesso
                    .Where(p => !resultPorNumero.Select(x => x.NumeroFormularioGrv).Contains(p))
                    .ToList();

                if (processosNaoEncontrados.Count > 0)
                {
                    return MensagemViewHelper.SetBadRequest(
                        $"Os seguintes processos não foram encontrados: {string.Join(", ", processosNaoEncontrados)}"
                    );
                }

                var processosJaEmPreLeilao = resultPorNumero
                    .Where(x => x.StatusOperacaoId == "1")
                    .Select(x => x.NumeroFormularioGrv)
                    .Distinct()
                    .ToList();

                if (processosJaEmPreLeilao.Count > 0)
                {
                    return MensagemViewHelper.SetBadRequest(
                        $"Os seguintes processos já estão em pré-leilão: {string.Join(", ", processosJaEmPreLeilao)}"
                    );
                }

                var processosStatusIncorretos = resultPorNumero
                    .Where(x => x.StatusOperacaoId != "V")
                    .Select(x => x.NumeroFormularioGrv)
                    .Distinct()
                    .ToList();

                if (processosStatusIncorretos.Count > 0)
                {
                    return MensagemViewHelper.SetBadRequest(
                        $"Os seguintes processos não estão no status necessário (V) para inserção ao pré leilão: {string.Join(", ", processosStatusIncorretos)}"
                    );
                }

                foreach (var item in resultPorNumero)
                {
                    targetGrvIds.Add(item.GrvId);
                }
            }

            if (targetGrvIds.Count == 0)
            {
                return MensagemViewHelper.SetBadRequest("Nenhum processo válido encontrado para atualização.");
            }

            try
            {
                var listaIds = targetGrvIds.ToList();

                await _context.Grv
                    .Where(x => listaIds.Contains(x.GrvId))
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(o => o.StatusOperacaoId, "1")
                        .SetProperty(o => o.UsuarioAlteracaoId, IdentificadorUsuario)
                        .SetProperty(o => o.DataAlteracao, DateTime.Now),
                        ct);

                return MensagemViewHelper.SetUpdateSuccess(listaIds.Count == 1
                    ? "Lote inserido em Pré Leilão!"
                    : "Lote(s) inserido(s) em Pré Leilão!");
            }
            catch (Exception ex)
            {
                return MensagemViewHelper.SetBadRequest(ex.Message);
            }
        }
        public async Task<PreLeilaoListDTO> ListPreLeiloesAsync(ProcessosPreLeilaoParameters parameters)
        {
            var resultView = new PreLeilaoListDTO();

            var erros = ValidarParametros(parameters);
            if (erros.Count > 0)
            {
                resultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);
                return resultView;
            }

            if (!DateTime.TryParseExact(parameters.Data, "dd/MM/yyyy", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime dataBase))
            {
                resultView.Mensagem = MensagemViewHelper.SetBadRequest("A Data informada é inválida");
                return resultView;
            }

            var query = BuildEstoqueQuery(parameters, dataBase);

            var listagem = await query
                .Select(v => new PreLeilaoDTO
                {
                    NumeroFormularioGrv = v.NumeroFormularioGrv,
                    Placa = v.Placa,
                    Chassi = v.Chassi,
                    Renavam = v.Renavam,
                    MarcaModelo = v.MarcaModelo,
                    TipoVeiculo = v.TipoVeiculo,
                    Cor = v.Cor,
                    FlagComboio = v.FlagComboio,
                    DataHoraRemocao = v.DataHoraRemocao,
                    DataHoraGuarda = v.DataHoraGuarda,
                    IdStatusOperacao = v.Status.ToString(),
                    IdGrv = v.IdGrv,
                    IdTarifaTipoVeiculo = v.IdTarifaTipoVeiculo,
                    IdCliente = v.IdCliente,
                    IdDeposito = v.IdDeposito,
                    IdReboquista = v.IdReboquista,
                    IdReboque = v.IdReboque,
                    IdAutoridadeResponsavel = v.IdAutoridadeResponsavel,
                    IdCor = v.IdCor,
                    IdDetranMarcaModelo = v.IdDetranMarcaModelo,
                    DataCadastro = v.DataCadastro,
                    Municipio = v.Municipio,
                    Uf = v.Uf
                })
                .ToListAsync();

            if (listagem.Count == 0)
            {
                resultView.Mensagem =
                    MensagemViewHelper.SetNotFound("Nenhum processo encontrado para os filtros informados");
                resultView.Listagem = listagem;
                return resultView;
            }

            await EnriquecerComLeilaoAnterior(listagem, parameters.IdLeilao ?? 0);

            resultView.Listagem = listagem;
            resultView.Mensagem = MensagemViewHelper.SetFound(listagem.Count);

            return resultView;
        }


        private static List<string> ValidarParametros(ProcessosPreLeilaoParameters parameters)
        {
            var erros = new List<string>();

            if (parameters.ClienteId <= 0)
                erros.Add("O Identificador do Cliente é obrigatório");

            if (string.IsNullOrWhiteSpace(parameters.Data))
                erros.Add("A Data é obrigatória");

            if (parameters.IdLeilao == null || parameters.IdLeilao <= 0)
                erros.Add("O Identificador do Leilão é obrigatório");

            if (parameters.Sobra == 1)
            {
                if (parameters.Leiloes == null || !parameters.Leiloes.Any(x => !string.IsNullOrWhiteSpace(x)))
                    erros.Add("Ao menos um Leilão deve ser informado quando Sobra for 1");

                if (parameters.Leiloes?.Any(x => !string.IsNullOrWhiteSpace(x)) == true)
                {
                    if (parameters.StatusLote == null || !parameters.StatusLote.Any(s => !string.IsNullOrWhiteSpace(s)))
                        erros.Add("Ao menos um Status de Lote deve ser informado quando Leilões forem informados");
                }
            }

            return erros;
        }

        private IQueryable<ViewEstoqueVeiculosModel> BuildEstoqueQuery(
            ProcessosPreLeilaoParameters parameters,
            DateTime dataBase)
        {
            var statusPermitidos = new[] { "G", "V", "L", "T", "1", "4" };

            var query = _context.Set<ViewEstoqueVeiculosModel>()
                .AsNoTracking()
                .Where(x => x.IdCliente == parameters.ClienteId &&
                            statusPermitidos.Contains(x.Status));

            if (parameters.DepositosIds?.Any() == true)
                query = query.Where(x => parameters.DepositosIds.Contains(x.IdDeposito));

            if (!string.IsNullOrWhiteSpace(parameters.NumeroProcesso))
                query = query.Where(x => x.NumeroFormularioGrv == parameters.NumeroProcesso);

            if (parameters.NumDiasPatio > 0)
            {
                var dataLimite = dataBase.Date.AddDays(-parameters.NumDiasPatio);
                query = query.Where(x => x.DataHoraGuarda != null &&
                                         x.DataHoraGuarda.Value.Date <= dataLimite);
            }

            if (parameters.NumDiasLeilao > 0)
            {
                var dataLimiteLeilao = dataBase.Date.AddDays(-parameters.NumDiasLeilao);
                query = query.Where(x => x.DataHoraGuarda != null &&
                                         x.DataHoraGuarda.Value.Date <= dataLimiteLeilao);
            }

            query = parameters.Sobra == 1
                ? AplicarFiltroSobra(query, parameters)
                : AplicarFiltroSemLeilao(query);

            query = query.OrderBy(x => x.NumeroFormularioGrv);

            if (parameters.NumLotes > 0)
                query = query.Take(parameters.NumLotes);

            return query;
        }

        private IQueryable<ViewEstoqueVeiculosModel> AplicarFiltroSobra(
            IQueryable<ViewEstoqueVeiculosModel> query,
            ProcessosPreLeilaoParameters parameters)
        {
            var lotQuery = _context.LeilaoLote
                .Where(l => l.LeilaoLoteStatus.FlagReaproveitavel == "S");

            if (parameters.Leiloes?.Any() == true)
            {
                var validLeiloes = parameters.Leiloes
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToList();

                lotQuery = lotQuery.Where(l => validLeiloes.Contains(l.Leilao.Descricao));
            }

            var validStatus = parameters.StatusLote?
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            if (validStatus?.Any() == true)
                lotQuery = lotQuery.Where(l => validStatus.Contains(l.LeilaoLoteStatus.Descricao));

            return query.Where(v => lotQuery.Any(l => l.GrvId == v.IdGrv));
        }

        private IQueryable<ViewEstoqueVeiculosModel> AplicarFiltroSemLeilao(
            IQueryable<ViewEstoqueVeiculosModel> query)
        {
            // GRVs que nunca foram para leilão
            return query.Where(v => !_context.LeilaoLote.Any(l => l.GrvId == v.IdGrv));
        }

        private async Task EnriquecerComLeilaoAnterior(List<PreLeilaoDTO> listagem, int idLeilaoAtual)
        {
            var grvIds = listagem.Select(x => x.IdGrv).ToList();

            var allLots = await _context.LeilaoLote
                .Include(l => l.Leilao)
                .Include(l => l.LeilaoLoteStatus)
                .Where(l => grvIds.Contains((int)l.GrvId) && l.LeilaoId != idLeilaoAtual)
                .AsNoTracking()
                .ToListAsync();

            var lastLotsByGrv = allLots
                .GroupBy(l => l.GrvId)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderByDescending(l =>
                            DateTime.TryParseExact(l.Leilao.DataLeilao, "dd/MM/yyyy",
                                CultureInfo.InvariantCulture, DateTimeStyles.None, out var d)
                                ? d
                                : DateTime.MinValue)
                        .ThenByDescending(l => l.LeilaoLoteId)
                        .First()
                );

            foreach (var dto in listagem)
            {
                if (!lastLotsByGrv.TryGetValue(dto.IdGrv, out var lastLot))
                    continue;

                dto.IdLeilaoAnterior = lastLot.LeilaoId;
                dto.IdLoteAnterior = lastLot.LeilaoLoteId;
                dto.DescLeilaoAnterior = lastLot.Leilao.Descricao;
                dto.DescStatusLoteAnterior = lastLot.LeilaoLoteStatus.Descricao;

                if (DateTime.TryParseExact(lastLot.Leilao.DataLeilao, "dd/MM/yyyy",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out var dataLeilao))
                {
                    dto.DataLeilaoAnterior = dataLeilao;
                }
            }
        }

        public async Task<MensagemDTO> GetAvisosLeilaoAsync(int GrvId, string StatusOperacaoId)
        {
            if (!new[] { "V", "L", "T", "1", "2", "4" }.Contains(StatusOperacaoId))
            {
                return null;
            }

            LeilaoLoteModel LeilaoLote = await _context.LeilaoLote
                .Include(x => x.LeilaoLoteStatus)
                .Include(x => x.Leilao)
                .Include(x => x.Leilao.LeilaoStatus)
                .Include(x => x.Grv)
                .OrderByDescending(x => x.Leilao.DataLeilao.Substring(6, 4) +
                                        x.Leilao.DataLeilao.Substring(3, 2) +
                                        x.Leilao.DataLeilao.Substring(0, 2))
                .ThenByDescending(x => x.LeilaoLoteId)
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.GrvId == GrvId);

            MensagemDTO mensagem = new();

            if (LeilaoLote != null)
            {
                DateTime DataHoraPorDeposito = new DepositoService(_context)
                    .GetDataHoraPorDeposito(LeilaoLote.Grv.DepositoId);

                DateTime dataLeilao = DateTime.ParseExact(LeilaoLote.Leilao.DataLeilao, "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);

                if (DataHoraPorDeposito.Date > dataLeilao.Date &&
                    LeilaoLote.Leilao.LeilaoStatus.Ativo != "I" &&
                    LeilaoLote.LeilaoLoteStatus.ValidaLote == "S")
                {
                    mensagem.AvisosImpeditivos.Add(
                        $"Este Processo está associado ao Leilão {LeilaoLote.Leilao.Descricao}, Data {dataLeilao:dd/MM/yyyy}, Lote {LeilaoLote.NumeroLote}");
                    mensagem.AvisosImpeditivos.Add("CANCELAR");
                }
                else if (LeilaoLote.Leilao.LeilaoStatus.Ativo != "I"
                         && LeilaoLote.LeilaoLoteStatus.ValidaLote == "S")
                {
                    if (new[] { "V", "1" }.Contains(StatusOperacaoId))
                    {
                        mensagem.AvisosImpeditivos.Add(
                            $"Este Processo está associado ao Leilão {LeilaoLote.Leilao.Descricao}, Data {dataLeilao:dd/MM/yyyy}, Lote {LeilaoLote.NumeroLote}, o veículo não pode ser atendido");
                        mensagem.AvisosImpeditivos.Add("CANCELAR_E_ENVIAR_EMAIL");
                    }
                    else if (new[] { "L", "T", "2", "4" }.Contains(StatusOperacaoId)
                             && (dataLeilao.Date - DataHoraPorDeposito.Date).TotalDays <= 1)
                    {
                        mensagem.AvisosImpeditivos.Add(
                            $"Este Processo está associado ao Leilão {LeilaoLote.Leilao.Descricao}, Data {dataLeilao:dd/MM/yyyy}, Lote {LeilaoLote.NumeroLote}, para dar prosseguimento a esta Liberação é necessário acionar a equipe do Leilões");
                        mensagem.AvisosImpeditivos.Add("CANCELAR");
                    }
                }

                if (mensagem.AvisosImpeditivos.Count > 0)
                {
                    return mensagem;
                }
            }

            mensagem.AvisosInformativos.Add("NAO_LEILAO");

            return mensagem;
        }
    }
}