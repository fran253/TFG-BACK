using TFG_BACK.Models;

namespace TFG_BACK.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task AddAsync(Usuario user);
        Task UpdateAsync(Usuario user);
        Task<bool> DeleteAsync(int id);
    }
}
