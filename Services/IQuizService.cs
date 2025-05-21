using System.Collections.Generic;
using System.Threading.Tasks;
using TFG_BACK.Models.DTOs;

public interface IQuizService
{
    Task<List<Quiz>> GetAllAsync();
    Task<Quiz?> GetByIdAsync(int id);
    Task<List<Quiz>> GetByCursoAsync(int idCurso);
    Task<List<Quiz>> GetByAsignaturaAsync(int idAsignatura);
    Task<List<Quiz>> GetByCursoYAsignaturaAsync(int idCurso, int idAsignatura);
    Task<int> AddAsync(Quiz quiz);
    Task UpdateAsync(Quiz quiz);
    Task DeleteAsync(int id);
    
    // Nuevos métodos para la funcionalidad de quizzes
    Task<int> CrearQuizCompletoAsync(CrearQuizDTO quizDTO);
    Task<ResultadoQuizDTO> ProcesarRespuestasAsync(ResponderQuizDTO respuestasDTO);
    Task<List<Quiz>> GetQuizzesPopularesAsync(int limite = 10);
    Task<List<Quiz>> GetQuizzesPorUsuarioAsync(int idUsuario);
    Task<int> GetTotalPreguntasAsync(int idQuiz);
}