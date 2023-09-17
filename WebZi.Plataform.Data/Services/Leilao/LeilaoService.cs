using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebZi.Plataform.Domain.Models.Atendimento;
using WebZi.Plataform.Domain.Models.Leilao;

namespace WebZi.Plataform.Data.Services.Leilao
{
    public class LeilaoService
    {
        private readonly AppDbContext _context;

        public LeilaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AtendimentoAvisoViewModel> GetAvisoLeilao(int GrvId, string StatusOperacaoId)
        {
            if (!new[] { "V", "L", "T", "1", "2", "4" }.Contains(StatusOperacaoId))
            {
                return null;
            }

            LeilaoLoteModel LeilaoLote = await _context.LeilaoLotes
                .Include(i => i.LeilaoLoteStatus)
                .Include(i => i.Leilao)
                .Include(i => i.Leilao.LeilaoStatus)
                .Where(w => w.GrvId.Equals(GrvId))
                .AsNoTracking()
                //.OrderByDescending(x => DateTime.Parse(x.Leilao.DataLeilao))
                .FirstOrDefaultAsync();

            if (LeilaoLote != null)
            {
                AtendimentoAvisoViewModel aviso = new();

                DateTime dataLeilao = DateTime.ParseExact(LeilaoLote.Leilao.DataLeilao, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (DateTime.Now.Date > dataLeilao.Date &&
                    LeilaoLote.Leilao.LeilaoStatus.Ativo != "I" &&
                    LeilaoLote.LeilaoLoteStatus.ValidaLote == "S")
                {
                    aviso.Erros.Add($"Este GRV está associado ao Leilão {LeilaoLote.Leilao.Descricao}, Data {dataLeilao:dd/MM/yyyy}, Lote {LeilaoLote.NumeroLote}");
                }
                else if (LeilaoLote.Leilao.LeilaoStatus.Ativo != "I" && LeilaoLote.LeilaoLoteStatus.ValidaLote == "S")
                {
                    if (new[] { "V", "1" }.Contains(StatusOperacaoId))
                    {
                        aviso.Avisos.Add($"Este GRV está associado ao Leilão {LeilaoLote.Leilao.Descricao}, Data {dataLeilao:dd/MM/yyyy}, Lote {LeilaoLote.NumeroLote}");
                    }
                    else if (new[] { "L", "T", "2", "4" }.Contains(StatusOperacaoId) && (dataLeilao.Date - DateTime.Now.Date).TotalDays <= 1)
                    {
                        aviso.Erros.Add($"Este GRV está associado ao Leilão {LeilaoLote.Leilao.Descricao}, Data {dataLeilao:dd/MM/yyyy}, Lote {LeilaoLote.NumeroLote}, para dar prosseguimento a esta Liberação é necessário acionar a equipe do Leilões");
                    }
                }

                if (aviso.Avisos.Count.Equals(0) && aviso.Erros.Count.Equals(0))
                {
                    return null;
                }

                return aviso;
            }
            else
            {
                return null;
            }
        }
    }
}