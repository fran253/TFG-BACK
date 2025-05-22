using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    // GET: api/quiz
    [HttpGet]
    public async Task<ActionResult<List<Quiz>>> GetAll()
    {
        try
        {
            var quizzes = await _quizService.GetAllAsync();
            return Ok(quizzes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // GET: api/quiz/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Quiz>> GetById(int id)
    {
        try
        {
            var quiz = await _quizService.GetByIdAsync(id);
            if (quiz == null)
                return NotFound($"No se encontró el quiz con ID {id}");
            
            return Ok(quiz);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // GET: api/quiz/usuario/{idUsuario}
    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<List<Quiz>>> GetByUsuario(int idUsuario)
    {
        try
        {
            var quizzes = await _quizService.GetByUsuarioAsync(idUsuario);
            return Ok(quizzes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // GET: api/quiz/populares
    [HttpGet("populares")]
    public async Task<ActionResult<List<Quiz>>> GetPopulares([FromQuery] int limite = 10)
    {
        try
        {
            var quizzes = await _quizService.GetQuizzesPopularesAsync(limite);
            return Ok(quizzes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // POST: api/quiz
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Quiz quiz)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var idQuiz = await _quizService.AddAsync(quiz);
            return CreatedAtAction(nameof(GetById), new { id = idQuiz }, new { IdQuiz = idQuiz });
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

    // PUT: api/quiz/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] Quiz quiz)
    {
        try
        {
            if (id != quiz.IdQuiz)
                return BadRequest("El ID no coincide");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _quizService.UpdateAsync(quiz);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // DELETE: api/quiz/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _quizService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}