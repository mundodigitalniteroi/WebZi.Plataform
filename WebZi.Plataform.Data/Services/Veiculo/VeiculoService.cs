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
                        IdentificadorTipoVeiculo = item.TipoVeiculoId,
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

        public async Task<EquipamentoOpcionalListDTO> ListarEquipamentoOpcionalETipoVeiculo()
        {
            EquipamentoOpcionalListDTO ResultView = new();

            List<TipoVeiculoModel> result = await _context.TipoVeiculo
                .Include(x => x.TiposVeiculosEquipamentosAssociacoes)
                .ThenInclude(x => x.EquipamentoOpcional)
                .AsNoTracking()
                .ToListAsync();

            if (result != null && result.Count > 0)
            {
                foreach (TipoVeiculoModel tipoVeiculo in result)
                {
                    foreach (TipoVeiculoEquipamentoAssociacaoModel item in tipoVeiculo.TiposVeiculosEquipamentosAssociacoes)
                    {
                        EquipamentoOpcionalDTO EquipamentoOpcionalView = new()
                        {
                            IdentificadorEquipamentoOpcional = item.EquipamentoOpcional.EquipamentoOpcionalId,
                            IdentificadorTipoVeiculo = item.TipoVeiculoId,
                            OrdemVistoria = item.EquipamentoOpcional.OrdemVistoria,
                            Descricao = item.EquipamentoOpcional.Descricao,
                            ItemObrigatorio = item.EquipamentoOpcional.ItemObrigatorio,
                            Status = item.EquipamentoOpcional.Status
                        };

                        ResultView.Listagem.Add(EquipamentoOpcionalView);
                    }
                }

                ResultView.Mensagem = MensagemViewHelper.SetFound(ResultView.Listagem.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<MarcaModeloListDTO> ListMarcaModeloAsync(string? MarcaModelo)
        {
            MarcaModeloListDTO ResultView = new();

            var query = _context.MarcaModelo
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(MarcaModelo))
            {
                var filtro = MarcaModelo.Trim();

                query = query
                    .Where(x => x.MarcaModelo.Contains(filtro))
                    .OrderBy(x => x.MarcaModelo)
                    .Take(100);
            }
            else
            {
                query = query
                    .OrderBy(x => x.MarcaModelo);
            }

            var result = await query.ToListAsync();

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