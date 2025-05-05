using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class SeguimientoController : ControllerBase
{
    private readonly ISeguimientoService _service;

    public SeguimientoController(ISeguimientoService service)
    {
        _service = service;
    }

    [HttpGet("alumno/{idAlumno}")]
    public async Task<ActionResult<List<Usuario>>> GetProfesores(int idAlumno)
    {
        return Ok(await _service.GetProfesoresSeguidos(idAlumno));
    }

    [HttpGet("profesor/{idProfesor}")]
    public async Task<ActionResult<List<Usuario>>> GetAlumnos(int idProfesor)
    {
        return Ok(await _service.GetSeguidoresDelProfesor(idProfesor));
    }

    [HttpPost]
    public async Task<ActionResult> Seguir([FromBody] Seguimiento seguimiento)
    {
        await _service.AddAsync(seguimiento);
        return Ok();
    }

    [HttpDelete("{idAlumno}/{idProfesor}")]
    public async Task<ActionResult> DejarDeSeguir(int idAlumno, int idProfesor)
    {
        await _service.DeleteAsync(idAlumno, idProfesor);
        return NoContent();
    }
}
