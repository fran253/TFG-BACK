using System.Collections.Generic;
using System.Threading.Tasks;

public interface IVideoService
{
    Task<List<Video>> GetAllAsync();
    Task<Video?> GetByIdAsync(int id);
    Task<List<Video>> GetByAsignaturaAsync(int idAsignatura);
    Task<List<Video>> GetByUsuarioAsync(int idUsuario);
    // Nuevo método para obtener videos por curso
    Task<List<Video>> GetByCursoAsync(int idCurso);
    Task AddAsync(Video video);
    Task UpdateAsync(Video video);
    Task DeleteAsync(int id);
}