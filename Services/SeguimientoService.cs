using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SeguimientoService : ISeguimientoService
{
    private readonly AcademIQDbContext _context;

    public SeguimientoService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<Usuario>> GetProfesoresSeguidos(int idAlumno)
    {
        return await _context.Seguimientos
            .Where(s => s.IdAlumno == idAlumno)
            .Select(s => s.Profesor)
            .ToListAsync();
    }

    public async Task<List<Usuario>> GetSeguidoresDelProfesor(int idProfesor)
    {
        return await _context.Seguimientos
            .Where(s => s.IdProfesor == idProfesor)
            .Select(s => s.Alumno)
            .ToListAsync();
    }

    public async Task AddAsync(Seguimiento seguimiento)
    {
        _context.Seguimientos.Add(seguimiento);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int idAlumno, int idProfesor)
    {
        var seguimiento = await _context.Seguimientos.FindAsync(idAlumno, idProfesor);
        if (seguimiento != null)
        {
            _context.Seguimientos.Remove(seguimiento);
            await _context.SaveChangesAsync();
        }
    }
}
