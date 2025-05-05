public interface IPreferenciasService
{
    Task<List<Preferencias>> GetAllAsync();
    Task<Preferencias?> GetByIdAsync(int id);
    Task AddAsync(Preferencias preferencias);
    Task UpdateAsync(Preferencias preferencias);
    Task DeleteAsync(int id);
}
