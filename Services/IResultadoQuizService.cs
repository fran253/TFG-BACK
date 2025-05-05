using System.Collections.Generic;
using System.Threading.Tasks;

public interface IResultadoQuizService
{
    Task<List<ResultadoQuiz>> GetAllAsync();
    Task<ResultadoQuiz?> GetByIdAsync(int id);
    Task<List<ResultadoQuiz>> GetByUsuarioAsync(int idUsuario);
    Task<List<ResultadoQuiz>> GetByQuizAsync(int idQuiz);
    Task AddAsync(ResultadoQuiz resultado);
    Task DeleteAsync(int id);
}
