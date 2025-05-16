using Microsoft.EntityFrameworkCore;
using TuProyecto.Models;

public class ReporteVideoService : IReporteVideoService
{
    private readonly AcademIQDbContext _context;

    public ReporteVideoService(AcademIQDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReporteVideo>> GetAllAsync()
    {
        return await _context.ReportesVideo
            .Include(r => r.Video)
            .Include(r => r.Usuario)
            .ToListAsync();
    }

    public async Task<List<ReporteVideo>> GetByVideoIdAsync(int idVideo)
    {
        return await _context.ReportesVideo
            .Where(r => r.IdVideo == idVideo)
            .Include(r => r.Usuario)
            .ToListAsync();
    }

    public async Task AddAsync(ReporteVideo reporte)
    {
        _context.ReportesVideo.Add(reporte);

        // Actualizar contador de reportes
        var video = await _context.Videos.FindAsync(reporte.IdVideo);
        if (video != null)
        {
            video.NumReportes += 1;
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteByVideoIdAsync(int idVideo)
    {
        var reportes = await _context.ReportesVideo
            .Where(r => r.IdVideo == idVideo)
            .ToListAsync();

        _context.ReportesVideo.RemoveRange(reportes);

        // Resetear contador en el vídeo
        var video = await _context.Videos.FindAsync(idVideo);
        if (video != null)
        {
            video.NumReportes = 0;
        }

        await _context.SaveChangesAsync();
    }
}
