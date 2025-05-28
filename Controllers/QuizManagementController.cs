using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFG_BACK.Models.DTOs;

[Route("api/[controller]")]
[ApiController]
public class QuizManagementController : ControllerBase
{
    private readonly IQuizManagementService _quizManagementService;

    public QuizManagementController(IQuizManagementService quizManagementService)
    {
        _quizManagementService = quizManagementService;
    }

    // POST: api/quizmanagement/crear-completo
    [HttpPost("crear-completo")]
    public async Task<ActionResult> CrearQuizCompleto([FromBody] CrearQuizCompletoDTO quizDTO)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var idQuiz = await _quizManagementService.CrearQuizCompletoAsync(quizDTO);
            return CreatedAtAction("ObtenerQuizCompleto", new { id = idQuiz }, new { IdQuiz = idQuiz });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
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


    // GET: api/quizmanagement/{id}/para-responder
    [HttpGet("{id}/para-responder")]
    public async Task<ActionResult<Quiz>> ObtenerQuizParaResponder(int id)
    {
        try
        {
            var quiz = await _quizManagementService.ObtenerQuizParaResponderAsync(id);
            return Ok(quiz);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // GET: api/quizmanagement/{id}/completo
    [HttpGet("{id}/completo")]
    public async Task<ActionResult<Quiz>> ObtenerQuizCompleto(int id)
    {
        try
        {
            var quiz = await _quizManagementService.ObtenerQuizCompletoAsync(id);
            return Ok(quiz);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }


    // DELETE: api/quizmanagement/{id}/usuario/{idUsuario}
    [HttpDelete("{id}/usuario/{idUsuario}")]
    public async Task<ActionResult> EliminarQuizCompleto(int id, int idUsuario)
    {
        try
        {
            var eliminado = await _quizManagementService.EliminarQuizCompletoAsync(id, idUsuario);
            
            if (!eliminado)
                return NotFound("Quiz no encontrado o el usuario no tiene permisos para eliminarlo");

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}