using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFG_BACK.Models.Common;

public class VideoService : IVideoService
{
    private readonly AcademIQDbContext _context;

    public VideoService(AcademIQDbContext context)
    {
        _context = context;
    }

    // metodos paginados
    public async Task<PagedResult<Video>> GetAllPagedAsync(int page = 1, int pageSize = 20)
    {
        var pagination = new PaginationRequest { Page = page, PageSize = pageSize };
        
        var totalCount = await _context.Videos.CountAsync();
        
        var videos = await _context.Videos
            .Include(v => v.Asignatura)
            .Include(v => v.Usuario)
            .Include(v => v.Curso)
            .OrderByDescending(v => v.FechaSubida)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return new PagedResult<Video>
        {
            Items = videos,
            TotalCount = totalCount,
            Page = pagination.Page,
            PageSize = pagination.PageSize
        };
    }


    public async Task<PagedResult<Video>> GetByCursoPagedAsync(int idCurso, int page = 1, int pageSize = 20)
    {
        var pagination = new PaginationRequest { Page = page, PageSize = pageSize };
        
        var totalCount = await _context.Videos
            .Where(v => v.IdCurso == idCurso)
            .CountAsync();
        
        var videos = await _context.Videos
            .Where(v => v.IdCurso == idCurso)
            .Include(v => v.Asignatura)
            .Include(v => v.Usuario)
            .Include(v => v.Curso)
            .OrderByDescending(v => v.FechaSubida)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

        return new PagedResult<Video>
        {
            Items = videos,
            TotalCount = totalCount,
            Page = pagination.Page,
            PageSize = pagination.PageSize
        };
    }


    public async Task<List<Video>> GetAllAsync()
    {
        return await _context.Videos
            .Include(v => v.Asignatura)
            .Include(v => v.Usuario)
            .Include(v => v.Curso)
            .OrderByDescending(v => v.FechaSubida)
            .ToListAsync();
    }

    public async Task<Video?> GetByIdAsync(int id)
    {
        return await _context.Videos
            .Include(v => v.Asignatura)
            .Include(v => v.Usuario)
            .Include(v => v.Curso)
            .FirstOrDefaultAsync(v => v.IdVideo == id);
    }

    public async Task<List<Video>> GetByCursoAsync(int idCurso)
    {
        return await _context.Videos
            .Where(v => v.IdCurso == idCurso)
            .Include(v => v.Asignatura)
            .Include(v => v.Usuario)
            .Include(v => v.Curso)
            .OrderByDescending(v => v.FechaSubida)
            .ToListAsync();
    }

    public async Task<List<Video>> GetByAsignaturaAsync(int idAsignatura)
    {
        return await _context.Videos
            .Where(v => v.IdAsignatura == idAsignatura)
            .Include(v => v.Asignatura)
            .Include(v => v.Usuario)
            .Include(v => v.Curso)
            .OrderByDescending(v => v.FechaSubida)
            .ToListAsync();
    }

    public async Task<List<Video>> GetByCursoAndAsignaturaAsync(int idCurso, int idAsignatura)
    {
        return await _context.Videos
            .Where(v => v.IdCurso == idCurso && v.IdAsignatura == idAsignatura)
            .Include(v => v.Asignatura)
            .Include(v => v.Usuario)
            .Include(v => v.Curso)
            .OrderByDescending(v => v.FechaSubida)
            .ToListAsync();
    }

    public async Task<List<Video>> GetByUsuarioAsync(int idUsuario)
    {
        return await _context.Videos
            .Where(v => v.IdUsuario == idUsuario)
            .Include(v => v.Asignatura)
            .Include(v => v.Usuario)
            .Include(v => v.Curso)
            .OrderByDescending(v => v.FechaSubida)
            .ToListAsync();
    }

    public async Task<int> AddAsync(Video video)
    {
        _context.Videos.Add(video);
        await _context.SaveChangesAsync();
        return video.IdVideo;
    }

    public async Task UpdateAsync(Video video)
    {
        _context.Videos.Update(video);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var video = await _context.Videos.FindAsync(id);
        if (video != null)
        {
            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<List<Video>> GetVideosReportadosAsync()
    {
        return await _context.Videos
            .Where(v => v.NumReportes > 0)
            .Include(v => v.Asignatura)
            .Include(v => v.Usuario)
            .Include(v => v.Curso)
            .OrderByDescending(v => v.NumReportes)
            .ToListAsync();
    }
    
    public async Task<int> GetContadorLikesAsync(int idVideo)
    {
        var video = await _context.Videos.FindAsync(idVideo);
        return video?.ContadorLikes ?? 0;
    }
}