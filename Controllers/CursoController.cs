using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CursoController : ControllerBase
{
    private readonly ICursoService _service;
    private readonly IUsuarioService _usuarioService;

    public CursoController(ICursoService service, IUsuarioService usuarioService)
    {
        _service = service;
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Curso>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Curso>> GetById(int id)
    {
        var curso = await _service.GetByIdAsync(id);
        return curso == null ? NotFound() : Ok(curso);
    }

    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] Curso curso)
    {
        await _service.AddAsync(curso);
        return CreatedAtAction(nameof(GetById), new { id = curso.IdCurso }, curso);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Editar(int id, [FromBody] Curso curso)
    {
        if (id != curso.IdCurso) return BadRequest();
        await _service.UpdateAsync(curso);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("crear")]
    public async Task<ActionResult> CrearCurso([FromBody] CursoCrearDTO dto)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        var usuario = await _usuarioService.GetByTokenAsync(token);
        if (usuario == null)
            return Unauthorized("Token inválido.");

        var curso = await _service.AddCursoConUsuarioAsync(dto, usuario.IdUsuario);

        if (curso == null)
            return BadRequest("Ya existe un curso con ese nombre.");

        return CreatedAtAction(nameof(GetById), new { id = curso.IdCurso }, curso);
    }
}
