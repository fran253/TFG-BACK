using TFG_BACK.Models;

namespace TFG_BACK.Services
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task<bool> DeleteAsync(int id);
    }
}
