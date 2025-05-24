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
        // Por ahora solo creamos el quiz básico
        // Las preguntas las implementaremos después
        
        var quiz = new Quiz
        {
            Nombre = quizDTO.Nombre,
            Descripcion = quizDTO.Descripcion,
            IdUsuario = quizDTO.IdUsuario,
            FechaCreacion = DateTime.Now
        };

        var idQuiz = await _quizService.AddAsync(quiz);
        return idQuiz;
    }

    public async Task<ResultadoQuizDTO> ProcesarRespuestasAsync(ResponderQuizDTO respuestasDTO)
    {
        // Implementación básica - por ahora retorna resultado vacío
        return new ResultadoQuizDTO
        {
            TotalPreguntas = 0,
            RespuestasCorrectas = 0,
            Porcentaje = 0,
            Detalles = new List<ResultadoDetallePreguntaDTO>()
        };
    }

    public async Task<EstadisticasQuizDTO> ObtenerEstadisticasQuizAsync(int idQuiz)
    {
        var quiz = await _context.Quizzes.FirstOrDefaultAsync(q => q.IdQuiz == idQuiz);
        
        if (quiz == null)
            throw new KeyNotFoundException($"No se encontró el quiz con ID {idQuiz}");

        // Buscar usuario por separado usando JOIN manual
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == quiz.IdUsuario);

        return new EstadisticasQuizDTO
        {
            IdQuiz = quiz.IdQuiz,
            Nombre = quiz.Nombre,
            TotalPreguntas = 0, // Por ahora 0
            TotalRespuestas = 0, // Por ahora 0
            PromedioCalificaciones = 0,
            PromedioValoraciones = 0,
            FechaCreacion = quiz.FechaCreacion,
            NombreCreador = usuario?.Nombre ?? "Usuario desconocido"
        };
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