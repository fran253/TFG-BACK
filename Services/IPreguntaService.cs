using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPreguntaService
{
    Task<List<Pregunta>> GetByQuizIdAsync(int idQuiz);
    Task<Pregunta?> GetByIdAsync(int id);
    Task<int> AddAsync(Pregunta pregunta);
    Task UpdateAsync(Pregunta pregunta);
    Task DeleteAsync(int id);
    
    // Validaciones
    Task<bool> ValidarLimitePreguntasAsync(int idQuiz);
    Task<int> ContarPreguntasDelQuizAsync(int idQuiz);
}