using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFavoritoService
{
    Task<List<Video>> GetFavoritosPorUsuario(int idUsuario);
    Task AddAsync(Favorito favorito);
    Task DeleteAsync(int idUsuario, int idVideo);
}
