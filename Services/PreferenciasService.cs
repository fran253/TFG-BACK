using TFG_BACK.Models;
using TFG_BACK.Repositories;

namespace TFG_BACK.Services
{
    public class PreferenciasService : IPreferenciasService
    {
        private readonly IPreferenciasRepository _preferenciasRepository;

        public PreferenciasService(IPreferenciasRepository preferenciasRepository)
        {
            _preferenciasRepository = preferenciasRepository;
        }

        public Task<List<Preferencias>> GetAllAsync()
        {
            return _preferenciasRepository.GetAllAsync();
        }

        public Task<Preferencias> GetByIdAsync(int id)
        {
            return _preferenciasRepository.GetByIdAsync(id);
        }

        public Task AddAsync(Preferencias preferencias)
        {
            return _preferenciasRepository.AddAsync(preferencias);
        }

        public Task UpdateAsync(Preferencias preferencias)
        {
            return _preferenciasRepository.UpdateAsync(preferencias);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _preferenciasRepository.DeleteAsync(id);
        }
    }
}
