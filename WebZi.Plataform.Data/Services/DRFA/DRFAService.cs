using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Strings;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.GRV;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.DTO.DRFA;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.DTO.Usuario;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.GRV.DRFA;
using WebZi.Plataform.Domain.ViewModel.GRV.Cadastro;

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

        public async Task<MensagemDTO> CreateDRFAGrv(int GrvId, GrvParameters grv)
        {
            var drfa = grv.DRFA;

            if (drfa == null)
            {
                return MensagemViewHelper.SetBadRequest("Os dados da DRFA são obrigatórios para veículos com motivo de apreensão Roubo/Furto.");
            }

            if (drfa.FlagRegistroRecuperacao == 'S' && drfa.RegistroRecuperacao == null)
            {
                return MensagemViewHelper.SetBadRequest("Preencha os dados de Registro de Recuperação.");
            }

            if (drfa.FlagAgendamento == 'S' && drfa.AgendamentoRetirada == null)
            {
                return MensagemViewHelper.SetBadRequest("Preencha os dados de Agendamento de Retirada.");
            }

            try
            {
                DRFAModel result = new()
                {
                    GrvId = GrvId,
                    TipoRegistroId = drfa.TipoRegistroId,
                    OrgaoEmissorId = drfa.OrgaoEmissorId,
                    AutoridadeDivisaoId = drfa.DivisaoId,
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
                await _context.SaveChangesAsync();

                if (drfa.FlagRegistroRecuperacao == 'S')
                {
                    CreateRegistroRecuperacao(result.GrvDrfaId, drfa.RegistroRecuperacao);
                }

                if (drfa.FlagAgendamento == 'S')
                {
                    CreateAgendamentoRecuperacao(result.GrvDrfaId, drfa.AgendamentoRetirada);
                }

                if (drfa.FlagRegistroRecuperacao == 'S' || drfa.FlagAgendamento == 'S')
                {
                    await _context.SaveChangesAsync();
                }

                return MensagemViewHelper.SetCreateSuccess();
            }
            catch (Exception ex)
            {
                return MensagemViewHelper.SetInternalServerError(ex);
            }
        }

        private void CreateRegistroRecuperacao(int drfaId, RegistroRecuperacaoParameters parameters)
        {
            RegistroRecuperacaoModel result = new()
            {
                DRFAId = drfaId,
                AutoridadeDivisaoId = parameters.DivisaoId,
                NumeroRegistroRecuperacao = parameters.NumeroRegistro.ToNullIfEmpty(),
                MatriculaAgente = parameters.MatriculaAgente.ToNullIfEmpty(),
                NomeAgente = parameters.NomeAgente.ToNullIfEmpty(),
                DataRegistroRecuperacao = parameters.DataDeRecuperacao
            };
            _context.DRFARegistroRecuperacao.Add(result);
        }

        private void CreateAgendamentoRecuperacao(int drfaId, AgendamentoRetiradaParameters parameters)
        {
            AgendamentoRetiradaModel result = new()
            {
                DRFAId = drfaId,
                UsuarioRegistroAgendamentoId = parameters.UsuarioId,
                NomeResponsavelAgendamento = parameters.NomeResponsavel.ToNullIfEmpty(),
                CpfResponsavelAgendamento = parameters.CPF?.Trim().ToNullIfEmpty(),
                DataRegistroAgendamento = parameters.DataDoRegistro,
                DataAgendamento = parameters.DataDoAgendamento
            };
            _context.DRFAAgendamentoRetirada.Add(result);
        }

        public async Task<ListArquivosDRFADTO> GetArquivos(int GrvId, int UsuarioId)
        {
            ListArquivosDRFADTO ResultView = new()
            {
                Mensagem = new GrvService(_context).ValidateInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            var bucketService = new BucketService(_context, _httpClientFactory);

            var furtoTask = bucketService.DownloadFileAsync(BucketNomeTabelaOrigemEnum.DRFAArquivoDeRouboFurto, GrvId);
            var recuperacaoTask = bucketService.DownloadFileAsync(BucketNomeTabelaOrigemEnum.DRFAArquivoRegistroRecuperacao, GrvId);

            await Task.WhenAll(furtoTask, recuperacaoTask);

            var furto = await furtoTask;
            var recuperacao = await recuperacaoTask;

            if (furto.Listagem?.Count > 0)
            {
                ResultView.ArquivoRegistroFurtoRoubo = furto.Listagem.First();
            }

            if (recuperacao.Listagem?.Count > 0)
            {
                ResultView.ArquivoDeRecuperacao = recuperacao.Listagem.First();
            }

            var qtd = (furto.Listagem?.Count ?? 0) + (recuperacao.Listagem?.Count ?? 0);

            ResultView.Mensagem = qtd > 0
                ? MensagemViewHelper.SetFound(qtd)
                : MensagemViewHelper.SetNotFound("Nenhum arquivo encontrado");

            return ResultView;
        }

        public async Task<DRFADTO> GetDRFAAsync(int processoId)
        {
            DRFAModel drfa = await _context.DRFA
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.GrvId == processoId);

            if (drfa == null)
            {
                return new DRFADTO
                {
                    Mensagem = MensagemViewHelper.SetNotFound("DRFA não encontrado para o processo informado.")
                };
            }

            var ResultView = _mapper.Map<DRFADTO>(drfa);

            var registroTask = drfa.FlagRegistroRecuperacao == 'S'
                ? _context.DRFARegistroRecuperacao
                    .AsNoTracking()
                    .FirstOrDefaultAsync(r => r.DRFAId == drfa.GrvDrfaId)
                : Task.FromResult<RegistroRecuperacaoModel>(null);

            var agendamentoTask = drfa.FlagRegistroAgendado == 'S'
                ? _context.DRFAAgendamentoRetirada
                    .AsNoTracking()
                    .Include(a => a.UsuarioRegistroAgendamento)
                    .FirstOrDefaultAsync(a => a.DRFAId == drfa.GrvDrfaId)
                : Task.FromResult<AgendamentoRetiradaModel>(null);

            await Task.WhenAll(registroTask, agendamentoTask);

            var registro = await registroTask;
            if (registro != null)
            {
                ResultView.RegistroRecuperacao = new RegistroRecuperacaoDTO
                {
                    IdentificadorRegistroRecuperacao = registro.GrvDRFARegistroRecuperacaoId,
                    IdentificadorDRFA = registro.DRFAId,
                    IdentificadorAutoridadeDivisao = registro.AutoridadeDivisaoId,
                    NumeroRegistroRecuperacao = registro.NumeroRegistroRecuperacao,
                    MatriculaAgente = registro.MatriculaAgente,
                    NomeAgente = registro.NomeAgente,
                    DataRegistroRecuperacao = registro.DataRegistroRecuperacao.ToString("yyyy-MM-dd HH:mm:ss")
                };
            }

            var agendamento = await agendamentoTask;
            if (agendamento != null)
            {
                ResultView.AgendamentoRetirada = new AgendamentoRetiradaDTO
                {
                    IdentificadorAgendamentoRetirada = agendamento.GrvDRFAAgendamentoRetiradaId,
                    IdentificadorDRFA = agendamento.DRFAId,
                    IdentificadorUsuarioRegistroAgendamento = agendamento.UsuarioRegistroAgendamentoId,
                    NomeResponsavelAgendamento = agendamento.NomeResponsavelAgendamento,
                    CpfResponsavelAgendamento = agendamento.CpfResponsavelAgendamento,
                    DataRegistroAgendamento = agendamento.DataRegistroAgendamento.ToString("yyyy-MM-dd HH:mm:ss"),
                    DataAgendamento = agendamento.DataAgendamento.ToString("yyyy-MM-dd HH:mm:ss"),
                    UsuarioRegistroAgendamento = agendamento.UsuarioRegistroAgendamento == null
                        ? null
                        : new UsuarioDTO
                        {
                            IdentificadorUsuario = agendamento.UsuarioRegistroAgendamento.UsuarioId,
                            Nome = agendamento.UsuarioRegistroAgendamento.Login
                        }
                };
            }

            return ResultView;
        }

        public async Task<MensagemDTO> UpdateDRFAGrv(GrvAtualizarParameters Grv)
        {
            if (Grv.DRFA == null)
            {
                return MensagemViewHelper.SetBadRequest("Os dados da DRFA são obrigatórios para veículos com motivo de apreensão Roubo/Furto.");
            }

            #region Consulta
            var drfa = await _context.DRFA
                .AsTracking()
                .FirstOrDefaultAsync(x => x.GrvId == Grv.IdentificadorGrv);
            #endregion Consulta

            if (drfa is null)
            {
                return MensagemViewHelper.SetBadRequest("DRFA não existe");
            }

            drfa.TipoRegistroId = Grv.DRFA.TipoRegistroId;
            drfa.OrgaoEmissorId = Grv.DRFA.OrgaoEmissorId;
            drfa.AutoridadeDivisaoId = Grv.DRFA.DivisaoId;
            drfa.UsuarioAlteracaoId = Grv.IdentificadorUsuario;
            drfa.AutoridadeDivisaoComplemento = Grv.DRFA.ComplementoDivisao;
            drfa.NumeroRegistroRouboFurto = Grv.DRFA.NumeroRegistro;
            drfa.RegistroRouboFurtoMatriculaAgente = Grv.DRFA.MatriculaAgente;
            drfa.RegistroRouboFurtoNomeAgente = Grv.DRFA.NomeAgente;
            drfa.LocalRemocaoEnderecoCompleto = Grv.DRFA.EnderecoCompleto;
            drfa.LocalRemocaoReferencia = Grv.DRFA.Referencia;
            drfa.LocalRemocaoLatitude = Grv.DRFA.Latitude;
            drfa.LocalRemocaoLongitude = Grv.DRFA.Longitude;
            drfa.EstadoGeralVeiculo = Grv.DRFA.EstadoGeralDoVeiculo;
            drfa.DataAlteracao = DateTime.UtcNow;
            drfa.FlagRegistroRecuperacao = Grv.DRFA.FlagRegistroRecuperacao;
            drfa.FlagRegistroAgendado = Grv.DRFA.FlagAgendamento;

            if (Grv.DRFA.FlagRegistroRecuperacao == 'S')
            {
                var response = await UpdateRegistroRecuperacaoAsync(drfa.GrvDrfaId, Grv.DRFA.RegistroRecuperacao);
                if (response.Erros?.Count > 0)
                {
                    return MensagemViewHelper.SetBadRequest(response.Erros);
                }
            }

            if (Grv.DRFA.FlagAgendamento == 'S')
            {
                var response = await UpdateAgendamentoRecuperacaoAsync(drfa.GrvDrfaId, Grv.DRFA.AgendamentoRetirada);
                if (response.Erros?.Count > 0)
                {
                    return MensagemViewHelper.SetBadRequest(response.Erros);
                }
            }

            await _context.SaveChangesAsync();
            return MensagemViewHelper.SetUpdateSuccess();
        }

        private async Task<MensagemDTO> UpdateRegistroRecuperacaoAsync(int drfaId, RegistroRecuperacaoParameters parameters)
        {
            if (parameters == null)
            {
                return MensagemViewHelper.SetBadRequest("Preencha os dados de Registro de Recuperação.");
            }

            #region Consulta
            var registroRecuperacao = await _context.DRFARegistroRecuperacao
                .AsTracking()
                .FirstOrDefaultAsync(x => x.DRFAId == drfaId);
            #endregion

            if (registroRecuperacao is null)
            {
                return MensagemViewHelper.SetBadRequest("Registro de Recuperação não encontrado");
            }

            try
            {
                registroRecuperacao.AutoridadeDivisaoId = parameters.DivisaoId;
                registroRecuperacao.NumeroRegistroRecuperacao = parameters.NumeroRegistro.ToNullIfEmpty();
                registroRecuperacao.MatriculaAgente = parameters.MatriculaAgente.ToNullIfEmpty();
                registroRecuperacao.NomeAgente = parameters.NomeAgente.ToNullIfEmpty();
                registroRecuperacao.DataRegistroRecuperacao = parameters.DataDeRecuperacao;
                return MensagemViewHelper.SetUpdateSuccess();
            }
            catch (Exception ex)
            {
                return MensagemViewHelper.SetInternalServerError("Ocorreu um erro ao atualizar o registro de recuperação.", ex);
            }
        }

        private async Task<MensagemDTO> UpdateAgendamentoRecuperacaoAsync(int drfaId, AgendamentoRetiradaParameters parameters)
        {
            if (parameters == null)
            {
                return MensagemViewHelper.SetBadRequest("Preencha os dados de Agendamento de Retirada.");
            }

            #region Consulta
            var agendamentoRetirada = await _context.DRFAAgendamentoRetirada
                .AsTracking()
                .FirstOrDefaultAsync(x => x.DRFAId == drfaId);
            #endregion

            if (agendamentoRetirada is null)
            {
                return MensagemViewHelper.SetBadRequest("Agendamento de Retirada não encontrado");
            }

            try
            {
                agendamentoRetirada.UsuarioRegistroAgendamentoId = parameters.UsuarioId;
                agendamentoRetirada.NomeResponsavelAgendamento = parameters.NomeResponsavel.ToNullIfEmpty();
                agendamentoRetirada.CpfResponsavelAgendamento = parameters.CPF?.Trim().ToNullIfEmpty();
                agendamentoRetirada.DataRegistroAgendamento = parameters.DataDoRegistro;
                agendamentoRetirada.DataAgendamento = parameters.DataDoAgendamento;
                return MensagemViewHelper.SetUpdateSuccess();
            }
            catch (Exception ex)
            {
                return MensagemViewHelper.SetInternalServerError("Ocorreu um erro ao atualizar o agendamento de retirada.", ex);
            }
        }
    }
}
