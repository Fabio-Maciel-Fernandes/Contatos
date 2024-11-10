using Contatos.Core.Models;
using Contatos.Infra.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Contatos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CompilacaoController : ControllerBase
    {
        private readonly IServices<Compilacao> _compilacaoService;

        public CompilacaoController(IServices<Compilacao> compilacaoService)
        {
            _compilacaoService = compilacaoService;
        }

        [HttpGet("GetAllAsync")]
        [ProducesResponseType(typeof(IEnumerable<Compilacao>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var resultado = await _compilacaoService.GetAllAsync(cancellationToken);

            if (resultado.Any())
            {
                return Ok(resultado);
            }
            else
            {
                return NoContent();
            }
        }

       
        // POST: api/compilacao
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(Compilacao compilacao, CancellationToken cancellationToken)
        {
            await _compilacaoService.CreateAsync(compilacao, cancellationToken);
            return Ok();
        }             
    }
}
