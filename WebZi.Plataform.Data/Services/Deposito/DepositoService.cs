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

        public DepositoService(AppDbContext context)
        {
            _context = context;
        }

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
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorDepositoInvalido);

                return ResultView;
            }

            DepositoModel result = await _context.Deposito
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DepositoId == DepositoId);

            if (result != null)
            {
                ResultView.Listagem.Add(_mapper.Map<DepositoViewModel>(result));

                ResultView.Mensagem = MensagemViewHelper.SetFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<DepositoViewModelList> GetByNameAsync(string Name)
        {
            DepositoViewModelList ResultView = new();

            if (string.IsNullOrWhiteSpace(Name))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Informe o Nome do Depósito");

                return ResultView;
            }

            List<DepositoModel> result = await _context.Deposito
                .Where(x => x.Nome.ToUpper().Contains(Name.ToUpper().Trim()))
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<DepositoViewModel>>(result
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

        public DateTime GetDataHoraPorDeposito(int DepositoId)
        {
            DepositoModel Deposito = _context.Deposito
                .Include(x => x.Endereco)
                .Where(x => x.DepositoId == DepositoId)
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

            ViewEnderecoCompletoModel CEP = _context.EnderecoCompleto
                .AsNoTracking()
                .FirstOrDefault(x => x.CEPId == Deposito.CEPId);

            List<EstadoModel> Estados = _context.Estado
                .AsNoTracking()
                .ToList();

            EstadoModel EstadoPrincipal = Estados.Find(x => x.UF == "RJ");

            EstadoModel Estado = Estados.Find(x => x.UF == CEP.UF);

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

                ResultView.Listagem = _mapper.Map<List<DepositoViewModel>>(Depositos
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

        public async Task<ClienteDepositoSimplificadoViewModelList> ListResumeAsync(int UsuarioId)
        {
            ClienteDepositoSimplificadoViewModelList ResultView = new();

            List<ViewUsuarioClienteDepositoModel> result = await _context.ViewUsuarioClienteDeposito
                .Where(x => x.UsuarioId == UsuarioId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<ClienteDepositoSimplificadoViewModel>>(result
                    .OrderBy(x => x.DepositoNome)
                    .ToList());

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