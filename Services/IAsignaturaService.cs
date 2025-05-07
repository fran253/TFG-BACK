using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAsignaturaService
{
    Task<List<Asignatura>> GetAllAsync();
    Task<Asignatura?> GetByIdAsync(int id);
    Task<List<Asignatura>> GetByIdCursoAsync(int idCurso);
    Task AddAsync(Asignatura asignatura);
    Task UpdateAsync(Asignatura asignatura);
    Task DeleteAsync(int id);
}
