using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Formats.Asn1;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.DTO.DRFA;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.Usuario;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.GRV.DRFA;
using WebZi.Plataform.Domain.Models.WebServices.DetranAlagoas.ConsultaVeiculoApreensao.Response;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebZi.Plataform.Data.Services.DRFA
{
    public class DRFAService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public DRFAService(AppDbContext context)
        {
            _context = context;
        }
        public DRFAService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public DRFAService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }
        //
        // public async Task<MensagemDTO> CreateDRFAGrv(int GrvId, GrvParameters grv)
        // {
        //     MensagemDTO ResultView = new();
        //     var drfa = grv.DRFA;
        //
        //     if (drfa == null)
        //     {
        //         return MensagemViewHelper.SetBadRequest("Os dados da DRFA são obrigatórios para veículos com motivo de apreensão Roubo/Furto.");
        //     }
        //
        //     try
        //     {
        //         DRFAModel result = new()
        //         {
        //             GrvId = GrvId,
        //             TipoRegistroId = drfa.TipoRegistroId,
        //             OrgaoEmissorId = drfa.OrgaoEmissorId,
        //             UsuarioCadastroId = grv.IdentificadorUsuario,
        //             UsuarioAlteracaoId = grv.IdentificadorUsuario,
        //             AutoridadeDivisaoComplemento = drfa.ComplementoDivisao.ToUpperTrim().ToNullIfEmpty(),
        //             NumeroRegistroRouboFurto = drfa.NumeroRegistro.ToUpperTrim().ToNullIfEmpty(),
        //             RegistroRouboFurtoMatriculaAgente = drfa.MatriculaAgente.ToUpperTrim().ToNullIfEmpty(),
        //             RegistroRouboFurtoNomeAgente = drfa.NomeAgente.ToUpperTrim().ToNullIfEmpty(),
        //             LocalRemocaoEnderecoCompleto = drfa.EnderecoCompleto.ToUpperTrim().ToNullIfEmpty(),
        //             LocalRemocaoReferencia = drfa.Referencia.ToUpperTrim().ToNullIfEmpty(),
        //             LocalRemocaoLongitude = drfa.Longitude.ToUpperTrim().ToNullIfEmpty(),
        //             LocalRemocaoLatitude = drfa.Latitude.ToUpperTrim().ToNullIfEmpty(),
        //             EstadoGeralVeiculo = drfa.EstadoGeralDoVeiculo.ToUpperTrim().ToNullIfEmpty(),
        //             FlagRegistroAgendado = drfa.FlagAgendamento,
        //             FlagRegistroRecuperacao = drfa.FlagRegistroRecuperacao,
        //             DataCadastro = DateTime.UtcNow,
        //         };
        //
        //         _context.DRFA.Add(result);
        //         _context.SaveChanges();
        //
        //         if (drfa.FlagRegistroRecuperacao == 'S')
        //         {
        //             if (drfa.RegistroRecuperacao == null)
        //             {
        //                 ResultView = MensagemViewHelper.SetBadRequest("Preencha os dados de Registro de Recuperação.");
        //                 return ResultView;
        //             }
        //
        //             var registroRecuperacao = CreateRegistroRecuperacao(result.GrvDrfaId, drfa.RegistroRecuperacao);
        //
        //             if (registroRecuperacao?.Erros != null && registroRecuperacao.Erros.Count > 0)
        //             {
        //                 ResultView = MensagemViewHelper.SetBadRequest(registroRecuperacao.Erros);
        //                 return ResultView;
        //             }
        //         }
        //         if (drfa.FlagAgendamento == 'S')
        //         {
        //             if (drfa.AgendamentoRetirada == null)
        //             {
        //                 ResultView = MensagemViewHelper.SetBadRequest("Preencha os dados de Agendamento de Retirada.");
        //                 return ResultView;
        //             }
        //
        //             var agendamentoRecuperacao = CreateAgendamentoRecuperacao(result.GrvDrfaId, drfa.AgendamentoRetirada);
        //
        //             if (agendamentoRecuperacao?.Erros != null && agendamentoRecuperacao.Erros.Count > 0)
        //             {
        //                 ResultView = MensagemViewHelper.SetBadRequest(agendamentoRecuperacao.Erros);
        //                 return ResultView;
        //             }
        //         }
        //         await _context.SaveChangesAsync();
        //         return ResultView;
        //     }
        //     catch (Exception ex)
        //     {
        //         ResultView = MensagemViewHelper.SetInternalServerError(ex);
        //         return ResultView;
        //     }
        //
        // }
        //
        // private MensagemDTO CreateRegistroRecuperacao(int DRFAId,  RegistroRecuperacaoParameters parameters)
        // {
        //     MensagemDTO ResultView = new();
        //     try
        //     {
        //         RegistroRecuperacaoModel result = new()
        //         {
        //             DRFAId = DRFAId,
        //             AutoridadeDivisaoId = parameters.DivisaoId,
        //             NumeroRegistroRecuperacao = parameters.NumeroRegistro.ToNullIfEmpty(),
        //             MatriculaAgente = parameters.MatriculaAgente.ToNullIfEmpty(),
        //             NomeAgente = parameters.NomeAgente.ToNullIfEmpty(),
        //             DataRegistroRecuperacao = parameters.DataDeRecuperacao
        //         };
        //         _context.DRFARegistroRecuperacao.Add(result);
        //         return ResultView;
        //     }catch(Exception ex)
        //     {
        //         ResultView = MensagemViewHelper.SetInternalServerError("Ocorreu um erro ao criar o registro de recuperação.", ex);
        //         return ResultView;
        //     }
        // }
        // private MensagemDTO CreateAgendamentoRecuperacao(int DRFAId, AgendamentoRetiradaParameters parameters)
        // {
        //     MensagemDTO ResultView = new();
        //     try
        //     {
        //         AgendamentoRetiradaModel result = new()
        //         {
        //             DRFAId = DRFAId,
        //             UsuarioRegistroAgendamentoId = parameters.UsuarioId,
        //             NomeResponsavelAgendamento = parameters.NomeResponsavel.ToNullIfEmpty(),
        //             CpfResponsavelAgendamento = parameters.CPF.Trim().ToNullIfEmpty(),
        //             DataRegistroAgendamento = parameters.DataDoRegistro,
        //             DataAgendamento = parameters.DataDoAgendamento
        //         };
        //         _context.DRFAAgendamentoRetirada.Add(result);
        //         ResultView = MensagemViewHelper.SetCreateSuccess();
        //         return ResultView;
        //     }catch (Exception ex)
        //     {
        //         ResultView = MensagemViewHelper.SetInternalServerError("Ocorreu um erro ao criar o agendamento de retirada.", ex);
        //         return ResultView;
        //     }
        // }
        //
        // public async Task<ListArquivosDRFADTO> GetArquivos(int GrvId, int UsuarioId)
        // {
        //     ListArquivosDRFADTO ResultView = new()
        //     {
        //         Mensagem = new GrvService(_context).ValidateInputGrv(GrvId, UsuarioId)
        //     };
        //
        //     if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
        //     {
        //         return ResultView;
        //     }
        //     var bucketService = new BucketService(_context, _httpClientFactory);
        //
        //     var furto = await bucketService.DownloadFileAsync(BucketNomeTabelaOrigemEnum.DRFAArquivoDeRouboFurto, GrvId);
        //
        //     if(furto.Listagem?.Count > 0)
        //     {
        //         ResultView.ArquivoRegistroFurtoRoubo = furto.Listagem.First();
        //     }
        //
        //     var recuperacao = await bucketService.DownloadFileAsync(BucketNomeTabelaOrigemEnum.DRFAArquivoRegistroRecuperacao, GrvId);
        //     if (recuperacao.Listagem?.Count > 0)
        //     {
        //         ResultView.ArquivoDeRecuperacao = recuperacao.Listagem.First();
        //     }
        //
        //     var qtd = (furto.Listagem?.Count ?? 0) + (recuperacao.Listagem?.Count ?? 0);
        //
        //     if(qtd > 0)
        //     {
        //         ResultView.Mensagem = MensagemViewHelper.SetFound(qtd);
        //     }
        //     else
        //     {
        //         ResultView.Mensagem = MensagemViewHelper.SetNotFound("Nenhum arquivo encontrado");
        //     }
        //
        //     return ResultView;
        // }
        //
        // public async Task<DRFADTO> GetDRFAAsync(int processoId)
        // {
        //     DRFADTO ResultView = new();
        //
        //     DRFAModel drfa = await _context.DRFA
        //         .AsNoTracking()
        //         .FirstOrDefaultAsync(x => x.GrvId == processoId);
        //     if (drfa == null)
        //     {
        //         ResultView.Mensagem = MensagemViewHelper.SetNotFound("DRFA não encontrado para o processo informado.");
        //         return ResultView;
        //     }
        //     ResultView = _mapper.Map<DRFADTO>(drfa);
        //
        //     if (drfa.FlagRegistroRecuperacao == 'S')
        //     {
        //         RegistroRecuperacaoModel registro = await _context.DRFARegistroRecuperacao
        //             .AsNoTracking()
        //             .FirstOrDefaultAsync(r => r.DRFAId == drfa.GrvDrfaId);
        //
        //         if (registro != null)
        //         {
        //             ResultView.RegistroRecuperacao = new RegistroRecuperacaoDTO
        //             {
        //                 IdentificadorRegistroRecuperacao = registro.GrvDRFARegistroRecuperacaoId,
        //                 IdentificadorDRFA = registro.DRFAId,
        //                 IdentificadorAutoridadeDivisao = registro.AutoridadeDivisaoId,
        //                 NumeroRegistroRecuperacao = registro.NumeroRegistroRecuperacao,
        //                 MatriculaAgente = registro.MatriculaAgente,
        //                 NomeAgente = registro.NomeAgente,
        //                 DataRegistroRecuperacao = registro.DataRegistroRecuperacao.ToString("yyyy-MM-dd HH:mm:ss")
        //             };
        //         }
        //     }
        //
        //     if (drfa.FlagRegistroAgendado == 'S')
        //     {
        //         AgendamentoRetiradaModel agendamento = await _context.DRFAAgendamentoRetirada
        //             .AsNoTracking()
        //             .Include(a => a.UsuarioRegistroAgendamento)
        //             .FirstOrDefaultAsync(a => a.DRFAId == drfa.GrvDrfaId);
        //
        //         if (agendamento != null)
        //         {
        //             ResultView.AgendamentoRetirada = new AgendamentoRetiradaDTO
        //             {
        //                 IdentificadorAgendamentoRetirada = agendamento.GrvDRFAAgendamentoRetiradaId,
        //                 IdentificadorDRFA = agendamento.DRFAId,
        //                 IdentificadorUsuarioRegistroAgendamento = agendamento.UsuarioRegistroAgendamentoId,
        //                 NomeResponsavelAgendamento = agendamento.NomeResponsavelAgendamento,
        //                 CpfResponsavelAgendamento = agendamento.CpfResponsavelAgendamento,
        //                 DataRegistroAgendamento = agendamento.DataRegistroAgendamento.ToString("yyyy-MM-dd HH:mm:ss"),
        //                 DataAgendamento = agendamento.DataAgendamento.ToString("yyyy-MM-dd HH:mm:ss"),
        //                 UsuarioRegistroAgendamento = agendamento.UsuarioRegistroAgendamento == null
        //                     ? null
        //                     : new UsuarioDTO
        //                     {
        //                         IdentificadorUsuario = agendamento.UsuarioRegistroAgendamento.UsuarioId,
        //                         Nome = agendamento.UsuarioRegistroAgendamento.Login
        //                     }
        //             };
        //         }
        //     }
        //
        //     return ResultView;
        // }
        //
        // public MensagemDTO UpdateDRFAGrv(GrvAtualizarParameters Grv)
        // {
        //     MensagemDTO ResultView = new();
        //
        //     if (Grv.DRFA == null)
        //     {
        //         return MensagemViewHelper.SetBadRequest("Os dados da DRFA são obrigatórios para veículos com motivo de apreensão Roubo/Furto.");
        //     }
        //
        //     #region Consulta
        //     var drfa = _context.DRFA
        //         .AsTracking()
        //         .FirstOrDefault(x => x.GrvId == Grv.IdentificadorGrv);
        //     #endregion Consulta
        //
        //     if (drfa is null)
        //     { 
        //         ResultView = MensagemViewHelper.SetBadRequest("DRFA não existe");
        //         return ResultView;
        //     }
        //
        //     drfa.TipoRegistroId = Grv.DRFA.TipoRegistroId;
        //     drfa.OrgaoEmissorId = Grv.DRFA.OrgaoEmissorId;
        //     drfa.AutoridadeDivisaoId = Grv.DRFA.DivisaoId;
        //     drfa.UsuarioAlteracaoId = Grv.IdentificadorUsuario;
        //     drfa.AutoridadeDivisaoComplemento = Grv.DRFA.ComplementoDivisao;
        //     drfa.NumeroRegistroRouboFurto = Grv.DRFA.NumeroRegistro;
        //     drfa.RegistroRouboFurtoMatriculaAgente = Grv.DRFA.MatriculaAgente;
        //     drfa.RegistroRouboFurtoNomeAgente = Grv.DRFA.NomeAgente;
        //     drfa.LocalRemocaoEnderecoCompleto = Grv.DRFA.EnderecoCompleto;
        //     drfa.LocalRemocaoReferencia = Grv.DRFA.Referencia;
        //     drfa.LocalRemocaoLatitude = Grv.DRFA.Latitude;
        //     drfa.LocalRemocaoLongitude = Grv.DRFA.Longitude;
        //     drfa.EstadoGeralVeiculo = Grv.DRFA.EstadoGeralDoVeiculo;
        //     drfa.DataAlteracao = DateTime.UtcNow;
        //     drfa.FlagRegistroRecuperacao = Grv.DRFA.FlagRegistroRecuperacao;
        //     drfa.FlagRegistroAgendado = Grv.DRFA.FlagAgendamento;
        //
        //     _context.DRFA.Update(drfa);
        //     if (Grv.DRFA.FlagRegistroRecuperacao == 'S')
        //     {
        //         var response = UpdateRegistroRecuperacao(drfa.GrvDrfaId, Grv.DRFA.RegistroRecuperacao);
        //         if (response.Erros?.Count > 0)
        //         {
        //             ResultView = MensagemViewHelper.SetBadRequest(response.Erros);
        //             return ResultView;
        //         }
        //     }
        //     if (Grv.DRFA.FlagAgendamento == 'S')
        //     {
        //         var response = UpdateAgendamentoRecuperacao(drfa.GrvDrfaId, Grv.DRFA.AgendamentoRetirada);
        //         if(response.Erros?.Count > 0)
        //         {
        //             ResultView = MensagemViewHelper.SetBadRequest(response.Erros);
        //             return ResultView;
        //         }
        //     }
        //     _context.SaveChanges();
        //     ResultView = MensagemViewHelper.SetUpdateSuccess();
        //     return ResultView;
        // }
        // private MensagemDTO UpdateRegistroRecuperacao(int DRFAId, RegistroRecuperacaoParameters parameters)
        // {
        //     MensagemDTO ResultView = new();
        //     #region Consulta
        //     var registroRecuperacao = _context.DRFARegistroRecuperacao
        //         .AsTracking()
        //         .FirstOrDefault(x => x.DRFAId == DRFAId);
        //     #endregion
        //
        //     if(registroRecuperacao is null)
        //     {
        //         ResultView = MensagemViewHelper.SetBadRequest("Registro de Recuperação não encontrado");
        //         return ResultView;
        //     }
        //
        //     try
        //     {
        //         registroRecuperacao.AutoridadeDivisaoId = parameters.DivisaoId;
        //         registroRecuperacao.NumeroRegistroRecuperacao = parameters.NumeroRegistro.ToNullIfEmpty();
        //         registroRecuperacao.MatriculaAgente = parameters.MatriculaAgente.ToNullIfEmpty();
        //         registroRecuperacao.NomeAgente = parameters.NomeAgente.ToNullIfEmpty();
        //         registroRecuperacao.DataRegistroRecuperacao = parameters.DataDeRecuperacao;
        //         _context.DRFARegistroRecuperacao.Update(registroRecuperacao);
        //         ResultView = MensagemViewHelper.SetUpdateSuccess();
        //         return ResultView;
        //     }
        //     catch (Exception ex)
        //     {
        //         ResultView = MensagemViewHelper.SetInternalServerError("Ocorreu um erro ao atualizar o registro de recuperação.", ex);
        //         return ResultView;
        //     }
        // }
        // private MensagemDTO UpdateAgendamentoRecuperacao(int DRFAId, AgendamentoRetiradaParameters parameters)
        // {
        //     MensagemDTO ResultView = new();
        //     #region Consulta
        //     var agendamentoRetirada = _context.DRFAAgendamentoRetirada
        //         .AsTracking()
        //         .FirstOrDefault(x => x.DRFAId == DRFAId);
        //     #endregion
        //     try
        //     {
        //         agendamentoRetirada.UsuarioRegistroAgendamentoId = parameters.UsuarioId;
        //         agendamentoRetirada.NomeResponsavelAgendamento = parameters.NomeResponsavel.ToNullIfEmpty();
        //         agendamentoRetirada.CpfResponsavelAgendamento = parameters.CPF.Trim().ToNullIfEmpty();
        //         agendamentoRetirada.DataRegistroAgendamento = parameters.DataDoRegistro;
        //         agendamentoRetirada.DataAgendamento = parameters.DataDoAgendamento;
        //         _context.DRFAAgendamentoRetirada.Update(agendamentoRetirada);
        //         ResultView = MensagemViewHelper.SetUpdateSuccess();
        //         return ResultView;
        //     }
        //     catch (Exception ex)
        //     {
        //         ResultView = MensagemViewHelper.SetInternalServerError("Ocorreu um erro ao atualizar o agendamento de retirada.", ex);
        //         return ResultView;
        //     }
        // }
    }
}
