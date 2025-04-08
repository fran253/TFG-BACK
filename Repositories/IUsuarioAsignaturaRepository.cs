using TFG_BACK.Models;

namespace TFG_BACK.Repositories
{
    public interface IUsuarioAsignaturaRepository
    {
        Task<List<UsuarioAsignatura>> GetAllAsync();
        Task<List<Asignatura>> GetByUsuarioIdAsync(int idUsuario);
        Task AddAsync(UsuarioAsignatura relacion);
        Task<bool> DeleteAsync(int idUsuario, int idAsignatura);
    }
}
