public interface IUsuarioAsignaturaService
{
    Task<List<Asignatura>> GetAsignaturasDeUsuario(int idUsuario);
    Task<List<Usuario>> GetUsuariosDeAsignatura(int idAsignatura);
    Task AddAsync(UsuarioAsignatura relacion);
    Task DeleteAsync(int idUsuario, int idAsignatura);
}
