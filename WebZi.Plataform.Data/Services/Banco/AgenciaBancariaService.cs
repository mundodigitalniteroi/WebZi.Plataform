﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.DTO.Banco;
using WebZi.Plataform.Domain.Models.Banco;

namespace WebZi.Plataform.Data.Services.Banco
{
    public class AgenciaBancariaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AgenciaBancariaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AgenciaBancariaListDTO> GetByIdAsync(int AgenciaBancariaId)
        {
            AgenciaBancariaListDTO ResultView = new();

            if (AgenciaBancariaId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador da Agência Bancária inválido");

                return ResultView;
            }

            AgenciaBancariaModel result = await _context.AgenciaBancaria
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.BancoId == AgenciaBancariaId);

            if (result != null)
            {
                ResultView.Listagem.Add(_mapper.Map<AgenciaBancariaDTO>(result));

                ResultView.Mensagem = MensagemViewHelper.SetFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<AgenciaBancariaListDTO> GetByCodigoAgenciaAsync(int BancoId, string CodigoAgencia)
        {
            AgenciaBancariaListDTO ResultView = new();

            List<string> erros = new();

            if (BancoId <= 0)
            {
                erros.Add("Identificador do Banco inválido");
            }

            if (string.IsNullOrWhiteSpace(CodigoAgencia))
            {
                erros.Add("Informe o Código da Agência");
            }

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);

                return ResultView;
            }

            AgenciaBancariaModel result = await _context.AgenciaBancaria
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.BancoId == BancoId && x.CodigoAgencia == CodigoAgencia);

            if (result != null)
            {
                ResultView.Listagem.Add(_mapper.Map<AgenciaBancariaDTO>(result));

                ResultView.Mensagem = MensagemViewHelper.SetFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<AgenciaBancariaListDTO> ListAsync(int BancoId)
        {
            AgenciaBancariaListDTO ResultView = new();

            if (BancoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador do Banco inválido");

                return ResultView;
            }

            List<AgenciaBancariaModel> result = await _context.AgenciaBancaria
                .Where(x => x.BancoId == BancoId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                result = result
                    .OrderBy(x => x.CodigoAgencia)
                    .ThenBy(x => x.ContaCorrente)
                    .ToList();

                ResultView.Listagem = _mapper.Map<List<AgenciaBancariaDTO>>(result);

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }
    }
}