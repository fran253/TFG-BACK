using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISeguimientoService
{
    Task<List<Usuario>> GetProfesoresSeguidos(int idAlumno);
    Task<List<Usuario>> GetSeguidoresDelProfesor(int idProfesor);
    Task AddAsync(Seguimiento seguimiento);
    Task DeleteAsync(int idAlumno, int idProfesor);
}
