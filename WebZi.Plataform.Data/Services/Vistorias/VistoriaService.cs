using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.DTO.Leilao.Vistoria;
using WebZi.Plataform.Domain.DTO.Vistoria;
using WebZi.Plataform.Domain.Models.Vistoria;

namespace WebZi.Plataform.Data.Services.Vistorias
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

        public async Task<VistoriaStatusListDTO> ListStatusVistoriaAsync()
        {
            VistoriaStatusListDTO ResultView = new();

            List<VistoriaStatusModel> result = await _context.VistoriaStatus
                .AsNoTracking()
                .ToListAsync();

            ResultView.Listagem = _mapper.Map<List<VistoriaStatusDTO>>(result
                .OrderBy(x => x.Descricao)
                .ToList());

            ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);

            return ResultView;
        }

        public async Task<VistoriaSituacaoChassiListDTO> ListSituacaoChassiAsync()
        {
            VistoriaSituacaoChassiListDTO ResultView = new();

            List<VistoriaSituacaoChassiModel> result = await _context.VistoriaSituacaoChassi
                .AsNoTracking()
                .ToListAsync();

            ResultView.Listagem = _mapper.Map<List<VistoriaSituacaoChassiDTO>>(result
                .OrderBy(x => x.Descricao)
                .ToList());

            ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);

            return ResultView;
        }

        public async Task<SelecionarVistoriaPreLeilaoDTO> GetVistoriaAsync(
            int? identificadorCliente, int? identificadorEmpresaVistoria, int identificadorProcesso,
            string? numeroProcesso)
        {
            SelecionarVistoriaPreLeilaoDTO ResultView = new();

            var result = await _context.Vistoria
                .Include(x => x.Grv)
                .Include(x => x.VistoriaStatus)
                .Include(x => x.VistoriaSituacaoChassi)
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.GrvId == identificadorProcesso &&
                    (identificadorCliente == 0 || x.Grv.ClienteId == identificadorCliente) &&
                    (identificadorEmpresaVistoria == 0 || x.EmpresaVistoriaId == identificadorEmpresaVistoria) &&
                    (numeroProcesso == null || x.Grv.NumeroFormularioGrv == numeroProcesso)
                );
            if (result == null || result.VistoriaStatus == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();

                return ResultView;
            }

            var url = await _context.BucketArquivo
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.TabelaOrigemId == result.VistoriaId &&
                    x.NomeTabelaOrigemId == 19);
            
            ResultView = _mapper.Map<SelecionarVistoriaPreLeilaoDTO>(result);

            ResultView.Url = url?.Url;

            ResultView.Mensagem = MensagemViewHelper.SetFound(1);

            return ResultView;
        }
    }
}