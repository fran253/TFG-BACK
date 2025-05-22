using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ComentarioVideoController : ControllerBase
{
    private readonly IComentarioVideoService _comentarioService;

    public ComentarioVideoController(IComentarioVideoService comentarioService)
    {
        _comentarioService = comentarioService;
    }

    [HttpGet("video/{idVideo}")]
    public async Task<ActionResult<List<ComentarioVideo>>> GetByVideo(int idVideo)
    {
        var comentarios = await _comentarioService.GetAllByVideoIdAsync(idVideo);
        return Ok(comentarios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ComentarioVideo>> GetById(int id)
    {
        var comentario = await _comentarioService.GetByIdAsync(id);
        if (comentario == null)
            return NotFound();
        return Ok(comentario);
    }

    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] ComentarioCrearDTO dto)
    {
        var nuevoComentario = new ComentarioVideo
        {
            IdUsuario = dto.IdUsuario,
            IdVideo = dto.IdVideo,
            Texto = dto.Texto,
            Fecha = DateTime.UtcNow
        };

        await _comentarioService.AddAsync(nuevoComentario);
        return CreatedAtAction(nameof(GetById), new { id = nuevoComentario.IdComentario }, nuevoComentario);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Editar(int id, [FromBody] ComentarioVideo comentario)
    {
        if (id != comentario.IdComentario)
            return BadRequest("El ID no coincide.");
        await _comentarioService.UpdateAsync(comentario);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        await _comentarioService.DeleteAsync(id);
        return NoContent();
    }
    [HttpPut("reportar/{id}")]
    public async Task<ActionResult> Reportar(int id)
    {
        var resultado = await _comentarioService.ReportarComentarioAsync(id);
        if (!resultado)
            return NotFound("Comentario no encontrado.");
        return NoContent();
    }

    [HttpGet("reportados")]
    public async Task<ActionResult<List<ComentarioVideo>>> GetReportados()
    {
        var comentarios = await _comentarioService.GetReportadosAsync();
        return Ok(comentarios);
    }
    
    [HttpPut("quitar-reportes/{id}")]
    public async Task<IActionResult> QuitarReportes(int id)
    {
        var comentario = await _comentarioService.GetByIdAsync(id);
        if (comentario == null)
            return NotFound();

        comentario.NumeroReportes = 0;
        await _comentarioService.UpdateAsync(comentario);
        return NoContent();
    }

}
