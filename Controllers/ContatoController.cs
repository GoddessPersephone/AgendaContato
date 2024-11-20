using AgendaContatoApi.Data;
using AgendaContatoApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaDeContatosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ContatoController : ControllerBase
    {
        private readonly ContatoRepository _repo;

        public ContatoController(ContatoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContatoModel>>> GetContatos()
        {
            var contatos = await _repo.GetContatosAsync();
            return Ok(contatos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContatoModel>> GetContato(int id)
        {
            var contato = await _repo.GetContatoByIdAsync(id);
            if (contato == null)
            {
                return NotFound();
            }
            return Ok(contato);
        }

        [HttpPost]
        public async Task<ActionResult<ContatoModel>> PostContato(ContatoModel contato)
        {
            await _repo.AddContatoAsync(contato);
            return CreatedAtAction(nameof(GetContato), new { id = contato.Id }, contato);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContato(int id, ContatoModel contato)
        {
            if (id != contato.Id)
            {
                return BadRequest();
            }

            await _repo.UpdateContatoAsync(contato);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContato(int id)
        {
            await _repo.DeleteContatoAsync(id);
            return NoContent();
        }
    }
}
