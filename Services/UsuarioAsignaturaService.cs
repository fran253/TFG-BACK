using TFG_BACK.Models;
using TFG_BACK.Repositories;

namespace TFG_BACK.Services
{
    public class UsuarioAsignaturaService : IUsuarioAsignaturaService
    {
        private readonly IUsuarioAsignaturaRepository _usuarioAsignaturaRepository;

        public UsuarioAsignaturaService(IUsuarioAsignaturaRepository usuarioAsignaturaRepository)
        {
            _usuarioAsignaturaRepository = usuarioAsignaturaRepository;
        }

        public Task<List<UsuarioAsignatura>> GetAllAsync()
        {
            return _usuarioAsignaturaRepository.GetAllAsync();
        }

        public Task<List<Asignatura>> GetByUsuarioIdAsync(int idUsuario)
        {
            return _usuarioAsignaturaRepository.GetByUsuarioIdAsync(idUsuario);
        }

        public Task AddAsync(UsuarioAsignatura relacion)
        {
            return _usuarioAsignaturaRepository.AddAsync(relacion);
        }

        public Task<bool> DeleteAsync(int idUsuario, int idAsignatura)
        {
            return _usuarioAsignaturaRepository.DeleteAsync(idUsuario, idAsignatura);
        }
    }
}
