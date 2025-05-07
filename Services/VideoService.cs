using Microsoft.EntityFrameworkCore;

public class VideoService : IVideoService
{
    private readonly AcademIQDbContext _context;

    public VideoService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<Video>> GetAllAsync()
    {
        return await _context.Videos
            .Include(v => v.Asignatura)
            .Include(v => v.Usuario)
            .Include(v => v.Curso)
            .ToListAsync();
    }

    public async Task<Video?> GetByIdAsync(int id)
    {
        return await _context.Videos
            .Include(v => v.Asignatura)
            .Include(v => v.Usuario)
            .Include(v => v.Curso)
            .FirstOrDefaultAsync(v => v.IdVideo == id);
    }

    public async Task<List<Video>> GetByAsignaturaAsync(int idAsignatura)
    {
        return await _context.Videos
            .Where(v => v.IdAsignatura == idAsignatura)
            .Include(v => v.Asignatura)
            .Include(v => v.Usuario)
            .ToListAsync();
    }

    public async Task<List<Video>> GetByUsuarioAsync(int idUsuario)
    {
        return await _context.Videos
            .Where(v => v.IdUsuario == idUsuario)
            .Include(v => v.Asignatura)
            .Include(v => v.Usuario)
            .ToListAsync();
    }

    // Nuevo método para obtener videos por curso
    public async Task<List<Video>> GetByCursoAsync(int idCurso)
    {
        return await _context.Videos
            .Where(v => v.IdCurso == idCurso)
            .Include(v => v.Asignatura)
            .Include(v => v.Usuario)
            .Include(v => v.Curso)
            .ToListAsync();
    }

    public async Task AddAsync(Video video)
    {
        _context.Videos.Add(video);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Video video)
    {
        _context.Videos.Update(video);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var video = await _context.Videos.FindAsync(id);
        if (video != null)
        {
            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();
        }
    }
}