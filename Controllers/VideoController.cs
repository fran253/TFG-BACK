using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuProyecto.Api.DTOs;
using TuProyecto.Api.DTOs.Video;
using TuProyecto.Api.DTOs.Marcador;



[Route("api/[controller]")]
[ApiController]
public class VideoController : ControllerBase
{
    private readonly IVideoService _videoService;
    private readonly IMarcadorVideoService _marcadorService;

    public VideoController(IVideoService videoService, IMarcadorVideoService marcadorService)
    {
        _videoService = videoService;
        _marcadorService = marcadorService;
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

    // GET: api/video/asignatura/{idAsignatura}
    [HttpGet("asignatura/{idAsignatura}")]
    public async Task<ActionResult<List<Video>>> GetByAsignatura(int idAsignatura)
    {
        var lista = await _videoService.GetByAsignaturaAsync(idAsignatura);
        return Ok(lista);
    }

    // GET: api/video/usuario/{idUsuario}
    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<List<Video>>> GetByUsuario(int idUsuario)
    {
        var lista = await _videoService.GetByUsuarioAsync(idUsuario);
        return Ok(lista);
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

    // ✅ NUEVO ENDPOINT PRO: api/video/registrar
    [HttpPost("registrar")]
    public async Task<ActionResult> RegistrarVideoConMarcadores([FromBody] RegistrarVideoRequest request)
    {
        if (string.IsNullOrEmpty(request.UrlVideo) || string.IsNullOrEmpty(request.UrlMiniatura))
            return BadRequest("Faltan URLs.");

        var nuevoVideo = new Video
        {
            Titulo = request.Titulo,
            Descripcion = request.Descripcion,
            Url = request.UrlVideo,
            Miniatura = request.UrlMiniatura,
            IdAsignatura = request.IdAsignatura,
            IdUsuario = request.IdUsuario
        };

        var idNuevoVideo = await _videoService.AddAsync(nuevoVideo);

        if (request.Marcadores != null && request.Marcadores.Count > 0)
        {
            foreach (var marcador in request.Marcadores)
            {
                var nuevoMarcador = new MarcadorVideo
                {
                    IdVideo = idNuevoVideo,
                    MinutoInicio = marcador.MinutoInicio,
                    MinutoFin = marcador.MinutoFin,
                    Titulo = marcador.Titulo
                };

                await _marcadorService.AddAsync(nuevoMarcador);
            }
        }

        return Ok(new { idVideo = idNuevoVideo });
    }
}
