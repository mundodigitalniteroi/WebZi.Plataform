using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Banco;
using WebZi.Plataform.Data.Services.Banco.PIX;
using WebZi.Plataform.Data.Services.Report;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.DTO.Banco;
using WebZi.Plataform.Domain.DTO.Banco.PIX;
using WebZi.Plataform.Domain.DTO.Report;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public BancoController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpGet("GerarBoleto")]
        // TODO: [Authorize]
        public ActionResult<BoletoDTO> GerarBoleto(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BoletoDTO ResultView = new();

            try
            {
                ResultView = _provider
                    .GetService<BoletoService>()
                    .Create(IdentificadorFaturamento, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("GerarPixDinamico")]
        // TODO: [Authorize]
        public async Task<ActionResult<PixDinamicoCompletoDTO>> GerarPixDinamico(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PixDinamicoCompletoDTO ResultView = new();

            try
            {
                PixDinamicoDTO pixDinamicoDTO = await _provider
                    .GetService<PixDinamicoService>()
                    .CreateAsync(IdentificadorFaturamento, IdentificadorUsuario);

                if (pixDinamicoDTO.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                {
                    ResultView.Mensagem = pixDinamicoDTO.Mensagem;
                    return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
                }

                GuiaPagamentoReboqueEstadiaDTO guiaPagamentoReboqueEstadiaDTO = await _provider
                 .GetService<GuiaPagamentoReboqueEstadiaService>()
                 .GetGuiaPagamentoReboqueEstadiaAsync(IdentificadorFaturamento, IdentificadorUsuario);

                ResultView.PixDinamico = pixDinamicoDTO;
                ResultView.GuiaPagamentoReboqueEstadia = guiaPagamentoReboqueEstadiaDTO;

                ResultView.Mensagem = pixDinamicoDTO.Mensagem;

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ConsultarPixDinamico")]
        // TODO: [Authorize]
        public async Task<ActionResult<PixDinamicoCompletoDTO>> ConsultarPixDinamico(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PixDinamicoCompletoDTO ResultView = new();

            try
            {
                PixDinamicoDTO pixDinamicoDTO = await _provider
                    .GetService<PixDinamicoService>()
                    .CreateAsync(IdentificadorFaturamento, IdentificadorUsuario);

                if (pixDinamicoDTO.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                {
                    ResultView.Mensagem = pixDinamicoDTO.Mensagem;
                    return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
                }

                GuiaPagamentoReboqueEstadiaDTO guiaPagamentoReboqueEstadiaDTO = await _provider
                 .GetService<GuiaPagamentoReboqueEstadiaService>()
                 .ConsultarGuiaPagamentoReboqueEstadiaAsync(IdentificadorFaturamento, IdentificadorUsuario);

                ResultView.PixDinamico = pixDinamicoDTO;
                ResultView.GuiaPagamentoReboqueEstadia = guiaPagamentoReboqueEstadiaDTO;

                ResultView.Mensagem = pixDinamicoDTO.Mensagem;

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("GerarPixEstatico")]
        // TODO: [Authorize]
        public async Task<ActionResult<PixEstaticoCompletoDTO>> GerarPixEstatico(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PixEstaticoCompletoDTO ResultView = new();

            try
            {
                PixEstaticoDTO pixEstatico = _provider
                    .GetService<PixEstaticoService>()
                    .Create(IdentificadorFaturamento, IdentificadorUsuario);
                if (pixEstatico.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                {
                    ResultView.Mensagem = pixEstatico.Mensagem;
                    return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
                }
                GuiaPagamentoReboqueEstadiaDTO guiaPagamentoReboqueEstadia = await _provider
                .GetService<GuiaPagamentoReboqueEstadiaService>()
                .ConsultarGuiaPagamentoReboqueEstadiaAsync(IdentificadorFaturamento, IdentificadorUsuario);

                ResultView.PixEstatico = pixEstatico;
                ResultView.GuiaPagamentoReboqueEstadia = guiaPagamentoReboqueEstadia;

                return StatusCode((int)HtmlStatusCodeEnum.Ok, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }
        [HttpGet("SenhasConfirmacao")]
        // TODO: [Authorize]
        public async Task<ActionResult<SenhaPixEstaticoDTO>> SenhasConfirmacao(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SenhaPixEstaticoDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<PixEstaticoService>()
                    .SearchPassword(IdentificadorFaturamento, IdentificadorUsuario);

                if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                {
                    return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
                }
                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpPost("ValidarSenha")]
        // TODO: [Authorize]
        public async Task<ActionResult<SenhaValidandoDTO>> ValidarSenha([FromBody] string senha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SenhaValidandoDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<PixEstaticoService>()
                    .ValidatePassword(senha);

                if (ResultView.Mensagem.HtmlStatusCode != HtmlStatusCodeEnum.Ok)
                {
                    return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
                }
                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarBanco")]
        // TODO: [Authorize]
        public async Task<ActionResult<BancoListDTO>> ListarBanco()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BancoListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<BancoService>()
                    .ListAsync();

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarAgenciaBancaria")]
        // TODO: [Authorize]
        public async Task<ActionResult<AgenciaBancariaListDTO>> ListarAgenciaBancaria(short IdentificadorBanco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AgenciaBancariaListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<AgenciaBancariaService>()
                    .ListAsync(IdentificadorBanco);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarBoleto")]
        // TODO: [Authorize]
        public ActionResult<BoletoDTO> SelecionarBoleto(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BoletoDTO ResultView = new();

            try
            {
                ResultView = _provider
                    .GetService<BoletoService>()
                    .GetBoletoNaoCancelado(IdentificadorFaturamento, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarBancoPorIdentificador")]
        // TODO: [Authorize]
        public async Task<ActionResult<BancoListDTO>> SelecionarBancoPorIdentificador(short IdentificadorBanco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BancoListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<BancoService>()
                    .GetByIdAsync(IdentificadorBanco);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("SelecionarBancoPorNome")]
        // TODO: [Authorize]
        public async Task<ActionResult<BancoListDTO>> SelecionarBancoPorNome(string Nome)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BancoListDTO ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<BancoService>()
                    .GetByNameAsync(Nome);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.SetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }
    }
}