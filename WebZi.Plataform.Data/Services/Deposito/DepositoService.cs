using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Services.Localizacao;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Localizacao;
using WebZi.Plataform.Domain.Models.Sistema;

namespace WebZi.Plataform.Data.Services.Deposito
{
    public class DepositoService
    {
        private readonly AppDbContext _context;

        public DepositoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DepositoModel>> List()
        {
            return await _context.Depositos
                .OrderBy(o => o.Descricao)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<DepositoModel> GetById(int id)
        {
            return await _context.Depositos
                .Where(w => w.DepositoId == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<DepositoModel> GetByName(string Name)
        {
            return await _context.Depositos
                .Where(w => w.Descricao == Name)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<DateTime> GetDataHoraPorDeposito(int DepositoId)
        {
            DepositoModel Deposito = await _context.Depositos
                .Include(i => i.CEP)
                .Where(w => w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            ConfiguracaoModel Configuracao = await _context.Configuracao
                .AsNoTracking()
                .FirstOrDefaultAsync();

            DateTime DataHoraAtual = DateTime.Now;

            if (Configuracao.HorarioVerao == "N")
            {
                return DataHoraAtual;
            }

            CEPModel CEP = await new CEPService(_context)
                .GetById(Deposito.CepId.Value);

            List<EstadoModel> Estados = await _context.Estados
                .AsNoTracking()
                .ToListAsync();

            EstadoModel EstadoPrincipal = Estados.Find(s => s.Uf == "RJ");

            EstadoModel Estado = Estados.Find(s => s.Uf == CEP.Municipio.Estado.Uf);

            DateTime DataInicioHorarioVerao = DateTimeHelper.GetBrazilFirstDaylightSavingDay(DataHoraAtual.Year);

            DateTime DataFimHorarioVerao = DateTimeHelper.GetBrazilLastDaylightSavingDay(DataHoraAtual.Year);

            if (DataHoraAtual.Month < DataInicioHorarioVerao.Month)
            {
                DataInicioHorarioVerao = DateTimeHelper.GetBrazilFirstDaylightSavingDay(DataHoraAtual.Year - 1);

                DataFimHorarioVerao = DateTimeHelper.GetBrazilLastDaylightSavingDay(DataHoraAtual.Year - 1);
            }

            bool HorarioVerao = DataHoraAtual.Date >= DataInicioHorarioVerao.Date && DataHoraAtual.Date <= DataFimHorarioVerao.Date;

            if (HorarioVerao && EstadoPrincipal != null && Estado != null)
            {
                if (EstadoPrincipal.UtcVeraoId > Estado.UtcId)
                {
                    DataHoraAtual = DataHoraAtual.AddHours((double)(EstadoPrincipal.UtcVeraoId - Estado.UtcVeraoId) * -1);
                }
                else if (EstadoPrincipal.UtcVeraoId < Estado.UtcVeraoId)
                {
                    DataHoraAtual = DataHoraAtual.AddHours((double)(EstadoPrincipal.UtcVeraoId - Estado.UtcVeraoId));
                }
            }

            return DataHoraAtual;
        }
    }
}