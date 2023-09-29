﻿using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.Data.Services.GRV
{
    public class StatusOperacaoService
    {
        private readonly AppDbContext _context;

        public StatusOperacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StatusOperacaoModel> GetById(char id)
        {
            return await _context.StatusOperacao
                .Where(w => w.StatusOperacaoId == id.ToString().ToUpper())
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<List<StatusOperacaoModel>> List()
        {
            var result = await _context.StatusOperacao
                .OrderBy(o => o.Descricao)
                .AsNoTracking()
                .ToListAsync();

            return result.OrderBy(o => o.Descricao).ToList();
        }
    }
}