using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<EquipamentoOpcionalViewModelList> ListarEquipamentoOpcionalAsync(byte TipoVeiculoId)
        {
            EquipamentoOpcionalViewModelList ResultView = new();

            if (TipoVeiculoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Identificador do Tipo de Veículo inválido");

                return ResultView;
            }

            TipoVeiculoModel result = await _context.TipoVeiculo
                .Include(x => x.TiposVeiculosEquipamentosAssociacoes)
                .ThenInclude(x => x.EquipamentoOpcional)
                .Where(x => x.TipoVeiculoId == TipoVeiculoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                EquipamentoOpcionalViewModel EquipamentoOpcionalView = new();

                foreach (var item in result.TiposVeiculosEquipamentosAssociacoes)
                {
                    EquipamentoOpcionalView = new()
                    {
                        IdentificadorEquipamentoOpcional = item.EquipamentoOpcional.EquipamentoOpcionalId,

                        OrdemVistoria = item.EquipamentoOpcional.OrdemVistoria,

                        Descricao = item.EquipamentoOpcional.Descricao,

                        ItemObrigatorio = item.EquipamentoOpcional.ItemObrigatorio,

                        Status = item.EquipamentoOpcional.Status
                    };

                    ResultView.Listagem.Add(EquipamentoOpcionalView);
                }

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.TiposVeiculosEquipamentosAssociacoes.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<MarcaModeloViewModelList> ListarMarcaModeloAsync(string MarcaModelo)
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
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Marca/Modelo inexistente");

                return ResultView;
            }

            ResultView.Listagem = _mapper.Map<List<MarcaModeloViewModel>>(result);

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }

        public async Task<TipoVeiculoViewModelList> ListarTipoVeiculoAsync()
        {
            TipoVeiculoViewModelList ResultView = new();

            List<TipoVeiculoModel> result = await _context.TipoVeiculo
                .AsNoTracking()
                .ToListAsync();

            ResultView.Listagem = _mapper.Map<List<TipoVeiculoViewModel>>(result.OrderBy(x => x.Descricao).ToList());

            ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);

            return ResultView;
        }
    }
}