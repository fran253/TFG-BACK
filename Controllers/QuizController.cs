using Microsoft.AspNetCore.Mvc;
using TFG_BACK.Models.DTOs;

[Route("api/[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;
    private readonly IUsuarioService _usuarioService;

    public QuizController(IQuizService quizService, IUsuarioService usuarioService)
    {
        _quizService = quizService;
        _usuarioService = usuarioService;
    }

    // GET: api/quiz
    [HttpGet]
    public async Task<ActionResult<List<QuizResponseDto>>> GetAll()
    {
        try
        {
            var quizzes = await _quizService.GetAllWithUserInfoAsync();
            return Ok(quizzes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Error interno: {ex.Message}" });
        }
    }

    // GET: api/quiz/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<QuizResponseDto>> GetById(int id)
    {
        try
        {
            var quiz = await _quizService.GetByIdWithUserInfoAsync(id);
            if (quiz == null)
                return NotFound(new { mensaje = $"No se encontró el quiz con ID {id}" });
            
            return Ok(quiz);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Error interno: {ex.Message}" });
        }
    }

    // GET: api/quiz/usuario/{idUsuario}
    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<List<QuizListDto>>> GetByUsuario(int idUsuario)
    {
        try
        {
            // Verificar que el usuario existe
            var usuario = await _usuarioService.GetByIdAsync(idUsuario);
            if (usuario == null)
                return NotFound(new { mensaje = $"No se encontró el usuario con ID {idUsuario}" });

            var quizzes = await _quizService.GetByUsuarioWithInfoAsync(idUsuario);
            return Ok(quizzes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Error interno: {ex.Message}" });
        }
    }

    // GET: api/quiz/populares
    [HttpGet("populares")]
    public async Task<ActionResult<List<QuizListDto>>> GetPopulares([FromQuery] int limite = 10)
    {
        try
        {
            if (limite <= 0 || limite > 100)
                limite = 10;

            var quizzes = await _quizService.GetQuizzesPopularesAsync(limite);
            return Ok(quizzes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Error interno: {ex.Message}" });
        }
    }

    // GET: api/quiz/{id}/estadisticas
    [HttpGet("{id}/estadisticas")]
    public async Task<ActionResult<QuizStatsDto>> GetEstadisticas(int id)
    {
        try
        {
            var stats = await _quizService.GetEstadisticasAsync(id);
            if (stats == null)
                return NotFound(new { mensaje = $"No se encontró el quiz con ID {id}" });

            return Ok(stats);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Error interno: {ex.Message}" });
        }
    }

    // POST: api/quiz
    [HttpPost]
    public async Task<ActionResult<object>> Create([FromBody] QuizCreateDto quizDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { mensaje = "Datos inválidos", errores });
            }

            // Verificar que el usuario existe
            var usuario = await _usuarioService.GetByIdAsync(quizDto.IdUsuario);
            if (usuario == null)
            {
                return BadRequest(new { mensaje = $"No existe un usuario con ID {quizDto.IdUsuario}" });
            }

            // Crear el quiz
            var quiz = new Quiz
            {
                Nombre = quizDto.Nombre,
                Descripcion = quizDto.Descripcion,
                IdUsuario = quizDto.IdUsuario,
                FechaCreacion = DateTime.Now
            };

            var idQuiz = await _quizService.AddAsync(quiz);
            
            // Obtener el quiz creado con información del usuario
            var quizCreado = await _quizService.GetByIdWithUserInfoAsync(idQuiz);

            return CreatedAtAction(
                nameof(GetById), 
                new { id = idQuiz }, 
                new { 
                    mensaje = "Quiz creado exitosamente",
                    quiz = quizCreado
                }
            );
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Error interno: {ex.Message}" });
        }
    }

    // PUT: api/quiz/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] QuizUpdateDto quizDto)
    {
        try
        {
            if (id != quizDto.IdQuiz)
                return BadRequest(new { mensaje = "El ID del quiz no coincide" });

            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new { mensaje = "Datos inválidos", errores });
            }

            var quizExistente = await _quizService.GetByIdAsync(id);
            if (quizExistente == null)
            {
                return NotFound(new { mensaje = $"No se encontró el quiz con ID {id}" });
            }

            // Actualizar propiedades
            quizExistente.Nombre = quizDto.Nombre;
            quizExistente.Descripcion = quizDto.Descripcion;

            await _quizService.UpdateAsync(quizExistente);
            
            // Obtener quiz actualizado con información del usuario
            var quizActualizado = await _quizService.GetByIdWithUserInfoAsync(id);

            return Ok(new { 
                mensaje = "Quiz actualizado exitosamente",
                quiz = quizActualizado
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Error interno: {ex.Message}" });
        }
    }

    // DELETE: api/quiz/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, [FromQuery] int idUsuario)
    {
        try
        {
            if (idUsuario <= 0)
                return BadRequest(new { mensaje = "Debe especificar el ID del usuario" });

            var quiz = await _quizService.GetByIdAsync(id);
            if (quiz == null)
            {
                return NotFound(new { mensaje = $"No se encontró el quiz con ID {id}" });
            }

            // Verificar que el usuario es el propietario
            if (quiz.IdUsuario != idUsuario)
            {
                return Forbid("Solo el creador del quiz puede eliminarlo");
            }

            await _quizService.DeleteAsync(id);
            
            return Ok(new { 
                mensaje = "Quiz eliminado exitosamente",
                idQuiz = id,
                nombre = quiz.Nombre
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Error interno: {ex.Message}" });
        }
    }

    // GET: api/quiz/test - Para debugging
    [HttpGet("test")]
    public async Task<ActionResult> Test()
    {
        try
        {
            var usuarios = await _usuarioService.GetAllAsync();
            var quizzes = await _quizService.GetAllAsync();
            
            return Ok(new { 
                usuariosCount = usuarios.Count,
                quizzesCount = quizzes.Count,
                mensaje = "Conexión a base de datos funcionando correctamente"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Error de conexión: {ex.Message}" });
        }
    }
}