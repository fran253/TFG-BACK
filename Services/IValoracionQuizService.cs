using System.Collections.Generic;
using System.Threading.Tasks;

public interface IValoracionQuizService
{
    Task<List<ValoracionQuiz>> GetByQuizIdAsync(int idQuiz);
    Task<ValoracionQuiz?> GetByUsuarioYQuizAsync(int idUsuario, int idQuiz);
    Task<double> GetPromedioValoracionesAsync(int idQuiz);
    Task AddAsync(ValoracionQuiz valoracion);
    Task UpdateAsync(ValoracionQuiz valoracion);
    Task DeleteAsync(int id);
}