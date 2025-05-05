using Microsoft.EntityFrameworkCore;

public class AsignaturaService : IAsignaturaService
{
    private readonly AcademIQDbContext _context;

    public AsignaturaService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<Asignatura>> GetAllAsync()
    {
        return await _context.Asignaturas.Include(a => a.Curso).ToListAsync();
    }

    public async Task<Asignatura?> GetByIdAsync(int id)
    {
        return await _context.Asignaturas.Include(a => a.Curso)
            .FirstOrDefaultAsync(a => a.IdAsignatura == id);
    }

    public async Task<List<Asignatura>> GetByIdCursoAsync(int idCurso)
    {
        return await _context.Asignaturas
            .Where(a => a.IdCurso == idCurso)
            .ToListAsync();
    }

    public async Task AddAsync(Asignatura asignatura)
    {
        _context.Asignaturas.Add(asignatura);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Asignatura asignatura)
    {
        _context.Asignaturas.Update(asignatura);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var asignatura = await _context.Asignaturas.FindAsync(id);
        if (asignatura != null)
        {
            _context.Asignaturas.Remove(asignatura);
            await _context.SaveChangesAsync();
        }
    }
}
