﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Bucket;
using WebZi.Plataform.Data.Services.Sistema;
using WebZi.Plataform.Data.Services.Veiculo;
using WebZi.Plataform.Data.Services.Vistoria;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.GRV;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.GGV;
using WebZi.Plataform.Domain.ViewModel.GRV;

namespace WebZi.Plataform.Data.Services.GGV
{
    public class GgvService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GgvService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DadosMestresViewModel> ListarDadosMestresAsync(byte TipoVeiculoId)
        {
            VistoriaService VistoriaService = new(_context);

            DadosMestresViewModel DadosMestres = new()
            {
                ListagemCorOstentada = await new SistemaService(_context, _mapper)
                    .ListarCorAsync(),

                ListagemEquipamento = await new VeiculoService(_context, _mapper)
                    .ListarEquipamentoOpcionalAsync(TipoVeiculoId),

                ListagemEstadoGeralVeiculo = await VistoriaService
                    .ListarEstadoGeralVeiculoAsync(),

                ListagemSituacaoChassi = await VistoriaService
                    .ListarSituacaoChassiAsync(),

                ListagemStatusVistoria = await VistoriaService
                    .ListarStatusVistoriaAsync(),

                ListagemTipoAvaria = await new TipoAvariaService(_context, _mapper)
                    .ListarTipoAvariaAsync(),

                ListagemTipoDirecao = await VistoriaService
                    .ListarTipoDirecaoAsync()
            };

            return DadosMestres;
        }

        public MensagemViewModel CadastrarFotos(GrvFotoViewModel Fotos)
        {
            MensagemViewModel ResultView = new GrvService(_context)
                .ValidarInputGrv(Fotos.IdentificadorGrv, Fotos.IdentificadorUsuario);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            if (Fotos.Fotos.Count == 0)
            {
                return MensagemViewHelper.GetBadRequest("Nenhuma imagem enviada para a API");
            }

            GrvModel Grv = GetGrv(Fotos.IdentificadorGrv);
            
            if (!new[] { "V", "L", "U", "T", "R", "E", "B", "D", "1", "2", "3", "4" }.Contains(Grv.StatusOperacao.StatusOperacaoId))
            {
                return MensagemViewHelper.GetBadRequest($"O Status da Operação deste GRV não permite o envio de Fotos. Status atual: {Grv.StatusOperacao.Descricao}");
            }

            new BucketArquivoService(_context)
                .SendFiles("GGVFOTOSVEICCAD", Fotos.IdentificadorGrv, Fotos.IdentificadorUsuario, Fotos.Fotos);

            return MensagemViewHelper.GetOkCreate(Fotos.Fotos.Count);
        }

        public async Task<ImageViewModelList> ListarFotosAsync(int GrvId, int UsuarioId)
        {
            ImageViewModelList ResultView = new()
            {
                Mensagem = new GrvService(_context).ValidarInputGrv(GrvId, UsuarioId)
            };

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            return await new BucketArquivoService(_context)
                .DownloadFiles("GGVFOTOSVEICCAD", GrvId);
        }

        public async Task<MensagemViewModel> ExcluirFotosAsync(int GrvId, int UsuarioId, List<int> ListagemTabelaOrigemId)
        {
            if (ListagemTabelaOrigemId.Count == 0)
            {
                return MensagemViewHelper.GetBadRequest("Informe os Identificadores das Fotos");
            }

            MensagemViewModel ResultView = new GrvService(_context).ValidarInputGrv(GrvId, UsuarioId);

            if (ResultView.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
            {
                return ResultView;
            }

            GrvModel Grv = await GetGrvAsync(GrvId);

            if (!new[] { "E", "B", "D", "G", "L", "R", "T", "U", "V" }.Contains(Grv.StatusOperacaoId))
            {
                return MensagemViewHelper.GetBadRequest("O GRV está em um Status de Operação que impede a exclusão de Fotos");
            }

            List<BucketArquivoModel> BucketArquivos = await _context.BucketArquivo
                .Include(x => x.BucketNomeTabelaOrigem)
                .Where(x => x.TabelaOrigemId != GrvId
                         && ListagemTabelaOrigemId.Contains(x.RepositorioArquivoId)
                         && x.BucketNomeTabelaOrigem.Codigo == "GGVFOTOSVEICCAD")
                .AsNoTracking()
                .ToListAsync();

            if (BucketArquivos?.Count > 0)
            {
                List<string> erros = new()
                {
                    $"A(s) seguinte(s) Fotos não pertencem ao GRV {GrvId}:"
                };

                foreach (BucketArquivoModel BucketArquivo in BucketArquivos)
                {
                    erros.Add(BucketArquivo.RepositorioArquivoId.ToString());
                }

                return MensagemViewHelper.GetBadRequest(erros);
            }

            new BucketArquivoService(_context)
                .DeleteFiles("GGVFOTOSVEICCAD", ListagemTabelaOrigemId);

            return MensagemViewHelper.GetOkDelete(BucketArquivos.Count, "Foto(s) excluída(s) com sucesso");
        }

        private GrvModel GetGrv(int GrvId)
        {
            return _context.Grv
                .Include(x => x.StatusOperacao)
                .Where(x => x.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefault();
        }

        private async Task<GrvModel> GetGrvAsync(int GrvId)
        {
            return await _context.Grv
                .Include(x => x.StatusOperacao)
                .Where(x => x.GrvId == GrvId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}