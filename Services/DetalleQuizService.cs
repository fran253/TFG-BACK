using Microsoft.EntityFrameworkCore;

public class DetalleQuizService : IDetalleQuizService
{
    private readonly AcademIQDbContext _context;

    public DetalleQuizService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<DetalleQuiz>> GetByQuizIdAsync(int idQuiz)
    {
        return await _context.DetallesQuiz
            .Where(d => d.IdQuiz == idQuiz)
            .ToListAsync();
    }

    public async Task<DetalleQuiz?> GetByIdAsync(int id)
    {
        return await _context.DetallesQuiz.FindAsync(id);
    }

    public async Task AddAsync(DetalleQuiz detalle)
    {
        _context.DetallesQuiz.Add(detalle);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(DetalleQuiz detalle)
    {
        _context.DetallesQuiz.Update(detalle);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var detalle = await _context.DetallesQuiz.FindAsync(id);
        if (detalle != null)
        {
            _context.DetallesQuiz.Remove(detalle);
            await _context.SaveChangesAsync();
        }
    }
}
