public interface IUsuarioService
{
    Task<List<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario?> GetByGmailAsync(string gmail);
    Task AddAsync(Usuario usuario);
    Task UpdateAsync(Usuario usuario);
    Task DeleteAsync(int id);
    Task<Usuario?> GetByTokenAsync(string token);    //TOKEN
    Task<List<RolEstadisticaDTO>> ObtenerEstadisticasPorRol(); //funcion para el grafico


}
