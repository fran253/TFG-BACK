// Modificar Controllers/UsuarioController.cs
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _service;

    public UsuarioController(IUsuarioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Usuario>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetById(int id)
    {
        var usuario = await _service.GetByIdAsync(id);
        return usuario == null ? NotFound() : Ok(usuario);
    }

    [HttpGet("gmail/{gmail}")]
    public async Task<ActionResult<Usuario>> GetByGmail(string gmail)
    {
        var usuario = await _service.GetByGmailAsync(gmail);
        return usuario == null ? NotFound() : Ok(usuario);
    }

    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] Usuario usuario)
    {
        await _service.AddAsync(usuario);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Editar(int id, [FromBody] Usuario usuario)
    {
        if (id != usuario.IdUsuario) return BadRequest();
        await _service.UpdateAsync(usuario);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    // Nuevos endpoints para cursos seguidos
    [HttpGet("{idUsuario}/cursos-seguidos")]
    public async Task<ActionResult<List<Curso>>> GetCursosSeguidos(int idUsuario)
    {
        return Ok(await _service.GetCursosSeguidosAsync(idUsuario));
    }

    [HttpPost("{idUsuario}/seguir-curso/{idCurso}")]
    public async Task<ActionResult> SeguirCurso(int idUsuario, int idCurso)
    {
        await _service.SeguirCursoAsync(idUsuario, idCurso);
        return Ok();
    }

    [HttpDelete("{idUsuario}/dejar-de-seguir-curso/{idCurso}")]
    public async Task<ActionResult> DejarDeSeguirCurso(int idUsuario, int idCurso)
    {
        await _service.DejarDeSeguirCursoAsync(idUsuario, idCurso);
        return NoContent();
    }

    [HttpGet("{idUsuario}/sigue-curso/{idCurso}")]
    public async Task<ActionResult<bool>> SigueCurso(int idUsuario, int idCurso)
    {
        return Ok(await _service.SigueCursoAsync(idUsuario, idCurso));
    }
}