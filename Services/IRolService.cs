using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRolService
{
    Task<List<Rol>> GetAllAsync();
    Task<Rol?> GetByIdAsync(int id);
    Task AddAsync(Rol rol);
    Task UpdateAsync(Rol rol);
    Task DeleteAsync(int id);
}
