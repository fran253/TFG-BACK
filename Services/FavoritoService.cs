using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class FavoritoService : IFavoritoService
{
    private readonly AcademIQDbContext _context;

    public FavoritoService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<Video>> GetFavoritosPorUsuario(int idUsuario)
    {
        return await _context.Favoritos
            .Where(f => f.IdUsuario == idUsuario)
            .Select(f => f.Video)
            .Include(v => v.Asignatura)
            .Include(v => v.Usuario)
            .ToListAsync();
    }
    
    public async Task AddAsync(Favorito favorito)
    {
        _context.Favoritos.Add(new Favorito
        {
            IdUsuario = favorito.IdUsuario,
            IdVideo = favorito.IdVideo
        });

        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(int idUsuario, int idVideo)
    {
        var favorito = await _context.Favoritos.FindAsync(idUsuario, idVideo);
        if (favorito != null)
        {
            _context.Favoritos.Remove(favorito);
            await _context.SaveChangesAsync();
        }
    }
}
