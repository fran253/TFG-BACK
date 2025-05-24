using Microsoft.EntityFrameworkCore;

public class ValoracionQuizService : IValoracionQuizService
{
    private readonly AcademIQDbContext _context;

    public ValoracionQuizService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<ValoracionQuiz>> GetByQuizIdAsync(int idQuiz)
    {
        return await _context.ValoracionesQuiz
            .Where(v => v.IdQuiz == idQuiz)
            .OrderByDescending(v => v.Fecha)
            .ToListAsync();
    }

    public async Task<ValoracionQuiz?> GetByUsuarioYQuizAsync(int idUsuario, int idQuiz)
    {
        return await _context.ValoracionesQuiz
            .FirstOrDefaultAsync(v => v.IdUsuario == idUsuario && v.IdQuiz == idQuiz);
    }

    public async Task<double> GetPromedioValoracionesAsync(int idQuiz)
    {
        var valoraciones = await _context.ValoracionesQuiz
            .Where(v => v.IdQuiz == idQuiz)
            .Select(v => v.Puntuacion)
            .ToListAsync();

        if (valoraciones.Count == 0)
            return 0;

        return valoraciones.Average();
    }

    public async Task AddAsync(ValoracionQuiz valoracion)
    {
        // Verificar si ya existe una valoración del mismo usuario para el mismo quiz
        var existente = await GetByUsuarioYQuizAsync(valoracion.IdUsuario, valoracion.IdQuiz);
        
        if (existente != null)
        {
            // Si ya existe, actualizar en lugar de agregar
            existente.Puntuacion = valoracion.Puntuacion;
            existente.Comentario = valoracion.Comentario;
            existente.Fecha = DateTime.Now;
            await UpdateAsync(existente);
        }
        else
        {
            // Si no existe, agregar nueva valoración
            _context.ValoracionesQuiz.Add(valoracion);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(ValoracionQuiz valoracion)
    {
        _context.ValoracionesQuiz.Update(valoracion);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var valoracion = await _context.ValoracionesQuiz.FindAsync(id);
        if (valoracion != null)
        {
            _context.ValoracionesQuiz.Remove(valoracion);
            await _context.SaveChangesAsync();
        }
    }
}