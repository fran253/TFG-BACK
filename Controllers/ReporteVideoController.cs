using Microsoft.AspNetCore.Mvc;
using TuProyecto.Models;

[ApiController]
[Route("api/[controller]")]
public class ReporteVideoController : ControllerBase
{
    private readonly IReporteVideoService _service;

    public ReporteVideoController(IReporteVideoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReporteVideoRespuestaDTO>>> GetAll()
    {
        var reportes = await _service.GetAllDTOAsync();
        return Ok(reportes);
    }

    [HttpGet("video/{idVideo}")]
    public async Task<ActionResult<List<ReporteVideoDTO>>> GetByVideo(int idVideo)
    {
        var reportes = await _service.GetDTOsByVideoIdAsync(idVideo);
        return Ok(reportes);
    }



    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] CrearReporteVideoDTO dto)
    {
        var nuevoReporte = new ReporteVideo
        {
            IdVideo = dto.IdVideo,
            IdUsuario = dto.IdUsuario,
            Motivo = dto.Motivo,
            Fecha = DateTime.Now
        };

        await _service.AddAsync(nuevoReporte);
        return Ok();
    }


    [HttpDelete("video/{idVideo}")]
    public async Task<ActionResult> EliminarPorVideo(int idVideo)
    {
        await _service.DeleteByVideoIdAsync(idVideo);
        return NoContent();
    }

    [HttpPut("aprobar/{idVideo}")]
    public async Task<ActionResult> AprobarVideo(int idVideo)
    {
        try
        {
            // Resetear el contador de reportes del video a 0
            await _service.ResetearReportesAsync(idVideo);
            return Ok(new { mensaje = "Video aprobado y contador de reportes reiniciado" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = $"Error al aprobar el video: {ex.Message}" });
        }
    }
}
