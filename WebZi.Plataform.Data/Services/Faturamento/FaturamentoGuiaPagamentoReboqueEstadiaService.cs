using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Domain.Models.Bucket;
using WebZi.Plataform.Domain.Models.Faturamento.Boleto;
using WebZi.Plataform.Domain.Models.Faturamento;
using WebZi.Plataform.Domain.Services.GRV;
using WebZi.Plataform.Domain.Services.Usuario;
using WebZi.Plataform.Domain.ViewModel.Generic;
using System.Net;
using WebZi.Plataform.Data.Services.Bucket;
using WebZi.Plataform.Domain.ViewModel.Faturamento;

namespace WebZi.Plataform.Data.Services.Faturamento
{
    public class FaturamentoGuiaPagamentoReboqueEstadiaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FaturamentoGuiaPagamentoReboqueEstadiaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ImageViewModel GetGuiaPagamentoReboqueEstadia(int FaturamentoId, int UsuarioId)
        {
            ImageViewModel ResultView = new();

            List<string> erros = new();

            if (FaturamentoId <= 0)
            {
                erros.Add("Identificador do Faturamento inválido");
            }

            if (UsuarioId <= 0)
            {
                erros.Add("Identificador do Usuário inválido");
            }

            if (erros.Count > 0)
            {
                ResultView.Mensagem.HtmlStatusCode = HtmlStatusCodeEnum.BadRequest;

                foreach (string erro in erros)
                {
                    ResultView.Mensagem.AvisosImpeditivos.Add(erro);
                }

                return ResultView;
            }

            if (!new UsuarioService(_context).IsUserActive(UsuarioId))
            {
                ResultView.Mensagem = MensagemViewHelper.GetUnauthorized();

                return ResultView;
            }

            FaturamentoModel Faturamento = new();

            Faturamento = _context.Faturamento
                .Include(i => i.TipoMeioCobranca)
                .Include(i => i.Atendimento)
                .ThenInclude(t => t.Grv)
                .Where(w => w.FaturamentoId == FaturamentoId)
                .OrderByDescending(o => o.DataCadastro)
                .AsNoTracking()
                .FirstOrDefault();

            if (Faturamento != null)
            {
                if (!new GrvService(_context, _mapper).UserCanAccessGrv(Faturamento.Atendimento.Grv, UsuarioId))
                {
                    ResultView.Mensagem = MensagemViewHelper.GetUnauthorized("Usuário sem permissão de acesso ao GRV ou GRV inexistente");

                    return ResultView;
                }
            }
            else if (Faturamento == null)
            {
                ResultView.Mensagem = MensagemViewHelper.GetNotFound("Faturamento não encontrado");

                return ResultView;
            }
            else if (Faturamento.TipoMeioCobranca.DocumentoImpressao == null || !Faturamento.TipoMeioCobranca.DocumentoImpressao.Equals("GuiaPagamentoEstadiaReboque.rdlc"))
            {
                ResultView.Mensagem = MensagemViewHelper.GetBadRequest($"Esse Faturamento está cadastrado em outra Forma de Pagamento: {Faturamento.TipoMeioCobranca.Descricao}");

                return ResultView;
            }

            GuiaPagamentoReboqueEstadiaViewModel GuiaPagamentoEstadiaReboque = new();
            
            if (!string.IsNullOrWhiteSpace(Faturamento.Atendimento.Grv.Placa))
            {
                GuiaPagamentoEstadiaReboque.grv_placa_chassi = Faturamento.Atendimento.Grv.Placa;
            }
            else
            {
                GuiaPagamentoEstadiaReboque.grv_placa_chassi = Faturamento.Atendimento.Grv.Chassi;
            }

            GuiaPagamentoEstadiaReboque.id_grv = Faturamento.Atendimento.GrvId;

            GuiaPagamentoEstadiaReboque.id_atendimento = Faturamento.Atendimento.AtendimentoId;

            GuiaPagamentoEstadiaReboque.grv_placa = Faturamento.Atendimento.Grv.Placa;

            GuiaPagamentoEstadiaReboque.grv_chassi = Faturamento.Atendimento.Grv.Chassi;

            GuiaPagamentoEstadiaReboque.grv_renavam = Faturamento.Atendimento.Grv.Renavam;

            GuiaPagamentoEstadiaReboque.grv_numero_formulario = Faturamento.Atendimento.Grv.NumeroFormularioGrv;

            GuiaPagamentoEstadiaReboque.grv_data_hora_remocao = Faturamento.Atendimento.Grv.DataHoraRemocao.ToString("dd/MM/yyyy HH:mm");

            GuiaPagamentoEstadiaReboque.grv_data_hora_guarda = Faturamento.Atendimento.Grv.DataHoraGuarda.ToString("dd/MM/yyyy HH:mm");

            GuiaPagamentoEstadiaReboque.grv_data_guarda = Faturamento.Atendimento.Grv.DataHoraGuarda.ToString("dd/MM/yyyy");

            GuiaPagamentoEstadiaReboque.grv_hora_guarda = Faturamento.Atendimento.Grv.DataHoraGuarda.ToString("HH:mm");

            GuiaPagamentoEstadiaReboque.grv_estacionamento_setor = Faturamento.Atendimento.Grv.EstacionamentoSetor;

            GuiaPagamentoEstadiaReboque.grv_estacionamento_numero_vaga = Faturamento.Atendimento.Grv.EstacionamentoNumeroVaga;

            GuiaPagamentoEstadiaReboque.grv_numero_chave = Faturamento.Atendimento.Grv.NumeroChave;
        }

        public byte[] GetLogomarca(int ClienteId)
        {
            byte[] Logomarca = new BucketArquivoService(_context)
                .DownloadFile("CADLOGOCLIENTE", ClienteId);

            return Logomarca ?? null;
        }
    }
}