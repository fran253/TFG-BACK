using Microsoft.EntityFrameworkCore;
using TFG_BACK.Models.DTOs;

public class QuizService : IQuizService
{
    private readonly AcademIQDbContext _context;
    private const int MAX_QUIZZES_POR_USUARIO = 50;

    public QuizService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<QuizResponseDto>> GetAllWithUserInfoAsync()
    {
        var quizzes = await (from q in _context.Quizzes
                           join u in _context.Usuarios on q.IdUsuario equals u.IdUsuario
                           select new QuizResponseDto
                           {
                               IdQuiz = q.IdQuiz,
                               Nombre = q.Nombre,
                               Descripcion = q.Descripcion,
                               IdUsuario = q.IdUsuario,
                               NombreUsuario = u.Nombre,
                               EmailUsuario = u.Gmail,
                               FechaCreacion = q.FechaCreacion,
                               TotalPreguntas = 0 // Por ahora 0, después contaremos las preguntas reales
                           }).OrderByDescending(q => q.FechaCreacion)
                           .ToListAsync();

        return quizzes;
    }

    public async Task<List<Quiz>> GetAllAsync()
    {
        return await _context.Quizzes
            .OrderByDescending(q => q.FechaCreacion)
            .ToListAsync();
    }

    public async Task<QuizResponseDto?> GetByIdWithUserInfoAsync(int id)
    {
        var quiz = await (from q in _context.Quizzes
                         join u in _context.Usuarios on q.IdUsuario equals u.IdUsuario
                         where q.IdQuiz == id
                         select new QuizResponseDto
                         {
                             IdQuiz = q.IdQuiz,
                             Nombre = q.Nombre,
                             Descripcion = q.Descripcion,
                             IdUsuario = q.IdUsuario,
                             NombreUsuario = u.Nombre,
                             EmailUsuario = u.Gmail,
                             FechaCreacion = q.FechaCreacion,
                             TotalPreguntas = 0
                         }).FirstOrDefaultAsync();

        return quiz;
    }

    public async Task<Quiz?> GetByIdAsync(int id)
    {
        return await _context.Quizzes.FirstOrDefaultAsync(q => q.IdQuiz == id);
    }

    public async Task<List<QuizListDto>> GetByUsuarioWithInfoAsync(int idUsuario)
    {
        var quizzes = await (from q in _context.Quizzes
                           join u in _context.Usuarios on q.IdUsuario equals u.IdUsuario
                           where q.IdUsuario == idUsuario
                           select new QuizListDto
                           {
                               IdQuiz = q.IdQuiz,
                               Nombre = q.Nombre,
                               Descripcion = q.Descripcion,
                               NombreCreador = u.Nombre,
                               FechaCreacion = q.FechaCreacion,
                               TotalPreguntas = 0
                           }).OrderByDescending(q => q.FechaCreacion)
                           .ToListAsync();

        return quizzes;
    }

    public async Task<List<Quiz>> GetByUsuarioAsync(int idUsuario)
    {
        return await _context.Quizzes
            .Where(q => q.IdUsuario == idUsuario)
            .OrderByDescending(q => q.FechaCreacion)
            .ToListAsync();
    }

    public async Task<List<QuizListDto>> GetQuizzesPopularesAsync(int limite = 10)
    {
        var quizzes = await (from q in _context.Quizzes
                           join u in _context.Usuarios on q.IdUsuario equals u.IdUsuario
                           select new QuizListDto
                           {
                               IdQuiz = q.IdQuiz,
                               Nombre = q.Nombre,
                               Descripcion = q.Descripcion,
                               NombreCreador = u.Nombre,
                               FechaCreacion = q.FechaCreacion,
                               TotalPreguntas = 0
                           }).OrderByDescending(q => q.FechaCreacion)
                           .Take(limite)
                           .ToListAsync();

        return quizzes;
    }

    public async Task<int> AddAsync(Quiz quiz)
    {
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

    public async Task<bool> UsuarioEsPropietarioAsync(int idQuiz, int idUsuario)
    {
        return await _context.Quizzes.AnyAsync(q => q.IdQuiz == idQuiz && q.IdUsuario == idUsuario);
    }

    public async Task<QuizStatsDto?> GetEstadisticasAsync(int idQuiz)
    {
        var stats = await (from q in _context.Quizzes
                          join u in _context.Usuarios on q.IdUsuario equals u.IdUsuario
                          where q.IdQuiz == idQuiz
                          select new QuizStatsDto
                          {
                              IdQuiz = q.IdQuiz,
                              Nombre = q.Nombre,
                              NombreCreador = u.Nombre,
                              FechaCreacion = q.FechaCreacion,
                              TotalPreguntas = 0, // Después lo calcularemos con las preguntas reales
                              TotalRespuestas = 0,
                              VecesRespondido = 0
                          }).FirstOrDefaultAsync();

        return stats;
    }
}