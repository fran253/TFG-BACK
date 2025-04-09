using Microsoft.AspNetCore.Mvc;
using TFG_BACK.Models;
using TFG_BACK.Services;

namespace TFG_BACK.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetAll()
        {
            var quizzes = await _quizService.GetAllAsync();
            return Ok(quizzes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetById(int id)
        {
            var quiz = await _quizService.GetByIdAsync(id);
            if (quiz == null)
                return NotFound();
            return Ok(quiz);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Quiz quiz)
        {
            await _quizService.AddAsync(quiz);
            return CreatedAtAction(nameof(GetById), new { id = quiz.IdQuizz }, quiz);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Quiz quiz)
        {
            if (id != quiz.IdQuizz)
                return BadRequest();

            var existente = await _quizService.GetByIdAsync(id);
            if (existente == null)
                return NotFound();

            await _quizService.UpdateAsync(quiz);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _quizService.DeleteAsync(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}
