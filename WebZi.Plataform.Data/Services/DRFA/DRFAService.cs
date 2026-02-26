using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Formats.Asn1;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.GRV.DRFA;
using WebZi.Plataform.Domain.Models.WebServices.DetranAlagoas.ConsultaVeiculoApreensao.Response;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebZi.Plataform.Data.Services.DRFA
{
    public class DRFAService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DRFAService(AppDbContext context)
        {
            _context = context;
        }
        public DRFAService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MensagemDTO> CreateDRFAGrv(int GrvId, GrvParameters grv)
        {
            MensagemDTO ResultView = new();
            var drfa = grv.DRFA;
            try
            {
                DRFAModel result = new()
                {
                    GrvId = GrvId,
                    TipoRegistroId = drfa.TipoRegistroId,
                    OrgaoEmissorId = drfa.OrgaoEmissorId,
                    UsuarioCadastroId = grv.IdentificadorUsuario,
                    UsuarioAlteracaoId = grv.IdentificadorUsuario,
                    AutoridadeDivisaoComplemento = drfa.ComplementoDivisao.ToUpperTrim().ToNullIfEmpty(),
                    NumeroRegistroRouboFurto = drfa.NumeroRegistro.ToUpperTrim().ToNullIfEmpty(),
                    RegistroRouboFurtoMatriculaAgente = drfa.MatriculaAgente.ToUpperTrim().ToNullIfEmpty(),
                    RegistroRouboFurtoNomeAgente = drfa.NomeAgente.ToUpperTrim().ToNullIfEmpty(),
                    LocalRemocaoEnderecoCompleto = drfa.EnderecoCompleto.ToUpperTrim().ToNullIfEmpty(),
                    LocalRemocaoReferencia = drfa.Referencia.ToUpperTrim().ToNullIfEmpty(),
                    LocalRemocaoLongitude = drfa.Longitude.ToUpperTrim().ToNullIfEmpty(),
                    LocalRemocaoLatitude = drfa.Latitude.ToUpperTrim().ToNullIfEmpty(),
                    EstadoGeralVeiculo = drfa.EstadoGeralDoVeiculo.ToUpperTrim().ToNullIfEmpty(),
                    FlagRegistroAgendado = drfa.FlagAgendamento,
                    FlagRegistroRecuperacao = drfa.FlagRegistroRecuperacao,
                    DataCadastro = DateTime.UtcNow,
                };

                _context.DRFA.Add(result);
                _context.SaveChanges();

                if (drfa.FlagRegistroRecuperacao == 'S')
                {
                    if (drfa.RegistroRecuperacao == null)
                    {
                        ResultView = MensagemViewHelper.SetBadRequest("Preencha os dados de Registro de Recuperação.");
                        return ResultView;
                    }

                    var registroRecuperacao = CreateRegistroRecuperacao(result.GrvDrfaId, drfa.RegistroRecuperacao);

                    if (registroRecuperacao?.Erros != null && registroRecuperacao.Erros.Count > 0)
                    {
                        ResultView = MensagemViewHelper.SetBadRequest(registroRecuperacao.Erros);
                        return ResultView;
                    }
                }
                if (drfa.FlagAgendamento == 'S')
                {
                    if (drfa.AgendamentoRetirada == null)
                    {
                        ResultView = MensagemViewHelper.SetBadRequest("Preencha os dados de Agendamento de Retirada.");
                        return ResultView;
                    }

                    var agendamentoRecuperacao = CreateAgendamentoRecuperacao(result.GrvDrfaId, drfa.AgendamentoRetirada);

                    if (agendamentoRecuperacao?.Erros != null && agendamentoRecuperacao.Erros.Count > 0)
                    {
                        ResultView = MensagemViewHelper.SetBadRequest(agendamentoRecuperacao.Erros);
                        return ResultView;
                    }
                }
                await _context.SaveChangesAsync();
                return ResultView;
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError(ex);
                return ResultView;
            }
 
        }

        private MensagemDTO CreateRegistroRecuperacao(int DRFAId,  RegistroRecuperacaoParameters parameters)
        {
            MensagemDTO ResultView = new();
            try
            {
                RegistroRecuperacaoModel result = new()
                {
                    DRFAId = DRFAId,
                    AutoridadeDivisaoId = parameters.DivisaoId,
                    NumeroRegistroRecuperacao = parameters.NumeroRegistro.ToNullIfEmpty(),
                    MatriculaAgente = parameters.MatriculaAgente.ToNullIfEmpty(),
                    NomeAgente = parameters.NomeAgente.ToNullIfEmpty(),
                    DataRegistroRecuperacao = parameters.DataDeRecuperacao
                };
                _context.DRFAArquivoRegistro.Add(result);
                return ResultView;
            }catch(Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError("Ocorreu um erro ao criar o registro de recuperação.", ex);
                return ResultView;
            }
        }
        private MensagemDTO CreateAgendamentoRecuperacao(int DRFAId, AgendamentoRetiradaParameters parameters)
        {
            MensagemDTO ResultView = new();
            try
            {
                AgendamentoRetiradaModel result = new()
                {
                    DRFAId = DRFAId,
                    UsuarioRegistroAgendamentoId = parameters.UsuarioId,
                    NomeResponsavelAgendamento = parameters.NomeResponsavel.ToNullIfEmpty(),
                    CpfResponsavelAgendamento = parameters.CPF.Trim().ToNullIfEmpty(),
                    DataRegistroAgendamento = parameters.DataDoRegistro,
                    DataAgendamento = parameters.DataDoAgendamento
                };
                _context.DRFAAgendamentoRetirada.Add(result);
                ResultView = MensagemViewHelper.SetCreateSuccess();
                return ResultView;
            }catch (Exception ex)
            {
                ResultView = MensagemViewHelper.SetInternalServerError("Ocorreu um erro ao criar o agendamento de retirada.", ex);
                return ResultView;
            }
        }
    }
}
