using AgendaContatoApi.Data;
using AgendaContatoApi.Data.Interface;
using AgendaContatoApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgendaDeContatosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ContatoController : ControllerBase
    {
        private readonly IAgendaContatoService _service;
        private readonly ILogger<ContatoController> _logger;
        public string mensagem = string.Empty;

        public ContatoController(IAgendaContatoService service, ILogger<ContatoController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContatoModel>>> ObterContatos()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    mensagem += "Erro: Modelo de informação incorreto!";
                    _logger.LogError(mensagem);
                    return BadRequest(mensagem);
                }
                var liContatos = await _service.Obter();
                if (liContatos is not null && liContatos.Count > 0)
                {
                    if (!string.IsNullOrEmpty(liContatos[0].ErroMensagem))
                    {
                        mensagem += liContatos[0].ErroMensagem;
                        _logger.LogError(mensagem);
                        return BadRequest(mensagem);
                    }
                    mensagem += "Sucesso!";
                    _logger.LogInformation(mensagem);
                    return Ok(liContatos);
                }
                mensagem += "Dados não localizados!";

                _logger.LogError(mensagem);
                return NotFound(mensagem);
            }
            catch (Exception ex)
            {
                mensagem += $"{ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                return BadRequest(mensagem);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContatoModel>> ObterContatoPorId(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    mensagem += "Erro: Modelo de informação incorreto!";
                    _logger.LogError(mensagem);
                    return BadRequest(mensagem);
                }

                var contato = await _service.ObterPorId(id);

                if (contato is not null)
                {
                    if (!string.IsNullOrEmpty(contato.ErroMensagem))
                    {
                        mensagem += contato.ErroMensagem;
                        _logger.LogError(mensagem);
                        return BadRequest(mensagem);
                    }
                    mensagem += "Sucesso!";
                    _logger.LogInformation(mensagem);
                    return Ok(contato);
                }
                mensagem += "Dados não localizados!";

                _logger.LogError(mensagem);
                return NotFound(mensagem);
            }
            catch (Exception ex)
            {
                mensagem += $"{ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                return BadRequest(mensagem);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<ContatoModel>>> InserirContatos(List<ContatoModel> liContato)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    mensagem += "Erro: Modelo de informação incorreto!";
                    _logger.LogError(mensagem);
                    return BadRequest(mensagem);
                }
                var listaModelInserida = await _service.Inserir(liContato);

                if (listaModelInserida is not null)
                {
                    if (!string.IsNullOrEmpty(listaModelInserida[0].ErroMensagem))
                    {
                        mensagem += listaModelInserida[0].ErroMensagem;
                        _logger.LogError(mensagem);
                        return BadRequest(mensagem);
                    }
                    mensagem += "Sucesso!";
                    _logger.LogInformation(mensagem);
                    return Ok(listaModelInserida);
                }
                mensagem += "Dados não localizados.";

                _logger.LogError(mensagem);
                return NotFound(mensagem);
            }
            catch (Exception ex)
            {
                mensagem += $"{ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                return BadRequest(mensagem);
            }
            //return CreatedAtAction(nameof(GetContato), new { id = liContato.Id }, liContato);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarContato(ContatoModel contato)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    mensagem += "Erro: Modelo de informação incorreto!";
                    _logger.LogError(mensagem);
                    return BadRequest(mensagem);
                }
                var modelAlterada = await _service.Alterar(contato);
                if (modelAlterada is not null)
                {
                    if (!string.IsNullOrEmpty(modelAlterada.ErroMensagem))
                    {
                        mensagem += modelAlterada.ErroMensagem;
                        _logger.LogError(mensagem);
                        return BadRequest(mensagem);
                    }
                    mensagem += "Sucesso!";
                    _logger.LogInformation(mensagem);
                    return Ok(modelAlterada);
                }
                mensagem += "Dados não localizados.";

                _logger.LogError(mensagem);
                return NotFound(mensagem);
            }
            catch (Exception ex)
            {
                mensagem += $"{ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                return BadRequest(mensagem);
            }
            /* if (id != contato.Id)
             {
                 return BadRequest();
             }

             await _repo.AlterarContatoAsync(contato);
             return NoContent();*/
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContato(int id)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    mensagem += "Erro: Modelo de informação incorreto!";
                    _logger.LogError(mensagem);
                    return BadRequest(mensagem);
                }


                var dadoExcluido = await _service.Deletar(id);

                /* if (dadoExcluido is not null)
                 {
                     if (!string.IsNullOrEmpty(dadoExcluido.ErroMensagem))
                     {
                         mensagem += dadoExcluido.ErroMensagem;
                         _logger.LogError(mensagem);
                         return BadRequest(mensagem);
                     }
                     mensagem += "Sucesso!";
                     _logger.LogInformation(mensagem);
                     return Ok(mensagem);
                 }*/

                mensagem += "Dados não localizados.";

                _logger.LogError(mensagem);
                return NotFound(mensagem);
            }
            catch (Exception ex)
            {
                mensagem += $"{ex}, {ex.Message}!";
                _logger.LogError(mensagem);
                return BadRequest(mensagem);
            }
        }
    }
}