using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.Banco;
using WebZi.Plataform.Data.Services.Banco.PIX;
using WebZi.Plataform.Data.Services.WebServices;
using WebZi.Plataform.Domain.ViewModel.Banco;
using WebZi.Plataform.Domain.ViewModel.Banco.PIX;
using WebZi.Plataform.Domain.ViewModel.Generic;

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
        public ActionResult<ImageViewModelList> GerarBoleto(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ImageViewModelList ResultView = new();

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
        public ActionResult<PixEstaticoGeradoViewModel> GerarPixDinamico(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PixEstaticoGeradoViewModel ResultView = new();

            try
            {
                ResultView = _provider
                    .GetService<PixDinamicoService>()
                    .Create(IdentificadorFaturamento, IdentificadorUsuario);

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
        public ActionResult<PixEstaticoGeradoViewModel> GerarPixEstatico(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PixEstaticoGeradoViewModel ResultView = new();

            try
            {
                ResultView = _provider
                    .GetService<PixEstaticoService>()
                    .Create(IdentificadorFaturamento, IdentificadorUsuario);

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
        public async Task<ActionResult<BancoViewModelList>> ListarBanco()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BancoViewModelList ResultView = new();

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
        public async Task<ActionResult<AgenciaBancariaViewModelList>> ListarAgenciaBancaria(short IdentificadorBanco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AgenciaBancariaViewModelList ResultView = new();

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
        public ActionResult<ImageViewModelList> SelecionarBoleto(int IdentificadorFaturamento, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ImageViewModelList ResultView = new();

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
        public async Task<ActionResult<BancoViewModelList>> SelecionarBancoPorIdentificador(short IdentificadorBanco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BancoViewModelList ResultView = new();

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
        public async Task<ActionResult<BancoViewModelList>> SelecionarBancoPorNome(string Nome)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BancoViewModelList ResultView = new();

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