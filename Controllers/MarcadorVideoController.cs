using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuProyecto.Api.DTOs.Marcador;

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
        // Aquí no necesitas hacer ningún cambio específico respecto a las columnas, ya que 'MinutoImportante' es la nueva propiedad.
        await _marcadorService.AddAsync(marcador);
        return CreatedAtAction(nameof(GetById), new { id = marcador.IdMarcador }, marcador);
    }

    // PUT: api/marcadorvideo/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> Editar(int id, [FromBody] MarcadorVideo marcador)
    {
        if (id != marcador.IdMarcador)
            return BadRequest("El ID no coincide.");

        // Asegúrate de que el marcador que se actualiza incluye la propiedad 'MinutoImportante' correctamente.
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
    
    // POST: api/marcadorvideo/video/{idVideo}/bulk
    [HttpPost("video/{idVideo}/bulk")]
    public async Task<ActionResult> CrearMarcadoresEnLote(int idVideo, [FromBody] List<MarcadorRequest> marcadores)
    {
        if (marcadores == null || marcadores.Count == 0)
            return BadRequest("No se han enviado marcadores");

        foreach (var marcador in marcadores)
        {
            var nuevoMarcador = new MarcadorVideo
            {
                IdVideo = idVideo,
                MinutoImportante = marcador.MinutoImportante,
                Titulo = marcador.Titulo
            };

            await _marcadorService.AddAsync(nuevoMarcador);
        }

        return Ok();
    }


}
