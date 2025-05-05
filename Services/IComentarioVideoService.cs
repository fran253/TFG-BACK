using System.Collections.Generic;
using System.Threading.Tasks;

public interface IComentarioVideoService
{
    Task<List<ComentarioVideo>> GetAllByVideoIdAsync(int idVideo);
    Task<ComentarioVideo?> GetByIdAsync(int id);
    Task AddAsync(ComentarioVideo comentario);
    Task UpdateAsync(ComentarioVideo comentario);
    Task DeleteAsync(int id);
}
