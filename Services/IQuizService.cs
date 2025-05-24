using TFG_BACK.Models.DTOs;

public interface IQuizService
{
    // Métodos básicos (sin DTOs)
    Task<List<Quiz>> GetAllAsync();
    Task<Quiz?> GetByIdAsync(int id);
    Task<List<Quiz>> GetByUsuarioAsync(int idUsuario);
    Task<int> AddAsync(Quiz quiz);
    Task UpdateAsync(Quiz quiz);
    Task DeleteAsync(int id);
    Task<bool> ValidarLimiteQuizzesUsuarioAsync(int idUsuario);
    
    // Métodos con DTOs (incluyen información del usuario)
    Task<List<QuizResponseDto>> GetAllWithUserInfoAsync();
    Task<QuizResponseDto?> GetByIdWithUserInfoAsync(int id);
    Task<List<QuizListDto>> GetByUsuarioWithInfoAsync(int idUsuario);
    Task<List<QuizListDto>> GetQuizzesPopularesAsync(int limite = 10);
    Task<QuizStatsDto?> GetEstadisticasAsync(int idQuiz);
    Task<bool> UsuarioEsPropietarioAsync(int idQuiz, int idUsuario);
}