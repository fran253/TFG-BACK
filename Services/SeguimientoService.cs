using TFG_BACK.Models;
using TFG_BACK.Repositories;

namespace TFG_BACK.Services
{
    public class SeguimientoService : ISeguimientoService
    {
        private readonly ISeguimientoRepository _seguimientoRepository;

        public SeguimientoService(ISeguimientoRepository seguimientoRepository)
        {
            _seguimientoRepository = seguimientoRepository;
        }

        public Task<List<Seguimiento>> GetAllAsync()
        {
            return _seguimientoRepository.GetAllAsync();
        }

        public Task<Seguimiento> GetByIdAsync(int id)
        {
            return _seguimientoRepository.GetByIdAsync(id);
        }

        public Task AddAsync(Seguimiento seguimiento)
        {
            return _seguimientoRepository.AddAsync(seguimiento);
        }

        public Task UpdateAsync(Seguimiento seguimiento)
        {
            return _seguimientoRepository.UpdateAsync(seguimiento);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _seguimientoRepository.DeleteAsync(id);
        }
    }
}
