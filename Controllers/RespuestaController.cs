using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class RespuestaController : ControllerBase
{
    private readonly IRespuestaService _respuestaService;

    public RespuestaController(IRespuestaService respuestaService)
    {
        _respuestaService = respuestaService;
    }

    // GET: api/respuesta/pregunta/{idPregunta}
    [HttpGet("pregunta/{idPregunta}")]
    public async Task<ActionResult<List<Respuesta>>> GetByPregunta(int idPregunta)
    {
        try
        {
            var respuestas = await _respuestaService.GetByPreguntaIdAsync(idPregunta);
            return Ok(respuestas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // GET: api/respuesta/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Respuesta>> GetById(int id)
    {
        try
        {
            var respuesta = await _respuestaService.GetByIdAsync(id);
            if (respuesta == null)
                return NotFound($"No se encontró la respuesta con ID {id}");
            
            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // POST: api/respuesta
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Respuesta respuesta)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var idRespuesta = await _respuestaService.AddAsync(respuesta);
            return CreatedAtAction(nameof(GetById), new { id = idRespuesta }, new { IdRespuesta = idRespuesta });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // PUT: api/respuesta/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Respuesta respuesta)
    {
        try
        {
            if (id != respuesta.IdRespuesta)
                return BadRequest("El ID no coincide");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _respuestaService.UpdateAsync(respuesta);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // DELETE: api/respuesta/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _respuestaService.DeleteAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // GET: api/respuesta/pregunta/{idPregunta}/count
    [HttpGet("pregunta/{idPregunta}/count")]
    public async Task<ActionResult<int>> GetCountByPregunta(int idPregunta)
    {
        try
        {
            var count = await _respuestaService.ContarRespuestasDeLaPreguntaAsync(idPregunta);
            return Ok(count);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // GET: api/respuesta/pregunta/{idPregunta}/validar-correctas
    [HttpGet("pregunta/{idPregunta}/validar-correctas")]
    public async Task<ActionResult<bool>> ValidarRespuestasCorrectas(int idPregunta)
    {
        try
        {
            var tieneCorrectas = await _respuestaService.ValidarAlMenosUnaCorrectaAsync(idPregunta);
            return Ok(tieneCorrectas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}