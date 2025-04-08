using TFG_BACK.Models;

namespace TFG_BACK.Repositories
{
    public interface IRolRepository
    {
        Task<List<Rol>> GetAllAsync();
        Task<Rol> GetByIdAsync(int id);
        Task AddAsync(Rol rol);
        Task UpdateAsync(Rol rol);
        Task<bool> DeleteAsync(int id);
    }
}
