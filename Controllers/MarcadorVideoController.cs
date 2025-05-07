using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class MarcadorVideoController : ControllerBase
{
    private readonly IMarcadorVideoService _marcadorService;

    public MarcadorVideoController(IMarcadorVideoService marcadorService)
    {
        _marcadorService = marcadorService;
    }

    // GET: api/marcadorvideo/video/{idVideo}
    [HttpGet("video/{idVideo}")]
    public async Task<ActionResult<List<MarcadorVideo>>> GetByVideo(int idVideo)
    {
        var marcadores = await _marcadorService.GetAllByVideoIdAsync(idVideo);
        return Ok(marcadores);
    }

    // GET: api/marcadorvideo/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<MarcadorVideo>> GetById(int id)
    {
        var marcador = await _marcadorService.GetByIdAsync(id);
        if (marcador == null)
            return NotFound();
        return Ok(marcador);
    }

    // POST: api/marcadorvideo
    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] MarcadorVideo marcador)
    {
        await _marcadorService.AddAsync(marcador);
        return CreatedAtAction(nameof(GetById), new { id = marcador.IdMarcador }, marcador);
    }

    // PUT: api/marcadorvideo/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> Editar(int id, [FromBody] MarcadorVideo marcador)
    {
        if (id != marcador.IdMarcador)
            return BadRequest("El ID no coincide.");
        await _marcadorService.UpdateAsync(marcador);
        return NoContent();
    }

    // DELETE: api/marcadorvideo/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        await _marcadorService.DeleteAsync(id);
        return NoContent();
    }
}
