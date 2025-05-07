using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class RolController : ControllerBase
{
    private readonly IRolService _service;

    public RolController(IRolService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Rol>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Rol>> GetById(int id)
    {
        var rol = await _service.GetByIdAsync(id);
        return rol == null ? NotFound() : Ok(rol);
    }

    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] Rol rol)
    {
        await _service.AddAsync(rol);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Editar(int id, [FromBody] Rol rol)
    {
        if (id != rol.IdRol) return BadRequest();
        await _service.UpdateAsync(rol);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
