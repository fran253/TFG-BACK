using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UsuarioAsignaturaController : ControllerBase
{
    private readonly IUsuarioAsignaturaService _service;

    public UsuarioAsignaturaController(IUsuarioAsignaturaService service)
    {
        _service = service;
    }

    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<List<Asignatura>>> GetAsignaturas(int idUsuario)
    {
        return Ok(await _service.GetAsignaturasDeUsuario(idUsuario));
    }

    [HttpGet("asignatura/{idAsignatura}")]
    public async Task<ActionResult<List<Usuario>>> GetUsuarios(int idAsignatura)
    {
        return Ok(await _service.GetUsuariosDeAsignatura(idAsignatura));
    }

    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] UsuarioAsignatura relacion)
    {
        await _service.AddAsync(relacion);
        return Ok();
    }

    [HttpDelete("{idUsuario}/{idAsignatura}")]
    public async Task<ActionResult> Eliminar(int idUsuario, int idAsignatura)
    {
        await _service.DeleteAsync(idUsuario, idAsignatura);
        return NoContent();
    }
}
