using TFG_BACK.Models;
using TFG_BACK.Repositories;

namespace TFG_BACK.Services
{
    public class DetalleQuizzService : IDetalleQuizzService
    {
        private readonly IDetalleQuizzRepository _detalleQuizzRepository;

        public DetalleQuizzService(IDetalleQuizzRepository detalleQuizzRepository)
        {
            _detalleQuizzRepository = detalleQuizzRepository;
        }

        public Task<List<DetalleQuizz>> GetAllAsync()
        {
            return _detalleQuizzRepository.GetAllAsync();
        }

        public Task<DetalleQuizz> GetByIdAsync(int id)
        {
            return _detalleQuizzRepository.GetByIdAsync(id);
        }

        public Task AddAsync(DetalleQuizz detalle)
        {
            return _detalleQuizzRepository.AddAsync(detalle);
        }

        public Task UpdateAsync(DetalleQuizz detalle)
        {
            return _detalleQuizzRepository.UpdateAsync(detalle);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _detalleQuizzRepository.DeleteAsync(id);
        }
    }
}
