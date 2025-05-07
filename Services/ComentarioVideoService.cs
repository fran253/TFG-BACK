using Microsoft.EntityFrameworkCore;

public class ComentarioVideoService : IComentarioVideoService
{
    private readonly AcademIQDbContext _context;

    public ComentarioVideoService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<ComentarioVideo>> GetAllByVideoIdAsync(int idVideo)
    {
        return await _context.ComentariosVideo
            .Include(c => c.Usuario)
            .Where(c => c.IdVideo == idVideo)
            .ToListAsync();
    }

    public async Task<ComentarioVideo?> GetByIdAsync(int id)
    {
        return await _context.ComentariosVideo
            .Include(c => c.Usuario)
            .FirstOrDefaultAsync(c => c.IdComentario == id);
    }

    public async Task AddAsync(ComentarioVideo comentario)
    {
        _context.ComentariosVideo.Add(comentario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ComentarioVideo comentario)
    {
        _context.ComentariosVideo.Update(comentario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var comentario = await _context.ComentariosVideo.FindAsync(id);
        if (comentario != null)
        {
            _context.ComentariosVideo.Remove(comentario);
            await _context.SaveChangesAsync();
        }
    }
}
