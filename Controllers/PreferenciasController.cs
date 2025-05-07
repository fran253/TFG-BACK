using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PreferenciasController : ControllerBase
{
    private readonly IPreferenciasService _service;

    public PreferenciasController(IPreferenciasService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Preferencias>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Preferencias>> GetById(int id)
    {
        var p = await _service.GetByIdAsync(id);
        return p == null ? NotFound() : Ok(p);
    }

    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] Preferencias p)
    {
        await _service.AddAsync(p);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Editar(int id, [FromBody] Preferencias p)
    {
        if (id != p.IdPreferencia)
            return BadRequest("ID no coincide.");
        await _service.UpdateAsync(p);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
