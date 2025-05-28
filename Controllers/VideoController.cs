using Microsoft.AspNetCore.Mvc;
using TuProyecto.Api.DTOs;
using TuProyecto.Api.DTOs.Marcador;
using TFG_BACK.Services;
using TFG_BACK.Models.Common;

[Route("api/[controller]")]
[ApiController]
public class VideoController : ControllerBase
{
    private readonly IVideoService _videoService;
    private readonly IMarcadorVideoService _marcadorService;
    private readonly IS3UploaderService _s3UploaderService;
    private readonly IUsuarioService _usuarioService;

    public VideoController(IVideoService videoService, IMarcadorVideoService marcadorService, IS3UploaderService s3UploaderService, IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
        _videoService = videoService;
        _marcadorService = marcadorService;
        _s3UploaderService = s3UploaderService;
    }

    // endpoints paginados
    [HttpGet("paged")]
    public async Task<ActionResult<PagedResult<Video>>> GetAllPaged(
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 20)
    {
        var result = await _videoService.GetAllPagedAsync(page, pageSize);
        return Ok(result);
    }


    [HttpGet("curso/{idCurso}/paged")]
    public async Task<ActionResult<PagedResult<Video>>> GetByCursoPaged(
        int idCurso,
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 20)
    {
        var result = await _videoService.GetByCursoPagedAsync(idCurso, page, pageSize);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<Video>>> GetAll()
    {
        var videos = await _videoService.GetAllAsync();
        return Ok(videos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Video>> GetById(int id)
    {
        var video = await _videoService.GetByIdAsync(id);
        if (video == null)
            return NotFound();
        return Ok(video);
    }

    [HttpGet("curso/{idCurso}")]
    public async Task<ActionResult<List<Video>>> GetByCurso(int idCurso)
    {
        var lista = await _videoService.GetByCursoAsync(idCurso);
        return Ok(lista);
    }

    [HttpGet("curso/{idCurso}/asignatura/{idAsignatura}")]
    public async Task<ActionResult<List<Video>>> GetByCursoAndAsignatura(int idCurso, int idAsignatura)
    {
        var lista = await _videoService.GetByCursoAndAsignaturaAsync(idCurso, idAsignatura);
        return Ok(lista);
    }

    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<List<Video>>> GetVideosByUsuario(int idUsuario)
    {
        var videos = await _videoService.GetByUsuarioAsync(idUsuario);
        return Ok(videos);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Editar(int id, [FromBody] Video video)
    {
        if (id != video.IdVideo)
            return BadRequest("El ID del video no coincide.");
        await _videoService.UpdateAsync(video);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        var video = await _videoService.GetByIdAsync(id);
        if (video == null)
            return NotFound();

        await _videoService.DeleteAsync(id);
        return NoContent();
    }

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

    [HttpDelete("borrar-propio/{idVideo}")]
    public async Task<IActionResult> BorrarVideo(int idVideo)
    {
        var video = await _videoService.GetByIdAsync(idVideo);
        if (video == null)
            return NotFound("Vídeo no encontrado");

        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var usuario = await _usuarioService.GetByTokenAsync(token);
        if (usuario == null)
            return Unauthorized("Token inválido");

        if (video.IdUsuario != usuario.IdUsuario)
            return Forbid("No puedes borrar un vídeo que no es tuyo");

        await _videoService.DeleteAsync(idVideo);
        return NoContent();
    }

    [HttpGet("reportados")]
    public async Task<ActionResult<List<Video>>> GetVideosReportados()
    {
        var lista = await _videoService.GetVideosReportadosAsync();
        return Ok(lista);
    }
    
    [HttpGet("likes/{idVideo}")]
    public async Task<ActionResult<int>> GetLikes(int idVideo)
    {
        var total = await _videoService.GetContadorLikesAsync(idVideo);
        return Ok(total);
    }
}