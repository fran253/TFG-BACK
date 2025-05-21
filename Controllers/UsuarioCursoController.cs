using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UsuarioCursoController : ControllerBase
{
    private readonly IUsuarioCursoService _service;

    public UsuarioCursoController(IUsuarioCursoService service)
    {
        _service = service;
    }

    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<List<Curso>>> GetCursos(int idUsuario)
    {
        return Ok(await _service.GetCursosDeUsuario(idUsuario));
    }

    [HttpGet("curso/{idCurso}")]
    public async Task<ActionResult<List<Usuario>>> GetUsuarios(int idCurso)
    {
        return Ok(await _service.GetUsuariosDeCurso(idCurso));
    }

    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] UsuarioCursoDTO dto)
    {
    var nuevo = new UsuarioCurso
    {
        IdUsuario = dto.IdUsuario,
        IdCurso = dto.IdCurso
    };

        await _service.AddAsync(nuevo);
        return Ok();
    }

    [HttpDelete("{idUsuario}/{idCurso}")]
    public async Task<ActionResult> Eliminar(int idUsuario, int idCurso)
    {
        await _service.DeleteAsync(idUsuario, idCurso);
        return NoContent();
    }
}