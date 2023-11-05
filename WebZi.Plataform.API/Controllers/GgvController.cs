﻿using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.CrossCutting.Web;
using WebZi.Plataform.Data.Helper;
using WebZi.Plataform.Data.Services.GGV;
using WebZi.Plataform.Domain.ViewModel;
using WebZi.Plataform.Domain.ViewModel.Faturamento;
using WebZi.Plataform.Domain.ViewModel.Generic;
using WebZi.Plataform.Domain.ViewModel.GGV;
using WebZi.Plataform.Domain.ViewModel.GGV.Cadastro;
using WebZi.Plataform.Domain.ViewModel.Veiculo;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GgvController : ControllerBase
    {
        private readonly IServiceProvider _provider;

        public GgvController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpPost("CadastrarFotos")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public ActionResult<MensagemViewModel> CadastrarFotos([FromBody] CadastroFotoGgvViewModel Fotos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = _provider
                    .GetService<GgvService>()
                    .CadastrarFotos(Fotos);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpDelete("ExcluirFotos")]
        // TODO: [Authorize]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<MensagemViewModel>> ExcluirFotos(int IdentificadorGrv, int IdentificadorUsuario, [FromBody] List<int> ListagemIdentificadorTabelaOrigem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MensagemViewModel ResultView;

            try
            {
                ResultView = await _provider
                    .GetService<GgvService>()
                    .ExcluirFotosAsync(IdentificadorGrv, IdentificadorUsuario, ListagemIdentificadorTabelaOrigem);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarDadosMestres")]
        // TODO: [Authorize]
        public async Task<ActionResult<DadosMestresViewModel>> ListarDadosMestres()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DadosMestresViewModel ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GgvService>()
                    .ListarDadosMestresAsync();

                return StatusCode((int)HtmlStatusCodeEnum.Ok, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }

        [HttpGet("ListarFotos")]
        // TODO: [Authorize]
        public async Task<ActionResult<ImageViewModelList>> ListarFotos(int IdentificadorGrv, int IdentificadorUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ImageViewModelList ResultView = new();

            try
            {
                ResultView = await _provider
                    .GetService<GgvService>()
                    .ListarFotosAsync(IdentificadorGrv, IdentificadorUsuario);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
            catch (Exception ex)
            {
                ResultView.Mensagem = MensagemViewHelper.GetInternalServerError(ex);

                return StatusCode((int)ResultView.Mensagem.HtmlStatusCode, ResultView);
            }
        }
    }
}