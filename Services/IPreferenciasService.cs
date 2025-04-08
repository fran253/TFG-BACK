using TFG_BACK.Models;

namespace TFG_BACK.Services
{
    public interface IPreferenciasService
    {
        Task<List<Preferencias>> GetAllAsync();
        Task<Preferencias> GetByIdAsync(int id);
        Task AddAsync(Preferencias preferencias);
        Task UpdateAsync(Preferencias preferencias);
        Task<bool> DeleteAsync(int id);
    }
}
