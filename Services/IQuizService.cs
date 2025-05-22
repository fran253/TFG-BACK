using System.Collections.Generic;
using System.Threading.Tasks;

public interface IQuizService
{
    Task<List<Quiz>> GetAllAsync();
    Task<Quiz?> GetByIdAsync(int id);
    Task<List<Quiz>> GetByUsuarioAsync(int idUsuario);
    Task<List<Quiz>> GetQuizzesPopularesAsync(int limite = 10);
    Task<int> AddAsync(Quiz quiz);
    Task UpdateAsync(Quiz quiz);
    Task DeleteAsync(int id);
    
    // Validaciones
    Task<bool> ValidarLimiteQuizzesUsuarioAsync(int idUsuario);
}