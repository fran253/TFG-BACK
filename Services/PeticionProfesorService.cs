using Microsoft.EntityFrameworkCore;
using TuProyecto.Models;

public class PeticionProfesorService : IPeticionProfesorService
{
    private readonly AcademIQDbContext _context;

    public PeticionProfesorService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task CrearPeticionAsync(PeticionProfesorDTO dto)
    {
        var peticion = new PeticionProfesor
        {
            IdUsuario = dto.IdUsuario,
            DocumentacionUrl = dto.DocumentacionUrl,
            Texto = dto.Texto,
            FechaPeticion = DateTime.UtcNow
        };

        _context.PeticionProfesor.Add(peticion);
        await _context.SaveChangesAsync();
    }

    public async Task<List<PeticionProfesor>> ObtenerTodasAsync()
    {
        return await _context.PeticionProfesor.Include(p => p.Usuario).ToListAsync();
    }

    public async Task AprobarPeticionAsync(int id)
    {
        var peticion = await _context.PeticionProfesor.FindAsync(id);
        if (peticion == null)
            throw new Exception("Petición no encontrada");

        var usuario = await _context.Usuarios.FindAsync(peticion.IdUsuario);
        if (usuario == null)
            throw new Exception("Usuario no encontrado");

        usuario.IdRol = 2;

        _context.PeticionProfesor.Remove(peticion); 
        await _context.SaveChangesAsync();
    }


    public async Task RechazarPeticionAsync(int id)
    {
        var peticion = await _context.PeticionProfesor.FindAsync(id);
        if (peticion == null)
            throw new Exception("Petición no encontrada");

        _context.PeticionProfesor.Remove(peticion);
        await _context.SaveChangesAsync();
    }
}
