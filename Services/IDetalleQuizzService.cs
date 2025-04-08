using TFG_BACK.Models;

namespace TFG_BACK.Services
{
    public interface IDetalleQuizzService
    {
        Task<List<DetalleQuizz>> GetAllAsync();
        Task<DetalleQuizz> GetByIdAsync(int id);
        Task AddAsync(DetalleQuizz detalle);
        Task UpdateAsync(DetalleQuizz detalle);
        Task<bool> DeleteAsync(int id);
    }
}
