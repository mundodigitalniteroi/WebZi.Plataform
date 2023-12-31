using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Veiculo;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.DTO.GRV.Pesquisa;
using WebZi.Plataform.Domain.DTO.Servico;
using WebZi.Plataform.Domain.DTO.Usuario;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Servico;
using WebZi.Plataform.Domain.Views.Usuario;

namespace WebZi.Plataform.Data.Services.Servico
{
    public class ServicoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ServicoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReboqueListDTO> GetReboqueByIdAsync(int ReboqueId)
        {
            ReboqueListDTO ResultView = new();

            if (ReboqueId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador do Reboque inválido");

                return ResultView;
            }

            ReboqueModel result = await _context.Reboque
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ReboqueId == ReboqueId);

            if (result != null)
            {
                ResultView.Listagem.Add(_mapper.Map<ReboqueDTO>(result));

                ResultView.Mensagem = MensagemViewHelper.SetFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboqueListDTO> GetReboqueByPlacaAsync(string Placa, int ClienteId, int DepositoId)
        {
            List<string> erros = new();

            if (string.IsNullOrWhiteSpace(Placa))
            {
                erros.Add("Informe a Placa");
            }
            else if (!Placa.IsPlaca())
            {
                erros.Add("Placa inválida");
            }

            if (ClienteId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
            }

            if (DepositoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorDepositoInvalido);
            }

            ReboqueListDTO ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);

                return ResultView;
            }

            ClienteModel Cliente = await _context.Cliente
                .Where(x => x.ClienteId == ClienteId)
                .AsNoTracking().
                FirstOrDefaultAsync();

            if (Cliente == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoCliente);

                return ResultView;
            }

            DepositoModel Deposito = await _context.Deposito
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DepositoId == DepositoId);

            if (Deposito == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoDeposito);

                return ResultView;
            }

            ReboqueModel result = await _context.Reboque
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Placa == Placa.ToUpper().Trim() && x.ClienteId == ClienteId && x.DepositoId == DepositoId);

            if (result != null)
            {
                ResultView.Listagem.Add(_mapper.Map<ReboqueDTO>(result));

                ResultView.Mensagem = MensagemViewHelper.SetFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboquistaListDTO> GetReboquistaByIdAsync(int ReboquistaId)
        {
            ReboquistaListDTO ResultView = new();

            if (ReboquistaId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Identificador do Reboquista inválido");

                return ResultView;
            }

            ReboquistaModel result = await _context.Reboquista
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ReboquistaId == ReboquistaId);

            if (result != null)
            {
                ResultView.Listagem.Add(_mapper.Map<ReboquistaDTO>(result));

                ResultView.Mensagem = MensagemViewHelper.SetFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboqueListDTO> ListReboqueAsync(int ClienteId, int DepositoId)
        {
            List<string> erros = new();

            if (ClienteId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
            }

            if (DepositoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorDepositoInvalido);
            }

            ReboqueListDTO ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);

                return ResultView;
            }

            ClienteModel Cliente = await _context.Cliente
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ClienteId == ClienteId);

            if (Cliente == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoCliente);

                return ResultView;
            }

            DepositoModel Deposito = await _context.Deposito
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DepositoId == DepositoId);

            if (Deposito == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoDeposito);

                return ResultView;
            }

            List<ReboqueModel> result = await _context.Reboque
                .Where(x => x.ClienteId == ClienteId
                    && x.DepositoId == DepositoId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<ReboqueDTO>>(result
                    .OrderBy(x => x.Placa)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<UsuarioClienteDepositoReboqueListDTO> ListReboqueByUsuarioIdAsync(int UsuarioId)
        {
            UsuarioClienteDepositoReboqueListDTO ResultView = new();

            List<ViewUsuarioClienteDepositoReboqueModel> result = await _context.ViewUsuarioClienteDepositoReboque
                .Where(x => x.UsuarioId == UsuarioId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<UsuarioClienteDepositoReboqueDTO>>(result
                    .OrderBy(x => x.ReboquePlaca)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboqueSimplificadoListDTO> ListResumeReboqueByUsuarioIdAsync(int UsuarioId)
        {
            ReboqueSimplificadoListDTO ResultView = new();

            UsuarioClienteDepositoReboqueListDTO result = await ListReboqueByUsuarioIdAsync(UsuarioId);

            if (result.Listagem?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<ReboqueSimplificadoDTO>>(result.Listagem);

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Listagem.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboquistaListDTO> ListReboquistaAsync(int ClienteId, int DepositoId)
        {
            List<string> erros = new();

            if (ClienteId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorClienteInvalido);
            }

            if (DepositoId <= 0)
            {
                erros.Add(MensagemPadraoEnum.IdentificadorDepositoInvalido);
            }

            ReboquistaListDTO ResultView = new();

            if (erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);

                return ResultView;
            }

            ClienteModel Cliente = await _context.Cliente
                .Where(x => x.ClienteId == ClienteId)
                .AsNoTracking().
                FirstOrDefaultAsync();

            if (Cliente == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoCliente);

                return ResultView;
            }

            DepositoModel Deposito = await _context.Deposito
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DepositoId == DepositoId);

            if (Deposito == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoDeposito);

                return ResultView;
            }

            List<ReboquistaModel> result = await _context.Reboquista
                .Where(x => x.ClienteId == ClienteId && x.DepositoId == DepositoId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<ReboquistaDTO>>(result
                    .OrderBy(x => x.Nome)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<UsuarioClienteDepositoReboquistaListDTO> ListReboquistaByUsuarioIdAsync(int UsuarioId)
        {
            UsuarioClienteDepositoReboquistaListDTO ResultView = new();

            List<ViewUsuarioClienteDepositoReboquistaModel> result = await _context.ViewUsuarioClienteDepositoReboquista
                .Where(x => x.UsuarioId == UsuarioId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<UsuarioClienteDepositoReboquistaDTO>>(result
                    .OrderBy(x => x.ReboquistaNome)
                    .ToList());

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ReboquistaSimplificadoListDTO> ListResumeReboquistaByUsuarioIdAsync(int UsuarioId)
        {
            ReboquistaSimplificadoListDTO ResultView = new();

            UsuarioClienteDepositoReboquistaListDTO result = await ListReboquistaByUsuarioIdAsync(UsuarioId);

            if (result.Listagem?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<ReboquistaSimplificadoDTO>>(result.Listagem);

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Listagem.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }
    }
}