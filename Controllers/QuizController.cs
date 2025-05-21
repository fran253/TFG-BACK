using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFG_BACK.Models.DTOs;

[Route("api/[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly IQuizService _service;

    public QuizController(IQuizService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Quiz>>> GetAll()
    {
        var quizzes = await _service.GetAllAsync();
        return Ok(quizzes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Quiz>> GetById(int id)
    {
        var quiz = await _service.GetByIdAsync(id);
        if (quiz == null) return NotFound();
        return Ok(quiz);
    }

    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] Quiz quiz)
    {
        await _service.AddAsync(quiz);
        return CreatedAtAction(nameof(GetById), new { id = quiz.IdQuiz }, quiz);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Editar(int id, [FromBody] Quiz quiz)
    {
        if (id != quiz.IdQuiz)
            return BadRequest("El ID no coincide.");
        await _service.UpdateAsync(quiz);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("curso/{idCurso}")]
    public async Task<ActionResult<List<Quiz>>> GetByCurso(int idCurso)
    {
        var quizzes = await _service.GetByCursoAsync(idCurso);
        return Ok(quizzes);
    }

    [HttpGet("asignatura/{idAsignatura}")]
    public async Task<ActionResult<List<Quiz>>> GetByAsignatura(int idAsignatura)
    {
        var quizzes = await _service.GetByAsignaturaAsync(idAsignatura);
        return Ok(quizzes);
    }

    [HttpGet("curso/{idCurso}/asignatura/{idAsignatura}")]
    public async Task<ActionResult<List<Quiz>>> GetByCursoYAsignatura(int idCurso, int idAsignatura)
    {
        var quizzes = await _service.GetByCursoYAsignaturaAsync(idCurso, idAsignatura);
        return Ok(quizzes);
    }

    // Nuevos endpoints
    
    // POST: api/Quiz/completo
    [HttpPost("completo")]
    public async Task<ActionResult> CrearQuizCompleto([FromBody] CrearQuizDTO quizDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            var idQuiz = await _service.CrearQuizCompletoAsync(quizDTO);
            return CreatedAtAction(nameof(GetById), new { id = idQuiz }, new { IdQuiz = idQuiz });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    // POST: api/Quiz/responder
    [HttpPost("responder")]
    public async Task<ActionResult<ResultadoQuizDTO>> ResponderQuiz([FromBody] ResponderQuizDTO respuestasDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
            
        try
        {
            var resultado = await _service.ProcesarRespuestasAsync(respuestasDTO);
            return Ok(resultado);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    // GET: api/Quiz/populares
    [HttpGet("populares")]
    public async Task<ActionResult<List<Quiz>>> GetQuizzesPopulares([FromQuery] int limite = 10)
    {
        var quizzes = await _service.GetQuizzesPopularesAsync(limite);
        return Ok(quizzes);
    }
    
    // GET: api/Quiz/usuario/{idUsuario}
    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<List<Quiz>>> GetQuizzesPorUsuario(int idUsuario)
    {
        var quizzes = await _service.GetQuizzesPorUsuarioAsync(idUsuario);
        return Ok(quizzes);
    }
    
    // GET: api/Quiz/{id}/preguntas/total
    [HttpGet("{id}/preguntas/total")]
    public async Task<ActionResult<int>> GetTotalPreguntas(int id)
    {
        var total = await _service.GetTotalPreguntasAsync(id);
        return Ok(total);
    }
}