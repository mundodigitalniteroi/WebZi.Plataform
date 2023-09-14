﻿using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Domain.Models.Faturamento;

namespace WebZi.Plataform.Data.Services.Faturamento
{
    public class TipoMeioCobrancaService
    {
        private readonly AppDbContext _context;

        public TipoMeioCobrancaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TipoMeioCobrancaModel> GetById(byte id)
        {
            return await _context.TiposMeiosCobrancas
                .Where(w => w.TipoMeioCobrancaId.Equals(id))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<List<TipoMeioCobrancaModel>> List()
        {
            var result = await _context.TiposMeiosCobrancas
                .Where(w => w.FlagAtivo.Equals("S"))
                .AsNoTracking()
                .ToListAsync();

            return result
                .OrderBy(o => o.Descricao)
                .ToList();
        }
    }
}