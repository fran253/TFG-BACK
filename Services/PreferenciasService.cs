using Microsoft.EntityFrameworkCore;

public class PreferenciasService : IPreferenciasService
{
    private readonly AcademIQDbContext _context;

    public PreferenciasService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<Preferencias>> GetAllAsync()
    {
        return await _context.Preferencias.ToListAsync();
    }

    public async Task<Preferencias?> GetByIdAsync(int id)
    {
        return await _context.Preferencias.FindAsync(id);
    }

    public async Task AddAsync(Preferencias preferencias)
    {
        _context.Preferencias.Add(preferencias);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Preferencias preferencias)
    {
        _context.Preferencias.Update(preferencias);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var p = await _context.Preferencias.FindAsync(id);
        if (p != null)
        {
            _context.Preferencias.Remove(p);
            await _context.SaveChangesAsync();
        }
    }
}
