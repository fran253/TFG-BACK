using Microsoft.EntityFrameworkCore;

public class UsuarioAsignaturaService : IUsuarioAsignaturaService
{
    private readonly AcademIQDbContext _context;

    public UsuarioAsignaturaService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<Asignatura>> GetAsignaturasDeUsuario(int idUsuario)
    {
        return await _context.UsuarioAsignaturas
            .Where(ua => ua.IdUsuario == idUsuario)
            .Select(ua => ua.Asignatura)
            .ToListAsync();
    }

    public async Task<List<Usuario>> GetUsuariosDeAsignatura(int idAsignatura)
    {
        return await _context.UsuarioAsignaturas
            .Where(ua => ua.IdAsignatura == idAsignatura)
            .Select(ua => ua.Usuario)
            .ToListAsync();
    }

    public async Task AddAsync(UsuarioAsignatura relacion)
    {
        _context.UsuarioAsignaturas.Add(relacion);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int idUsuario, int idAsignatura)
    {
        var rel = await _context.UsuarioAsignaturas.FindAsync(idUsuario, idAsignatura);
        if (rel != null)
        {
            _context.UsuarioAsignaturas.Remove(rel);
            await _context.SaveChangesAsync();
        }
    }
}
