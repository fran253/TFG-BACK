using System.Collections.Generic;
using System.Threading.Tasks;

public interface IQuizService
{
    Task<List<Quiz>> GetAllAsync();
    Task<Quiz?> GetByIdAsync(int id);
    Task AddAsync(Quiz quiz);
    Task UpdateAsync(Quiz quiz);
    Task DeleteAsync(int id);
    Task<List<Quiz>> GetByCursoAsync(int idCurso);
    Task<List<Quiz>> GetByAsignaturaAsync(int idAsignatura);
    Task<List<Quiz>> GetByCursoYAsignaturaAsync(int idCurso, int idAsignatura);

}
