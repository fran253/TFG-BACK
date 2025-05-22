using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AsignaturaController : ControllerBase
{
    private readonly IAsignaturaService _asignaturaService;

    public AsignaturaController(IAsignaturaService asignaturaService)
    {
        _asignaturaService = asignaturaService;
    }

    // GET: api/asignatura
    [HttpGet]
    public async Task<ActionResult<List<Asignatura>>> GetAll()
    {
        var asignaturas = await _asignaturaService.GetAllAsync();
        return Ok(asignaturas);
    }

    // GET: api/asignatura/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Asignatura>> GetById(int id)
    {
        var asignatura = await _asignaturaService.GetByIdAsync(id);
        if (asignatura == null)
            return NotFound();

        return Ok(asignatura);
    }

    // GET: api/asignatura/curso/{idCurso}
    [HttpGet("curso/{idCurso}")]
    public async Task<ActionResult<List<Asignatura>>> GetByCurso(int idCurso)
    {
        var lista = await _asignaturaService.GetByIdCursoAsync(idCurso);
        if (lista == null || lista.Count == 0)
            return NotFound("No se encontraron asignaturas para este curso.");
        return Ok(lista);
    }

    // POST: api/asignatura
    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] AsignaturaCrearDTO dto)
    {
        var nuevaAsignatura = new Asignatura
        {
            Nombre = dto.Nombre,
            Imagen = dto.Imagen,
            Descripcion = dto.Descripcion,
            IdCurso = dto.IdCurso,
            FechaCreacion = DateTime.UtcNow
        };

        await _asignaturaService.AddAsync(nuevaAsignatura);

        return CreatedAtAction(nameof(GetById), new { id = nuevaAsignatura.IdAsignatura }, nuevaAsignatura);
    }

    // PUT: api/asignatura/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> Editar(int id, [FromBody] Asignatura asignatura)
    {
        if (id != asignatura.IdAsignatura)
            return BadRequest("El ID no coincide con la asignatura.");

        await _asignaturaService.UpdateAsync(asignatura);
        return NoContent();
    }

    // DELETE: api/asignatura/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        var existente = await _asignaturaService.GetByIdAsync(id);
        if (existente == null)
            return NotFound();

        await _asignaturaService.DeleteAsync(id);
        return NoContent();
    }
    
}
