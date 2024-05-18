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
    public class ContatoController : ControllerBase
    {
        private readonly IServices<Contato> _contatoService;

        public ContatoController(IServices<Contato> contatoService)
        {
            _contatoService = contatoService;
        }

        [HttpGet("GetAllAsync")]
        [ProducesResponseType(typeof(IEnumerable<Contato>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var resultado = await _contatoService.GetAllAsync(cancellationToken);

            if (resultado.Any())
            {
                return Ok(resultado);
            }
            else
            {
                return NoContent();
            }
        }

        // GET: api/contato/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Contato), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetContato(int id, CancellationToken cancellationToken)
        {
            var contato = await _contatoService.GetByIdAsync(id, cancellationToken);

            if (contato == null)
            {
                return NoContent();
            }

            return Ok(contato);
        }

        // POST: api/contato
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(Contato contato, CancellationToken cancellationToken)
        {
            await _contatoService.CreateAsync(contato, cancellationToken);
            return Ok();
        }

        // PUT: api/contato/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Contato), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, Contato contato, CancellationToken cancellationToken)
        {
            if (!await _contatoService.ExistAsync(id, cancellationToken))
            {
                return NotFound();
            }

            contato.id = id;
            await _contatoService.UpdateAsync(contato, cancellationToken);

            return NoContent();
        }

        // DELETE: api/contato/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Contato), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {

            if (!await _contatoService.ExistAsync(id, cancellationToken))
            {
                return NotFound();
            }

            await _contatoService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }       
    }
}
