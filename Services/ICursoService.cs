public interface ICursoService
{
    Task<List<Curso>> GetAllAsync();
    Task<Curso?> GetByIdAsync(int id);
    Task AddAsync(Curso curso);
    Task UpdateAsync(Curso curso);
    Task DeleteAsync(int id);
    Task<Curso?> AddCursoConUsuarioAsync(CursoCrearDTO dto, int idUsuario);
}
