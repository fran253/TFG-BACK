using TFG_BACK.Models;
using TFG_BACK.Repositories;

namespace TFG_BACK.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Task<List<Usuario>> GetAllAsync()
        {
            return _usuarioRepository.GetAllAsync();
        }

        public Task<Usuario> GetByIdAsync(int id)
        {
            return _usuarioRepository.GetByIdAsync(id);
        }

        public Task AddAsync(Usuario usuario)
        {
            return _usuarioRepository.AddAsync(usuario);
        }

        public Task UpdateAsync(Usuario usuario)
        {
            return _usuarioRepository.UpdateAsync(usuario);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _usuarioRepository.DeleteAsync(id);
        }
    }
}
