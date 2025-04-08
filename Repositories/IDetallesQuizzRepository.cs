using TFG_BACK.Models;

namespace TFG_BACK.Repositories
{
    public interface IDetalleQuizzRepository
    {
        Task<List<DetalleQuizz>> GetAllAsync();
        Task<DetalleQuizz> GetByIdAsync(int id);
        Task AddAsync(DetalleQuizz detalle);
        Task UpdateAsync(DetalleQuizz detalle);
        Task<bool> DeleteAsync(int id);
    }
}
