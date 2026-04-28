using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Domain.DTO.Leilao;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Models.Leilao;
using WebZi.Plataform.Domain.ViewModel.Liberacao;
using WebZi.Plataform.Domain.Views.Veiculos;

namespace WebZi.Plataform.Data.Services.Leilao
{
    public class LeilaoService
    {
        private readonly AppDbContext _context;

        public LeilaoService(AppDbContext context)
        {
            _context = context;
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