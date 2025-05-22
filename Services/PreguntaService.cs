using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PreguntaService : IPreguntaService
{
    private readonly AcademIQDbContext _context;
    private const int MAX_PREGUNTAS_POR_QUIZ = 20;

    public PreguntaService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pregunta>> GetByQuizIdAsync(int idQuiz)
    {
        return await _context.Preguntas
            .Where(p => p.IdQuiz == idQuiz)
            .Include(p => p.Respuestas.OrderBy(r => r.Orden))
            .OrderBy(p => p.Orden)
            .ToListAsync();
    }

    public async Task<Pregunta?> GetByIdAsync(int id)
    {
        return await _context.Preguntas
            .Include(p => p.Respuestas.OrderBy(r => r.Orden))
            .Include(p => p.Quiz)
            .FirstOrDefaultAsync(p => p.IdPregunta == id);
    }

    public async Task<int> AddAsync(Pregunta pregunta)
    {
        // Validar límite de preguntas por quiz
        if (!await ValidarLimitePreguntasAsync(pregunta.IdQuiz))
        {
            throw new InvalidOperationException($"El quiz ha alcanzado el límite máximo de {MAX_PREGUNTAS_POR_QUIZ} preguntas.");
        }

        // Asignar orden automáticamente
        var ultimoOrden = await _context.Preguntas
            .Where(p => p.IdQuiz == pregunta.IdQuiz)
            .MaxAsync(p => (int?)p.Orden) ?? 0;
        
        pregunta.Orden = ultimoOrden + 1;

        _context.Preguntas.Add(pregunta);
        await _context.SaveChangesAsync();
        return pregunta.IdPregunta;
    }

    public async Task UpdateAsync(Pregunta pregunta)
    {
        _context.Preguntas.Update(pregunta);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var pregunta = await _context.Preguntas.FindAsync(id);
        if (pregunta != null)
        {
            _context.Preguntas.Remove(pregunta);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ValidarLimitePreguntasAsync(int idQuiz)
    {
        var count = await ContarPreguntasDelQuizAsync(idQuiz);
        return count < MAX_PREGUNTAS_POR_QUIZ;
    }

    public async Task<int> ContarPreguntasDelQuizAsync(int idQuiz)
    {
        return await _context.Preguntas.CountAsync(p => p.IdQuiz == idQuiz);
    }
}