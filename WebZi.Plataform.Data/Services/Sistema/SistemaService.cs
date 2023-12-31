﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.Models.Sistema;

namespace WebZi.Plataform.Data.Services.Sistema
{
    public class SistemaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SistemaService(AppDbContext context)
        {
            _context = context;
        }

        public SistemaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CorListDTO> ListarCorAsync(string Cor = "")
        {
            List<CorModel> result = await _context.Cor
                .Where(x => !string.IsNullOrWhiteSpace(Cor) ? x.Cor.Contains(Cor.ToUpper().Trim()) : true)
                .AsNoTracking()
                .ToListAsync();

            CorListDTO ResultView = new();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper
                    .Map<List<CorDTO>>(result.OrderBy(x => x.Cor)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Cor não encontrada");
            }

            return ResultView;
        }

        public async Task<ConfiguracaoModel> GetConfiguracaoSistemaAsync()
        {
            return await _context.Configuracao
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}