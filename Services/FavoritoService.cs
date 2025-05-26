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

    // En tu archivo FavoritoService.cs, reemplaza SOLO este método:

    public async Task<List<Video>> GetFavoritosPorUsuario(int idUsuario)
    {
        try
        {
            Console.WriteLine($"Obteniendo favoritos para usuario: {idUsuario}");
            
            // MÉTODO SIMPLE: Dos consultas separadas (evita problemas de Include/Select)
            
            // 1. Obtener IDs de videos favoritos
            var favoritosIds = await _context.Favoritos
                .Where(f => f.IdUsuario == idUsuario)
                .Select(f => f.IdVideo)
                .ToListAsync();

            Console.WriteLine($"IDs de favoritos encontrados: {string.Join(", ", favoritosIds)}");

            // 2. Si no hay favoritos, retornar lista vacía
            if (!favoritosIds.Any())
            {
                Console.WriteLine("No se encontraron favoritos");
                return new List<Video>();
            }

            // 3. Obtener videos completos con sus includes
            var videos = await _context.Videos
                .Where(v => favoritosIds.Contains(v.IdVideo))
                .Include(v => v.Asignatura)
                .Include(v => v.Usuario)
                .ToListAsync();

            Console.WriteLine($"Videos favoritos obtenidos: {videos.Count}");
            return videos;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en GetFavoritosPorUsuario: {ex.Message}");
            throw;
        }
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

    public async Task<bool> ExisteFavorito(int idUsuario, int idVideo)
    {
        return await _context.Favoritos.AnyAsync(f => f.IdUsuario == idUsuario && f.IdVideo == idVideo);
    }


    public async Task<bool> ToggleFavoritoAsync(int idUsuario, int idVideo)
    {
        var favorito = await _context.Favoritos.FindAsync(idUsuario, idVideo);
        var video = await _context.Videos.FindAsync(idVideo);
        if (video == null) return false;

        if (favorito != null)
        {
            _context.Favoritos.Remove(favorito);
            video.ContadorLikes--;
            await _context.SaveChangesAsync();
            return false; 
        }
        else
        {
            _context.Favoritos.Add(new Favorito { IdUsuario = idUsuario, IdVideo = idVideo });
            video.ContadorLikes++;
            await _context.SaveChangesAsync();
            return true; 
        }
    }


}
