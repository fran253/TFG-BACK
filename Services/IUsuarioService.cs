// Modificar Services/IUsuarioService.cs
public interface IUsuarioService
{
    Task<List<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario?> GetByGmailAsync(string gmail);
    Task AddAsync(Usuario usuario);
    Task UpdateAsync(Usuario usuario);
    Task DeleteAsync(int id);
    
    // Nuevos métodos para manejar cursos seguidos
    Task<List<Curso>> GetCursosSeguidosAsync(int idUsuario);
    Task SeguirCursoAsync(int idUsuario, int idCurso);
    Task DejarDeSeguirCursoAsync(int idUsuario, int idCurso);
    Task<bool> SigueCursoAsync(int idUsuario, int idCurso);
}