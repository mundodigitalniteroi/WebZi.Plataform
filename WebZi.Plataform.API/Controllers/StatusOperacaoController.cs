using Microsoft.AspNetCore.Mvc;
using WebZi.Plataform.Domain.Models.GRV;

namespace WebZi.Plataform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusOperacaoController : ControllerBase
    {
        // GET: api/<StatusOperacaoController1>
        [HttpGet("id")]
        public IEnumerable<StatusOperacaoModel> Get(char id)
        {
            // GrvService service = serviceCollection.BuildServiceProvider().GetService<GrvService>();

            return null; // db.StatusOperacoes.ToList();
        }
    }
}