using TuProyecto.Models;
using TuProyecto.Api.DTOs;

public interface IReporteVideoService
{
    Task<List<ReporteVideo>> GetAllAsync();
    Task<List<ReporteVideo>> GetByVideoIdAsync(int idVideo);
    Task<List<ReporteVideoDTO>> GetAllDTOAsync();
    Task<List<ReporteVideoDTO>> GetDTOsByVideoIdAsync(int id);
    Task AddAsync(ReporteVideo reporte);
    Task DeleteByVideoIdAsync(int idVideo);
    Task ResetearReportesAsync(int idVideo);
}
