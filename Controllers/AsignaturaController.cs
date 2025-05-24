using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AsignaturaController : ControllerBase
{
    private readonly IAsignaturaService _asignaturaService;
    private readonly ICursoService _cursoService;
    private readonly IUsuarioService _usuarioService;

    public AsignaturaController(
        IAsignaturaService asignaturaService,
        ICursoService cursoService,
        IUsuarioService usuarioService)
    {
        _asignaturaService = asignaturaService;
        _cursoService = cursoService;
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Asignatura>>> GetAll()
    {
        var asignaturas = await _asignaturaService.GetAllAsync();
        return Ok(asignaturas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Asignatura>> GetById(int id)
    {
        var asignatura = await _asignaturaService.GetByIdAsync(id);
        return asignatura == null ? NotFound() : Ok(asignatura);
    }

    [HttpGet("curso/{idCurso}")]
    public async Task<ActionResult<List<Asignatura>>> GetByCurso(int idCurso)
    {
        var lista = await _asignaturaService.GetByIdCursoAsync(idCurso);
        return lista == null || lista.Count == 0
            ? NotFound("No se encontraron asignaturas para este curso.")
            : Ok(lista);
    }

    [HttpPost]
    public async Task<ActionResult> Crear([FromBody] AsignaturaCrearDTO dto)
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var usuario = await _usuarioService.GetByTokenAsync(token);
        if (usuario == null)
            return Unauthorized("Token inválido.");

        var curso = await _cursoService.GetByIdAsync(dto.IdCurso);
        if (curso == null)
            return NotFound("El curso no existe.");

        if (curso.IdUsuario != usuario.IdUsuario)
            return Forbid("No tienes permiso para añadir asignaturas a este curso.");

        var nuevaAsignatura = new Asignatura
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            IdCurso = dto.IdCurso,
            FechaCreacion = DateTime.UtcNow
        };

        await _asignaturaService.AddAsync(nuevaAsignatura);

        return CreatedAtAction(nameof(GetById), new { id = nuevaAsignatura.IdAsignatura }, nuevaAsignatura);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Editar(int id, [FromBody] Asignatura asignatura)
    {
        if (id != asignatura.IdAsignatura)
            return BadRequest("El ID no coincide con la asignatura.");

        await _asignaturaService.UpdateAsync(asignatura);
        return NoContent();
    }

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
