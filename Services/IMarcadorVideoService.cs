using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMarcadorVideoService
{
    Task<List<MarcadorVideo>> GetAllByVideoIdAsync(int idVideo);
    Task<MarcadorVideo?> GetByIdAsync(int id);
    Task AddAsync(MarcadorVideo marcador);
    Task UpdateAsync(MarcadorVideo marcador);
    Task DeleteAsync(int id);
}
