using Microsoft.AspNetCore.Mvc;
using TuProyecto.Api.DTOs;
using TuProyecto.Api.DTOs.Marcador;
using TFG_BACK.Services;

[Route("api/[controller]")]
[ApiController]
public class VideoController : ControllerBase
{
    private readonly IVideoService _videoService;
    private readonly IMarcadorVideoService _marcadorService;
    private readonly IS3UploaderService _s3UploaderService;

    public VideoController(IVideoService videoService, IMarcadorVideoService marcadorService, IS3UploaderService s3UploaderService)
    {
        _videoService = videoService;
        _marcadorService = marcadorService;
        _s3UploaderService = s3UploaderService;
    }

    // GET: api/video
    [HttpGet]
    public async Task<ActionResult<List<Video>>> GetAll()
    {
        var videos = await _videoService.GetAllAsync();
        return Ok(videos);
    }

    // GET: api/video/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Video>> GetById(int id)
    {
        var video = await _videoService.GetByIdAsync(id);
        if (video == null)
            return NotFound();
        return Ok(video);
    }

    // GET: api/video/curso/{idCurso}
    [HttpGet("curso/{idCurso}")]
    public async Task<ActionResult<List<Video>>> GetByCurso(int idCurso)
    {
        var lista = await _videoService.GetByCursoAsync(idCurso);
        return Ok(lista);
    }

    // GET: api/video/curso/{idCurso}/asignatura/{idAsignatura}
    [HttpGet("curso/{idCurso}/asignatura/{idAsignatura}")]
    public async Task<ActionResult<List<Video>>> GetByCursoAndAsignatura(int idCurso, int idAsignatura)
    {
        var lista = await _videoService.GetByCursoAndAsignaturaAsync(idCurso, idAsignatura);
        return Ok(lista);
    }

    // GET: api/video/usuario/{idUsuario} - YA EXISTE, ACTUALIZADO CON TRY-CATCH
    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<List<Video>>> GetVideosByUsuario(int idUsuario)
    {
        try
        {
            var videos = await _videoService.GetByUsuarioAsync(idUsuario);
            return Ok(videos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    // PUT: api/video/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> Editar(int id, [FromBody] Video video)
    {
        if (id != video.IdVideo)
            return BadRequest("El ID del video no coincide.");
        await _videoService.UpdateAsync(video);
        return NoContent();
    }

    // DELETE: api/video/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        var video = await _videoService.GetByIdAsync(id);
        if (video == null)
            return NotFound();

        await _videoService.DeleteAsync(id);
        return NoContent();
    }

    // POST: api/video/registrar
    [HttpPost("registrar")]
    public async Task<ActionResult> RegistrarVideo([FromForm] RegistrarVideoRequest request)
    {
        if (request.Video == null || request.Miniatura == null)
            return BadRequest("Faltan los archivos de vídeo o miniatura.");

        var urlVideo = await _s3UploaderService.SubirArchivoAsync(request.Video, "video");
        var urlMiniatura = await _s3UploaderService.SubirArchivoAsync(request.Miniatura, "miniatura");

        var nuevoVideo = new Video
        {
            Titulo = request.Titulo,
            Descripcion = request.Descripcion,
            Url = urlVideo,
            Miniatura = urlMiniatura,
            IdAsignatura = request.IdAsignatura,
            IdUsuario = request.IdUsuario,
            IdCurso = request.IdCurso
        };

        var idNuevoVideo = await _videoService.AddAsync(nuevoVideo);

        return Ok(new { idVideo = idNuevoVideo });
    }

    // GET: api/video/reportados
    [HttpGet("reportados")]
    public async Task<ActionResult<List<Video>>> GetVideosReportados()
    {
        var lista = await _videoService.GetVideosReportadosAsync();
        return Ok(lista);
    }
}