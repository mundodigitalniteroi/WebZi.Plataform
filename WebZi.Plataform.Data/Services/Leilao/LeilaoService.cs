using Microsoft.EntityFrameworkCore;
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
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Data.Services.Faturamento;
using WebZi.Plataform.Data.Services.Report;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.Leilao;
using WebZi.Plataform.Domain.DTO.Report;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.Models.Arrematantes;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Leilao;
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

        public LeilaoService(AppDbContext context)
        {
            _context = context;
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

            if (Grv.StatusOperacaoId is not "1")
            {
                ResultView = MensagemViewHelper.SetBadRequest($"Grv não esta no status correto para cadastro de arrematante. {Grv.StatusOperacao.Descricao}");
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
                .FirstOrDefaultAsync(x => x.ArrematanteId == parameters.IdentificadorArrematante, ct);

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
                if (arrematante.Grv.StatusOperacaoId == "3")
                {
                    arrematante.Grv.StatusOperacaoId = "1";
                }
                arrematante.Grv.UsuarioAlteracaoId = usuarioId;
                arrematante.Grv.DataAlteracao = DateTime.Now;
            }

            _context.Arrematantes.Remove(arrematante);

            try
            {
                await _context.SaveChangesAsync(ct);
                return MensagemViewHelper.SetDeleteSuccess("Arrematante desvinculado e excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return MensagemViewHelper.SetBadRequest(ex.Message);
            }
        }

        public async Task<MensagemDTO> DesvincularGrvDoLeilaoAsync(int identificadorProcesso, int? usuarioId, CancellationToken ct)
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
                await _context.SaveChangesAsync(ct);
                return MensagemViewHelper.SetUpdateSuccess("Processo desvinculado do leilão com sucesso.");
            }
            catch (Exception ex)
            {
                return MensagemViewHelper.SetBadRequest(ex.Message);
            }
        }

        public async Task<SelecionarArrematanteDTO> SelecionarArrematantePorProcessoAsync(int identificadorProcesso, CancellationToken ct)
        {
            SelecionarArrematanteDTO resultView = new();

            if (identificadorProcesso <= 0)
            {
                resultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador do processo inválido.");
                return resultView;
            }

            var arrematante = await _context.Arrematantes
                .Include(x => x.Grv)
                .ThenInclude(x => x.Cor)
                .Include(x => x.Grv)
                .ThenInclude(x => x.MarcaModelo)
                .Where(x => x.GrvId == identificadorProcesso)
                .Select(x => new SelecionarArrematanteDTO
                {
                    Processo = new ArrematanteProcessoDTO
                    {
                        IdentificadorProcesso = x.GrvId,
                        NumeroProcesso = x.NumeroProcesso,
                        StatusOperacaoId = x.Grv.StatusOperacaoId,
                        StatusOperacaoDescricao = x.Grv.StatusOperacao.Descricao
                    },
                    Veiculo = new ArrematanteVeiculoDTO
                    {
                        Placa = x.Grv.Placa,
                        PlacaOstentada = x.Grv.PlacaOstentada,
                        Chassi = x.Grv.Chassi,
                        Renavam = x.Grv.Renavam,
                        MarcaModelo = x.Grv.MarcaModelo.MarcaModelo,
                        Cor = x.Grv.Cor.Cor,
                        TipoVeiculo = x.Grv.TipoVeiculo.Descricao,
                        VeiculoUF = x.Grv.VeiculoUF,
                        DataHoraGuarda = x.Grv.DataHoraGuarda,
                        ClienteNome = x.Grv.Cliente.Nome,
                        DepositoNome = x.Grv.Deposito.Nome,
                        DepositoEndereco = !string.IsNullOrEmpty(x.Grv.Deposito.EnderecoMob)
                            ? x.Grv.Deposito.EnderecoMob
                            : (x.Grv.Deposito.Logradouro + (string.IsNullOrEmpty(x.Grv.Deposito.NumeroEndereco) ? "" : ", " + x.Grv.Deposito.NumeroEndereco)),
                        DepositoTelefone = x.Grv.Deposito.TelefoneMob
                    },
                    Arrematante = new ArrematanteDadosDTO
                    {
                        IdentificadorArrematante = x.ArrematanteId,
                        Nome = x.Nome,
                        CpfCnpj = x.CpfCnpj,
                        TelefoneCelular = x.TelefoneCelular,
                        Email = x.Email,
                        Logradouro = x.Logradouro,
                        Numero = x.Numero,
                        Complemento = x.Complemento,
                        Bairro = x.Bairro,
                        Cidade = x.Cidade,
                        Estado = x.Estado,
                        Cep = x.Cep,
                        DataCadastro = x.DataCadastro,
                        Leilao = new ArrematanteLeilaoDTO
                        {
                            NomeLeilao = x.NomeLeilao,
                            NumeroLote = x.NumeroLote,
                            ValorArrematacao = x.ValorArrematacao,
                            ValorTaxaAdministrativa = x.ValorTaxaAdministrativa,
                            ValorOutrasTaxas = x.ValorOutrasTaxas,
                            ValorComissao = x.ValorComissao,
                            ValorTotal = x.ValorTotal,
                            DataLeilao = x.DataLeilao
                        }
                    }
                })
                .FirstOrDefaultAsync(ct);

            if (arrematante == null)
            {
                var grvExiste = await _context.Grv.AnyAsync(x => x.GrvId == identificadorProcesso, ct);
                if (!grvExiste)
                {
                    resultView.Mensagem = MensagemViewHelper.SetNotFound("Processo não encontrado.");
                    return resultView;
                }

                resultView.Mensagem = MensagemViewHelper.SetNotFound("Nenhum arrematante vinculado a este processo.");
                return resultView;
            }

            var statusValidos = new[] { "3", "7", "6" };

            if (string.IsNullOrEmpty(arrematante.Processo?.StatusOperacaoId) || !statusValidos.Contains(arrematante.Processo.StatusOperacaoId))
            {
                var statusDescricao = arrematante.Processo?.StatusOperacaoDescricao;
                resultView.Mensagem = MensagemViewHelper.SetBadRequest($"O processo não está em status válido para consulta de arrematante (Status permitidos: 3, 6 ou 7). Status atual: {statusDescricao}");
                return resultView;
            }

            if (arrematante.Veiculo != null)
            {
                var lote = await _context.LeilaoLote
                    .Where(l => l.GrvId == identificadorProcesso)
                    .OrderByDescending(l => l.LeilaoLoteId)
                    .Select(l => new { l.AnoFabricacao, l.AnoModelo })
                    .FirstOrDefaultAsync(ct);

                if (lote != null)
                {
                    arrematante.Veiculo.AnoFabricacao = lote.AnoFabricacao;
                    arrematante.Veiculo.AnoModelo = lote.AnoModelo;
                }
            }

            arrematante.Mensagem = MensagemViewHelper.SetFound();
            return arrematante;
        }


        public async Task<GuiaDeclaracaoRetiradaLeilaoDTO> CreateDeclaracaoRetirada(int GrvId, int UsuarioId, CancellationToken ct)
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
                .Include(x => x.Cor)
                .Include(x => x.MarcaModelo)
                .Include(x => x.Atendimento)
                .Include(x => x.Liberacao)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == GrvId, cancellationToken: ct);

            if (Grv.StatusOperacaoId is not "6" and not "7")
            {
                ResultView.Mensagem = MensagemViewHelper
                    .SetBadRequest(
                        $"O Status atual deste Processo não permite a geração do Documento. Status atual: {Grv.StatusOperacao?.Descricao}");

                return ResultView;
            }
            else if (Grv.StatusOperacaoId == "E")
            {
                if (Grv.Liberacao?.DataCadastro != null && DateTime.Now.Date > Grv.Liberacao.DataCadastro.Date)
                {
                    ResultView.Mensagem.Alertas
                        .Add(
                            $"Este Processo foi entregue em {Grv.Liberacao.DataCadastro:dd/MM/yyyy}, as informações impressas no Documento estão desatualizadas");
                }
            }

            int? FaturamentoId = await new FaturamentoService(_context).GetUltimoFaturamentoIdAsync(GrvId);

            ResultView.IdentificadorProcesso = Grv.GrvId;

            ResultView.NumeroProcesso = Grv.NumeroFormularioGrv;

            ResultView.ClienteNome = Grv?.Cliente.Nome ?? "";

            //ResultView.ClienteEndereco = Grv.Cliente.Endereco ?? "";


            //string depositoNome = GuiaPagamentoReboqueEstadia?.DepositoNome ?? Grv.Deposito?.Nome ?? "";
            //string numFormulario = GuiaPagamentoReboqueEstadia?.NumeroFormularioGrv ?? Grv.NumeroFormularioGrv ?? "";

            //ResultView.NumeroProcesso = $"Registro: {numFormulario}";



            //string respNome = GuiaPagamentoReboqueEstadia?.AtendimentoResponsavelNome ?? Grv.Atendimento?.ResponsavelNome ?? "Não informado";
            //string respDoc = GuiaPagamentoReboqueEstadia?.AtendimentoResponsavelDocumento ?? Grv.Atendimento?.ResponsavelDocumento ?? "Não informado";


            //string depositoEndereco = GuiaPagamentoReboqueEstadia?.DepositoEndereco ?? "";

            //ResultView.Titulo = "DECLARAÇÃO DE RETIRADA DE VEÍCULO ARREMATADO";

            //ResultView.TextoDeclaracaoRetirada1 =
            //                                    $"Eu {respNome}, portador(a) do CPF {respDoc}, declaro que às {DateTime.Now:HH:mm} do dia \n" +
            //                                    "31 DE JULHO DE 2026 retirei do Depósito SAO GONCALO o veículo, conforme descrito abaixo, referente \n" +
            //                                    "ao Lote nº 30, do Leilão realizado no dia 23/06/2026. ";

            //ResultView.VeiculoMarcaModelo = GuiaPagamentoReboqueEstadia?.MarcaModelo ?? Grv.MarcaModelo?.MarcaModelo ?? "";

            //ResultView.VeiculoPlaca = VeiculoHelper.FormatPlaca(GuiaPagamentoReboqueEstadia?.Placa ?? Grv.Placa ?? "");

            //ResultView.VeiculoRenavam = GuiaPagamentoReboqueEstadia?.Renavam ?? Grv.Renavam ?? "";

            //ResultView.VeiculoChassi = GuiaPagamentoReboqueEstadia?.Chassi ?? Grv.Chassi ?? "";

            //ResultView.VeiculoCor = GuiaPagamentoReboqueEstadia?.Cor ?? "";

            //ResultView.TextoDeclaracaoRetirada2 =
            //    $@"Eu {respNome}, portador do CPF {respDoc}, declaro que no dia {DateTime.Now.ToString("dd 'de' MMMM 'de' yyyy", CultureInfo.GetCultureInfo("pt-BR"))}, " +
            //    $"recebi do depósito {depositoNome} o veículo de placa {veicPlacaFormatada}, Marca/Modelo {veicMarcaModeloStr}, Cor {veicCorStr}, recolhido às {dataHoraGuardaStr.Right(5)} do dia {dataHoraGuardaStr.Left(10)}, " +
            //    $"no endereco {depositoEndereco}";

            //ResultView.TextoDeclaracaoRetirada3 =
            //    $@"Declaro também que o veículo se encontrava nas mesmas condições, quando foi removido e ainda lacrado, " +
            //    "conforme numeração abaixo descrita, sendo estes lacres conferidos na minha presença, nada havendo para reclamar agora ou no futuro.";

            //ResultView.ProprietarioProcurador = respNome;

            //ResultView.ProprietarioCpf = respDoc;

            //string estSetor = GuiaPagamentoReboqueEstadia?.EstacionamentoSetor ?? Grv.EstacionamentoSetor;
            //string estVaga = GuiaPagamentoReboqueEstadia?.EstacionamentoNumeroVaga ?? Grv.EstacionamentoNumeroVaga;
            //string numChave = GuiaPagamentoReboqueEstadia?.NumeroChave ?? Grv.NumeroChave;

            //ResultView.GrvEstacionamentoSetor = !string.IsNullOrWhiteSpace(estSetor)
            //    ? estSetor
            //    : "Não informado";

            //ResultView.GrvEstacionamentoNumeroVaga = !string.IsNullOrWhiteSpace(estVaga)
            //    ? estVaga
            //    : "Não informado";

            //ResultView.GrvNumeroChave = !string.IsNullOrWhiteSpace(numChave)
            //    ? numChave
            //    : "Não informado";

            ViewUsuarioModel Usuario = await _context.ViewUsuario
                .FirstOrDefaultAsync(x => x.UsuarioId == UsuarioId);

            if (Usuario != null)
            {
                ResultView.UsuarioNome = Usuario.NomeCompleto;
                ResultView.UsuarioMatricula = Usuario.Matricula;
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

            // 1. Tratamento quando busca por IdentificadoresProcesso (ID do processo)
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

            // 2. Tratamento quando busca por NumerosDeProcesso
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

                // Verificar duplicidade de números de processos no banco de dados
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