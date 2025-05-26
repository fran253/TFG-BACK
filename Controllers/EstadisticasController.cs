using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class EstadisticasController : ControllerBase
{
    private readonly AcademIQDbContext _context;

    public EstadisticasController(AcademIQDbContext context)
    {
        _context = context;
    }

    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<object>> GetEstadisticasUsuario(int idUsuario)
    {
        try
        {
            // Contar cursos donde el usuario está inscrito (no creador, ya que no existe ese campo)
            var totalCursosInscritos = await _context.UsuarioCursos
                .CountAsync(uc => uc.IdUsuario == idUsuario);

            // Contar videos creados por el usuario
            var totalVideos = await _context.Videos
                .CountAsync(v => v.IdUsuario == idUsuario);

            // Contar quizzes creados por el usuario
            var totalQuizzes = await _context.Quizzes
                .CountAsync(q => q.IdUsuario == idUsuario);

            // Contar quizzes respondidos por el usuario
            var totalQuizzesRespondidos = await _context.ResultadosQuiz
                .CountAsync(r => r.IdUsuario == idUsuario);

            // Obtener la fecha del último video subido como referencia de actividad
            var fechaUltimaActividad = await _context.Videos
                .Where(v => v.IdUsuario == idUsuario)
                .OrderByDescending(v => v.FechaSubida)
                .Select(v => v.FechaSubida)
                .FirstOrDefaultAsync();

            // Si no tiene videos, buscar en quizzes
            if (fechaUltimaActividad == default)
            {
                var fechaUltimoQuizRespondido = await _context.ResultadosQuiz
                    .Where(r => r.IdUsuario == idUsuario)
                    .OrderByDescending(r => r.Fecha)
                    .Select(r => r.Fecha)
                    .FirstOrDefaultAsync();

                fechaUltimaActividad = fechaUltimoQuizRespondido != default ? fechaUltimoQuizRespondido : DateTime.UtcNow;
            }

            var estadisticas = new
            {
                IdUsuario = idUsuario,
                TotalCursosInscritos = totalCursosInscritos,
                TotalVideos = totalVideos,
                TotalQuizzes = totalQuizzes,
                TotalQuizzesRespondidos = totalQuizzesRespondidos,
                FechaUltimaActividad = fechaUltimaActividad
            };

            return Ok(estadisticas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // Endpoint adicional para estadísticas generales del sistema
    [HttpGet("sistema")]
    public async Task<ActionResult<object>> GetEstadisticasSistema()
    {
        try
        {
            var totalUsuarios = await _context.Usuarios.CountAsync();
            var totalCursos = await _context.Cursos.CountAsync();
            var totalVideos = await _context.Videos.CountAsync();
            var totalQuizzes = await _context.Quizzes.CountAsync();

            var estadisticas = new
            {
                TotalUsuarios = totalUsuarios,
                TotalCursos = totalCursos,
                TotalVideos = totalVideos,
                TotalQuizzes = totalQuizzes,
                FechaConsulta = DateTime.UtcNow
            };

            return Ok(estadisticas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}