using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TFG_BACK.Services;

[ApiController]
[Route("api/[controller]")]
public class PeticionProfesorController : ControllerBase
{
   private readonly AcademIQDbContext _context;
   private readonly IS3UploaderService _s3UploaderService;

   public PeticionProfesorController(AcademIQDbContext context, IS3UploaderService s3UploaderService)
   {
       _context = context;
       _s3UploaderService = s3UploaderService;
   }

   [HttpPost]
   public async Task<IActionResult> CrearPeticion([FromForm] CrearPeticionProfesorRequest request)
   {
       if (request.Documentacion == null)
           return BadRequest("La imagen de documentación es requerida.");

       // Subir imagen a S3
       var urlDocumentacion = await _s3UploaderService.SubirArchivoAsync(request.Documentacion, "peticiones");

       var nuevaPeticion = new PeticionProfesor
       {
           IdUsuario = request.IdUsuario,
           DocumentacionUrl = urlDocumentacion,
           Texto = request.Texto,
           FechaPeticion = DateTime.UtcNow
       };

       _context.PeticionProfesor.Add(nuevaPeticion);
       await _context.SaveChangesAsync();

       return Ok(new { mensaje = "Petición enviada correctamente", url = urlDocumentacion });
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