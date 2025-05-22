using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PreguntaController : ControllerBase
{
    private readonly IPreguntaService _preguntaService;

    public PreguntaController(IPreguntaService preguntaService)
    {
        _preguntaService = preguntaService;
    }

    // GET: api/pregunta/quiz/{idQuiz}
    [HttpGet("quiz/{idQuiz}")]
    public async Task<ActionResult<List<Pregunta>>> GetByQuiz(int idQuiz)
    {
        try
        {
            var preguntas = await _preguntaService.GetByQuizIdAsync(idQuiz);
            return Ok(preguntas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // GET: api/pregunta/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Pregunta>> GetById(int id)
    {
        try
        {
            var pregunta = await _preguntaService.GetByIdAsync(id);
            if (pregunta == null)
                return NotFound($"No se encontró la pregunta con ID {id}");
            
            return Ok(pregunta);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // POST: api/pregunta
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Pregunta pregunta)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var idPregunta = await _preguntaService.AddAsync(pregunta);
            return CreatedAtAction(nameof(GetById), new { id = idPregunta }, new { IdPregunta = idPregunta });
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

    // PUT: api/pregunta/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Pregunta pregunta)
    {
        try
        {
            if (id != pregunta.IdPregunta)
                return BadRequest("El ID no coincide");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _preguntaService.UpdateAsync(pregunta);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // DELETE: api/pregunta/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _preguntaService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // GET: api/pregunta/quiz/{idQuiz}/count
    [HttpGet("quiz/{idQuiz}/count")]
    public async Task<ActionResult<int>> GetCountByQuiz(int idQuiz)
    {
        try
        {
            var count = await _preguntaService.ContarPreguntasDelQuizAsync(idQuiz);
            return Ok(count);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}