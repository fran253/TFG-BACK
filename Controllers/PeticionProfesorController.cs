using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PeticionProfesorController : ControllerBase
{
    private readonly AcademIQDbContext _context;

    public PeticionProfesorController(AcademIQDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CrearPeticion([FromBody] PeticionProfesorDTO dto)
    {
        var nuevaPeticion = new PeticionProfesor
        {
            IdUsuario = dto.IdUsuario,
            DocumentacionUrl = dto.DocumentacionUrl,
            Texto = dto.Texto,
            FechaPeticion = DateTime.UtcNow
        };

        _context.PeticionProfesor.Add(nuevaPeticion);
        await _context.SaveChangesAsync();

        return Ok(new { mensaje = "Petición enviada correctamente" });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PeticionProfesor>>> ObtenerPeticiones()
    {
        return await _context.PeticionProfesor
            .Include(p => p.Usuario)
            .ToListAsync();
    }

    [HttpPut("aprobar/{id}")]
    public async Task<IActionResult> AprobarPeticion(int id)
    {
        var peticion = await _context.PeticionProfesor.FindAsync(id);
        if (peticion == null)
            return NotFound();

        var usuario = await _context.Usuarios.FindAsync(peticion.IdUsuario);
        if (usuario == null)
            return NotFound("Usuario no encontrado");

        usuario.IdRol = 2;
        _context.PeticionProfesor.Remove(peticion);
        await _context.SaveChangesAsync();

        return Ok(new { mensaje = "Petición aprobada y rol actualizado" });
    }

    [HttpDelete("rechazar/{id}")]
    public async Task<IActionResult> RechazarPeticion(int id)
    {
        var peticion = await _context.PeticionProfesor.FindAsync(id);
        if (peticion == null)
            return NotFound();

        _context.PeticionProfesor.Remove(peticion);
        await _context.SaveChangesAsync();

        return Ok(new { mensaje = "Petición rechazada y eliminada" });
    }
}
