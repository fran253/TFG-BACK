public interface ICursoService
{
    Task<List<Curso>> GetAllAsync();
    Task<Curso?> GetByIdAsync(int id);
    Task AddAsync(Curso curso);
    Task UpdateAsync(Curso curso);
    Task DeleteAsync(int id);
    Task<Curso?> AddCursoConUsuarioAsync(CursoCrearDTO dto, int idUsuario, string? urlImagen = null);
    Task<List<CursoVideosDTO>> GetTopCursosConMasVideosAsync(int cantidad);
    Task<List<Curso>> GetCursosPorUsuarioAsync(int idUsuario);
    
    // NUEVOS MÉTODOS PARA ESTADÍSTICAS
    Task<int> ContarCursosPorUsuarioAsync(int idUsuario);
    Task<Curso> ObtenerUltimoCursoPorUsuarioAsync(int idUsuario);
}