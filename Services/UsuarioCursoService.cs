using Microsoft.EntityFrameworkCore;

public class UsuarioCursoService : IUsuarioCursoService
{
    private readonly AcademIQDbContext _context;

    public UsuarioCursoService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<Curso>> GetCursosDeUsuario(int idUsuario)
    {
        return await _context.UsuarioCursos
            .Where(uc => uc.IdUsuario == idUsuario)
            .Select(uc => uc.Curso)
            .ToListAsync();
    }

    public async Task<List<Usuario>> GetUsuariosDeCurso(int idCurso)
    {
        return await _context.UsuarioCursos
            .Where(uc => uc.IdCurso == idCurso)
            .Select(uc => uc.Usuario)
            .ToListAsync();
    }

    public async Task AddAsync(UsuarioCurso relacion)
    {
        _context.UsuarioCursos.Add(relacion);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int idUsuario, int idCurso)
    {
        var rel = await _context.UsuarioCursos.FindAsync(idUsuario, idCurso);
        if (rel != null)
        {
            _context.UsuarioCursos.Remove(rel);
            await _context.SaveChangesAsync();
        }
    }
}
