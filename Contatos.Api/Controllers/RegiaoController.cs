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
    public class RegiaoController : ControllerBase
    {
        private readonly IServices<Regiao> _regiaoService;

        public RegiaoController(IServices<Regiao> regiaoService)
        {
            _regiaoService = regiaoService;
        }

        [HttpGet("GetAllAsync")]
        [ProducesResponseType(typeof(IEnumerable<Regiao>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var resultado = await _regiaoService.GetAllAsync(cancellationToken);

            if (resultado.Any())
            {
                return Ok(resultado);
            }
            else
            {
                return NoContent();
            }
        }

        // GET: api/regiao/5
        [HttpGet("{ddd}")]
        [ProducesResponseType(typeof(Regiao), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRegiao(int ddd, CancellationToken cancellationToken)
        {
            var regiao = await _regiaoService.GetByIdAsync(ddd, cancellationToken);

            if (regiao == null)
            {
                return NoContent();
            }

            return Ok(regiao);
        }

        // POST: api/regiao
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(Regiao regiao, CancellationToken cancellationToken)
        {
            await _regiaoService.CreateAsync(regiao, cancellationToken);
            return Ok();
        }

        // PUT: api/regiao/5
        [HttpPut("{ddd}")]
        [ProducesResponseType(typeof(Regiao), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int ddd, Regiao regiao, CancellationToken cancellationToken)
        {
            if (!await _regiaoService.ExistAsync(ddd, cancellationToken))
            {
                return NotFound();
            }

            regiao.DDD = ddd;
            await _regiaoService.UpdateAsync(regiao, cancellationToken);

            return NoContent();
        }

        // DELETE: api/regiao/5
        [HttpDelete("{ddd}")]
        [ProducesResponseType(typeof(Regiao), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int ddd, CancellationToken cancellationToken)
        {

            if (!await _regiaoService.ExistAsync(ddd, cancellationToken))
            {
                return NotFound();
            }

            await _regiaoService.DeleteAsync(ddd, cancellationToken);

            return NoContent();
        }       
    }
}
