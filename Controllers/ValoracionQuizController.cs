using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFG_BACK.Models.DTOs;

[Route("api/[controller]")]
[ApiController]
public class ValoracionQuizController : ControllerBase
{
    private readonly IValoracionQuizService _service;

    public ValoracionQuizController(IValoracionQuizService service)
    {
        _service = service;
    }

    // GET: api/ValoracionQuiz/quiz/5
    [HttpGet("quiz/{idQuiz}")]
    public async Task<ActionResult<List<ValoracionQuiz>>> GetByQuiz(int idQuiz)
    {
        var valoraciones = await _service.GetByQuizIdAsync(idQuiz);
        return Ok(valoraciones);
    }

    // GET: api/ValoracionQuiz/promedio/5
    [HttpGet("promedio/{idQuiz}")]
    public async Task<ActionResult<double>> GetPromedio(int idQuiz)
    {
        var promedio = await _service.GetPromedioValoracionesAsync(idQuiz);
        return Ok(promedio);
    }

    // POST: api/ValoracionQuiz
    [HttpPost]
    public async Task<ActionResult> Valorar([FromBody] ValorarQuizDTO valoracionDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var valoracion = new ValoracionQuiz
        {
            IdUsuario = valoracionDTO.IdUsuario,
            IdQuiz = valoracionDTO.IdQuiz,
            Puntuacion = valoracionDTO.Puntuacion,
            Comentario = valoracionDTO.Comentario,
            Fecha = DateTime.Now
        };

        await _service.AddAsync(valoracion);
        return Ok();
    }

    // DELETE: api/ValoracionQuiz/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}