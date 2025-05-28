using Microsoft.EntityFrameworkCore;
using TFG_BACK.Models.DTOs;

public class QuizManagementService : IQuizManagementService
{
    private readonly AcademIQDbContext _context;
    private readonly IQuizService _quizService;

    public QuizManagementService(AcademIQDbContext context, IQuizService quizService)
    {
        _context = context;
        _quizService = quizService;
    }

    public async Task<int> CrearQuizCompletoAsync(CrearQuizCompletoDTO quizDTO)
    {
        // Validar que la asignatura pertenece al curso
        var asignaturaValida = await _context.Asignaturas
            .AnyAsync(a => a.IdAsignatura == quizDTO.IdAsignatura && a.IdCurso == quizDTO.IdCurso);

        if (!asignaturaValida)
        {
            throw new InvalidOperationException("La asignatura no pertenece al curso indicado.");
        }

        var quiz = new Quiz
        {
            Nombre = quizDTO.Nombre,
            Descripcion = quizDTO.Descripcion,
            IdUsuario = quizDTO.IdUsuario,
            IdCurso = quizDTO.IdCurso,
            IdAsignatura = quizDTO.IdAsignatura,
            FechaCreacion = DateTime.Now
        };

        var idQuiz = await _quizService.AddAsync(quiz);
        return idQuiz;
    }




   

    public async Task<bool> EliminarQuizCompletoAsync(int idQuiz, int idUsuario)
    {
        var quiz = await _context.Quizzes
            .FirstOrDefaultAsync(q => q.IdQuiz == idQuiz && q.IdUsuario == idUsuario);

        if (quiz == null)
            return false;

        await _quizService.DeleteAsync(idQuiz);
        return true;
    }

    public async Task<Quiz> ObtenerQuizParaResponderAsync(int idQuiz)
    {
        var quiz = await _context.Quizzes.FirstOrDefaultAsync(q => q.IdQuiz == idQuiz);

        if (quiz == null)
            throw new KeyNotFoundException($"No se encontró el quiz con ID {idQuiz}");

        return quiz;
    }

    public async Task<Quiz> ObtenerQuizCompletoAsync(int idQuiz)
    {
        var quiz = await _context.Quizzes.FirstOrDefaultAsync(q => q.IdQuiz == idQuiz);

        if (quiz == null)
            throw new KeyNotFoundException($"No se encontró el quiz con ID {idQuiz}");

        return quiz;
    }
}