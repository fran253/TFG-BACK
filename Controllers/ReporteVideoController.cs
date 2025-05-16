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
    public async Task<ActionResult<List<ReporteVideo>>> GetAll()
    {
        var reportes = await _service.GetAllAsync();
        return Ok(reportes);
    }

    [HttpGet("video/{idVideo}")]
    public async Task<ActionResult<List<ReporteVideo>>> GetByVideo(int idVideo)
    {
        var reportes = await _service.GetByVideoIdAsync(idVideo);
        return Ok(reportes);
    }

    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] ReporteVideo reporte)
    {
        await _service.AddAsync(reporte);
        return Ok();
    }

    [HttpDelete("video/{idVideo}")]
    public async Task<ActionResult> EliminarPorVideo(int idVideo)
    {
        await _service.DeleteByVideoIdAsync(idVideo);
        return NoContent();
    }
}
