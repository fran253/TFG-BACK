using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

}
