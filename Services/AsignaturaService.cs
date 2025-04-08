using TFG_BACK.Models;
using TFG_BACK.Repositories;

namespace TFG_BACK.Services
{
    public class AsignaturaService : IAsignaturaService
    {
        private readonly IAsignaturaRepository _asignaturaRepository;

        public AsignaturaService(IAsignaturaRepository asignaturaRepository)
        {
            _asignaturaRepository = asignaturaRepository;
        }

        public Task<List<Asignatura>> GetAllAsync()
        {
            return _asignaturaRepository.GetAllAsync();
        }

        public Task<Asignatura> GetByIdAsync(int id)
        {
            return _asignaturaRepository.GetByIdAsync(id);
        }

        public Task AddAsync(Asignatura asignatura)
        {
            return _asignaturaRepository.AddAsync(asignatura);
        }

        public Task UpdateAsync(Asignatura asignatura)
        {
            return _asignaturaRepository.UpdateAsync(asignatura);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _asignaturaRepository.DeleteAsync(id);
        }
    }
}
