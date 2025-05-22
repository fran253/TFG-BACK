using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class QuizService : IQuizService
{
    private readonly AcademIQDbContext _context;
    private const int MAX_QUIZZES_POR_USUARIO = 50; // Límite opcional

    public QuizService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<Quiz>> GetAllAsync()
    {
        return await _context.Quizzes
            .Include(q => q.Usuario)
            .Include(q => q.Preguntas)
                .ThenInclude(p => p.Respuestas)
            .OrderByDescending(q => q.FechaCreacion)
            .ToListAsync();
    }

    public async Task<Quiz?> GetByIdAsync(int id)
    {
        return await _context.Quizzes
            .Include(q => q.Usuario)
            .Include(q => q.Preguntas.OrderBy(p => p.Orden))
                .ThenInclude(p => p.Respuestas.OrderBy(r => r.Orden))
            .FirstOrDefaultAsync(q => q.IdQuiz == id);
    }

    public async Task<List<Quiz>> GetByUsuarioAsync(int idUsuario)
    {
        return await _context.Quizzes
            .Where(q => q.IdUsuario == idUsuario)
            .Include(q => q.Preguntas)
            .OrderByDescending(q => q.FechaCreacion)
            .ToListAsync();
    }

    public async Task<List<Quiz>> GetQuizzesPopularesAsync(int limite = 10)
    {
        return await _context.Quizzes
            .Include(q => q.Usuario)
            .Include(q => q.Preguntas)
            .OrderByDescending(q => q.Resultados.Count)
            .Take(limite)
            .ToListAsync();
    }

    public async Task<int> AddAsync(Quiz quiz)
    {
        // Validar límite de quizzes por usuario (opcional)
        if (!await ValidarLimiteQuizzesUsuarioAsync(quiz.IdUsuario))
        {
            throw new InvalidOperationException($"El usuario ha alcanzado el límite máximo de {MAX_QUIZZES_POR_USUARIO} quizzes.");
        }

        _context.Quizzes.Add(quiz);
        await _context.SaveChangesAsync();
        return quiz.IdQuiz;
    }

    public async Task UpdateAsync(Quiz quiz)
    {
        _context.Quizzes.Update(quiz);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var quiz = await _context.Quizzes.FindAsync(id);
        if (quiz != null)
        {
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ValidarLimiteQuizzesUsuarioAsync(int idUsuario)
    {
        var count = await _context.Quizzes.CountAsync(q => q.IdUsuario == idUsuario);
        return count < MAX_QUIZZES_POR_USUARIO;
    }
}