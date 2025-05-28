using TFG_BACK.Models.Common;

public interface IVideoService
{

    Task<PagedResult<Video>> GetAllPagedAsync(int page = 1, int pageSize = 20);
    Task<PagedResult<Video>> GetByCursoPagedAsync(int idCurso, int page = 1, int pageSize = 20);


    Task<List<Video>> GetAllAsync();
    Task<Video?> GetByIdAsync(int id);
    Task<List<Video>> GetByCursoAsync(int idCurso);
    Task<List<Video>> GetByAsignaturaAsync(int idAsignatura);
    Task<List<Video>> GetByCursoAndAsignaturaAsync(int idCurso, int idAsignatura);
    Task<List<Video>> GetByUsuarioAsync(int idUsuario);
    Task<List<Video>> GetVideosReportadosAsync();
    Task<int> AddAsync(Video video); 
    Task UpdateAsync(Video video);
    Task DeleteAsync(int id);
    Task<int> GetContadorLikesAsync(int idVideo);

}
