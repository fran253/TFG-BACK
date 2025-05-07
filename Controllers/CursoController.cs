using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CursoController : ControllerBase
{
    private readonly ICursoService _service;

    public CursoController(ICursoService service)
    {
        _service = service;
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
}
