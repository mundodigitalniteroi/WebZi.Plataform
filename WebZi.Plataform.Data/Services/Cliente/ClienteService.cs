using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Database;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.DTO.Cliente;
using WebZi.Plataform.Domain.DTO.Generic;
using WebZi.Plataform.Domain.DTO.GRV.Pesquisa;
using WebZi.Plataform.Domain.DTO.Sistema;
using WebZi.Plataform.Domain.Enums;
using WebZi.Plataform.Domain.Models.Cliente;
using WebZi.Plataform.Domain.Models.Sistema;
using WebZi.Plataform.Domain.Models.Usuario;

namespace WebZi.Plataform.Data.Services.Cliente
{
    public class ClienteService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public ClienteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ClienteService(AppDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ClienteListDTO> GetByIdAsync(int ClienteId)
        {
            ClienteListDTO ResultView = new();

            if (ClienteId <= 0)
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorClienteInvalido);

                return ResultView;
            }

            ClienteModel result = await _context.Cliente
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ClienteId == ClienteId);

            if (result != null)
            {
                ResultView.Listagem.Add(_mapper.Map<ClienteDTO>(result));

                ResultView.Mensagem = MensagemViewHelper.SetFound();
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<ClienteListDTO> GetByNameAsync(string Name)
        {
            ClienteListDTO ResultView = new();

            if (string.IsNullOrWhiteSpace(Name))
            {
                ResultView.Mensagem = MensagemViewHelper.SetBadRequest("Informe o Nome do Cliente");

                return ResultView;
            }

            List<ClienteModel> result = await _context.Cliente
                .Where(x => x.Nome.ToUpper().Contains(Name.ToUpper().Trim()))
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<ClienteDTO>>(result
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

        public async Task<ImageListDTO> GetLogomarcaAsync(int ClienteId)
        {
            ImageListDTO ResultView = await new BucketService(_context, _httpClientFactory)
                .DownloadFileAsync("CADLOGOCLIENTE", ClienteId);

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok && ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.NotFound)
            {
                return ResultView;
            }

            if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.NotFound)
            {
                return ResultView;
            }
            else
            {
                ConfiguracaoLogoModel ConfiguracaoLogo = await _context.ConfiguracaoLogo
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                ResultView.Listagem.Add(new ImageDTO { Imagem = ConfiguracaoLogo.LogoPadraoSistema });

                ResultView.Mensagem = MensagemViewHelper.SetFound();

                return ResultView;
            }
        }

        public async Task<ClienteListDTO> ListAsync(int UsuarioId)
        {
            ClienteListDTO ResultView = new();

            List<UsuarioClienteModel> result = await _context.UsuarioCliente
                .Include(x => x.Cliente)
                .Where(x => x.UsuarioId == UsuarioId)
                .AsNoTracking()
                .ToListAsync();

            if (result?.Count > 0)
            {
                List<ClienteModel> Clientes = new();

                foreach (UsuarioClienteModel UsuarioCliente in result)
                {
                    Clientes.Add(UsuarioCliente.Cliente);
                }

                ResultView.Listagem = _mapper.Map<List<ClienteDTO>>(Clientes
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

        public async Task<ClienteSimplificadoListDTO> ListResumeAsync(int UsuarioId)
        {
            ClienteSimplificadoListDTO ResultView = new();

            ClienteListDTO result = await ListAsync(UsuarioId);

            if (result.Listagem?.Count > 0)
            {
                ResultView.Listagem = _mapper.Map<List<ClienteSimplificadoDTO>>(result.Listagem);

                ResultView.Mensagem = MensagemViewHelper.SetFound(result.Listagem.Count);
            }
            else
            {
                ResultView.Mensagem = MensagemViewHelper.SetNotFound();
            }

            return ResultView;
        }

        public async Task<MensagemDTO> ValidateClienteAsync(int ClienteId)
        {
            if (ClienteId <= 0)
            {
                return MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.IdentificadorClienteInvalido);
            }
            else
            {
                if (!await _context.Cliente.AsNoTracking().AnyAsync(x => x.ClienteId == ClienteId))
                {
                    return MensagemViewHelper.SetBadRequest(MensagemPadraoEnum.NaoEncontradoCliente);
                }
            }

            return MensagemViewHelper.SetOk();
        }
    }
}