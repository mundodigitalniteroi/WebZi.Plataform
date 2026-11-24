using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Cliente;
using WebZi.Plataform.Data.Services.Deposito;
using WebZi.Plataform.Domain.DTO.ClienteDeposito;
using WebZi.Plataform.Domain.DTO.GRV.Pesquisa;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Usuario;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebZi.Plataform.Data.Services.ClienteDeposito
{
    public class ClienteDepositoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ClienteDepositoService(AppDbContext context)
        {
            _context = context;
        }

        public ClienteDepositoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetPorIdentificadorDTO> GetPorIdentificadorAsync(int userId, int identificador, CancellationToken ct)
        {

            GetPorIdentificadorDTO ResultView = new();
            var erros = new List<string>();
            var clienteDeposito = await _context.ClienteDeposito
                .AsNoTracking()
                .Where(x => x.ClienteDepositoId == identificador && x.FlagAtivo == "S")
                .Select(x => new
                {
                    x.ClienteId,
                    x.DepositoId,
                    x.FlagPossuiParcelamento,
                    x.FlagAtivo
                })
                .FirstOrDefaultAsync(ct);

            if (clienteDeposito == null)
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound(MensagemPadraoEnum.NaoEncontradoAssociacaoClienteDeposito);
                return ResultView;
            }


            var cliente = await _context.UsuarioCliente
                .AsNoTracking()
                .Where(x => x.ClienteId == clienteDeposito.ClienteId && x.UsuarioId == userId && x.Cliente.FlagAtivo == "S")
                .Select(x => new
                {
                    x.UsuarioId,
                    x.ClienteId,
                    x.Cliente.AgenciaBancaria.AgenciaBancariaId,
                    x.Cliente.Endereco.CEPId,
                    x.Cliente.Endereco.TipoLogradouroId,
                    x.Cliente.Endereco.BairroId,
                    x.Cliente.EmpresaId,
                    x.Cliente.PixTipoChaveId,
                    x.Cliente.Nome,
                    x.Cliente.CNPJ,
                    x.Cliente.Logradouro,
                    x.Cliente.NumeroEndereco,
                    x.Cliente.ComplementoEndereco,
                    x.Cliente.GpsLatitude,
                    x.Cliente.GpsLongitude,
                    x.Cliente.MetragemGuarda,
                    x.Cliente.MetragemTotal,
                    x.Cliente.HoraDiaria,
                    x.Cliente.MaximoDiariasParaCobranca,
                    x.Cliente.MaximoDiasVencimento,
                    x.Cliente.FlagUsarHoraDiaria,
                    x.Cliente.FlagEmissaoNotaFiscal,
                    x.Cliente.FlagCadastrarQuilometragem,
                    x.Cliente.FlagPermiteAlteracaoTipoVeiculo,
                    x.Cliente.FlagLancarIpvaMultas,
                    x.Cliente.FlagClientePossuiCodigoIdentificacao,
                    x.Cliente.FlagCobrarDiariasDiasCorridos,
                    x.Cliente.FlagClienteRealizaFaturamentoArrecadacao,
                    x.Cliente.FlagEnderecoCadastroManual,
                    x.Cliente.CodigoOrgao,
                    x.Cliente.FlagPossuiPixEstatico,
                    x.Cliente.PixChave,
                    x.Cliente.FlagPossuiPixDinamico,
                    x.Cliente.FlagUsaAlvaraDeLiberacao,
                    x.Cliente.FlagAtivo
                })
                .FirstOrDefaultAsync(ct);

            if(cliente is null)
            {
                erros.Add(MensagemPadraoEnum.NaoEncontradoClienteAssociado);
            }

            var deposito = await _context.UsuarioDeposito
                .Select(x => new
                {
                    x.UsuarioId,
                    x.DepositoId,
                    x.Deposito.Nome,
                    x.Deposito.FlagAtivo
                })
                .Where(x => x.DepositoId == clienteDeposito.DepositoId && x.UsuarioId == userId && x.FlagAtivo == "S")
                .FirstOrDefaultAsync(ct);

            if (deposito is null)
            {
                erros.Add(MensagemPadraoEnum.NaoEncontradoDepositoAssociado);
            }

            if(erros.Count > 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(erros);
                return ResultView;
            }

            ResultView = new GetPorIdentificadorDTO
            {
                Mensagem = MensagemViewHelper.SetFound(),
                IdentificadorCliente = cliente.ClienteId,
                IdentificadorDeposito = deposito.DepositoId,
                IdentificadorAgenciaBancaria = cliente.AgenciaBancariaId,
                IdentificadorCEP = cliente.CEPId,
                IdentificadorTipoLogradouro = cliente.TipoLogradouroId,
                IdentificadorBairro = cliente.BairroId,
                IdentificadorEmpresa = cliente.EmpresaId,
                IdentificadorTipoChavePIX = cliente.PixTipoChaveId,
                NomeCliente = cliente.Nome,
                NomeDeposito = deposito.Nome,
                CNPJ = cliente.CNPJ,
                Logradouro = cliente.Logradouro,
                Numero = cliente.NumeroEndereco,
                Complemento = cliente.ComplementoEndereco,
                GpsLatitude = cliente.GpsLatitude,
                GpsLongitude = cliente.GpsLongitude,
                MetragemGuarda = cliente.MetragemGuarda,
                MetragemTotal = cliente.MetragemTotal,
                HoraDiaria = cliente.HoraDiaria,
                MaximoDiariasParaCobranca = cliente.MaximoDiariasParaCobranca,
                MaximoDiasVencimento = cliente.MaximoDiasVencimento,
                FlagUsarHoraDiaria = cliente.FlagUsarHoraDiaria,
                FlagEmissaoNotaFiscal = cliente.FlagEmissaoNotaFiscal,
                FlagCadastrarQuilometragem = cliente.FlagCadastrarQuilometragem,
                FlagPermiteAlteracaoTipoVeiculo = cliente.FlagPermiteAlteracaoTipoVeiculo,
                FlagLancarIpvaMultas = cliente.FlagLancarIpvaMultas,
                FlagPossuiClienteCodigoIdentificacao = cliente.FlagClientePossuiCodigoIdentificacao,
                FlagCobrarDiariasDiasCorridos = cliente.FlagCobrarDiariasDiasCorridos,
                FlagClienteRealizaFaturamentoArrecadacao = cliente.FlagClienteRealizaFaturamentoArrecadacao,
                FlagEnderecoCadastroManual = cliente.FlagEnderecoCadastroManual,
                CodigoOrgao = cliente.CodigoOrgao,
                FlagPossuiPIXEstatico = cliente.FlagPossuiPixEstatico,
                ChavePIX = cliente.PixChave,
                FlagPossuiPIXDinamico = cliente.FlagPossuiPixDinamico,
                FlagUsaAlvaraDeLiberacao = cliente.FlagUsaAlvaraDeLiberacao,
                FlagPossuiParcelamento = clienteDeposito.FlagPossuiParcelamento,
            };



            return ResultView;
        }
        public async Task<MensagemDTO> ValidateClienteDepositoAsync(int ClienteId, int DepositoId)
        {
            MensagemDTO ResultView = new();

            ResultView = MensagemViewHelper.SetNewMessages(ResultView, await new ClienteService(_context)
                .ValidateClienteAsync(ClienteId));

            ResultView = MensagemViewHelper.SetNewMessages(ResultView, await new DepositoService(_context)
                .ValidateDepositoAsync(DepositoId));

            if (ClienteId > 0 && DepositoId > 0)
            {
                if (!await _context.ClienteDeposito.AsNoTracking().AnyAsync(x => x.ClienteId == ClienteId && x.DepositoId == DepositoId))
                {
                    ResultView = MensagemViewHelper.SetNewMessage(ResultView, MensagemPadraoEnum.NaoEncontradoAssociacaoClienteDeposito, MensagemTipoAvisoEnum.Impeditivo);
                }
            }

            if (ResultView.AvisosImpeditivos.Count + ResultView.Erros.Count > 0)
            {
                ResultView.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;

                return ResultView;
            }

            return MensagemViewHelper.SetOk();
        }

        public async Task<ClienteDepositoFlagParcelamentoDTO> GetClienteDepositoFlagParcelamento(int ClienteId, int DepositoId)
        {

            ClienteDepositoFlagParcelamentoDTO ResultView = new();

            #region Validações
            ResultView.Mensagem = MensagemViewHelper.SetNewMessages(ResultView.Mensagem, await new ClienteService(_context)
                .ValidateClienteAsync(ClienteId));

            ResultView.Mensagem = MensagemViewHelper.SetNewMessages(ResultView.Mensagem, await new DepositoService(_context)
                .ValidateDepositoAsync(DepositoId));
            #endregion Validações
            #region Consultas
            var query = await _context
                    .ClienteDeposito
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ClienteId == ClienteId && x.DepositoId == DepositoId);
            #endregion Consultas

            if (query is null)
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();

            if (ResultView.Mensagem.AvisosImpeditivos.Count > 0 || ResultView.Mensagem.Erros.Count > 0)
                return ResultView;

            ResultView = _mapper.Map<ClienteDepositoFlagParcelamentoDTO>(query);
            ResultView.Mensagem = MensagemViewHelper.SetFound();
            return ResultView;
        }
    }
}