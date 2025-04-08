using TFG_BACK.Models;

namespace TFG_BACK.Repositories
{
    public interface ISeguimientoRepository
    {
        Task<List<Seguimiento>> GetAllAsync();
        Task<Seguimiento> GetByIdAsync(int id);
        Task AddAsync(Seguimiento seguimiento);
        Task UpdateAsync(Seguimiento seguimiento);
        Task<bool> DeleteAsync(int id);
    }
}
