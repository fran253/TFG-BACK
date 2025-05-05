using Microsoft.EntityFrameworkCore;

public class MarcadorVideoService : IMarcadorVideoService
{
    private readonly AcademIQDbContext _context;

    public MarcadorVideoService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<MarcadorVideo>> GetAllByVideoIdAsync(int idVideo)
    {
        return await _context.MarcadoresVideo
            .Where(m => m.IdVideo == idVideo)
            .ToListAsync();
    }

    public async Task<MarcadorVideo?> GetByIdAsync(int id)
    {
        return await _context.MarcadoresVideo
            .FirstOrDefaultAsync(m => m.IdMarcador == id);
    }

    public async Task AddAsync(MarcadorVideo marcador)
    {
        _context.MarcadoresVideo.Add(marcador);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MarcadorVideo marcador)
    {
        _context.MarcadoresVideo.Update(marcador);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var marcador = await _context.MarcadoresVideo.FindAsync(id);
        if (marcador != null)
        {
            _context.MarcadoresVideo.Remove(marcador);
            await _context.SaveChangesAsync();
        }
    }
}
