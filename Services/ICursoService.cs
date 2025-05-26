public interface ICursoService
{
    Task<List<Curso>> GetAllAsync();
    Task<Curso?> GetByIdAsync(int id);
    Task AddAsync(Curso curso);
    Task UpdateAsync(Curso curso);
    Task DeleteAsync(int id);
    Task<List<Curso>> GetCursosPorUsuarioAsync(int idUsuario); 
}

    Task<Curso?> AddCursoConUsuarioAsync(CursoCrearDTO dto, int idUsuario);
    Task<List<CursoVideosDTO>> GetTopCursosConMasVideosAsync(int cantidad);
}
 
