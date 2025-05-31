using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EstadisticasController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly ICursoService _cursoService;
    private readonly IVideoService _videoService;
    private readonly IQuizService _quizService;

    public EstadisticasController(
        IUsuarioService usuarioService,
        ICursoService cursoService, 
        IVideoService videoService,
        IQuizService quizService)
    {
        _usuarioService = usuarioService;
        _cursoService = cursoService;
        _videoService = videoService;
        _quizService = quizService;
    }

    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<UsuarioEstadisticasDTO>> GetEstadisticasUsuario(int idUsuario)
    {
        // Verificar que el usuario existe
        var usuario = await _usuarioService.GetByIdAsync(idUsuario);
        if (usuario == null)
            return NotFound("Usuario no encontrado");

        try
        {
            // Obtener estadísticas
            var totalCursos = await _cursoService.ContarCursosPorUsuarioAsync(idUsuario);
            var totalVideos = await _videoService.ContarVideosPorUsuarioAsync(idUsuario);
            var totalQuizzes = await _quizService.ContarQuizzesPorUsuarioAsync(idUsuario);

            // Obtener fecha de última actividad
            DateTime? fechaUltimaActividad = null;
            var fechas = new List<DateTime>();

            var ultimoCurso = await _cursoService.ObtenerUltimoCursoPorUsuarioAsync(idUsuario);
            if (ultimoCurso?.FechaCreacion != null)
                fechas.Add(ultimoCurso.FechaCreacion);

            var ultimoVideo = await _videoService.ObtenerUltimoVideoPorUsuarioAsync(idUsuario);
            if (ultimoVideo?.FechaSubida != null)
                fechas.Add(ultimoVideo.FechaSubida);

            var ultimoQuiz = await _quizService.ObtenerUltimoQuizPorUsuarioAsync(idUsuario);
            if (ultimoQuiz?.FechaCreacion != null)
                fechas.Add(ultimoQuiz.FechaCreacion);

            if (fechas.Any())
                fechaUltimaActividad = fechas.Max();

            var estadisticas = new UsuarioEstadisticasDTO
            {
                IdUsuario = idUsuario,
                TotalCursos = totalCursos,
                TotalVideos = totalVideos,
                TotalQuizzes = totalQuizzes,
                FechaUltimaActividad = fechaUltimaActividad
            };

            return Ok(estadisticas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = "Error al obtener estadísticas", error = ex.Message });
        }
    }
}

