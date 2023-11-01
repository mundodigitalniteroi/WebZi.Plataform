﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Vistoria;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Vistoria;

namespace WebZi.Plataform.Data.Services.Vistoria
{
    public class VistoriaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VistoriaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VistoriaStatusViewModelList> ListarStatusVistoriaAsync()
        {
            VistoriaStatusViewModelList ResultView = new();

            List<VistoriaStatusModel> result = await _context.VistoriaStatus
                .AsNoTracking()
                .ToListAsync();

            result = result
                .OrderBy(x => x.Descricao)
                .ToList();

            foreach (var item in result)
            {
                ResultView.Listagem.Add(new()
                {
                    IdentificadorStatus = item.VistoriaStatusId,
                    Descricao = item.Descricao
                });
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        public async Task<VistoriaSituacaoChassiViewModelList> ListarSituacaoChassiAsync()
        {
            VistoriaSituacaoChassiViewModelList ResultView = new();

            List<VistoriaSituacaoChassiModel> result = await _context.VistoriaSituacaoChassi
                .AsNoTracking()
                .ToListAsync();

            ResultView.Listagem = _mapper.Map<List<VistoriaSituacaoChassiViewModel>>(result
                .OrderBy(o => o.Descricao)
                .ToList());

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }
    }
}