using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Veiculo;
using WebZi.Plataform.Domain.ViewModel.Veiculo;

namespace WebZi.Plataform.Data.Services.Veiculo
{
    public class VeiculoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VeiculoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EquipamentoOpcionalViewModelList> ListarEquipamentoOpcional(int TipoVeiculoId)
        {
            EquipamentoOpcionalViewModelList ResultView = new();

            //List<EquipamentoOpcionalModel> result = await _context.EquipamentoOpcional
            //    .OrderBy(x => x.Descricao)
            //    .AsNoTracking()
            //    .ToListAsync();

            TipoVeiculoModel result = await _context.TipoVeiculo
                .Include(x => x.TiposVeiculosEquipamentosAssociacoes)
                .ThenInclude(x => x.EquipamentoOpcional)
                .Where(x => x.TipoVeiculoId == TipoVeiculoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            EquipamentoOpcionalViewModel EquipamentoOpcionalView = new();

            foreach (var item in result.TiposVeiculosEquipamentosAssociacoes)
            {
                EquipamentoOpcionalView = new()
                {
                    EquipamentoOpcionalId = item.EquipamentoOpcional.EquipamentoOpcionalId,

                    OrdemVistoria = item.EquipamentoOpcional.OrdemVistoria,

                    Descricao = item.EquipamentoOpcional.Descricao,

                    ItemObrigatorio = item.EquipamentoOpcional.ItemObrigatorio,

                    Status = item.EquipamentoOpcional.Status
                };

                ResultView.ListagemEquipamentoOpcional.Add(EquipamentoOpcionalView);
            }

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.TiposVeiculosEquipamentosAssociacoes.Count);

            return ResultView;
        }

        public async Task<MarcaModeloViewModelList> ListarMarcaModelo(string MarcaModelo)
        {
            MarcaModeloViewModelList ResultView = new();

            if (string.IsNullOrWhiteSpace(MarcaModelo))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Informe a descrição da Marca/Modelo");

                return ResultView;
            }

            List<MarcaModeloModel> result = await _context.MarcaModelo
                .Where(w => w.MarcaModelo.Contains(MarcaModelo.ToUpper().Trim()))
                .OrderBy(x => x.MarcaModelo)
                .Take(100)
                .AsNoTracking()
                .ToListAsync();

            if (result == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Marca/Modelo não encontrado");

                return ResultView;
            }

            ResultView.ListagemMarcaModelo = _mapper.Map<List<MarcaModeloViewModel>>(result);

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        public async Task<TipoVeiculoViewModelList> ListarTipoVeiculo()
        {
            TipoVeiculoViewModelList ResultView = new();

            List<TipoVeiculoModel> result = await _context.TipoVeiculo
                .AsNoTracking()
                .ToListAsync();

            if (result == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Marca/Modelo não encontrado");

                return ResultView;
            }

            ResultView.ListagemTipoVeiculo = _mapper.Map<List<TipoVeiculoViewModel>>(result.OrderBy(x => x.Descricao).ToList());

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }
    }
}