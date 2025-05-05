using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class VideoController : ControllerBase
{
    private readonly IVideoService _videoService;

    public VideoController(IVideoService videoService)
    {
        _videoService = videoService;
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

    // POST: api/video
    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] Video video)
    {
        await _videoService.AddAsync(video);
        return CreatedAtAction(nameof(GetById), new { id = video.IdVideo }, video);
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
}
