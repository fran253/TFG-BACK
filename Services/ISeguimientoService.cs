using TFG_BACK.Models;

namespace TFG_BACK.Services
{
    public interface ISeguimientoService
    {
        Task<List<Seguimiento>> GetAllAsync();
        Task<Seguimiento> GetByIdAsync(int id);
        Task AddAsync(Seguimiento seguimiento);
        Task UpdateAsync(Seguimiento seguimiento);
        Task<bool> DeleteAsync(int id);
    }
}
