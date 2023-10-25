using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Date;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Deposito;
using WebZi.Plataform.Domain.Models.Localizacao;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Usuario;
using WebZi.Plataform.Domain.ViewModel.Deposito;
using WebZi.Plataform.Domain.ViewModel.GRV.Pesquisa;
using WebZi.Plataform.Domain.Views.Localizacao;
using WebZi.Plataform.Domain.Views.Usuario;

namespace WebZi.Plataform.Data.Services.Deposito
{
    public class DepositoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DepositoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DepositoViewModelList> GetByIdAsync(int DepositoId)
        {
            DepositoViewModelList ResultView = new();

            if (DepositoId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest(MensagemPadraoEnum.IdentificadorDepositoInvalido);

                return ResultView;
            }

            DepositoModel result = await _context.Deposito
                .Where(w => w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (result != null)
            {
                ResultView.Listagem.Add(_mapper.Map<DepositoViewModel>(result));

                ResultView.Mensagem = MensagemViewHelper.GetOkFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<DepositoViewModelList> GetByNameAsync(string Name)
        {
            DepositoViewModelList ResultView = new();

            if (string.IsNullOrWhiteSpace(Name))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest("Informe o Nome do Depósito");

                return ResultView;
            }

            List<DepositoModel> result = await _context.Deposito
                .Where(w => w.Nome.ToUpper().Contains(Name.ToUpper().Trim()))
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<DepositoViewModel>>(result.OrderBy(o => o.Nome).ToList());

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public DateTime GetDataHoraPorDeposito(int DepositoId)
        {
            DepositoModel Deposito = _context.Deposito
                .Include(i => i.Endereco)
                .Where(w => w.DepositoId == DepositoId)
                .AsNoTracking()
                .FirstOrDefault();

            ConfiguracaoModel Configuracao = _context.Configuracao
                .AsNoTracking()
                .FirstOrDefault();

            DateTime DataHoraAtual = DateTime.Now;

            if (Configuracao.HorarioVerao == "N")
            {
                return DataHoraAtual;
            }

            ViewEnderecoCompletoModel CEP = _context.Endereco
                .Where(w => w.CEPId == Deposito.CEPId)
                .AsNoTracking()
                .FirstOrDefault();

            List<EstadoModel> Estados = _context.Estado
                .AsNoTracking()
                .ToList();

            EstadoModel EstadoPrincipal = Estados.Find(s => s.UF == "RJ");

            EstadoModel Estado = Estados.Find(s => s.UF == CEP.UF);

            DateTime DataInicioHorarioVerao = DateTimeHelper.GetBrazilFirstDaylightSavingDay(DataHoraAtual.Year);

            DateTime DataFimHorarioVerao = DateTimeHelper.GetBrazilLastDaylightSavingDay(DataHoraAtual.Year);

            if (DataHoraAtual.Month < DataInicioHorarioVerao.Month)
            {
                DataInicioHorarioVerao = DateTimeHelper.GetBrazilFirstDaylightSavingDay(DataHoraAtual.Year - 1);

                DataFimHorarioVerao = DateTimeHelper.GetBrazilLastDaylightSavingDay(DataHoraAtual.Year - 1);
            }

            bool HorarioVerao = DataHoraAtual.Date >= DataInicioHorarioVerao.Date && DataHoraAtual.Date <= DataFimHorarioVerao.Date;

            if (HorarioVerao && EstadoPrincipal != null && Estado != null)
            {
                if (EstadoPrincipal.UtcVeraoId > Estado.UtcId)
                {
                    DataHoraAtual = DataHoraAtual.AddHours((double)(EstadoPrincipal.UtcVeraoId - Estado.UtcVeraoId) * -1);
                }
                else if (EstadoPrincipal.UtcVeraoId < Estado.UtcVeraoId)
                {
                    DataHoraAtual = DataHoraAtual.AddHours((double)(EstadoPrincipal.UtcVeraoId - Estado.UtcVeraoId));
                }
            }

            return DataHoraAtual;
        }

        public async Task<DepositoViewModelList> ListAsync(int UsuarioId)
        {
            DepositoViewModelList ResultView = new();

            List<UsuarioDepositoModel> result = await _context.UsuarioDeposito
                .Include(x => x.Deposito)
                .Where(x => x.UsuarioId == UsuarioId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                List<DepositoModel> Depositos = new();

                foreach (UsuarioDepositoModel UsuarioDeposito in result)
                {
                    Depositos.Add(UsuarioDeposito.Deposito);
                }

                ResultView.Listagem = _mapper.Map<List<DepositoViewModel>>(Depositos.OrderBy(o => o.Nome).ToList());

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }

        public async Task<ClienteDepositoSimplificadoViewModelList> ListagemSimplificada(int UsuarioId)
        {
            ClienteDepositoSimplificadoViewModelList ResultView = new();

            List<ViewUsuarioClienteDepositoModel> result = await _context.ViewUsuarioClienteDeposito
                .Where(x => x.UsuarioId == UsuarioId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<ClienteDepositoSimplificadoViewModel>>(result.OrderBy(o => o.DepositoNome).ToList());

                ResultView.Mensagem = MensagemViewHelper.GetOkFound(result.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound();
            }

            return ResultView;
        }
    }
}