using TFG_BACK.Models;

namespace TFG_BACK.Repositories
{
    public interface IPreferenciasRepository
    {
        Task<List<Preferencias>> GetAllAsync();
        Task<Preferencias> GetByIdAsync(int id);
        Task AddAsync(Preferencias pref);
        Task UpdateAsync(Preferencias pref);
        Task<bool> DeleteAsync(int id);
    }
}
