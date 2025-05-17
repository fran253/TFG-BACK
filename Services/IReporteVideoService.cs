using TuProyecto.Models;

public interface IReporteVideoService
{
    Task<List<ReporteVideo>> GetAllAsync();
    Task<List<ReporteVideo>> GetByVideoIdAsync(int idVideo);
    Task AddAsync(ReporteVideo reporte);
    Task DeleteByVideoIdAsync(int idVideo);
}
