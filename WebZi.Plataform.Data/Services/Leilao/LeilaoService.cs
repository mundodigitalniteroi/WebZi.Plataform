using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Domain.Models.Leilao;
using WebZi.Plataform.Domain.ViewModel;

namespace WebZi.Plataform.Data.Services.Leilao
{
    public class LeilaoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LeilaoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MensagemViewModel> GetAvisosLeilao(int GrvId, string StatusOperacaoId)
        {
            if (!new[] { "V", "L", "T", "1", "2", "4" }.Contains(StatusOperacaoId))
            {
                return null;
            }

            LeilaoLoteModel LeilaoLote = await _context.LeilaoLote
                .Include(x => x.LeilaoLoteStatus)
                .Include(w => w.Leilao)
                .Include(w => w.Leilao.LeilaoStatus)
                .Include(w => w.Grv)
                .Where(w => w.GrvId == GrvId)
                .OrderByDescending(w => (int)(object)(w.Leilao.DataLeilao.Substring(6, 4) + w.Leilao.DataLeilao.Substring(3, 2) + w.Leilao.DataLeilao.Substring(0, 2)))
                .AsNoTracking()
                .FirstOrDefaultAsync();

            MensagemViewModel mensagem = new();

            if (LeilaoLote != null)
            {
                DateTime DataHoraPorDeposito = new DepositoService(_context)
                    .GetDataHoraPorDeposito(LeilaoLote.Grv.DepositoId);

                DateTime dataLeilao = DateTime.ParseExact(LeilaoLote.Leilao.DataLeilao, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (DataHoraPorDeposito.Date > dataLeilao.Date &&
                    LeilaoLote.Leilao.LeilaoStatus.Ativo != "I" &&
                    LeilaoLote.LeilaoLoteStatus.ValidaLote == "S")
                {
                    mensagem.AvisosImpeditivos.Add($"Este GRV está associado ao Leilão {LeilaoLote.Leilao.Descricao}, Data {dataLeilao:dd/MM/yyyy}, Lote {LeilaoLote.NumeroLote}");
                    mensagem.AvisosImpeditivos.Add("CANCELAR");
                }
                else if (LeilaoLote.Leilao.LeilaoStatus.Ativo != "I"
                      && LeilaoLote.LeilaoLoteStatus.ValidaLote == "S")
                {
                    if (new[] { "V", "1" }.Contains(StatusOperacaoId))
                    {
                        mensagem.AvisosImpeditivos.Add($"Este GRV está associado ao Leilão {LeilaoLote.Leilao.Descricao}, Data {dataLeilao:dd/MM/yyyy}, Lote {LeilaoLote.NumeroLote}, o veículo não pode ser atendido");
                        mensagem.AvisosImpeditivos.Add("CANCELAR_E_ENVIAR_EMAIL");
                    }
                    else if (new[] { "L", "T", "2", "4" }.Contains(StatusOperacaoId)
                         && (dataLeilao.Date - DataHoraPorDeposito.Date).TotalDays <= 1)
                    {
                        mensagem.AvisosImpeditivos.Add($"Este GRV está associado ao Leilão {LeilaoLote.Leilao.Descricao}, Data {dataLeilao:dd/MM/yyyy}, Lote {LeilaoLote.NumeroLote}, para dar prosseguimento a esta Liberação é necessário acionar a equipe do Leilões");
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