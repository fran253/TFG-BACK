using TFG_BACK.Models;

namespace TFG_BACK.Services
{
    public interface IAsignaturaService
    {
        Task<List<Asignatura>> GetAllAsync();
        Task<Asignatura> GetByIdAsync(int id);
        Task AddAsync(Asignatura asignatura);
        Task UpdateAsync(Asignatura asignatura);
        Task<bool> DeleteAsync(int id);
    }
}
