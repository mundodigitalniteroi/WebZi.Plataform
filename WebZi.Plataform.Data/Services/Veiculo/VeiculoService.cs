using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.DTO.Veiculo;
using WebZi.Plataform.Domain.Models.Veiculo;

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

        public async Task<EquipamentoOpcionalListDTO> ListEquipamentoOpcionalAsync(byte TipoVeiculoId)
        {
            EquipamentoOpcionalListDTO ResultView = new();

            if (TipoVeiculoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador do Tipo de Veículo inválido");

                return ResultView;
            }

            TipoVeiculoModel result = await _context.TipoVeiculo
                .Include(x => x.TiposVeiculosEquipamentosAssociacoes)
                .ThenInclude(x => x.EquipamentoOpcional)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TipoVeiculoId == TipoVeiculoId);

            if (result != null)
            {
                EquipamentoOpcionalDTO EquipamentoOpcionalView = new();

                foreach (TipoVeiculoEquipamentoAssociacaoModel item in result.TiposVeiculosEquipamentosAssociacoes)
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

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.TiposVeiculosEquipamentosAssociacoes.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<MarcaModeloListDTO> ListMarcaModeloAsync(string MarcaModelo)
        {
            MarcaModeloListDTO ResultView = new();

            if (string.IsNullOrWhiteSpace(MarcaModelo))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Informe a descrição da Marca/Modelo");

                return ResultView;
            }

            List<MarcaModeloModel> result = await _context.MarcaModelo
                .Where(x => x.MarcaModelo.Contains(MarcaModelo.ToUpperTrim()))
                .OrderBy(x => x.MarcaModelo)
                .Take(100)
                .AsNoTracking()
                .ToListAsync();

            if (result == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound("Marca/Modelo inexistente");

                return ResultView;
            }

            ResultView.Listagem = _mapper.Map<List<MarcaModeloDTO>>(result);

            ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);

            return ResultView;
        }

        public async Task<TipoVeiculoListDTO> ListTipoVeiculoAsync()
        {
            TipoVeiculoListDTO ResultView = new();

            List<TipoVeiculoModel> result = await _context.TipoVeiculo
                .AsNoTracking()
                .ToListAsync();

            ResultView.Listagem = _mapper
                .Map<List<TipoVeiculoDTO>>(result
                .OrderBy(x => x.Descricao)
                .ToList());

            ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);

            return ResultView;
        }
    }
}