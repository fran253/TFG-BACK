using TFG_BACK.Models;
using TFG_BACK.Repositories;

namespace TFG_BACK.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public Task<List<Rol>> GetAllAsync()
        {
            return _rolRepository.GetAllAsync();
        }

        public Task<Rol> GetByIdAsync(int id)
        {
            return _rolRepository.GetByIdAsync(id);
        }

        public Task AddAsync(Rol rol)
        {
            return _rolRepository.AddAsync(rol);
        }

        public Task UpdateAsync(Rol rol)
        {
            return _rolRepository.UpdateAsync(rol);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _rolRepository.DeleteAsync(id);
        }
    }
}
