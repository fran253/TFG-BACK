public interface IUsuarioCursoService
{
    Task<List<Curso>> GetCursosDeUsuario(int idUsuario);
    Task<List<Usuario>> GetUsuariosDeCurso(int idCurso);
    Task AddAsync(UsuarioCurso relacion);
    Task DeleteAsync(int idUsuario, int idCurso);
}
